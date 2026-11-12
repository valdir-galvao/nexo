# PHASE 114 — Perícia do log de runtime e ausência de gameplay

Data: 2026-11-12 America/Sao_Paulo

## Evidências observadas

O trecho recebido registra apenas o boot:

- `NEXO_BOOT Awake` e `NEXO_BOOT Start`.
- `NEXO_RENDER ... renderers=19 materials=19 incompatibleShaders=0 magentaObjects=0 canvases=1 debugObjects=1`.
- Unity 6000.3.0f1, IL2CPP, OpenGL ES 3, x86_64.

Não aparecem eventos de `HandleStartButtonPressed`, `BeginModernMission`, `Interact`, `InvestigateCurrentObjective`, `OpenChallenge` ou mudança de estado para gameplay. O código atual também não emitia logs nesses pontos, portanto a ausência não prova sozinha que um toque ocorreu, mas comprova que o trecho não contém evidência da transição.

## Causa mais provável

A tela inicial é criada como uma camada Canvas sobre o mundo. `BuildWorld()` cria a ilha, fonte, canal, válvula, portal e avatar no `Start()`, mas `showMenu/showIntro` permanecem verdadeiros até que o botão seja realmente acionado. Sem a transição `BeginModernMission()`, o jogador vê somente a apresentação e o blockout verde/amarelo; HUD, marcador, água animada, avatar e controles permanecem ocultos por `RefreshGameplayHud()`.

O log não mostra o acionamento do botão. Além disso, ele é de uma execução `x86_64`, enquanto o APK da rodada `43137667979` baixado nesta sessão é ARM64. Logo, ele não é evidência do artefato ARM64 atual.

## O que não é a causa

- Não há fallback magenta: `incompatibleShaders=0` e `magentaObjects=0`.
- Os erros EGL/GFXSTREAM e `libswappywrapper` são avisos do emulador e não impedem o boot neste trecho.
- Não há crash, ANR ou erro de licença no log fornecido.

## Pendência verificável

É necessário executar o APK X86_64 com um toque automatizado no botão e coletar logs após o toque. A próxima execução deve registrar explicitamente os eventos de entrada, `BeginModernMission`, `InvestigateCurrentObjective` e `OpenChallenge`, além de capturar screenshot antes/depois. Sem essa evidência, a interatividade não pode ser declarada validada.
