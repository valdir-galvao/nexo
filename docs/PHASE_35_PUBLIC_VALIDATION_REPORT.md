# Fase 35 - validacao publica controlada

Data: 29/10/2026 (America/Sao_Paulo)
Commit final testado: `b4713f2e7100441e147ade8e0ca5e7a3a2efa890`
Repositorio: `valdir-galvao/nexo`

## Estado dos runs

| Run | Plataforma | Estado | Evidencia |
|---|---|---|---|
| 40830355578 | Unity tests | PASS | EditMode 16/16; PlayMode 1/1; categoria K005 no XML |
| 40830355608 | Android | CANCELLED | ficou em Build Android por aproximadamente 15 min; nenhum APK/metadata |
| 40830355598 | iOS | FAIL | projeto Xcode gerado em `build/iOS/iOS`; GameCI falhou na validacao pos-build |

## Causas determinadas

1. O primeiro PlayMode foi `NotRunnable` porque NUnit rejeita hifen em nomes de categoria. `Category("K-005")` foi substituido por `Category("K005")`; o XML preserva a identificacao K005.
2. A execucao seguinte iniciou em `Exploring` porque o save persistente podia sobreviver entre jobs. O SetUp remove somente `nexo-save.json`, seu backup e temporario, tornando o teste deterministico.
3. Android falhou inicialmente porque GameCI forneceu `BUILD_PATH=build/Android/Android` e o script tratou o diretorio como arquivo APK. `AndroidBuild` agora acrescenta `Nexo.apk` quando o caminho nao termina em `.apk`. A nova execucao entrou no build, mas foi cancelada por tempo sem gerar artefato.
4. iOS chegou a gerar o Xcode e exibiu `Build succeeded` no Unity, mas `unity-builder@v6` retornou erro na validacao pos-build. Nao foi falha de licenca nem de compilacao C#.

## Licenca e runner

A ativacao Personal por `UNITY_EMAIL`/`UNITY_PASSWORD` foi concluida nos runs; nao houve erro 403 do CLI nem falta de espaco antes do GameCI. Nenhum segredo foi registrado.

## Privacidade

O repositorio foi publico apenas durante a janela de teste e foi restaurado para `private=true`/`visibility=private` via API apos o encerramento.

## Artefatos

Os artefatos de testes foram baixados localmente em `C:/Temp/nexo-ci-public-40830355578`. Nao existe `Nexo.apk` para download nesta rodada.

## Proxima acao

Corrigir o contrato de saida/validacao do builder iOS e executar Android em runner com tempo suficiente, mantendo o repositorio privado; repetir somente apos revisar a configuracao de `buildsPath`/`BUILD_PATH`.
