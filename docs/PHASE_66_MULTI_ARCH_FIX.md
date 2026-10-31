# Fase 66 - multi-arquitetura Android e logging

Data: 31/10/2026 America/Sao_Paulo

## Alteracoes

- `AndroidBuild.PerformBuild` configura `PlayerSettings.Android.targetArchitectures` como `ARM64 | X86_64`, cobrindo aparelhos reais e emuladores x86_64.
- A build usa IL2CPP conforme o perfil existente; a flag `NEXO_DEVELOPMENT_BUILD=1` habilita `Development | AllowDebugging` para homologacao e preserva a possibilidade de build sem essas flags fora do CI.
- `NexoVerticalSliceBootstrap` registra `[NEXO_BOOT] Game Manager & UI Canvas Initialized Successfully!` em `Awake` e `Start`, permitindo localizar o boot via logcat.
- O workflow Android injeta `NEXO_DEVELOPMENT_BUILD=1` apenas no job de homologacao.

## Verificacao

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`).
- `git diff --check`: PASS.
- YAML do workflow Android: PASS.
- Nenhuma build foi disparada nesta correcao.
