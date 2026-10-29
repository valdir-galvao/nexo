# NEXO ‚Äî Execution Register

Execution ID: `NEXO-20261027-A`
Updated: 27/10/2026, America/Sao_Paulo.

## Baseline

- Initial commit inspected: `0c0c281e04cbdb1b2f8a8ec2a675dcde0d5769e0`.
- Current commit: `0c0c281e04cbdb1b2f8a8ec2a675dcde0d5769e0`.
- Branch: `main`; working tree clean; remote branch matches local.
- Unity: `6000.3.0f1`; candidate version: `0.1.0-rc1`.
- Host: Windows; Unity Editor, dotnet, adb and xcodebuild unavailable.

## State machine

| State | Status | Evidence |
|---|---|---|
| INSPECTION | PASS | repository, Git, tools, workflows and secrets tracking inspected |
| BASELINE | PASS | version, packages, source structure and static validators recorded |
| INFRASTRUCTURE | BLOCKED | Unity secrets and Editor execution unavailable; config static PASS |
| TESTS | NOT_RUN | Unity EditMode/PlayMode cannot run locally |
| BUILD_ANDROID | BLOCKED | GameCI/secrets not executed; no APK |
| BUILD_IOS | BLOCKED | GameCI/macOS not executed; no Xcode artifact |
| DEVICE_QA | NOT_RUN | no Android/iOS device available |
| REVIEWS | BLOCKED | legal/ethical/qualified reviews external to repository |
| RELEASE_CANDIDATE | BLOCKED | source candidate exists; build evidence absent |

## Commands executed

- `git status --short`, `git branch --show-current`, `git log`, `git remote -v` ‚Äî PASS.
- `git ls-remote origin refs/heads/main` ‚Äî PASS; hash matches local.
- `python tools/run_static_qa.py` ‚Äî PASS (`json=4`, `cs=23`).
- `python tools/validate_learning_content.py` ‚Äî PASS.
- `git diff --check` ‚Äî PASS.
- Tool discovery for Unity/dotnet/adb/xcodebuild ‚Äî NOT_FOUND.

## Pending dependencies

GitHub secrets `UNITY_LICENSE`, `UNITY_EMAIL`, `UNITY_PASSWORD`; Unity/GameCI
execution; Android/iOS devices; legal, ethical and qualified pedagogical review.

## Fase B

Configuration review: PASS. YAML, versions, permissions, cache and artifact
metadata validated. Execution result: BLOCKED because credentials and Unity/GameCI
are unavailable. Full details: `docs/PHASE_B_INFRASTRUCTURE.md`.

## Fase C

Result: BLOCKED. Static validators PASS, but import, compilation, EditMode,
PlayMode and persistence runtime tests are NOT_RUN. Details:
`docs/PHASE_C_TEST_REPORT.md`.

## Fase D

Result: `BLOCKED`. The prerequisite Fase C is not approved, so no mission flow,
save/resume, device, or UI functional test was executed. The complete matrix is
in `docs/PHASE_D_FUNCTIONAL_REPORT.md`; all runtime rows are `NOT_RUN`.

## Fase E

Result: `BLOCKED` for Android and iOS. Fases C and D are not approved and no
build was started. Unity/adb/xcodebuild are unavailable locally; no APK, AAB,
Xcode artifact, installation test, or artifact hash exists. Details:
`docs/PHASE_E_BUILD_REPORT.md`.

## Fase F

Result: `BLOCKED`. Fase E has no Android or iOS artifact, and no device or
simulator test was run. Installation, mission flow, offline behavior,
accessibility, performance, suspension, and resume remain `NOT_RUN`. Details:
`docs/PHASE_F_DEVICE_QA_REPORT.md`.

## Fase G

Result: `BLOCKED`. No functional version or device-tested build exists. Technical
privacy review, legal review, pedagogical review, and ethics review are all
`PENDING`; no compliance or educational efficacy claim is made. Details:
`docs/PHASE_G_TRUST_SAFETY_REPORT.md`.

