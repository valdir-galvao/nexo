import contextlib
import io
import json
from pathlib import Path
import tempfile
import unittest
from unittest.mock import patch
import agents


class ExecutorTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        self.state = self.root / '.agents-local'
        self.state.mkdir()
        self.patches = [patch.object(agents, 'ROOT', self.root), patch.object(agents, 'STATE', self.state)]
        for p in self.patches:
            p.start()
            self.addCleanup(p.stop)
        (self.root / '.env.txt').write_text('DEEPSEEK_API_KEY=first\nDEEPSEEK_API_KEY=local-test-secret\n')
        agents.save(self.state / 'availability.json', {'providers': {'deepseek': {'model': 'test', 'inference_status': 'ok'}}})

    def invoke(self, text='isolated task'):
        with contextlib.redirect_stdout(io.StringIO()):
            agents.guarded_task('deepseek', text)

    def test_duplicate_last_value(self):
        values, duplicates = agents.credentials()
        self.assertEqual(values['DEEPSEEK_API_KEY'], 'local-test-secret')
        self.assertEqual(duplicates, ['DEEPSEEK_API_KEY'])

    def test_cache_avoids_second_request(self):
        response = {'choices': [{'message': {'content': 'proposal'}}], 'usage': {'total_tokens': 10}}
        with patch.object(agents, 'request', return_value=response) as request:
            self.invoke()
            self.invoke()
            self.assertEqual(request.call_count, 1)

    def test_uncertain_failure_is_not_retried(self):
        with patch.object(agents, 'request', side_effect=RuntimeError('network_error')) as request:
            with self.assertRaisesRegex(RuntimeError, 'network_error'):
                self.invoke()
            with self.assertRaisesRegex(RuntimeError, 'already_attempted'):
                self.invoke()
            self.assertEqual(request.call_count, 1)

    def test_secret_blocked_before_transmission(self):
        with patch.object(agents, 'request') as request:
            with self.assertRaisesRegex(RuntimeError, 'secret_in_input'):
                self.invoke('local-test-secret')
            request.assert_not_called()

    def test_concurrent_executor_blocked(self):
        (self.state / 'executor.lock').touch()
        with self.assertRaisesRegex(RuntimeError, 'executor_busy'):
            self.invoke()

    def test_targeted_probe_preserves_other_provider(self):
        with patch.object(agents, 'request', return_value={'data': [{'id': 'mistral-small-latest'}]}) as request:
            with contextlib.redirect_stdout(io.StringIO()):
                agents.probe('mistral')
            request.assert_called_once_with('mistral', '/models')
        report = json.loads((self.state / 'availability.json').read_text())
        self.assertEqual(report['providers']['deepseek']['inference_status'], 'ok')
        self.assertEqual(report['providers']['mistral']['model'], 'mistral-small-latest')

    def test_smoke_records_validation(self):
        response = {'choices': [{'message': {'content': 'OK'}}]}
        with patch.object(agents, 'request', return_value=response):
            with contextlib.redirect_stdout(io.StringIO()):
                agents.guarded_task('deepseek', 'Responda apenas OK.', smoke=True)
        report = json.loads((self.state / 'availability.json').read_text())
        self.assertEqual(report['providers']['deepseek']['inference_status'], 'ok')

    def test_typesafe_uses_structured_endpoint(self):
        agents.save(self.state / 'availability.json', {'providers': {'typesafe': {'model': 'jev-latest'}}})
        response = {'answers': {'movement': {'type': 'noul', 'noul': 0.97}}, 'usage': {'input_tokens': 10}}
        with patch.object(agents, 'request', return_value=response) as request:
            with contextlib.redirect_stdout(io.StringIO()):
                agents.guarded_task('typesafe', 'Responda apenas OK.', smoke=True)
            name, path, body = request.call_args.args
            self.assertEqual((name, path), ('typesafe', '/systemone'))
            self.assertEqual(body['questions']['movement']['type'], 'noul')
            self.assertNotIn('messages', body)

    def test_typesafe_rejects_wrong_answer_schema(self):
        agents.save(self.state / 'availability.json', {'providers': {'typesafe': {'model': 'jev-latest'}}})
        with patch.object(agents, 'request', return_value={'answers': {'movement': {'type': 'noul', 'noul': 5}}}):
            with self.assertRaisesRegex(RuntimeError, 'invalid_structured_response'):
                agents.guarded_task('typesafe', 'Responda apenas OK.', smoke=True)


if __name__ == '__main__':
    unittest.main()
