# PHASE 103 — Correção da interação do portal

Data: 2026-11-11

## Diagnóstico

A execução AVD `42932184190` comprovou que o aplicativo inicializa e renderiza corretamente: `SCENE_LOADED mode=2 scene=NexoVerticalSlice` e `[NEXO_RENDER] ... api=OpenGLES3 ... incompatibleShaders=0 magentaObjects=0`. Os avisos EGL/Gralloc são do SwiftShader do emulador e não causam o quadro verde.

O comportamento após `Iniciar Missão` era causado por duas limitações do código:

- O botão `Interagir` chamava `InteractAtScreenPoint`, que fazia `Physics.Raycast` com alcance máximo de 4 unidades. O jogador inicia em aproximadamente `(-6, 1, -5)` e o portal em `(6, 1.2, -1)`, portanto o raycast nunca alcançava o portal.
- O mundo só recebia movimento por eixos/toque; sem entrada direcional, a cena permanecia parada. Não havia animação idle no portal para indicar que a interação estava disponível.

## Correção aplicada

Em `Assets/NEXO/Scripts/UI/NexoVerticalSliceBootstrap.cs`:

- O botão `Interagir` abre diretamente o desafio quando a porta da fase está desbloqueada.
- O alcance do raycast foi ampliado para 40 unidades, mantendo a checagem de collider e fase.
- O portal recebe pulso de escala contínuo com `Mathf.Sin` enquanto o desafio não está aberto.
- A escala original é preservada para evitar deformação acumulada, inclusive ao desbloquear a próxima fase.

A correção não altera a geometria verde intencional, os shaders, a pipeline ou a arquitetura de entrada.

## Verificação

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=26`, exit 0).
- `git diff --check`: PASS (exit 0).
- Nenhuma nova build foi disparada nesta etapa; os APKs anteriores continuam vinculados ao commit `e3bcd1e` e comprovam a HUD pós-clique, mas ainda não contêm esta correção local.
- Repositório permanece privado.

Próxima ação: executar nova build/AVD somente após enviar este commit, caso seja necessária a validação remota do clique e do pulso do portal.
