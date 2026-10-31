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

P78 - 29/10/2026 America/Sao_Paulo - Fase 33: removido `jlumbroso/free-disk-space` de unity-tests, unity-android e unity-ios; inserido step shell leve com limpeza tolerante e `df -h`. Todos os workflows YAML passaram no parser; static QA PASS json=5 cs=24; git diff check PASS. Commit local preparado; nao houve push para evitar nova rodada enquanto billing/private runner permanece bloqueado.

P79 - 29/10/2026 America/Sao_Paulo - Fase 34: repositorio ficou publico durante o teste e commit 0ea514d foi enviado. A limpeza shell passou em todos os jobs. Smoke/runner provisionou corretamente. iOS falhou no GameCI; Unity tests 40826804198 e Android 40826804229 chegaram ao GameCI, mas ficaram mais de 8 minutos sem progresso e foram cancelados. Nenhum NUnit/K-005/APK foi confirmado. Repositorio restaurado para privado via API e confirmado.

P80 - 29/10/2026 America/Sao_Paulo - Diagnostico iOS: log do job 123291599782 mostrou `Failed to resolve the latest game-ci CLI release: GitHub API returned 403` em game-ci/unity-builder@v6, antes do Unity. Corrigidos os tres workflows com `cliVersion: v0.1.72` fixo e `gitPrivateToken: ${{ github.token }}`, evitando consulta anonima de release. YAML e static QA PASS; teste remoto pendente.

P81 - 29/10/2026 America/Sao_Paulo - Correcao GameCI: log do iOS confirmou HTTP 403 ao resolver release latest; `cliVersion: v0.1.72` e `gitPrivateToken: ${{ github.token }}` adicionados nos tres workflows. Rodada publica 40827573852/40827573784/40827573928 passou pelo CLI e entrou no Unity/GameCI, demonstrando que o 403 foi eliminado; execucoes ficaram sem progresso por varios minutos e foram canceladas. Repositorio restaurado para privado. Nenhum APK/Xcode/teste formal aprovado.

P82 - 29/10/2026 America/Sao_Paulo - Auditoria profunda dos logs: GameCI CLI fixado funcionou; logs Unity revelaram ULF com machine binding incompat?vel (fallback email/password funcionou), input `buildScenes` invalido no GameCI v6 e ownership root causando Permission denied em metadata apos cancelamento. Removido UNITY_LICENSE dos workflows para usar ativacao por conta, removido buildScenes invalido e adicionado chown seguro antes de metadata. YAML/static QA/git diff check PASS. Commit local; push pendente para nao disparar runner privado bloqueado.


P83 - 29/10/2026 - Teste publico controlado: run 40828275631 FAIL em PlayMode por categoria NUnit K-005 invalida; EditMode passou 16/16. Android/iOS em andamento.
P84 - 29/10/2026 - Commits 148fd33 e c9746a8 corrigiram categoria e isolamento de save. Run 40829558218 PASS (EditMode 16/16; PlayMode 1/1; K005). Android 40829558400 FAIL por colisao build/Android/Android; iOS 40829558264 FAIL apos gerar Xcode, na validacao GameCI.
P85 - 29/10/2026 - Commit b4713f2 normalizou BUILD_PATH Android. Run 40830355578 PASS (16/16 + 1/1 K005). Android 40830355608 cancelado por permanecer em Build Android sem concluir; nenhum APK. iOS 40830355598 gerou projeto Xcode, mas GameCI classificou FAIL na validacao pos-build. Repositorio restaurado privado e confirmado; nenhum segredo exposto.


P86 - 29/10/2026 - Ajustes locais: cache actions/cache v4 documentado e configurado com chaves estaveis; `buildName: Nexo` adicionado aos builders; iOS usa `AcceptExternalModificationsToPlayer` para export sem assinatura. `python tools/run_static_qa.py` PASS (json=5, cs=24), YAML PASS e diff check PASS. Push nao executado.


P87 - 29/10/2026 - Push dos commits locais concluido no commit 4120620 durante janela publica. Cache Android executou com sucesso. Unity tests falhou por crash nativo SIGSEGV em PlayMode; Android produziu APK local no runner (26 MB) mas GameCI falhou antes do upload; iOS exportou Xcode mas GameCI falhou na validacao. Nao ha artefato APK baixavel. Test artifacts em C:/Temp/nexo-ci-public-40842677684. Visibilidade restaurada para private.

