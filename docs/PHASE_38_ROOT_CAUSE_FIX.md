# Fase 38 — correção de causa raiz GameCI

## Evidências
- GameCI v6 valida métodos customizados procurando `Build succeeded!`; os métodos Unity apenas retornavam `BuildResult.Succeeded`.
- O PlayMode caiu em SIGSEGV no stack `ScriptingCoverage::FilterRecordedMethods`, compatível com instrumentação Code Coverage headless.

## Correções
- Android e iOS imprimem `Build succeeded!` somente após `BuildResult.Succeeded`.
- `coverageEnabled: false` no workflow Unity tests.
- Cache `Library` removido dos builds; cache de pacotes preservado.
- `runAsHostUser: true` nos builders.
- Metadados e uploads rodam com `if: always()` e aceitam ausência temporária de arquivos para preservar diagnósticos.

## Validação local
- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=24`).
- `git diff --check`: PASS.
- Unity Editor local indisponível; testes remotos continuam obrigatórios.
