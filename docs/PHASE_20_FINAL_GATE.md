# Fase 20 — Gate final de testes e builds

Data: 29/10/2026 (America/Sao_Paulo)
Commit testado: `abca64ab78edb3c24f92583835bf04709f1af2db`

## Resultado

- Unity tests `40667016837`: **FAIL**.
  - EditMode: **FAIL antes da execução NUnit**, pois `game-ci` não conseguiu resolver a versão mais recente do CLI: GitHub API `403`. Pass rate não é calculável como aprovação; testes executados: 0.
  - PlayMode: **FAIL**, `1` teste descoberto, `0` aprovado, `1` reprovado. `NexoPlayModeTests.VerticalSliceSceneInitializesIntoIntro` falhou em `Nexo.PlayModeTests.cs:16`, com `ArgumentException: Invalid value of 'activeInputHandler' setting: -1`.
  - K-005: não há teste/evidência de aprovação neste run; estado **NOT_RUN**.
- Android `40667016637`: **FAIL**. Não houve APK nem `BUILD_METADATA.txt` de build; o Unity terminou com `UnityException: Error` e `BuildResult.Failed`.
- iOS `40667016611`: **FAIL**. A versão iOS inválida não aparece mais; o projeto avançou para IL2CPP/exportação, mas o workflow terminou com falha e não gerou artefato final verificável.

## Artefatos

Os únicos artefatos disponíveis são diagnósticos e resultados de teste parcial, vinculados ao commit acima. Não existe `Nexo.apk` para download nem exportação Xcode aprovada.

Estado global: **REPROVADO POR DEFEITO**. O gate móvel permanece bloqueado.