P88 - 29/10/2026 America/Sao_Paulo - Auditoria de causa raiz e correcoes: GameCI v6 exigia o marcador literal `Build succeeded!` para validar metodos customizados; Android/iOS agora o emitem apos `BuildResult.Succeeded`. PlayMode teve `coverageEnabled: false` para evitar o crash nativo SIGSEGV em `ScriptingCoverage::FilterRecordedMethods`. Removido cache `Library` dos builds para evitar estado nativo obsoleto; mantido cache de pacotes. Builders usam `runAsHostUser: true`; metadata e uploads usam `if: always()` e `if-no-files-found: warn`, preservando APK/Xcode mesmo em falha pos-build. `python tools/run_static_qa.py`: PASS (`json=5`, `cs=24`); `git diff --check`: PASS. Push e nova rodada GameCI pendentes; repositÛrio deve permanecer privado fora da janela controlada.

P89 - 29/10/2026 America/Sao_Paulo - A rodada publica 40845079148 falhou antes do Unity porque GameCI CLI v0.1.72 rejeita o argumento gerado por `coverageEnabled: false` (`Unknown argument: noCoverageEnabled`). Removido esse input incompatÌvel; o CLI volta ao fluxo suportado. Android/iOS da rodada 40845082453/40845085890 continuam em execuÁ„o e ser„o avaliados separadamente. Correcoes de build (`Build succeeded!`, runAsHostUser, uploads always) permanecem aplicadas.

P90 - 29/10/2026 America/Sao_Paulo - Auditoria do iOS run 40845528526: Unity importou e licenciou corretamente, mas `IosBuild` falhou com `InvalidOperationException: The build cannot be appended` porque `AcceptExternalModificationsToPlayer` exige projeto Xcode preexistente. CorreÁ„o aplicada: exportaÁ„o inicial usa `BuildOptions.None`; marcador GameCI permanece. Artefato de diagnÛstico e metadata foram preservados, sem Xcode v·lido.

P91 - 29/10/2026 America/Sao_Paulo - Rodada publica final no commit 9d68562 concluida: Unity tests 40846649570 PASS (EditMode e PlayMode); iOS 40846649571 PASS com exportacao Xcode limpa; Android 40846649527 PASS com artefato. APK baixado em `C:/Temp/nexo-android-40846649527/Android/Nexo.apk` (26,334,869 bytes; SHA256 `1a9f29a6ff32b5ee97246e49cf7e58c9818c4eac9d7a0a0fd82e8efe13fb6f37`) e `BUILD_METADATA.txt` presente. Projeto Xcode baixado em `C:/Temp/nexo-ios-40846649571/iOS` (zip 227,007,711 bytes). Repositorio restaurado para PRIVATE e confirmado via API. Nenhuma publicacao em loja; testes fisicos permanecem pendentes.


P100 - 30/10/2026 02:38 -03 - Fase 41 conclu?da parcialmente. Reposit?rio foi tornado p?blico, commit `c11667a` enviado e a build Android `41008010979` terminou SUCCESS. Limpeza shell, cache Android, Unity 6000.3.0f1 e `buildName: Nexo` executaram; artefato baixado em `C:/Temp/nexo-android-41008010979/Android/Nexo.apk` (26.336.061 bytes, SHA256 `C225356933C5663BE87AB72D7AB6244E1B7EBD2349FE0C0EDB7E86888D2B0B99`) com `BUILD_METADATA.txt` vinculando commit/run. Testes `41008011072`: EditMode SUCCESS, PlayMode FAILURE no passo Run Unity tests; gate global n?o aprovado. iOS `41008010970` ainda estava IN_PROGRESS na restaura??o de privacidade. Reposit?rio confirmado PRIVATE via API. Pr?xima a??o: diagnosticar PlayMode e concluir iOS separadamente; n?o usar o APK como evid?ncia de aprova??o global.


P101 - 30/10/2026 02:46 -03 - Fase 42: reconstru??o visual da tela inicial aplicada em `NexoVerticalSliceBootstrap`. O bootstrap agora garante Main Camera com fundo s?lido azul profundo, CanvasScaler responsivo 1920x1080/Match 0.5, GraphicRaycaster, EventSystem fora de batchmode, painel de introdu??o, t?tulo 76 px, subt?tulo 32 px, instru??o 26 px e bot?o de a??o 32 px. A UI usa CanvasGroup para entrada suave e o bot?o chama a transi??o real para a miss?o, sem criar f?sica durante o Intro. Static QA: PASS (`json=5`, `cs=24`); `git diff --check`: PASS. Unity/GameCI visual e PlayMode ainda precisam ser reexecutados; a rodada anterior teve PlayMode FAIL.


