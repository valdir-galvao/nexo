# PHASE 111 — UX FORENSICS AND GUIDED GAMEPLAY

Data: 2026-11-11 22:32:24 -03:00

## Leitura do log Android
O log não contém exceção C# nem falha de inicialização do NEXO. O bootstrap registra Awake, Start e NEXO_RENDER com incompatibleShaders=0 e magentaObjects=0. Os erros EGL_BAD_CONFIG, Bad attribute idx, irtgpu.kumquat e liblibswappywrapper.so são avisos/limitações do emulador GFXSTREAM/SwiftShader; o player cria contexto OpenGL ES e continua executando.

## Causa de UX
A execução anterior apresentava somente o blockout procedural e não explicava o próximo passo. O avatar era um único cubo, o portal não tinha alvo visual suficiente e o jogador dependia do raycast/OnGUI para descobrir a interação.

## Correções aplicadas
- Avatar Explorer composto por corpo, cabeça, visor, mochila e pernas.
- Cartão de orientação pós-início com instruções numeradas.
- O cartão não bloqueia os controles nem o botão INVESTIGAR.
- Portal mantém aura de partículas, luz pulsante e escala animada.
- HUD mantém instrução de objetivo, score, fase e controles móveis.

## Estado
Static QA e diff-check executados localmente. Nova build e smoke visual desta alteração ainda dependem de execução Unity/GameCI.
