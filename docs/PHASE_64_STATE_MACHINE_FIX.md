# Fase 64 - inicializacao imediata da maquina de estados

Data: 31/10/2026 America/Sao_Paulo

## Diagnostico

O bootstrap criava a missao e a HUD, mas adiava `EnsureWorldBuilt()` ate a entrada na missao. Isso deixava a primeira apresentacao dependente da transicao de fade e podia exibir apenas o fundo/placeholder antes de existirem portais e alvos de raycast.

## Correcao

- `EnsureWorldBuilt()` agora executa imediatamente apos `mission.Initialize()`, mantendo a intro como camada visual superior.
- `EnsureInteractiveHudVisible()` garante Canvas ativo, `GraphicRaycaster` habilitado e grupo de intro interativo no primeiro frame.
- `RefreshModernIntro()` atualiza explicitamente `interactable` e `blocksRaycasts`.
- `BeginModernMission()` reafirma a inicializacao do mundo antes da transicao, tornando a entrada idempotente.
- Nao foram introduzidos objetos placeholder adicionais nem removidos os listeners de Button/PhysicsRaycaster.

## Verificacao

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`).
- `git diff --check`: PASS.
- Nenhum build ou workflow foi disparado nesta fase.