P102 - 30/10/2026 02:53 -03 - Fase 44: game juice nativo aplicado no bootstrap. O bot?o principal recebeu punch el?stico por coroutine e ColorBlock de estados; o t?tulo pulsa entre branco e ciano; part?culas leves (28 m?ximas) sobem em plano de fundo; FadeIntroIn/FadeIntoMission usam easing suave e param as part?culas na entrada do gameplay. N?o foram adicionados plugins externos. `python tools/run_static_qa.py`: PASS (`json=5`, `cs=24`); `git diff --check`: PASS. Unity/GameCI ainda precisa comprovar compila??o e runtime.


P103 - 30/10/2026 02:54 -03 - Fase 44: commit `7315dae` enviado durante janela p?blica, mas runs Unity tests `41010986193`, Android `41010986218` e iOS `41010986188` falharam antes dos steps, sem runner executando; logs de jobs retornaram `404 BlobNotFound`. N?o h? evid?ncia de compila??o GameCI, APK novo ou teste PlayMode. Static QA local permaneceu PASS (`json=5`, `cs=24`). Reposit?rio restaurado e confirmado PRIVATE. Gate: BLOCKED por infraestrutura Actions; melhorias de game juice permanecem versionadas.


P104 - 30/10/2026 02:58 -03 - Fase 45 interrompida na Porta 3. Porta 1 PASS (`python tools/run_static_qa.py`: `json=5`, `cs=24`; `git diff --check` PASS). Porta 2 PASS: `7315dae` est? na branch `orchestrator/phase-a-baseline` e o HEAD local era `4bd6757`. A visibilidade foi alterada para PUBLIC, mas o push `--force-with-lease` foi rejeitado pelo GitHub com HTTP 403: `Your repository is disabled`. Nenhum run novo foi criado, portanto n?o houve build nem APK desta fase. Visibilidade restaurada e confirmada PRIVATE via API. Portas 4?6: NOT_RUN.


P105 - 30/10/2026 03:18 -03 - Fase 46 conclu?da para Android. API confirmou reposit?rio habilitado (`disabled=false`); visibilidade p?blica aberta, commits enviados e run Android `41011579731` terminou SUCCESS sem BlobNotFound. Artefato baixado em `C:/Temp/nexo-android-41011579731/Android/Nexo.apk`, tamanho 26.929.197 bytes, SHA256 `18c79151ec8bad6fc46183baee891b39306a13ab832df49f4f750dd0a6a054a7`; `BUILD_METADATA.txt` vincula Unity 6000.3.0f1, commit `1cbe67b` e run. Visibilidade restaurada e confirmada PRIVATE. Testes Unity `41011579727` terminaram FAILURE; este APK comprova build Android, n?o aprova??o do gate PlayMode.


P106 - 30/10/2026 03:25 -03 - Fase 47: blindagem URP e transi??o bootstrap/gameplay aplicada. `EnsureIncludedShaders` mant?m os quatro shaders essenciais via `m_AlwaysIncludedShaders`, com retries quando GraphicsSettings.asset ainda n?o est? dispon?vel. O bootstrap pr?-carrega URP Lit/Unlit, UI/Default e TMP, cria material URP fallback para primitivas, desabilita intera??o durante o fade, aguarda um frame e s? ent?o executa `BuildWorld()`. Game Juice foi preservado. Static QA: PASS (`json=5`, `cs=24`); `git diff --check`: PASS.


P107 - 30/10/2026 03:44 -03 - Fase 47 conclu?da para builds m?veis. Commit `ed6a49e` enviado em janela p?blica; Android run `41013991314` SUCCESS, artefato baixado em `C:/Temp/nexo-android-41013991314/Android/Nexo.apk` (26.933.361 bytes; SHA256 `569c02a42679fa7efb92835bf0c213b219c0269f82a5840b640a4f5fbf74f3a0`) com metadata vinculando Unity 6000.3.0f1 e commit. iOS `41013991290` SUCCESS. Unity tests `41013991357` FAILURE; gate PlayMode permanece pendente. Reposit?rio restaurado e confirmado PRIVATE.


