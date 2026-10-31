# Fase 59 - Correcao visual e vinculacao de interacao

Data: 31/10/2026 (America/Sao_Paulo)

## Causa tratada

O desafio dependia do `OnGUI` e da interacao centralizada em `E`/botao, enquanto o Canvas dinamico nao tinha garantia explicita de sorting, EventSystem e raycast fisico. Isso podia deixar o quiz invisivel ou impedir o toque no portal.

## Correcoes

- Canvas HUD forca `ScreenSpaceOverlay`, `sortingOrder=10`, `CanvasScaler` e `GraphicRaycaster` ativo.
- `EnsureEventSystem` garante `EventSystem` ativo e `StandaloneInputModule` habilitado.
- A Main Camera recebe `PhysicsRaycaster` ativo.
- `Update` aceita clique do mouse e toque iniciado na tela, usando `Camera.ScreenPointToRay` e `Physics.Raycast`.
- O quiz agora possui painel Canvas real, `Image`, tres `Button` com `targetGraphic`, textos e callbacks ativos.
- `OnGUI` permanece como fallback se a criacao do Canvas falhar.
- Pulse/tremor continuam alterando o `transform.localScale`/`localPosition` do portal visivel.
- Referencias novas usam null checks e o input direto ignora toques sobre a UI.

## Validacao

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`).
- `git diff --check`: PASS.
- Unity Editor, PlayMode e dispositivo: NOT_RUN nesta fase.
- APK/IPA: nao gerados nesta correcao.

## Proxima acao

Executar Unity/GameCI e validar no Android: abertura do portal por toque, visibilidade do painel, respostas, pulse, score e feedback.
