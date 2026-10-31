# NEXO — Fase 70: correção de infraestrutura CI/IL2CPP

## Mudanças

- Timeout do job Android ampliado de 30 para 45 minutos, mantendo limite rígido.
- Adicionado passo explícito para remover caches potencialmente corrompidos de Unity, Gradle e wrapper.
- Chave de cache Unity versionada como `android-cache-v2` e vinculada ao workflow, invalidando alterações de infraestrutura.
- `GRADLE_OPTS` configurado com heap de 4 GiB, daemon desativado, logging `info` e warnings completos.
- Unity headless usa `-single-instance` e `-disable-assembly-reload`, além dos parâmetros já existentes de batchmode/nographics.
- IL2CPP, ARM64/x86_64, API mínima 29 e target 35 permanecem inalterados.

## Verificação

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`, exit code 0).
- Parser YAML Python: PASS.
- `git diff --check`: PASS.
- Nenhuma build remota foi disparada nesta fase.

## Próximo passo

Executar uma nova build Android controlada e verificar se o Unity/IL2CPP supera o passo `Build Android package`, preservando os logs de diagnóstico e o artefato somente após sucesso verificável.