P108 - 30/10/2026 06:46 -03 - Fase 48 implementada localmente sem gerar app. Criado `Assets/Resources/Materials/URP_Default_Gameplay.mat` e `.meta`; o editor resolve `Universal Render Pipeline/Lit` (fallback Unlit) e serializa o shader no asset. `NexoVerticalSliceBootstrap` prioriza `Resources.Load<Material>("Materials/URP_Default_Gameplay")`, corrige shader ausente em runtime e mant?m fallback. PlayMode agora verifica material e shader antes de concluir a inicializa??o. Static QA: PASS (`json=5`, `cs=24`); `git diff --check`: PASS. Push/build/download deliberadamente n?o executados por orienta??o do usu?rio.

P109 - 30/10/2026 America/Sao_Paulo - Fase 49 aplicada localmente. Criado `Assets/link.xml` com preservacao explicita de PhysicsModule, Physics2DModule, CoreModule e UI. `NexoVerticalSliceBootstrap` agora configura x/y/z do VelocityOverLifetime no mesmo modo TwoConstants, usa `CreatePrimitiveSafe` e trata falha de CharacterController sem derrubar o boot. `python tools/run_static_qa.py`: PASS (`json=5`, `cs=24`); `git diff --check`: PASS. Nenhum Editor Unity local. A etapa publica/build/download ainda depende de canal GitHub autenticado; `gh` nao esta instalado. Nao declarar APK sem run e artefato verificaveis.

P110 - 30/10/2026 America/Sao_Paulo - Ciclo remoto da Fase 49 concluido. Commit `42ab119` foi enviado em janela publica e o repositorio foi restaurado para PRIVATE via API. Android run `41036558774` SUCCESS; APK baixado em `C:/Temp/nexo-android-41036558774/Android/Nexo.apk` (30.604.235 bytes; SHA256 `241E6619F8BE1ED5BACB1BFAEEDE771DDA98032C1F378E698BABD8B29082E782`) com `BUILD_METADATA.txt` vinculando Unity 6000.3.0f1 e commit. iOS run `41036558638` SUCCESS. Unity tests `41036558837` FAILURE: EditMode passou e PlayMode falhou; logs/artefatos detalhados retornaram 401 para o token atual e a causa raiz do PlayMode permanece pendente. O APK prova build Android, nao aprova o gate global.

P111 - 30/10/2026 America/Sao_Paulo - Causa raiz do PlayMode isolada e corrigida. Logs do run `41043607179` mostraram erro nao esperado `Scene 'NexoVerticalSlice' couldn't be loaded because it has not been added to the active build profile or shared scene list`; o runner tambem havia revelado anteriormente `Arial.ttf is no longer a valid built in font`. Correcoes: `LegacyRuntime.ttf`, registro defensivo do Build Profile Unity 6 e fallback do teste sem chamar `SceneManager.LoadSceneAsync` quando o perfil nao esta confirmado. Static QA PASS. Run `41044424896` terminou SUCCESS (EditMode e PlayMode). Repositorio restaurado PRIVATE. O commit de codigo remoto e `663e32e`; documentacao desta conclusao permanece local para evitar novo disparo.

P112 - 30/10/2026 America/Sao_Paulo - Artefatos corrigidos capturados do commit `663e32e`. Unity Tests `41044424896` PASS (EditMode/PlayMode). Android `41044424987` PASS: `C:/Temp/nexo-android-41044424987/Android/Nexo.apk`, 30.602.315 bytes, SHA256 `F6436D38B466D8C3707A373CAC848E159118CCA92CA5E93A15ACEECD0A83E534`; `BUILD_METADATA.txt` presente. iOS `41044424886` PASS: `C:/Temp/nexo-ios-41044424886/Nexo.ipa`, SHA256 `D061263310086937F50F5EC867E20418747AB0FE493EEA44F25EDE2E991B36C4`; metadata vincula Unity 6000.3.0f1 e commit. Runs privados posteriores falharam por instabilidade de runner, sem invalidar os artefatos PASS vinculados ao commit corrigido. Repositorio permanece PRIVATE.

P113 - 30/10/2026 America/Sao_Paulo - Fase 50 aplicada localmente. `NexoVerticalSliceBootstrap.BuildWorld` nao usa mais `GameObject.CreatePrimitive`, `PrimitiveType`, `CapsuleCollider` ou `CharacterController`. O mundo usa malha cubica visual gerada explicitamente, `MeshRenderer`/material URP e `BoxCollider` simples somente nos objetos interativos; o avatar move por Transform. Static QA PASS (`json=5`, `cs=24`); `git diff --check` PASS. Commit local `a1898c5`. Ciclo publico bloqueado: GitHub marcou o repositorio como disabled e recusou push HTTP 403; visibilidade restaurada para PRIVATE. Nenhuma nova APK foi gerada para este commit.

