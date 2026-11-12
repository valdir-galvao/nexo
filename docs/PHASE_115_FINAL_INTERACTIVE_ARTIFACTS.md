# PHASE 115 — Final interactive artifacts and AVD smoke

Data: 2026-11-12 America/Sao_Paulo

## Correção do diagnóstico

O smoke anterior tocava `1080,700`, fora do botão de introdução para a resolução usada pelo AVD. Além disso, o log era coletado antes do toque. O harness foi corrigido no commit `bf6958d` para calcular a resolução, tocar em 16% da altura da tela, coletar log após a interação e falhar quando os marcadores de gameplay não aparecem.

## Gate e runs

- Unity Tests `43150113739`: SUCCESS.
- Android ARM64 `43150113668`: SUCCESS.
- Android X86_64 + AVD `43150590272`: SUCCESS.
- iOS `43150113744`: SUCCESS.

## Evidência AVD

O log X86_64 contém:

- `[NEXO_INPUT] Pointer detected at (540.00, 696.00); intro=True`
- `[NEXO_INPUT] Start Mission button activated.`
- `[NEXO_INPUT] BeginModernMission requested; transitioning from intro.`
- `[NEXO_GAMEPLAY] FadeIntoMission started; gameplay HUD activated.`
- `[NEXO_GAMEPLAY] Mission gameplay active; objective source ready.`
- `NEXO_RENDER ... incompatibleShaders=0 magentaObjects=0`

Captura pós-toque: `C:/Temp/nexo-release-final/Diagnostics/Run-43150590272/screenshot-after-start.png`. Ela mostra HUD `Exploring`, água, pontuação, objetivo da fonte, avatar Explorer, controles direcionais e botão INVESTIGAR.

## Artefatos

- X86_64 APK: `C:/Temp/nexo-release-final/Android/Run-43150590272/Android/Nexo.apk`
  - 59.560.995 bytes
  - SHA-256 `DCB340DFAC601992BB65C23C08409AB2106B8B2EFE18E855FB3B5BDB588A0AA0`
- ARM64 APK: `C:/Temp/nexo-release-final/Android/Run-43150113668/Android/Nexo.apk`
  - 59.560.927 bytes
  - SHA-256 `931098035B6C84C054053035F85E5A4A27BBB1263FB02AFB52133D8DF103AA9B`
- IPA sem assinatura: `C:/Temp/nexo-release-final/iOS/Run-43150113744/Nexo.ipa`
  - 40.279.633 bytes
  - SHA-256 `E011B4C54E23D2B597DEC69317AB54E2AEDEFCB1BC1F14C5241590DE0FE211CC`

## Segurança

O repositório foi público somente durante os dispatches e foi confirmado `PRIVATE` após os downloads. A validação técnica do fluxo de entrada e gameplay no AVD X86_64 está aprovada; compatibilidade universal em hardware físico ainda não foi comprovada.