## Next authorized action

Do not advance automatically. After the owner authorizes continuation, configure
the approved Unity secrets and run `Unity tests`; record the workflow URL and
artifacts before attempting Android or iOS builds.

## Fase B2 ‚Äî auditoria CI/GameCI

- Commit inicial: `e59d126`; branch isolada preservada.
- Quatro workflows parseados com PyYAML 6.0.3; permiss√µes, actions, runners,
  secrets referenciados, falhas e artefatos revisados.
- Corre√ß√£o aplicada: gatilhos `push` removidos de Android e iOS; ambos agora
  exigem `workflow_dispatch` para evitar builds autom√°ticos n√£o autorizados.
- PlayMode n√£o est√° coberto; iOS gera somente projeto Xcode n√£o assinado.
- Secrets: `N√ÉO VERIFIC√ÅVEL` localmente; nenhum valor foi exposto.
- Relat√≥rio: `docs/PHASE_B2_CI_AUDIT.md`.
- Estado: `PASS` para auditoria est√°tica/corre√ß√£o; execu√ß√£o GameCI permanece
  `BLOCKED`. Nenhum workflow foi disparado.

## Fase B3 ‚Äî primeiro teste Unity controlado

- Commit confirmado: `8cf86c7`; branch isolada; √°rvore limpa antes do registro.
- Pr√©-condi√ß√µes est√°ticas confirmadas; `unity-tests.yml` executa somente
  EditMode e n√£o inicia builds de plataforma.
- Resultado: `BLOCKED`. `gh` n√£o est√° instalado e n√£o h√° mecanismo autenticado
  dispon√≠vel nesta sess√£o para disparar `workflow_dispatch`.
- Secrets Unity: presen√ßa `N√ÉO VERIFIC√ÅVEL`; nenhum valor foi lido ou exposto.
- Importa√ß√£o, resolu√ß√£o de pacotes, compila√ß√£o e testes Unity: `NOT_RUN`.
- Relat√≥rio: `docs/PHASE_B3_GAMECI_TEST_REPORT.md`.

## Fase C ‚Äî testes completos pr√©-build

- Commit auditado: `37b4246`; branch isolada; nenhum pacote m√≥vel gerado.
- Manifesto: 36 depend√™ncias; lockfile: 2 entradas. Resolu√ß√£o Unity real
  `BLOCKED`; lockfile n√£o foi editado manualmente.
- Assemblies: runtime separado; testes limitados a Editor; 15 `[Test]`, nenhum
  `[UnityTest]`/PlayMode. EditMode, PlayMode e persist√™ncia runtime `NOT_RUN`.
- K-005 confirmado em `docs/KNOWN_ISSUES.md` e `docs/QA_REPORT.md`: contrato de
  transfer√™ncia existe, UI/registro durante a miss√£o n√£o est√£o integrados.
- Validadores locais: `validate_learning_content.py` e `run_static_qa.py` PASS.
- Relat√≥rio: `docs/PHASE_C_PREBUILD_TEST_REPORT.md`.
- Estado: `BLOCKED`; depende de Unity/GameCI para resolu√ß√£o, testes e integra√ß√£o.

## Continua√ß√£o P38

Nenhum canal autenticado de dispatch foi encontrado (`gh` ausente); apenas nomes
de vari√°veis locais foram inspecionados, sem leitura de valores. Nenhuma API,
workflow ou build foi executado. Fase C permanece `BLOCKED`.

## Fase 03 ‚Äî prepara√ß√£o CI/GameCI

- Workflows revalidados com Unity `6000.3.0f1`; testes s√£o manuais/EditMode e
  Android/iOS continuam sem gatilho autom√°tico.
- Criado `tools/resolve_unity_packages.ps1` para resolu√ß√£o real pelo Unity
  Package Manager e verifica√ß√£o de depend√™ncias diretas no lockfile.
