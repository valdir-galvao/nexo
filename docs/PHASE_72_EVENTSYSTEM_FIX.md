# NEXO — Fase 72: EventSystem e entrada de UI

## Alterações

- `CreateIfMissing()` chama `EnsureRuntimeEventSystem()` antes de criar/recuperar o bootstrap.
- `Awake()` repete a garantia de forma idempotente no primeiro frame do componente.
- `EnsureRuntimeEventSystem()` procura qualquer `EventSystem`, cria `Nexo EventSystem` quando ausente, ativa o objeto e garante `StandaloneInputModule` habilitado.
- O Canvas runtime continua exigindo `CanvasScaler` em `ScaleWithScreenSize` com resolução `1920x1080`, `MatchWidthOrHeight` e `GraphicRaycaster` ativo.

## Verificação

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`, exit code 0).
- `git diff --check`: PASS.
- Nenhum build Android/iOS foi executado nesta fase.

## Estado

Correção pronta para validação em Unity/GameCI e teste de toque/clique em dispositivo.
