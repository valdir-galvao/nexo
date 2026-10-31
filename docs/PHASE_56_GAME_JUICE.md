# Fase 56 - Gameplay interativo e Game Juice

Data: 31/10/2026 (America/Sao_Paulo)

## Implementacao

- `NexoVerticalSliceBootstrap` cria uma porta verde interativa (`GreenDoor`) sem primitivas Unity ou colliders complexos.
- A interacao abre um desafio curto de fluxo: 6 unidades saem da fonte e 2 ficam no canal; a resposta esperada e 4.
- Respostas sao bloqueadas apos a primeira tentativa, exibem feedback explicativo e permitem continuar ou tentar novamente.
- A pontuacao local e exibida no HUD e incrementada somente no acerto.
- A porta recebe pulse de escala; respostas incorretas acionam tremor visual e respostas corretas acionam brilho verde.
- O acerto dispara particulas leves e temporarias com o shader do material de gameplay ja validado; se o shader nao existir, o efeito e omitido com seguranca.
- Todas as referencias novas possuem verificacoes defensivas e o fluxo de Update e bloqueado enquanto o desafio esta aberto.

## Validacao

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`).
- `git diff --check`: PASS.
- Unity Editor, PlayMode, toque em aparelho e build de plataforma: NOT_RUN nesta fase; o Unity nao esta instalado localmente.
- APK/IPA e workflows CI: nao executados por instrucao explicita.

## Riscos e proxima acao

A validacao visual e de input deve ser feita no Unity/GameCI ou em dispositivo Android antes de distribuir uma build. O teste deve cobrir raycast na porta, respostas, feedback, pontuacao e ausencia de regressao de shaders URP.
