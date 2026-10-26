# NEXO — P3 Gameplay Implementation

Status: implementação versionada; validação Unity pendente. Data: 26/10/2026.

## Execução

Ao carregar `NexoVerticalSlice`, `NexoVerticalSliceBootstrap` cria uma ilha
temporária, avatar, câmera, fonte, canal, válvula, reservatório e ponte. O
bootstrap é automático para permitir uma primeira execução sem prefabs finais.
Teclado usa setas/WASD e `E`; a tela oferece botões de Interagir, Ajuda, Pausa e
posições da válvula para teste em telas pequenas. O prólogo mostra entrada direta
com opção de pular.

## Máquina de estados

`MissionStateMachine` restringe `Initializing → Intro → Exploring → Inspecting /
PuzzleActive → Feedback → MissionComplete`, com `Paused` acessível dos estados
ativos. Sinais inválidos são rejeitados; uma missão concluída não pode concluir
novamente. O estado persistido inclui checkpoint, água, pistas, alocação e válvula.

## Testes

`Assets/NEXO/Tests` testa transições inválidas, conclusão idempotente, tentativa
errada sem penalidade, alocação, alinhamento da válvula e save inválido. Os testes
serão executados pelo workflow `Unity tests` em EditMode.

## Limitações conhecidas

O cenário é um gerador temporário de primitivas, sem arte final, animação final,
Cinemachine ou áudio produzido. Não foi possível compilar no Windows porque Unity
Editor e SDKs não estão instalados. A validação física Android/iOS, FPS, input
touch nativo e restauração após suspensão permanecem pendentes do GameCI e de
dispositivo real. O fluxo Garden é representado no domínio, mas a UI P3 expõe a
rota Reservoir para manter a fatia mínima determinística.
