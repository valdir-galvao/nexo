# PHASE 95 — Diagnóstico do run Android 41324298511

## Resultado

`FAIL` no workflow Android. A preparação passou e o job alcançou `Build Android package` usando Unity 6000.3.0f1, ARM64 e Mono2x para homologação. O processo terminou após aproximadamente 51 minutos com `No space left on device` ao gravar o log interno do runner GitHub Actions em `/home/runner/actions-runner/cached/2.338.2/_diag/Worker_20261101-160312-utc.log`.

## Evidência

- Run: `41324298511`
- Commit: `a6426fb`
- Job: `Build Android APK`
- Preparação: PASS
- Build Android package: interrompido pelo runner; sem conclusão reportada
- Artefatos: nenhum APK/metadata utilizável
- AVD: SKIPPED, pois depende do job Android
- Erro primário: falta de espaço no volume do runner

O erro ocorre na infraestrutura do runner, não há evidência nesta execução de exceção do aplicativo ou falha de compilação C#.

## Correção aplicada

O workflow foi ajustado para remover, antes do GameCI, caches e toolchains host que não são necessários para o container Unity: toolcache, SDKs host, caches de gerenciadores de pacotes, listas APT e diretórios volumosos pré-instalados. A limpeza continua sem tocar no checkout do projeto e sem alterar a configuração de release do jogo.

## Estado

`BLOCKED`: ainda falta uma execução remota que conclua o build e produza um APK. A estratégia continua Android-only, custo zero, sem alteração de visibilidade do repositório. O perfil Mono2x+ARM64 é homologação; IL2CPP permanece no caminho de release.

## Próxima ação

Enviar a limpeza ampliada, aguardar o novo run Android e somente após `success` baixar o artefato e executar o smoke test AVD quando a arquitetura X86_64 for solicitada.
