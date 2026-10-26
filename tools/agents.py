"""Explicit, cached API tasks. No generated code is executed. Standard library only."""
import argparse
import datetime as dt
import hashlib
import json
from pathlib import Path
import re
import urllib.request
import urllib.error
import os

ROOT = Path(__file__).resolve().parents[1]
STATE = ROOT / '.agents-local'
CONFIG = json.loads((ROOT / 'agents.json').read_text(encoding='utf-8'))


def credentials():
    # Read only AI credentials; never send the entire env to a provider.
    values, duplicates = {}, []
    path = ROOT / '.env'
    if not path.exists():
        path = ROOT / '.env.txt'
    for line in path.read_text(encoding='utf-8-sig').splitlines():
        match = re.match(r'^\s*(?:export\s+)?([A-Z_][A-Z0-9_]*)\s*=\s*(.*?)\s*$', line)
        if match and match[1] in {p['key'] for p in CONFIG['providers'].values()}:
            key, value = match.groups()
            value = value.strip('\"\'')
            if key in values:
                duplicates.append(key)
            if value:
                values[key] = value
    return values, sorted(set(duplicates))


def request(provider, path, body=None):
    keys, _ = credentials()
    cfg = CONFIG['providers'][provider]
    key = keys.get(cfg['key'])
    if not key:
        raise RuntimeError('missing_key')
    headers = {'Content-Type': 'application/json'}
    headers.update({'x-goog-api-key': key} if provider == 'gemini' else {'Authorization': 'Bearer ' + key})
    req = urllib.request.Request(cfg['base'] + path, data=None if body is None else json.dumps(body).encode(), headers=headers)
    try:
        with urllib.request.urlopen(req, timeout=60) as response:
            return json.load(response)
    except urllib.error.HTTPError as exc:
        # Raw error bodies can echo credentials or submitted content.
        raise RuntimeError('http_' + str(exc.code)) from None
    except urllib.error.URLError:
        raise RuntimeError('network_error') from None


def save(path, value):
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(value, indent=2, ensure_ascii=False), encoding='utf-8')


def probe(provider=None):
    path = STATE / 'availability.json'
    report = json.loads(path.read_text(encoding='utf-8')) if path.exists() else {'providers': {}}
    report.update({'checked_at': dt.datetime.now(dt.timezone.utc).isoformat(), 'duplicates': credentials()[1]})
    for name, cfg in CONFIG['providers'].items():
        if provider and name != provider:
            continue
        try:
            data = request(name, '/models')
            if name == 'gemini':
                models = [m['name'].removeprefix('models/') for m in data.get('models', []) if 'generateContent' in m.get('supportedGenerationMethods', [])]
            elif name == 'typesafe':
                models = [m['name'] for m in data.get('models', [])]
            else:
                models = [m['id'] for m in data.get('data', [])]
            selected = next((m for m in cfg['preferred'] if m in models), None)
            previous = report['providers'].get(name, {})
            entry = {'status': 'catalog_ok' if selected else 'model_selection_required', 'model': selected, 'models': models}
            if selected and selected == previous.get('model') and 'inference_status' in previous:
                entry['inference_status'] = previous['inference_status']
            report['providers'][name] = entry
        except RuntimeError as exc:
            report['providers'][name] = {'status': str(exc)}
    save(STATE / 'availability.json', report)
    print(json.dumps(report['providers'].get(provider) if provider else report, indent=2, ensure_ascii=False))