- Script parseado, YAML parseado, validadores est√°ticos e pedag√≥gicos PASS.
- Unity/gh ausentes; nenhum dispatch, resolu√ß√£o real, build ou upload ocorreu.
- Relat√≥rio: `docs/PHASE_03_CI_PREPARATION.md`.
- Estado: prepara√ß√£o `PASS`; execu√ß√£o CI e lockfile `BLOCKED`.

## Fase 04 ‚Äî prepara√ß√£o de push/dispatch

- HEAD confirmado: `2d0cf2d`; commits `9499032` e `2d0cf2d` presentes.
- Arquivos K-005, teste PlayMode, `.meta` e script de resolu√ß√£o est√£o rastreados.
- Guia: `docs/PHASE_04_PUSH_DISPATCH_GUIDE.md`.
- Nenhum push ou dispatch foi executado. O workflow atual cobre somente
  EditMode; PlayMode requer execu√ß√£o real antes de ser declarado aprovado.

## Fase 05 ‚Äî matriz PlayMode no CI

- `unity-tests.yml` ajustado para matriz `editmode`/`playmode`, com artefatos e
  metadata separados por modalidade.
- YAML validado com PyYAML; nenhum workflow foi disparado.

## Fase 06 ‚Äî push e monitoramento remoto

- Push da branch conclu√≠do no commit `aa0eeee`.
- GitHub criou runs `40457020841` (tests), `40457019766` (Android) e
  `40457018772` (iOS); todos terminaram `failure` com zero jobs e sem logs de
  Unity. Nenhum artefato foi produzido.
- Dispatch manual retornou HTTP 422; `gh` ausente e permiss√£o/canal de Actions
  n√£o confirmados. N√£o houve repeti√ß√£o de builds.
- Relat√≥rio: `docs/PHASE_06_REMOTE_RUN_REPORT.md`.

## Fase 07 ‚Äî desbloqueio de triggers e runs

- Gatilhos `push` adicionados √† branch de valida√ß√£o; guard `hashFiles` em n√≠vel
  de job removido. YAML v√°lido.
- Commit remoto testado: `5e348f8`.
- Runs `40457436612`, `40457436774` e `40457436628` instanciaram jobs, mas
  falharam nas etapas Unity/GameCI. Testes n√£o produziram JUnit; builds n√£o
  produziram artefatos.
- Logs detalhados n√£o foram acess√≠veis com a credencial dispon√≠vel.
- Relat√≥rio: `docs/PHASE_07_CI_TRIGGER_REPORT.md`.

## Fase 02 ‚Äî corre√ß√µes locais

- Base: `f32e74c`; altera√ß√µes mantidas na branch isolada.
- K-005 implementado com registro persistente e UI de transfer√™ncia; teste NUnit
  de regress√£o adicionado.
- Assembly PlayMode e `[UnityTest]` de inicializa√ß√£o da cena adicionados.
- Validadores locais: PASS (`json=5`, `cs=24`), conte√∫do PASS, 9 testes de
  executor PASS.
- Lockfile: regenera√ß√£o Unity `BLOCKED`; n√£o foi editado manualmente.
- Unity compilation, EditMode, PlayMode e Package Manager: `NOT_RUN`.
- Relat√≥rio: `docs/PHASE_02_HARDENING_REPORT.md`.

## Fase B1 ‚Äî valida√ß√£o local (execu√ß√£o atual)

- In√≠cio: 27/10/2026, America/Sao_Paulo.
- Commit inicial confirmado: `eddb705b52f5007c7a9263bbad21b32907ba151e`.
- Branch de trabalho: `orchestrator/phase-a-baseline`; `main` remoto permanece
  em `0c0c281e04cbdb1b2f8a8ec2a675dcde0d5769e0`.
- √Årvore inicial: limpa. Ferramentas Unity, dotnet, adb e xcodebuild continuam
  indispon√≠veis; nenhum build ou workflow foi iniciado.
