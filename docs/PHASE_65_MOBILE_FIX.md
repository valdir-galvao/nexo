# Fase 65 - blindagem mobile de renderizacao e input

Data: 31/10/2026 America/Sao_Paulo

## Alteracoes

- `PreloadGraphics` prioriza `Unlit/Color`, depois `UI/Default`, URP Unlit e Sprites/Default; o material instanciado aplica a cor diretamente antes de ser atribuido ao renderer.
- O Canvas e a Safe Area sao ativados e movidos para o ultimo sibling no boot.
- O loop `Update` agora detecta primeiro toque (`Input.touchCount` + `TouchPhase.Began`) e clique (`Input.GetMouseButtonDown(0)`). Durante menu/prologo, o toque inicia a missao; durante gameplay, ele dispara o raycast da porta sem depender de `EventSystem`.
- `InteractAtScreenPoint` nao exige mais `EventSystem.current`; `PhysicsRaycaster`/Physics.Raycast continuam como caminho de interacao.

## Verificacao

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`).
- `git diff --check`: PASS.
- Nenhuma build foi executada nesta fase.
