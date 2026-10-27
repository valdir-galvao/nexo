# Fase 07 — diagnóstico de triggers e execução remota

## Correção aplicada

Os três workflows receberam `push` restrito a
`orchestrator/phase-a-baseline`. A falha anterior com zero jobs foi causada pelo
guard `if: hashFiles(...)` no nível do job; esse guard foi removido. A verificação
de versão permanece como passo explícito após checkout.

Commit da correção: `5e348f8`.

## Runs finais

| Workflow | Run | Jobs | Resultado | Etapa que falhou |
|---|---:|---:|---|---|
| Unity tests | 40457436612 | 2 | `failure` | `Run Unity tests` em EditMode e PlayMode |
| Android | 40457436774 | 1 | `failure` | `Build Android package` |
| iOS | 40457436628 | 1 | `failure` | `Build unsigned iOS Xcode project` |

Os jobs foram criados e executados. Isso confirma que o bloqueio de trigger foi
resolvido. EditMode e PlayMode não chegaram a produzir JUnit; os dois artefatos
disponíveis continham apenas `BUILD_METADATA.txt` com Unity `6000.3.0f1`, commit
`5e348f8` e o `run_id`.

Android e iOS não produziram artefatos.

## Limitação dos logs

O endpoint de logs detalhados não pôde ser lido com a credencial disponível;
apenas estados, etapas e artefatos foram consultados. A primeira etapa falha é
conhecida, mas a mensagem raiz (licença, pacote ou compilação) exige abrir o log
do job na interface GitHub Actions ou usar token com permissão de leitura de
Actions.

## Estado

Triggers: `PASS`. Jobs instanciados: `PASS`. Testes e builds: `FAIL`.
Nenhum artefato móvel foi aprovado e nenhuma instalação foi realizada.
