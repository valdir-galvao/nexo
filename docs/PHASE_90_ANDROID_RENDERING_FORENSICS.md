# NEXO — Diagnóstico forense de tela e interatividade Android

Data: 2026-11-01 (America/Sao_Paulo)
Estado: PARTIAL / BLOCKED para validação final do APK atual.

## Evidências reais

- O repositório permanece privado.
- Unity Tests `41293713412` passou em EditMode e PlayMode.
- O AVD histórico executou o APK `41211856635` com API 35, `google_apis`, x86_64, Pixel 6 e SwiftShader/OpenGL ES 3. O log registrou `Fully drawn ... UnityPlayerActivity`, `[NEXO_BOOT]` e carregamento da cena `NexoVerticalSlice`, sem FATAL EXCEPTION, SIGSEGV ou exceção Unity.
- A captura do AVD mostra a interface do NEXO renderizada, mas com a sobreposição do Android “Viewing full screen / To exit, swipe down...”. Essa camada é a confirmação educativa do modo imersivo do sistema e foi capturada antes de ser dispensada; não é um painel do NEXO.
- A captura histórica também mostra quadrados magenta. A auditoria de código localizou a origem no `ParticleSystem` `Nexo Intro Floating Lights`, que não tinha material explícito. O commit `54e67ec` atribui material mobile-safe derivado do material Unlit e desativa o renderer quando o fallback não existe.
- A geometria verde é intencional: `Island` e `GreenDoor` são criados no bootstrap com cores verdes. Não há evidência de `Color.magenta`, sprite magenta ou shader graph magenta no código auditado.

## Causas separadas

1. **Sobreposição de tela cheia:** comportamento do Android/Unity em modo imersivo no primeiro uso do AVD; não é falha de cena. A captura do smoke test ocorre antes da dispensa.
2. **Quadrados magenta no APK antigo:** material implícito do ParticleSystem, corrigido no código por material explícito e fallback seguro. Ainda falta confirmação no APK novo.
3. **Fundo verde:** design procedural intencional, associado a `Island`/`GreenDoor`; não é shader ausente.
4. **Interatividade:** o Canvas possui `EventSystem`, `StandaloneInputModule`, `GraphicRaycaster`, `ScreenSpaceCamera`/fallback Overlay e botão com `onClick`. O `Update` também trata toque e mouse, portanto há duas rotas de entrada. A validação de toque no APK atual ainda está pendente.
5. **Bloqueio atual:** Android run `41299986311` permanece `in_progress` no passo `Build Android package` desde 11:02 UTC, sem logs finais, APK ou job AVD. Isso impede validar a correção `54e67ec` no emulador.

## Correção justificada para o smoke test

A próxima execução deve marcar a confirmação imersiva do Android como confirmada antes da captura e aguardar a janela de boot. Isso remove uma sobreposição do sistema da evidência visual sem alterar o jogo. Não foi aplicada durante o build em andamento.

## Estado

- Renderização histórica: PASS parcial (app abriu e cena renderizou).
- Correção de partículas: aplicada no código, runtime novo: NOT_RUN.
- Interatividade: infraestrutura presente, teste de toque novo: NOT_RUN.
- Build Android novo: BLOCKED por hang/timeout do GameCI.
- Compatibilidade ampla: não comprovada por um único AVD.

## Correção aplicada nesta etapa

- 	ools/android_smoke_test.sh agora confirma immersive_mode_confirmations no AVD antes do launch e aguarda 20 s antes da captura. Isso remove o overlay educativo da evidência sem modificar o app.
- A sintaxe Bash foi validada com C:\Program Files\Git\bin\bash.exe -n (PASS).
- Não foram feitas alterações arbitrárias no bootstrap ou nos materiais.

