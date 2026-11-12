# Phase 116 — Auditoria do debug log e network HAR

Data: 2026-11-12 (America/Sao_Paulo)

## Arquivos analisados

- `C:/Users/valdir.galvao/Downloads/2026-11-12 18-58-08 debug log.txt` (71.514 bytes)
- `C:/Users/valdir.galvao/Downloads/2026-11-12 18-58-02 network log.har` (176.584 bytes, 44 entradas)

## Evidências do runtime

O log confirma Unity `6000.3.0f1`, IL2CPP, x86_64, cena `NexoVerticalSlice` e OpenGL ES 3. A auditoria registrou `incompatibleShaders=0` e `magentaObjects=0`. Não foram encontrados `Exception`, `FATAL EXCEPTION`, `SIGSEGV`, `ANR` ou `NullReference`.

Também há evidência de entrada e transição: `[NEXO_INPUT] Start Mission button activated`, `BeginModernMission requested`, `[NEXO_GAMEPLAY] FadeIntoMission started` e `Mission gameplay active`.

Não há `Interact requested`, `Investigating objective step` ou abertura do desafio. O toque inicial foi aceito, mas nenhuma pista ou portal foi investigada depois.

## Causa funcional observável

O `NexoVerticalSliceBootstrap` ativa `gameplayGuidePanel` quando a missão começa (`gameplayGuideVisible = true`). O painel contém `COMO JOGAR` e permanece ativo até a abertura do desafio; não existe ação explícita para dispensá-lo. Isso deixa a cena visualmente dominada pelo painel escuro sobre o blockout verde/amarelo e torna ambíguo que o jogador deve pressionar `INVESTIGAR` ou usar os controles direcionais.

A interação 3D usa `Physics.Raycast` no ponto tocado. Toques fora da fonte, canal, válvula, portal ou botão `INVESTIGAR` apenas geram `Pointer detected` e não avançam o objetivo. A ausência dos marcadores de investigação confirma esse fluxo.

## Avisos do emulador e HAR

`EGL_BAD_CONFIG`/`Bad attribute idx` é fallback do GFXSTREAM para OpenGL ES 3.1; o Unity continua executando. A primeira tentativa de carregar `liblibswappywrapper.so` falha, mas a biblioteca correta é carregada logo depois. O HTTP 400 de `android.googleapis.com/auth/devicekey` pertence ao ambiente Android/Google. O HAR tem 43 respostas 200 e uma 400, sem host do NEXO, Appetize ou GitHub.

## Conclusão

O log não comprova falha de inicialização, renderização ou shader. Comprova a transição para gameplay, mas não investigação nem desafio. O sintoma “quadro verde, porta amarela e COMO JOGAR” é causado pela composição atual do blockout, pelo guia persistente e pela falta de feedback para toques fora dos alvos. A correção necessária é de UX/roteamento: fechar o guia explicitamente ou ocultá-lo após o primeiro movimento, destacar `INVESTIGAR` e informar quando o toque não atingir um alvo.

Nenhuma alteração de código foi feita nesta auditoria.
