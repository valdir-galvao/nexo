# NEXO — Fase 71: release Android/iOS concluída

## Pré-push

- Static QA: PASS (`json=5`, `cs=25`).
- `git diff --check`: PASS.
- Commit enviado: `366eddb656f3b11b5007b40e46693c70a7a461e0`.

## Runs

- Unity Tests `41208174763`: **SUCCESS**.
- Android `41208174770`: **SUCCESS**.
- iOS `41208174746`: **SUCCESS**.

## Artefatos

### Android

- Arquivo: `C:/Temp/nexo-release-final/Android/Nexo.apk`
- Tamanho: `59,521,171` bytes.
- SHA-256: `9fc5b1a4aced867c4d0b77f45aa123b4d33d52f61a54c5784c91261c5cb2b8f5`
- `BUILD_METADATA.txt` confirma Unity `6000.3.0f1`, IL2CPP, API alvo 35, mínima 29 e `ARM64+x86_64`.
- Inspeção ZIP do APK confirmou bibliotecas em `lib/arm64-v8a/` e `lib/x86_64/`.

### iOS

- Arquivo: `C:/Temp/nexo-release-final/ios/Nexo.ipa`
- Tamanho: `40,255,957` bytes.
- SHA-256: `2cd6e30272bb4881e13890fb759846341535b173de536b299a03bdc929cb13fb`.

## Segurança

O repositório foi publicado apenas durante a execução dos runners e downloads. Após a coleta, a API confirmou `private=true` e `disabled=false`.

## Estado

`CANDIDATO TÉCNICO APROVADO` para validação privada nos dispositivos-alvo. A publicação em lojas e a assinatura/distribuição iOS permanecem fora do escopo.