- `python tools/run_static_qa.py`: PASS, exit 0, 125 ms antes da corre√ß√£o;
  PASS, exit 0, 120 ms ap√≥s a corre√ß√£o (`json=4`, `cs=23`).
- `python tools/validate_learning_content.py`: PASS, exit 0, 100 ms antes;
  PASS, exit 0, 113 ms ap√≥s (`mission-silent-reservoir-learning.json`).
- `python tools/test_agents.py -v`: PASS, exit 0, 554 ms antes;
  PASS, exit 0, 361 ms ap√≥s; 9 testes executados.
- Defeito encontrado: `.gitignore` ignorava `Assets/Scripts/Editor/Build/` por
  causa de `[Bb]uild/`, embora os workflows referenciem esses scripts.
- Corre√ß√£o: exce√ß√µes espec√≠ficas adicionadas ao `.gitignore`; scripts Android/iOS
  e seus `.meta` preparados para versionamento. Corre√ß√£o local ainda n√£o foi
  enviada √† `main`.
- `git diff --cached --check`: PASS. `tools/__pycache__` continua ignorado;
  nenhum arquivo gerado indevido rastre√°vel foi encontrado.
- Estado B1: `PASS` para valida√ß√µes locais e corre√ß√£o demonstrada; Unity/CI,
  importa√ß√£o, compila√ß√£o, PlayMode, dispositivos e assinatura permanecem
  `BLOCKED`/`NOT_RUN`.

P45 ó 27/10/2026 ó Fase 08 GameCI: diagnÛstico de licenciamento. Captura de logs adicionada e YAML validado; push `bb06877`; runs de testes/Android falharam na etapa Unity. API de secrets retornou zero nomes; causa prim·ria È ausÍncia de `UNITY_LICENSE`, `UNITY_EMAIL`, `UNITY_PASSWORD`. C#/packages ainda n„o determinados. RelatÛrio `docs/PHASE_08_GAMECI_DIAGNOSTICS.md`.

P46 ó 27/10/2026 ó Fase 10: `buildsPath` corrigido, dump de Editor.log adicionado e fluxo Unity Personal gratuito documentado. YAML validado; execuÁ„o bloqueada por secrets externos ausentes. RelatÛrio `docs/PHASE_10_LICENSE_BUILD_FIX.md`.

P47 ó 27/10/2026 ó Fase 13: auditoria GameCI v6. Pedido de licenÁa Personal somente por email/senha È incompatÌvel com a documentaÁ„o atual; nenhuma alteraÁ„o de workflow aplicada. RelatÛrio `docs/PHASE_13_GAMECI_LICENSE_AUDIT.md`.

P48 ó 28/10/2026 ó Fase 14: auditoria local de licenÁa/workflows. `manual.alf` n„o È licenÁa final; `.gitignore` atualizado. Workflows sem novo erro estrutural; secrets GitHub ausentes. RelatÛrio `docs/PHASE_14_WORKFLOW_LICENSE_AUDIT.md`.

P49 ó 28/10/2026 ó `Unity_lic.ulf` verificado: XML de licenÁa Unity v·lido, ignorado e n„o rastreado. Secrets do GitHub continuam ausentes; workflows j· apontam para `UNITY_LICENSE`. Nenhum valor foi exibido.

P50 ó 28/10/2026 ó VerificaÁ„o de Secrets: API do repositÛrio `valdir-galvao/nexo` retornou zero. YAML v·lido; execuÁ„o bloqueada atÈ os trÍs nomes aparecerem no escopo correto.

P51 ó 28/10/2026 ó GitHub Actions: credenciais encontradas como variables, `SECRET_COUNT=0`. Nenhum valor registrado. RotaÁ„o obrigatÛria apÛs exposiÁ„o e migraÁ„o para Repository secrets.

P52 ó 28/10/2026 ó Secrets Unity confirmados via API por nome; nenhum valor exposto. Gate de credenciais PASS; prÛximo passo È `unity-tests.yml` apÛs limpar variables duplicadas.