def task(provider, text, smoke=False):
    if len(text) > CONFIG['max_input_chars']:
        raise RuntimeError('input_limit')
    keys, _ = credentials()
    if any(secret and secret in text for secret in keys.values()):
        raise RuntimeError('secret_in_input')
    availability = json.loads((STATE / 'availability.json').read_text(encoding='utf-8'))
    if not smoke and availability['providers'][provider].get('inference_status') != 'ok':
        raise RuntimeError('inference_not_validated')
    model = availability['providers'][provider].get('model')
    if not model:
        raise RuntimeError('provider_unavailable')
    system = 'Voce e um colaborador supervisionado. Papel: ' + CONFIG['providers'][provider]['role'] + '. Responda somente a tarefa delimitada; nao execute comandos; explicite limitacoes.'
    limit = 32 if smoke else CONFIG['max_output_tokens']
    typed = None
    if provider == 'typesafe':
        typed = {'state': 'O personagem deve correr e saltar.', 'questions': {'movement': {'type': 'noul', 'instructions': 'O texto especifica movimentos de um personagem?'}}} if smoke else json.loads(text)
        if not isinstance(typed, dict) or set(typed) != {'state', 'questions'}:
            raise RuntimeError('typesafe_requires_state_and_questions')
        questions = typed['questions']
        if not isinstance(questions, dict) or not 1 <= len(questions) <= 4:
            raise RuntimeError('typesafe_question_limit_1_to_4')
        # Initial integration uses bounded yes/no judgments only.
        if any(not isinstance(q, dict) or q.get('type') != 'noul' or not q.get('instructions') or set(q) - {'type', 'instructions', 'criteria'} for q in questions.values()):
            raise RuntimeError('typesafe_requires_noul_questions')
    digest = hashlib.sha256(json.dumps([provider, model, system, text, limit], ensure_ascii=False).encode()).hexdigest()
    cache = STATE / 'cache' / (digest + '.json')
    if cache.exists():
        print(cache.read_text(encoding='utf-8'))
        return
    ledger = STATE / 'calls.json'
    calls = json.loads(ledger.read_text()) if ledger.exists() else []
    today = dt.datetime.now(dt.timezone.utc).date().isoformat()
    if sum(c['day'] == today for c in calls) >= CONFIG['max_calls_per_day']:
        raise RuntimeError('daily_call_limit')
    # Reserve before transmission. Failed/uncertain attempts also consume budget;
    # identical requests cannot be automatically resent after a timeout.
    if any(c['hash'] == digest for c in calls):
        raise RuntimeError('already_attempted_requires_manager_review')
    calls.append({'day': today, 'provider': provider, 'model': model, 'hash': digest, 'status': 'reserved'})
    save(ledger, calls)
    if provider == 'typesafe':
        result = request(provider, '/systemone', {'model': model, **typed})
        answers = result.get('answers', {})
        if set(answers) != set(typed['questions']) or any(not isinstance(a, dict) or a.get('type') != 'noul' or not isinstance(a.get('noul'), (int, float)) or isinstance(a['noul'], bool) or not 0 <= a['noul'] <= 1 for a in answers.values()):
            raise RuntimeError('invalid_structured_response_requires_review')
        content = json.dumps(answers, ensure_ascii=False)
        usage = result.get('usage', {})
    elif provider == 'gemini':
        body = {'systemInstruction': {'parts': [{'text': system}]}, 'contents': [{'parts': [{'text': text}]}], 'generationConfig': {'maxOutputTokens': limit}}
        result = request(provider, '/models/' + model + ':generateContent', body)
        content = '\n'.join(p.get('text', '') for c in result.get('candidates', []) for p in c.get('content', {}).get('parts', []))
        usage = result.get('usageMetadata', {})
    else:
        result = request(provider, '/chat/completions', {'model': model, 'messages': [{'role': 'system', 'content': system}, {'role': 'user', 'content': text}], 'max_tokens': limit})
        content = result['choices'][0]['message'].get('content', '')
        usage = result.get('usage', {})
    if not content.strip():
        raise RuntimeError('empty_response_requires_review')
    output = {'provider': provider, 'model': model, 'response': content, 'usage': usage}
    save(cache, output)
    calls[-1]['status'] = 'completed'
    calls[-1]['usage'] = usage
    save(ledger, calls)
    print(json.dumps(output, ensure_ascii=False, indent=2))


def guarded_task(provider, text, smoke=False):
    STATE.mkdir(parents=True, exist_ok=True)
    lock = STATE / 'executor.lock'
    try:
        fd = os.open(lock, os.O_CREAT | os.O_EXCL | os.O_WRONLY)
    except FileExistsError:
        raise RuntimeError('executor_busy_or_uncertain_previous_exit_review_lock') from None
    try:
        os.close(fd)
        try:
            task(provider, text, smoke)
        except RuntimeError as exc:
            if smoke and str(exc).startswith(('http_', 'network_', 'empty_response')):
                path = STATE / 'availability.json'
                report = json.loads(path.read_text(encoding='utf-8'))
                report['providers'][provider]['inference_status'] = str(exc)
                save(path, report)
            raise
        else:
            if smoke:
                path = STATE / 'availability.json'
                report = json.loads(path.read_text(encoding='utf-8'))
                report['providers'][provider]['inference_status'] = 'ok'
                save(path, report)
    finally:
        lock.unlink()


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('command', choices=['probe', 'smoke', 'task'])
    parser.add_argument('--provider', choices=list(CONFIG['providers']))
    parser.add_argument('--file', type=Path)
    args = parser.parse_args()
    try:
        if args.command == 'probe':
            probe(args.provider)
        elif not args.provider or (args.command == 'task' and not args.file):
            parser.error('Informe --provider e, para task, --file.')
        else:
            guarded_task(args.provider, 'Responda apenas OK.' if args.command == 'smoke' else args.file.read_text(encoding='utf-8'), args.command == 'smoke')
    except (RuntimeError, FileNotFoundError, json.JSONDecodeError) as exc:
        parser.exit(1, str(exc) + '\n')
