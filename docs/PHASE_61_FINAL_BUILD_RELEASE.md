# Fase 61 - Release build end-to-end

Data: 31/10/2026 America/Sao_Paulo
Commit de preparacao: `221a7dc` (alteracoes pendentes serao consolidadas no commit desta fase).

## Preflight

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`).
- `git diff --check`: PASS.
- Escopo consolidado: Game Juice visual/sonoro, progressao de tres fases, raycasting/UI e Safe Area.
- Perfil Android auditado: IL2CPP, ARMv7+ARM64, Min API 29, Target API 35.

## Execucao remota

Esta fase autoriza ciclo controlado de visibilidade: publico somente durante a execucao dos workflows, seguido de restauracao privada apos download e verificacao dos artefatos. Os estados dos runs, hashes e caminhos locais devem ser registrados abaixo sem expor secrets.

### Artefatos desta fase

- Android APK: `PENDING` ate o run remoto terminar.
- iOS IPA: `PENDING` ate o run remoto terminar.
- Smoke test e validacao fisica: registrar separadamente do sucesso de compilacao.

## Gate

Nenhum pacote e considerado gerado antes de existir um artefato local, hash e run vinculado ao commit desta fase.

## Resultado da execucao remota

O push do commit `9233d4a4f748ba1f29f745aa0eca8b7b3e4dc8d3` disparou:

- Unity Android: run `41174760334`, `failure`.
- Unity tests: run `41174760342`, `failure` nos jobs EditMode e PlayMode.
- Unity iOS: run `41174760333`, `failure`.

Os jobs falharam antes de apresentar steps executados (`jobs` sem steps e logs retornados vazios, ZIP de 22 bytes). Isso caracteriza bloqueio de provisionamento/capacidade do runner antes do Unity/GameCI, e nao falha comprovada de codigo ou licenciamento. Nenhum APK, IPA ou projeto Xcode foi gerado nesta rodada. A visibilidade foi restaurada para `private=true`, `disabled=false` imediatamente apos a verificacao.

Estado da release desta fase: `BLOQUEADO POR DEPENDENCIA EXTERNA` (runner). Os artefatos anteriores permanecem validos somente para seus commits de origem; nao representam o commit `9233d4a`.

## Retentativa controlada

A retentativa dos mesmos runs (attempt 2) repetiu o bloqueio:

- Android job `124460448114`: failure, runner vazio, zero steps, 3 s.
- Unity tests jobs `124460450339` e `124460450471`: failure, runner vazio, zero steps.
- iOS job `124460453792`: failure, runner vazio, zero steps.

A API confirma `visibility=private`, `private=true`, `disabled=false`. O diagnóstico fica fechado como indisponibilidade/provisionamento do GitHub-hosted runner; nao ha correcao de codigo a aplicar para esse sintoma. A proxima tentativa deve ocorrer somente apos disponibilidade confirmada dos runners ou em runner self-hosted autorizado.

## Diagnostico da retentativa publica (attempt 3)

A visibilidade publica permitiu provisionar runners reais. O resultado revelou duas falhas independentes:

- Android run `41174760334`: o job iniciou, mas o Docker/Unity falhou por falta de disco. O log registra 21 MB livres e `no space left on device` ao registrar `unityci/editor:ubuntu-6000.3.0f1-android-3`. Nenhum APK foi produzido.
- Unity tests run `41174760342`: EditMode SUCCESS; PlayMode falhou com `Caught fatal signal - signo:11` durante `Begin MonoManager ReloadAssembly` no Unity 6000.3.0f1. A licença foi resolvida e o projeto importou; a falha ocorreu no processo nativo antes da descoberta dos testes PlayMode.
- iOS run `41174760333`: ainda em execução no passo `Build unsigned iOS Xcode project` quando a visibilidade foi restaurada; artefato ainda nao confirmado.

A visibilidade foi restaurada para privada (`private=true`, `disabled=false`). O erro de runner vazio foi superado, mas nao houve release nova. As correcoes necessarias sao: limpeza de disco mais agressiva/runner com maior volume para GameCI Android e isolamento/reproducao do crash nativo PlayMode (sem alterar asserts por tentativa).
