# Fase 60 — Correção do fechamento no Android

Data: 31/10/2026 (America/Sao_Paulo)

## Causa raiz

O smoke test do APK anterior `41077501341` reproduziu exatamente o problema:

```text
** No activities found to run, monkey aborted.
```

A causa era `Assets/Plugins/Android/AndroidManifest.xml`, que substituía o manifesto gerado pelo Unity e declarava apenas `<application>`, sem uma Activity com `MAIN`/`LAUNCHER`. O APK instalava, mas o Android não tinha uma Activity inicial para abrir.

## Correção

O manifesto customizado e seu `.meta` foram removidos no commit `761fdcf`, permitindo que o Unity gere o manifesto padrão com `com.unity3d.player.UnityPlayerActivity` e o intent launcher.

## Evidência

- Build Android `41167786858`: SUCCESS.
- Smoke test no AVD API 35: SUCCESS.
- Log: `START ... com.nexo.educational/com.unity3d.player.UnityPlayerActivity`.
- Log: `Displayed ... UnityPlayerActivity` e `Fully drawn`.
- APK: `C:/Temp/nexo-android-41167786858/Android/Nexo.apk`.
- Tamanho: 31.264.831 bytes.
- SHA256: `32d686f440182802c0e7f9faa9b5afebbb1f61a5f875bfb2a75d193c0ecdae32`.
- Diagnósticos: `C:/Temp/nexo-emulator-41167786858/`.
- Screenshot capturada mostra apenas a confirmação de tela cheia do Android; não houve crash do app.

O repositório foi restaurado para `private` e `disabled=false`.
