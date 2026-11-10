# PHASE 97 — Correção do smoke test e validação X86_64

## Execuções

- ARM64 run `42717060218`: SUCCESS, APK 59.5 MB, SHA-256 `03988140361DC080C313EA979626CD87F1C2AA500AE1D536CF769789154E43EC`.
- X86_64 run `42721282381`: SUCCESS, APK 59.5 MB, SHA-256 `1393C9C2F7559D87919862A5AF61D7C389555D77825F2525CAB95A6BE167463F`.
- AVD API 35/x86_64/SwiftShader: instalação e Activity Unity confirmadas; `ResumedActivity` apontou `com.nexo.educational/com.unity3d.player.UnityPlayerActivity`.

## Problema encontrado no diagnóstico

A captura do smoke test mostrou `Pixel Launcher isn't responding`, um diálogo do sistema do emulador cobrindo a aplicação. Isso invalida a screenshot como evidência visual do jogo. Os logs `nexo-runtime.log` também vieram vazios porque o script tentava ler apenas `files/nexo-runtime.log`, enquanto o Unity grava em `/storage/emulated/0/Android/data/com.nexo.educational/files/`.

## Correções aplicadas

- `RuntimeDiagnostics` agora envia a linha `[NEXO_RENDER]` para o arquivo local e também para o logcat.
- `tools/android_smoke_test.sh` identifica e dispensa somente o diálogo de ANR do Pixel Launcher, relança o NEXO, confirma `ResumedActivity` do Unity antes da captura e lê o caminho correto do runtime log.
- `ConfigureMobileGraphics` passa a selecionar apenas OpenGL ES 3 no perfil Android, evitando a sondagem implícita de Vulkan/ES 3.1+ incompatível com SwiftShader. Metal permanece configurado para iOS.

## Estado

O APK foi compilado e instalado no AVD, mas a validação visual final ainda requer um novo run X86_64 após estas correções, porque a captura anterior foi contaminada pelo diálogo do launcher e não contém `[NEXO_RENDER]`. A correção agora torna essa falha observável e impede falso PASS visual.
