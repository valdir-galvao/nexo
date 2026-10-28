# Fase 15 — Monitoramento dos runs GameCI

Data da verificação: 28/10/2026 (America/Sao_Paulo)
Commit testado: `992586a5c0dd3a8c8739e5468526e67c28ef53e6` (`992586a`)
Branch: `orchestrator/phase-a-baseline`

## Resultado dos runs

| Run | Workflow | Resultado | Evidência |
|---|---|---|---|
| `40661478289` | Unity tests | **FAIL** | EditMode e PlayMode falharam durante compilação de scripts, antes da descoberta/execução NUnit |
| `40661478460` | Unity Android build | **FAIL** | Compilação abortada por erros C#; nenhum APK/`BUILD_METADATA.txt` produzido |
| `40661478368` | Unity iOS Xcode project | **FAIL** | Exportação abortada por erros C#; nenhum projeto Xcode produzido |

## Causa raiz

Os três workflows chegaram ao Unity 6000.3.0f1 e resolveram os pacotes, mas a compilação de `Assembly-CSharp-Editor` falhou com:

- `Assets/Scripts/Editor/Build/AndroidBuild.cs(20,27): error CS0246: BuildFailedException`
- `Assets/Scripts/Editor/Build/IosBuild.cs(20,27): error CS0246: BuildFailedException`
- `Scripts have compiler errors.`

A causa é uma referência de tipo sem o namespace/assembly correto nos scripts de build do Editor. O defeito é de código C#, não de licença nem de espaço em disco.

## Licenciamento e espaço

A licença Personal foi resolvida nos runners (`Successfully resolved entitlement details`, `Product: Unity Personal`, `Successfully updated license`) e retornada ao final. As mensagens de ausência de ULF durante a limpeza são informativas do modo entitlement-based e não foram a causa da falha. O erro anterior de espaço em disco não reapareceu nesta rodada.

## Artefatos

- Testes: artefatos de logs/diagnóstico foram gerados para EditMode e PlayMode; não há relatório NUnit aprovado porque a compilação falhou antes dos testes.
- Android: apenas `nexo-android-diagnostics-6000.3.0f1-992586a5c0dd3a8c8739e5468526e67c28ef53e6` (146 bytes); sem APK.
- iOS: apenas `nexo-ios-diagnostics-6000.3.0f1-992586a5c0dd3a8c8739e5468526e67c28ef53e6` (146 bytes); sem Xcode exportado.

## Próxima correção necessária

Adicionar a referência correta para `BuildFailedException` nos dois scripts de build, executar validação local disponível e repetir primeiro `unity-tests.yml`. Somente após os testes Unity passarem deve-se repetir Android/iOS. Nenhum build de dispositivo foi instalado ou publicado.

Estado da Fase 15: **FAIL**. Estado global: **REPROVADO POR DEFEITO**.
