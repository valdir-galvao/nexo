# PHASE 87 — Diagnóstico Android com AVD existente

Data: 2026-11-01 (America/Sao_Paulo)
Branch: `orchestrator/phase-a-baseline`
Unity exigido: `6000.3.0f1`
Custo: R$ 0

## Resultado

`FASE 87: BLOCKED`. A integração original do AVD está presente no repositório, mas não há ADB/emulador local nem sessão GitHub autenticada para executar o workflow. A solicitação de alternar visibilidade para público não foi executada: `gh` não está instalado e não há autorização observável para a API. O repositório não foi alterado para público.

## AVD existente identificado

Os workflows atuais usam `reactivecircus/android-emulator-runner@v2` com:

- API level: 35
- target: `google_apis`
- arquitetura: `x86_64`
- perfil: `pixel_6`
- GPU: `swiftshader_indirect`
- opções: `-no-window -no-snapshot -noaudio -no-boot-anim`
- animações desativadas

O script `tools/android_smoke_test.sh` instala o primeiro `Nexo.apk` encontrado, limpa o logcat, inicia `com.nexo.educational` via `monkey`, aguarda 15 segundos, coleta `dumpsys activity`, logcat, screenshot e `nexo-runtime.log`.

## Verificação local

- `adb`: não encontrado no PATH.
- `emulator`: não encontrado no PATH.
- `gh`: não encontrado no PATH.
- Package name confirmado em `ProjectSettings/ProjectSettings.asset`: `com.nexo.educational` para Android.
- Unity `6000.3.0f1`: não disponível localmente.
- APK novo: não gerado.

## Dependências

O parser local confirmou:

- `Packages/manifest.json`: 36 dependências diretas.
- `Packages/packages-lock.json`: 2 entradas.
- 34 dependências sem entrada no lockfile, incluindo TextMeshPro 3.0.6, UGUI 2.0.0 e Test Framework 1.5.1.

Isso não comprova que os pacotes estejam indisponíveis; comprova que a resolução Unity não está registrada. O lockfile não foi editado.

## Build e execução

| Etapa | Estado | Motivo |
|---|---|---|
| Resolução Unity | BLOCKED | Editor 6000.3.0f1 ausente |
| Build Android | NOT_RUN | sem Unity/CI autenticado |
| Instalação AVD | NOT_RUN | sem ADB/emulador local |
| Inicialização APK | NOT_RUN | nenhum APK novo |
| Logs/screenshot | NOT_RUN | AVD não executado |
| Diagnóstico visual | NOT_RUN | requer APK executado |

Não há logs Android novos, erros de execução ou captura de tela desta fase. Portanto não é possível afirmar se os quadrados magenta, a tela verde ou o fechamento do app persistem no APK atual.

## Causa raiz e próximos passos

O bloqueio comprovado nesta fase é de ambiente: ausência de ferramentas locais e de autenticação GitHub, não um novo defeito Android comprovado. A menor sequência segura é:

1. usar uma sessão GitHub autenticada;
2. executar o workflow Android já existente, sem criar outro AVD;
3. preservar o APK e os diagnósticos do job AVD;
4. somente então classificar falhas de dependência, inicialização ou renderização com base em logcat/screenshot.

Nenhum workflow, secret, visibilidade, dependência ou código foi alterado.
