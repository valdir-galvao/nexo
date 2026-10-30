# Fase 54 ? Compatibilidade mobile e ciclo de vida

Data: 30/10/2026 (America/Sao_Paulo)
Branch: `orchestrator/phase-a-baseline`

## Runtime

`NexoVerticalSliceBootstrap` configura `QualitySettings.vSyncCount = 0`, `Application.targetFrameRate = 60` e `Screen.sleepTimeout = SleepTimeout.NeverSleep` no in?cio da execu??o.

`OnApplicationPause` preserva a escala anterior, pausa o tempo e as part?culas durante a suspens?o e restaura o valor ao retornar. `OnApplicationFocus` pausa/retoma part?culas da introdu??o sem alterar o estado de pausa da miss?o.

## UI

A varredura de prefabs e cenas rastreados encontrou zero `CanvasScaler` serializados. O ?nico CanvasScaler ? criado pelo bootstrap e j? usa `ScaleWithScreenSize`, refer?ncia 1920x1080, `MatchWidthOrHeight` e match 0.5.

## Valida??o

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=24`, exit 0).
- `git diff --check`: PASS (exit 0).
- Unity/GameCI/dispositivo: NOT_RUN nesta fase.