P53 ó 28/10/2026 ó `unity-tests.yml` executado com secrets presentes. EditMode e PlayMode falharam no passo Unity; sem JUnit/Editor.log e sem acesso aos logs de job. Licenciamento deixou de ser o bloqueio prim·rio. Android/iOS n„o executados.

P54 ó 28/10/2026 ó Auditoria dos jobs GameCI: causa raiz `no space left on device` ao baixar imagem IL2CPP; licenciamento e C# n„o foram a causa. Workflows Ubuntu receberam `jlumbroso/free-disk-space@v1.3.1`; YAML validado. Logs recuperados via redirect 302 da API.

P55 ó 28/10/2026 ó CorreÁ„o das assemblies de testes enviada em `992586a`; static QA passou. Runs `40661478289`, `40661478460`, `40661478368` em execuÁ„o.

P56 ó 28/10/2026 ó Auditoria quantitativa: pipeline 5m/WFO, dados, SQLite e harness ausentes. Validadores Unity locais passaram. Artefato `artifacts/audit_package/meta_filter_phase10_status.json` com BLOCKED.

P57 ó 28/10/2026 ó Fase 15 monitorada: runs 40661478289 (testes), 40661478460 (Android), 40661478368 (iOS), commit 992586a, todos FAIL por CS0246 BuildFailedException em scripts Editor. LicenÁa Personal resolvida; sem APK/Xcode. RelatÛrio docs/PHASE_15_RUN_MONITORING.md. PrÛxima aÁ„o: correÁ„o de cÛdigo e regress„o Unity.


P58 ó 28/10/2026 ó Fase 16: correÁ„o CS0246 em 3d9ab9a, static QA PASS. Run 40663615864 Unity tests ainda IN_PROGRESS; Android/iOS acionados, aprovaÁ„o pendente.


P59 ó 28/10/2026 ó Fase 17: cena explÌcita nos pipelines Android/iOS, static QA PASS, commit 74556d0. Run 40665421997 Unity tests IN_PROGRESS; gate n„o aprovado.


P60 ó 28/10/2026 ó Fase 18: gate Unity tests 40665421997 IN_PROGRESS; iOS 40665421999 FAIL por iOS Version has not been set up correctly; Android 40665422007 IN_PROGRESS. K-005 e builds n„o aprovados.


P61 ó 28/10/2026 ó Fase 19: bundleVersion/buildNumber iOS v·lidos em bca64a, static QA PASS. Unity tests 40667016837 IN_PROGRESS; K-005 e builds n„o aprovados.


P62 ó 29/10/2026 ó Fase 20: 40667016837 FAIL; EditMode bloqueado por CLI 403, PlayMode 0/1, K-005 NOT_RUN. Android/iOS FAIL sem artefatos finais. RelatÛrio docs/PHASE_20_FINAL_GATE.md.

P63 ó 29/10/2026 ó Fase 21: activeInputHandler definido como 0, static QA PASS, push 5f7adbb. Run 40814474545 IN_PROGRESS; builds mÛveis n„o aprovados.

P64 ó 29/10/2026 ó Fase 22: run 40814474545 FAIL; InputSystem -1 resolvido, mas PlayMode falhou porque cena n„o est· no perfil/lista ativa. EditMode falhou por CLI 403; K-005 NOT_RUN. Android/iOS ainda IN_PROGRESS sem artefatos.

P65 ó 29/10/2026 ó Fase 23: cena j· habilitada no asset; adicionada garantia program·tica EnsureBuildScenes, commit 3ecbbe0, static QA PASS. Unity tests 40815968590 IN_PROGRESS; builds pendentes.

P66 ó 29/10/2026 ó Fase 24: EditMode 16/16 passou, mas job FAIL por integraÁ„o Checks API; CLI 403 superado. PlayMode 0/1 por cena fora do active build profile; K-005 NOT_RUN. Android IN_PROGRESS sem APK; iOS FAIL sem Xcode.

