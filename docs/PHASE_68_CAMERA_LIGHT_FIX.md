# NEXO — Fase 68: Correção de câmera, iluminação e vínculo do Canvas

## Escopo

Ajustar o bootstrap para que a apresentação visual exista desde o primeiro ciclo de inicialização: câmera principal ativa, iluminação direcional disponível e Canvas procedural explicitamente associado à câmera.

## Alterações

- `StartCore()` agora executa `EnsurePresentationCamera()`, `EnsureGameplayLight()` e só então `EnsureMobileCanvas()`.
- A câmera criada como fallback recebe a tag `MainCamera`, fica ativa, é posicionada em `(0, 0, -10)` e olha para a origem.
- O Canvas runtime usa `RenderMode.ScreenSpaceCamera`, `worldCamera = view` (com fallback para `Camera.main`) e `planeDistance = 10`.
- A luz direcional é garantida antes de `BuildWorld()`, evitando que a cena dependa da ordem de montagem dos objetos.
- O fallback de câmera usado em erro também recebe tag, posição e orientação determinísticas.

## Verificação

- `python tools/run_static_qa.py` → `static_qa_ok: json=5 cs=25` (exit code 0).
- `git diff --check` → PASS (exit code 0).
- Não foram executados Unity Editor, GameCI, builds Android/iOS ou testes em dispositivo nesta fase.

## Estado

A correção está aplicada na árvore local e pronta para validação Unity/GameCI. O teste visual em Android/iOS continua pendente até uma execução de build autorizada.
