# PHASE 108 — Artefatos finais após dispatch público

A sequência correta foi executada: visibilidade PUBLIC confirmada antes dos dispatches, workflows iniciados, artefatos baixados e visibilidade restaurada para PRIVATE.

## Resultados

- Unity Tests `43119213947`: SUCCESS; EditMode e PlayMode concluídos.
- Android `43119218369`: SUCCESS; APK ARM64 gerado.
- iOS `43119224274`: SUCCESS; IPA/Xcode artifact gerado.

## Artefatos

- APK: `C:/Temp/nexo-release-final/Android/Nexo-ARM64-fixed.apk`
  - 59.528.831 bytes
  - SHA-256 `0A4BB575D586695514764A36EFA6560DCC146547B307F46B2F877D638E2B2D79`
- IPA: `C:/Temp/nexo-release-final/iOS/Final-43119224274/Nexo.ipa`
  - 40.260.002 bytes
  - SHA-256 `5258A26AC239ADC7BA285439F047127D6637158BC51F02391717B46DBCFE07A4`

Ambos estão vinculados ao commit `28c18ce51e36f0483096d315319d57ceecf13fe5` e Unity `6000.3.0f1`. O Android foi compilado no perfil de homologação Mono2x/ARM64; o IPA é exportação sem assinatura Apple para testes locais.

Estado final do repositório: PRIVATE.
