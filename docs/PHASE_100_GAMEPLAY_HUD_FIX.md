# PHASE 100 — HUD de gameplay após Iniciar Missão

## Problema reproduzido

O fluxo antigo escondia o Canvas de introdução e deixava a informação de gameplay principalmente em `OnGUI`. Em alguns emuladores, o cenário permanecia visível, mas a HUD pós-clique ficava pequena ou não aparecia de forma confiável. Como o movimento do avatar depende de input, a cena também parecia estática quando nenhum controle era acionado.

## Correção

`NexoVerticalSliceBootstrap` agora cria uma HUD de gameplay no Canvas, separada da introdução, com:

- estado da máquina de jogo;
- água disponível;
- pontuação e recorde;
- objetivo da fase;
- feedback/prompt de interação.

A HUD é ativada quando `showMenu` e `showIntro` ficam falsos, atualizada a cada frame e protegida por null checks. O painel de desafio continua acima dela.

## Validação local

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=26`).
- `git diff --check`: PASS.
- Build Unity/AVD após esta alteração: NOT_RUN; requer novo workflow remoto.

## Limitação conhecida

O avatar só se move mediante entrada horizontal/vertical; não existe animação automática de câmera ou personagem. A ausência de movimento sem input é comportamento atual, não congelamento comprovado.
