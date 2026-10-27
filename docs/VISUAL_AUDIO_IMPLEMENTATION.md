# NEXO — P4 Visual, UI and Audio Implementation

Status: integração inicial versionada; validação Unity e profiling pendentes.
Data: 27/10/2026.

## Moodboard e paleta

O moodboard textual combina ilhas suspensas, pedra clara, madeira azulada,
vegetação leve, água turquesa, mecanismos antigos e sinais de energia dourada.
A paleta central está em `NexoVisualTheme`: céu azul profundo, pedra, madeira,
água, sinal dourado e texto claro. Símbolos e rótulos duplicam o significado da
cor para manter leitura com daltonismo ou tela sem áudio.

## Implementação entregue

- `NexoVisualTheme`: cores compartilhadas para materiais e estados importantes.
- Bootstrap P3 atualizado: menu inicial, prólogo, HUD de água/estado, inspeção,
  puzzle da válvula, diário de descobertas, ajuda, pausa, configurações e tela de
  conclusão; layouts usam `Screen.safeArea`.
- `NexoAudioDirector`: ambiente fica silencioso por fallback seguro; descoberta e
  feedback usam tons procedurais curtos, com volume de efeitos, volume mestre e
  mute persistidos em `SaveData.audio`.
- Todos os botões chamam ações reais do domínio ou fecham a própria tela.

## Temporário versus definitivo

Os cubos, cápsula, materiais simples e tons procedurais são temporários. Arte
final, personagens expressivos, animações, partículas, música adaptativa e clips
licenciados ficam para P4.1 após validar câmera e composição no GameCI. Não foram
adicionados assets binários grandes nem dependências externas.

## Aceite e medição

Validar no Unity: proporções 16:9, 19.5:9 e área segura; contraste e escala de
texto; interação sem cor/áudio; mute; pausa; diário; conclusão. Medir no Android
mínimo: FPS médio e 1% low, frame time, draw calls, memória, tamanho do build e
tempo de carregamento. Meta inicial: 30 FPS, 33,3 ms e 100–150 draw calls na cena;
registrar resultado real antes de ajustar materiais ou efeitos.
