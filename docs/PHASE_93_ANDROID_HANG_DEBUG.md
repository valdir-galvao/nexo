# NEXO — Instrumentação de diagnóstico do hang Android

Data: 2026-11-01 (America/Sao_Paulo)

## Alteração

O workflow Android agora inicia um watchdog antes do GameCI. A cada 30 segundos ele salva em `diagnostics/build-watchdog.log`:

- espaço em disco;
- memória;
- containers Docker e `docker stats`;
- processos Unity, Gradle, Java, IL2CPP, Bee e GameCI com tempo, CPU e memória.

O passo `Build Android package` recebeu `timeout-minutes: 50`, abaixo do limite de 60 minutos do job. Isso permite que o passo seguinte pare o watchdog e colete `diagnostics/build-watchdog.log`, `Editor.log` e arquivos de build antes de o job ser encerrado pelo timeout global.

## Objetivo diagnóstico

A instrumentação diferencia:

- falta de espaço ou memória;
- container Docker parado;
- processo Unity/IL2CPP consumindo CPU;
- Gradle/Java bloqueado;
- ausência de progresso com processo ocioso.

Não altera código do jogo, arquitetura, licença ou visibilidade do repositório.

## Validação local

- Static QA: PASS (`json=5`, `cs=26`)
- YAML via PyYAML: PASS
- `git diff --check`: PASS

O próximo run deve ser usado como execução pericial. Se houver novo hang, o artefato de diagnóstico será a evidência principal para a correção seguinte.

## Causa encontrada no run 41305021307

O primeiro run instrumentado falhou em 4 segundos porque o watchdog gravou arquivos dentro de diagnostics/ antes do GameCI. O ersioningStrategy: Semantic classificou a árvore como suja e abortou: Branch is dirty. Refusing to base semantic version on uncommitted changes. A correção move o heartbeat para $RUNNER_TEMP e só copia o log após o GameCI terminar.
