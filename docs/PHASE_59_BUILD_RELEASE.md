# Fase 59 - Preparacao da release Android

Data: 31/10/2026 (America/Sao_Paulo)

## Versao candidata

- Unity: `6000.3.0f1`
- Produto: NEXO
- Bundle: `com.nexo.educational`
- Versao: `0.1.0-rc1`
- Tag planejada: `v0.1.0-rc1`
- Cena de build: `Assets/Scenes/NexoVerticalSlice.unity`

## Perfil Android

- Build sem `Development Build` no metodo customizado: Release tecnico.
- IL2CPP (`scriptingBackend: Android: 1`).
- ARMv7 + ARM64 (`androidTargetArchitectures: 3`).
- Min API 29 e Target API 35.
- O workflow valida esses valores antes de iniciar o Unity Builder.
- `BUILD_METADATA.txt` passa a registrar modo, APIs, arquiteturas, backend, commit e run ID.

## Gate local

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`).
- `git diff --check`: PASS.
- Parser YAML do workflow Android: PASS.
- Unity Editor e build local: indisponiveis nesta maquina; validacao remota GameCI e o proximo gate.

## Estado

Configuracao pronta para disparo remoto. O artefato APK so pode ser considerado aprovado apos o workflow concluir com sucesso e o smoke test do emulador confirmar instalacao e abertura.

## Resultado remoto


P131 - 31/10/2026 America/Sao_Paulo - Release candidate executada no commit `404947660f932631110cb238dbbdc91b5fb2506b`. Unity Tests `41171249209`: SUCCESS. Android `41171249169`: build Release SUCCESS e smoke test AVD API 35 SUCCESS; APK em `C:/Temp/nexo-release-4049476/android/Android/Nexo.apk`, 31.274.715 bytes, SHA256 `5075FDFFC3FC7F65C24B354EA5B5DF9A655EAA76458D79B899F358875862B312`. iOS `41171249180`: SUCCESS; IPA em `C:/Temp/nexo-release-4049476/ios-xcode3/Nexo.ipa`, SHA256 `879E864EE7404BF95E4DD6F9C5728E9CA78455F8F6561C83F221E2F1C7AAACB0`; projeto Xcode no mesmo diretorio. Repositorio restaurado PRIVATE (`disabled=false`).
