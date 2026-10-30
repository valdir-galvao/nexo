# Fase 55 ? Diagn?stico forense de crash no boot

Data: 30/10/2026 (America/Sao_Paulo)

## Pontos auditados

- `RuntimeInitializeOnLoadMethod`: cria??o do bootstrap agora protegida por `try/catch`, com valida??o do objeto/componente criado.
- `Start`: fluxo dividido em `StartCore` e `Start`; exce??es n?o tratadas s?o capturadas com `Debug.LogException`, mantendo uma c?mera de fallback.
- `PreloadGraphics`: material `Resources` e shaders possuem fallback; nenhum material nulo ? usado para construir o mundo.
- Canvas, c?mera, part?culas, `MeshFilter`, `MeshRenderer`, colliders e interactables possuem checagens defensivas.
- `OnApplicationPause`/`OnApplicationFocus`: callbacks protegidos contra exce??es de plataforma.
- `EditorBuildSettings.asset`: h? uma ?nica cena habilitada, `Assets/Scenes/NexoVerticalSlice.unity`, no ?ndice 0; n?o h? cenas ?rf?s na fila.

## Potenciais causas mitigadas

1. Exce??o em servi?o de ?udio ou persist?ncia durante `Start` podia encerrar a inicializa??o sem apresentar UI. Agora o fluxo captura a exce??o e cria c?mera segura.
2. Falha de cria??o de componentes gr?ficos/part?culas podia interromper o boot. Componentes e recursos s?o validados antes do uso.
3. Callback de pausa/foco podia acessar part?culas destru?das durante suspens?o. Agora h? prote??o expl?cita.

## Limita??o de evid?ncia

N?o h? dispositivo Android conectado nem `adb/logcat` dispon?vel neste ambiente. Static QA n?o substitui logcat. A confirma??o de crash em aparelho exige reproduzir o fechamento e coletar `adb logcat` ou relat?rio Android tombstone.

## Valida??o local

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=24`).
- `git diff --check`: PASS.
- Build Unity/GameCI desta corre??o: ainda n?o executado nesta fase.