P67 ó 29/10/2026 ó Fase 25: permissıes checks/statuses adicionadas e cena injetada no SetUp PlayMode, commit 196920d, static QA PASS. Unity tests 40817889017 IN_PROGRESS.

P68 ó 29/10/2026 ó Fase 26: EditMode 16/16, porÈm workflow FAIL; PlayMode 0/1 por active build profile; K-005 NOT_RUN. Android FAIL sem APK/metadata; iOS FAIL sem Xcode.

P69 ó 29/10/2026 ó Fase 27: BuildProfile.GetActiveBuildProfile + scenes injection implementado em d838499, static QA PASS. Runs 40820112279/40820112292/40820112467 IN_PROGRESS.

P70 ó 29/10/2026 ó Fase 28: BuildProfile API n„o resolveu cena no runner; PlayMode 0/1, EditMode 16/16 mas job FAIL, K-005 NOT_RUN. Android/iOS FAIL apenas diagnÛsticos; sem APK.

P71 ó 29/10/2026 ó Fase 29: BUILD_SCENES/buildScenes est·tico nos trÍs workflows, injetor removido, PlayMode usa LoadSceneAsyncInPlayMode; commit 930945a, static QA PASS. Runs 40822563212/40822563109/40822563115 IN_PROGRESS.

P72 ó 29/10/2026 ó Fase 30: run 40822563212 ainda IN_PROGRESS; Android 40822563109 IN_PROGRESS; iOS 40822563115 FAIL. Sem conclus„o de PlayMode/K-005 ou APK.

P73 ó 29/10/2026 ó Fase 30 encerrada: EditMode 16/16, PlayMode 0/1 (cena carregou, bootstrap nulo), K-005 NOT_RUN. Android ainda IN_PROGRESS; iOS FAIL. Sem APK.

P74 ó 29/10/2026 ó Fase 31: teste async/FindFirst/K-005 enviado em 41596d2; run 40824140558 FAIL sem steps/logs (BlobNotFound), K-005 NOT_RUN. Android/iOS FAIL sem artefatos.

P75 - 29/10/2026 America/Sao_Paulo - Fase 32: removed the three actions/cache Library restore steps from unity-tests.yml, unity-android.yml and unity-ios.yml. Unity remains 6000.3.0f1. YAML parser PASS; static QA PASS json=5 cs=24; git diff check PASS; commit 7e5f4d4 pushed. Push runs 40824551082 tests, 40824551010 Android and 40824550999 iOS all FAILED in 3-4 seconds with no steps. Job logs unavailable (404/BlobNotFound). No Unity, NUnit, K-005, APK or Xcode evidence. Operational gate BLOCKED.

P76 - 29/10/2026 America/Sao_Paulo - Investigacao de causa raiz: workflow minimo `runner-smoke.yml`, sem Unity/GameCI, em `ubuntu-22.04`, tambem falhou em ~3 s com zero steps e `runner_name` vazio (run 40825064094, job 123286108200). Isso confirma falha de provisionamento/capacidade/politica do GitHub-hosted runner antes do primeiro step; cache, Unity, C# e GameCI nao sao a causa primaria. O workflow de smoke foi mantido apenas manual (`workflow_dispatch`) para diagnostico futuro. Fase 32 permanece BLOCKED externamente; nenhuma nova compilacao deve ser repetida ate revisar billing/quota/politica Actions do repositorio.

P77 - 29/10/2026 America/Sao_Paulo - Teste controlado de visibilidade: repositorio alterado para publico, smoke run 40825528099 PASS com runner provisionado; Unity tests 40825567214 e Android 40825569992 iniciaram runners reais, mas ficaram presos no `Free runner disk space` e foram cancelados; iOS 40825572493 iniciou runner, checkout e verificacao de versao, mas falhou no passo GameCI. Repositorio restaurado para privado e confirmado via API. Diagnostico: quota/politica de runners explica zero-step em privado; existe bloqueio adicional no step de limpeza de disco e falha iOS independente. Nenhum APK, NUnit ou K-005 aprovado.
