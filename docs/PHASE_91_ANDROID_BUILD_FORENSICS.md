# NEXO — Perícia da geração do APK Android

Data: 2026-11-01 (America/Sao_Paulo)

## Execução periciada

- Run: `41299986311`
- Commit: `91273558bdd38eac1f9445279b894476d4e0c7cb`
- Workflow: `unity-android.yml`
- Runner: `ubuntu-latest`
- Unity: `6000.3.0f1`
- Resultado: `FAIL`
- Job: `Build Android APK`
- Início do job: 11:01:06 UTC
- Início do passo Unity: 11:02:05 UTC
- Encerramento: 11:55:11 UTC

## Evidência

Todos os passos anteriores ao GameCI passaram: limpeza, checkout, versão Unity, perfil Android, cache e diretório de logs. O passo `Build Android package` permaneceu `in_progress` até o encerramento do job. A API do GitHub retornou `log not found` para o job e `gh run download` retornou `no valid artifacts found to download`. O job AVD foi `skipped` porque dependia do sucesso do build.

Não existe APK produzido por este run e não há `BUILD_METADATA.txt` desse run. Não é uma falha de instalação, shader ou interatividade: o aplicativo nem chegou a ser empacotado.

## Causa classificada

Causa comprovada: timeout/hang no processo GameCI/Unity IL2CPP/Gradle durante `Build Android package`, sem log final disponibilizado pelo runner. A causa interna do hang (processo Unity, Docker, IL2CPP, Gradle ou infraestrutura do runner) não pode ser determinada porque o próprio runner não preservou logs.

A comparação com o run histórico `41211856635` mostra que o mesmo projeto, Unity `6000.3.0f1`, arquitetura ARM64+x86_64 e workflow já completaram a build. Portanto, não há evidência de que a geometria verde, o ParticleSystem ou o Canvas tenham causado esta falha específica.

## Correções aplicadas

- Timeout do job já havia sido ampliado de 45 para 60 minutos.
- Cache Unity/Gradle é invalidado antes da build.
- O smoke test foi corrigido no commit `52ce0ce` para confirmar o overlay imersivo do AVD e capturar a tela após 20 segundos.
- Nenhuma alteração arbitrária foi feita no bootstrap ou nos materiais para mascarar o hang.

## Próxima correção necessária

A próxima tentativa deve ser feita em uma execução limpa e observável, com diagnóstico persistente do `Editor.log`/Gradle e sem reutilização de cache nativo. Se o mesmo timeout ocorrer novamente, o bloqueio deve ser tratado como limitação do runner/GameCI, não como defeito funcional do aplicativo.

Estado: `BLOCKED` para APK novo; evidência histórica do APK anterior permanece válida apenas para aquela versão.
