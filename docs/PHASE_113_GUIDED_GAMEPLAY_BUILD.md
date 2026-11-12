# PHASE 113 — Guided gameplay build recovery

Data: 2026-11-12 America/Sao_Paulo
Commit testado: `721392139476e4eed1dbfdcffe70de2f70207756`

## Causa e correção

A primeira rodada do commit `34262f7` falhou antes da descoberta dos testes por `CS0106` em `NexoVerticalSliceBootstrap.cs`: os métodos `CreateExplorerAvatar`, `CreateVisualPart`, `CreateWaterRipples`, `CreateObjectiveMarker` e `SetObjectiveStep` ficaram dentro de `BuildWorld()` por uma chave ausente. A correção mínima foi fechar `BuildWorld()` antes de `CreateExplorerAvatar` e remover a chave excedente. Não houve evidência de falha de licença: a ativação GameCI retornou `ASSIGN_SEAT`/HTTP 200.

## Validação local

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=26`, exit 0).
- `git diff --check`: PASS.
- Commit da correção: `7213921`.

## Runs remotos

- Unity Tests `43137663651`: SUCCESS; EditMode e PlayMode concluídos após a correção de compilação.
- Android `43137667979`: SUCCESS; `Build Android package` concluído, artefato publicado.
- iOS `43137671962`: SUCCESS; projeto Xcode e IPA sem assinatura publicados.

O smoke AVD não foi executado nesta rodada porque o dispatch Android usou a arquitetura padrão ARM64; o job de emulador exige X86_64. Isso não invalida a build, mas deixa a validação visual em emulador como pendência separada.

## Artefatos

- APK ARM64: `C:/Temp/nexo-release-final/Android/Run-43137667979/Android/Nexo.apk`
  - tamanho: 59.553.859 bytes
  - SHA-256: `E85254108987A54EF95B47D6446EAAAA428DAB5F8496AA30F19C821B3F7B761D`
- Metadados: `C:/Temp/nexo-release-final/Android/Run-43137667979/BUILD_METADATA.txt`
- IPA sem assinatura: `C:/Temp/nexo-release-final/iOS/Run-43137671962/Nexo.ipa`
  - tamanho: 40.274.914 bytes
  - SHA-256: `BC97F4244427B0411041C353E5E42C5BFA12521DA6C747C8ADA69470B7921BF4`

## Segurança

O repositório foi público somente durante o ciclo autorizado e foi confirmado `PRIVATE` após o download dos artefatos. O estado global é `BUILD APROVADO, TESTES PENDENTES` para validação visual/AVD desta revisão; a suíte Unity e as compilações remotas estão aprovadas.
