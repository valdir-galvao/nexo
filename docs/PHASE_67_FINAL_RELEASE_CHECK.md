# Fase 67 - verificacao final e artefatos

Data: 31/10/2026 America/Sao_Paulo
Commit remoto testado: `e3b625cfe2a71c981ed8a7715525c70b8e90e420`

- Static QA: PASS (`json=5`, `cs=25`).
- Unity Tests `41202218139`: SUCCESS em EditMode e PlayMode.
- Android `41202218132`: SUCCESS; APK baixado em `C:/Temp/nexo-release-final-f67/Android/Nexo.apk`.
- O APK foi inspecionado como ZIP e contem `lib/arm64-v8a` e `lib/x86_64`; nao depende de `ndk_translation`.
- APK: 59517971 bytes; SHA256 `5a2720970426be3e6fb2d095c647010e28b63bcb163f6e0452002a576f948d48`.
- iOS `41202218048`: SUCCESS; IPA em `C:/Temp/nexo-release-final-f67/ios/Nexo.ipa`, 40258517 bytes, SHA256 `6a01342f8d35339fa4e14c922970e69da94f5af6a34e409be42ee4dffc34dd80`.
- Repositorio restaurado para privado (`disabled=false`).

A metainformacao do workflow foi corrigida localmente para registrar `ARM64+x86_64`; o APK ja contem ambas as arquiteturas. Essa alteracao de metadata ainda nao foi enviada para evitar um novo build automatico.