P114 - 30/10/2026 America/Sao_Paulo - Fase 50 compilada com sucesso no commit `f3630de`. Unity Tests `41050092394` PASS; Android `41050092233` PASS com APK em `C:/Temp/nexo-android-41050092233/Android/Nexo.apk` (30.600.759 bytes; SHA256 `5574289E57EBA3D2F8F4215DA6366274B3FF0EBA18208985CE396D3386168852`); iOS `41050092372` PASS com IPA em `C:/Temp/nexo-ios-41050092372/Nexo.ipa` (39.328.261 bytes; SHA256 `CCC1BD4C1C28FD48415A7E20166969F1100C91C961236F8E64B86FA668BD4753`). BUILD_METADATA confirma Unity 6000.3.0f1 e commit. Repositorio restaurado PRIVATE e API confirmou disabled=false.


## Execution NEXO-20261030-F51

- Branch: `orchestrator/phase-a-baseline`; commit inicial: `bc05508` (documenta??o local); altera??es Fase 51 ainda n?o commitadas no momento do registro.
- Escopo: corre??o local de fallback gr?fico e ilumina??o do bootstrap; sem altera??o de secrets, workflows ou assinatura.
- Evid?ncia: `python tools/run_static_qa.py` retornou 0 (`json=5`, `cs=24`); `git diff --check` retornou 0.
- Achados adicionais: n?o existe `ProjectSettings/GraphicsSettings.asset` rastreado; `Assets/Resources/Materials/URP_Default_Gameplay.mat` tinha shader nulo serializado. O runtime agora resolve Unlit e o Editor tenta corrigir o material independentemente desse arquivo.
- Estado: PASS local / Unity GameCI NOT_RUN.
- Pr?xima a??o: executar importa??o e PlayMode em Unity/GameCI para confirmar o comportamento visual no dispositivo.


## Execution NEXO-20261030-F51b

- Corre??o incremental: sele??o de shader condicionada ao pipeline ativo (`GraphicsSettings.currentRenderPipeline`).
- Evid?ncia: Static QA exit 0 (`json=5`, `cs=24`); `git diff --check` exit 0. Commit `c68bf53`.
- Estado: PASS local / Unity GameCI NOT_RUN.


## Execution NEXO-20261030-F52

- Varredura de materiais: 1 `.mat` inspecionado; 0 nulos ap?s corre??o.
- Arquivos de configura??o verificados: `ProjectSettings/EditorBuildSettings.asset`, `ProjectSettings/ProjectSettings.asset`; `GraphicsSettings.asset` ausente e tratado defensivamente.
- Valida??o: Static QA exit 0 (`json=5`, `cs=24`), `git diff --check` exit 0.
- Estado: PASS local / Unity GameCI NOT_RUN. Relat?rio: `docs/PHASE_52_SHADER_FORENSICS.md`.


## Execution NEXO-20261030-F53

- Auditoria de link.xml, PlayerSettings, bootstrap e AndroidManifest conclu?da.
- Evid?ncias: XMLs v?lidos; Static QA exit 0 (`json=5`, `cs=24`); `git diff --check` exit 0.
- Estado: PASS local / Unity GameCI NOT_RUN. Relat?rio: `docs/PHASE_53_PREBUILD_AUDIT.md`.


## Execution NEXO-20261030-F54

- Travas mobile e callbacks de ciclo de vida aplicados no bootstrap.
- Auditoria CanvasScaler: nenhum prefab/cena serializado; scaler runtime conforme contrato.
- Evid?ncia: Static QA exit 0 (`json=5`, `cs=24`), `git diff --check` exit 0. Estado: PASS local / Unity GameCI NOT_RUN.


## Execution NEXO-20261030-RELEASE54

- Commit testado: `d5c1af98e17c934c405aba1b1c259199fa50b7c3`.
- Unity Tests `41059686100`: PASS (EditMode e PlayMode).
- Android `41059686028`: PASS; APK `C:/Temp/nexo-android-41059686028/Android/Nexo.apk`, SHA256 `a32ce8d42cf1e294089f9fddc9f9840fd574808c20ed47614d81cfadf9b4089e`.
- iOS `41059686091`: PASS; IPA `C:/Temp/nexo-ios-41059686091/Nexo.ipa`, SHA256 `c8aa06ce737df79eede565486f96ed02cf78d9ee2e50853ee3d6a4a31c8648ae`; Xcode em `C:/Temp/nexo-ios-41059686091/iOS`.
- Visibilidade restaurada para PRIVATE; API confirmou `disabled=false`.


