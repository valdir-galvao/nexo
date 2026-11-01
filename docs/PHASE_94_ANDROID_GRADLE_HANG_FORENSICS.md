# NEXO — Fase 94: perícia do travamento Gradle Android

**Execução:** 2026-11-01, America/Sao_Paulo  
**Run:** `41305244451`  
**Commit testado:** `57e2b29463c165a5bfe804ec5ec77c715ea7b869`  
**Estado:** `FAIL` no empacotamento Android; APK não gerado.

## Evidência

- O erro anterior de branch suja foi eliminado: a versão semântica foi calculada e o container GameCI iniciou normalmente.
- Unity 6000.3.0f1 abriu o projeto, resolveu a etapa de build e concluiu a fase Unity/IL2CPP.
- O processo chegou a `GradleMain ... assembleRelease`.
- O watchdog registrou, às 13:01:34 UTC, `/dev/root` com 72 GB usados, 505 MB livres e 100% de uso.
- Na mesma amostra, o container Android permanecia ativo e o Gradle ainda existia; havia memória disponível e swap, sem indicação de falha de licença ou exceção C#.
- O GameCI encerrou `Build Android package` pelo timeout de 50 minutos. O job terminou `FAIL`.

## Diagnóstico

A causa comprovada do travamento desta execução é **exaustão de armazenamento do runner durante o Gradle `assembleRelease`**. O limite de tempo apenas encerrou um processo que ficou sem espaço para finalizar o empacotamento. Não há evidência nesta execução de defeito no bootstrap, cena, PlayMode, licença Unity ou resolução C#.

## Artefatos

- `BUILD_METADATA.txt`: presente, mas é metadado emitido por uma etapa `always()` e não prova que o build foi concluído.
- APK: **ausente**.
- Diagnóstico: watchdog e lista de arquivos preservados em `artifacts/run-41305244451/`.
- AVD: `SKIPPED`, pois depende do sucesso do build Android.

## Próxima correção mínima

1. Liberar espaço antes do GameCI sem tocar no checkout: remover caches/SDKs descartáveis do runner e invalidar caches Gradle/Unity quando necessário.
2. Monitorar espaço dentro do container e falhar cedo com diagnóstico se o volume ficar abaixo de uma margem segura.
3. Reexecutar somente após essa correção; baixar o APK e executar o AVD apenas se `Build Android package` terminar com `SUCCESS`.

## Classificação

`BLOCKED`: a infraestrutura gratuita do runner não manteve espaço suficiente para concluir o empacotamento. Não há APK novo nem validação funcional Android nesta rodada.
