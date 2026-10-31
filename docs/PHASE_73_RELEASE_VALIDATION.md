# NEXO — Validação final da Fase 73

## Resultado

- Commit: `ce335a6dc1648a0b38aaaa2cc739550fd0268ae3`
- Unity Tests: run `41211856704` — `success`
- Android Build: run `41211856635` — `success`
- Android Emulator smoke: job concluído com `success`
- iOS Xcode/IPA: run `41211856610` — `success`
- Repositório: `private=true`, `disabled=false`

## Evidência Android

O APK foi instalado pelo `adb` no AVD `Google sdk_gphone64_x86_64`, Android 15/API 35, com OpenGL ES 3 via SwiftShader. O smoke test registrou `Performing Streamed Install / Success`, a arquitetura `x86_64`, a cena `NexoVerticalSlice` e a sessão `[SESSION_START]`. O logcat registrou `[NEXO_BOOT] Awake` e `[NEXO_BOOT] Start`; não houve `FATAL EXCEPTION`, `NullReferenceException`, `Shader error` ou erro de shader Unity.

- APK: `C:/Temp/nexo-release-final/Android/Nexo.apk`
- Tamanho: 59.521.191 bytes
- SHA-256: `fb491cf5780c3432a7407bc603e04f6e37f66f01c30f975e784d765e0aeaf9b2`
- Metadata: `C:/Temp/nexo-release-final/Android/BUILD_METADATA.txt`
- Arquiteturas confirmadas no APK: `arm64-v8a`, `x86_64`
- Diagnósticos: `C:/Temp/nexo-release-final/Android/diagnostics/`
- Evidência visual: `C:/Temp/nexo-release-final/Android/diagnostics/screenshot.png`

A captura contém um aviso do sistema do AVD sobre modo fullscreen; ele é uma sobreposição do emulador. A UI do NEXO aparece por trás, com título, subtítulo, botão de missão e partículas. As cores verde/amarela são parte da paleta procedural do tema; não houve fallback magenta nem erro de shader no log.

## Evidência iOS

O workflow exportou e empacotou um IPA sem assinatura Apple para teste posterior em ambiente autorizado.

- IPA: `C:/Temp/nexo-release-final/ios/Nexo.ipa`
- Tamanho: 40.255.180 bytes
- SHA-256: `c220651d31d322726219a18211b04e6ca1587851141bc4e2b32d3053b31238d1`
- Metadata: `C:/Temp/nexo-release-final/ios/BUILD_METADATA.txt`

O artefato iOS não foi instalado nesta máquina Windows; assinatura e instalação em iPhone continuam dependentes de provisionamento Apple e dispositivo autorizado.

## Correções estruturais aplicadas

- `NexoQualityProfile`: perfis Low/Medium/High com seleção por RAM, memória gráfica e CPU, além de override manual persistente.
- `ConfigureMobileGraphics`: OpenGLES3 primeiro e Vulkan opcional no Android; Metal no iOS; API mínima/target e arquiteturas preservadas.
- `RuntimeDiagnostics`: GPU, API gráfica, memória, resolução, perfil e erros de runtime sem dados pessoais.
- `NexoVerticalSliceBootstrap`: inicialização ordenada, câmera/luz/Canvas defensivos, fallback de Canvas Overlay e logs de boot distintos.

## Limitações e próximos testes

Não foram executados Appetize.io, aparelhos Android físicos, simulador iOS ou iPhone físico. Portanto, o resultado é `CANDIDATO TÉCNICO APROVADO` para a infraestrutura e o smoke test Android emulado, com validação física multiplataforma pendente. Não há evidência suficiente para declarar compatibilidade universal.