## Execution NEXO-20261030-F55

- Crash forense: caminho `CreateIfMissing`/`Start`/callbacks auditado e protegido.
- Cena de build: ?nica cena habilitada no ?ndice 0.
- Evid?ncia: Static QA exit 0 (`json=5`, `cs=24`); `git diff --check` exit 0.
- Estado: PASS est?tico / dispositivo e logcat NOT_RUN.


## Execution NEXO-20261030-F56

- Diagn?stico persistente instalado em `RuntimeDiagnostics.cs`; log local limitado a 2 MiB, sem rede/segredos.
- Evid?ncia: Static QA exit 0 (`json=5`, `cs=25`), `git diff --check` exit 0.
- Estado: PASS est?tico / APK com este diagn?stico ainda n?o gerado.

P123 | 2026-10-30 | commit dffaf29 | runtime audit + GameCI validation | Unity Tests 41077497576 PASS; Android 41077501341 SUCCESS/APK; iOS 41077505847 cancelled and 41080123981 queued/cancelled | repo private, disabled=false | iOS runner macOS pending.

P124 | 2026-10-30 | commit 58a8202 | headless Android emulator smoke workflow | local validators PASS; remote dispatch 404 (workflow not on default branch) | repo private.

P125 | 2026-10-30 | commit 719a223 | integrated Android emulator smoke | run 41081847271 cancelled during Unity build; no emulator diagnostics | repo private.

P126 | 2026-10-31 | commit 761fdcf | Android boot fix + AVD smoke | run 41167786858 build SUCCESS, emulator SUCCESS, APK validated | repo private.

P127 | 2026-10-31 America/Sao_Paulo | Fase 56 | orchestrator/phase-a-baseline | implementacao local de gameplay/Game Juice | `python tools/run_static_qa.py` PASS; `git diff --check` PASS | Unity/GameCI e builds NOT_RUN | validar fluxo interativo antes de release

P128 | 2026-10-31 America/Sao_Paulo | Fase 57 | orchestrator/phase-a-baseline | progressao multi-fase e PlayerPrefs | Static QA PASS; diff check PASS | Unity/GameCI e builds NOT_RUN | validar transicoes no Unity/GameCI

P129 | 2026-10-31 America/Sao_Paulo | Fase 58 | orchestrator/phase-a-baseline | audio procedural e mute em lifecycle | Static QA PASS; diff check PASS | Unity/GameCI e builds NOT_RUN | validar audio no Unity/GameCI

P130 | 2026-10-31 America/Sao_Paulo | Fase 59 | orchestrator/phase-a-baseline | pre-build Release Android | Static QA/YAML/diff PASS; perfil IL2CPP ARMv7+ARM64 API29/35 | build remoto pendente | tag candidata v0.1.0-rc1

P131 - 31/10/2026 America/Sao_Paulo - Release candidate executada no commit `404947660f932631110cb238dbbdc91b5fb2506b`. Unity Tests `41171249209`: SUCCESS. Android `41171249169`: build Release SUCCESS e smoke test AVD API 35 SUCCESS; APK em `C:/Temp/nexo-release-4049476/android/Android/Nexo.apk`, 31.274.715 bytes, SHA256 `5075FDFFC3FC7F65C24B354EA5B5DF9A655EAA76458D79B899F358875862B312`. iOS `41171249180`: SUCCESS; IPA em `C:/Temp/nexo-release-4049476/ios-xcode3/Nexo.ipa`, SHA256 `879E864EE7404BF95E4DD6F9C5728E9CA78455F8F6561C83F221E2F1C7AAACB0`; projeto Xcode no mesmo diretorio. Repositorio restaurado PRIVATE (`disabled=false`).

P132 - 31/10/2026 America/Sao_Paulo - Fase 59 visual corrigiu Canvas overlay/sorting, EventSystem, StandaloneInputModule, PhysicsRaycaster, toque direto e quiz Canvas com Buttons/Images. Static QA PASS (json=5, cs=25) e git diff --check PASS. Nao houve build nesta correcao. Relatorio: docs/PHASE_59_VISUAL_FIX.md.

P133 | 2026-10-31 America/Sao_Paulo | Fase 60 | orchestrator/phase-a-baseline | auditoria pre-build de arquitetura, cena e Safe Area | Static QA PASS (json=5, cs=25); git diff --check PASS | Unity/GameCI NOT_RUN; sem build | validar Safe Area em runtime controlado
