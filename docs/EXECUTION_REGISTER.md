# NEXO — Execution Register

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

- `git status --short`, `git branch --show-current`, `git log`, `git remote -v` — PASS.
- `git ls-remote origin refs/heads/main` — PASS; hash matches local.
- `python tools/run_static_qa.py` — PASS (`json=4`, `cs=23`).
- `python tools/validate_learning_content.py` — PASS.
- `git diff --check` — PASS.
- Tool discovery for Unity/dotnet/adb/xcodebuild — NOT_FOUND.

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

## Fase B1 — validação local (execução atual)

- Início: 27/10/2026, America/Sao_Paulo.
- Commit inicial confirmado: `eddb705b52f5007c7a9263bbad21b32907ba151e`.
- Branch de trabalho: `orchestrator/phase-a-baseline`; `main` remoto permanece
  em `0c0c281e04cbdb1b2f8a8ec2a675dcde0d5769e0`.
- Árvore inicial: limpa. Ferramentas Unity, dotnet, adb e xcodebuild continuam
  indisponíveis; nenhum build ou workflow foi iniciado.
- `python tools/run_static_qa.py`: PASS, exit 0, 125 ms antes da correção;
  PASS, exit 0, 120 ms após a correção (`json=4`, `cs=23`).
- `python tools/validate_learning_content.py`: PASS, exit 0, 100 ms antes;
  PASS, exit 0, 113 ms após (`mission-silent-reservoir-learning.json`).
- `python tools/test_agents.py -v`: PASS, exit 0, 554 ms antes;
  PASS, exit 0, 361 ms após; 9 testes executados.
- Defeito encontrado: `.gitignore` ignorava `Assets/Scripts/Editor/Build/` por
  causa de `[Bb]uild/`, embora os workflows referenciem esses scripts.
- Correção: exceções específicas adicionadas ao `.gitignore`; scripts Android/iOS
  e seus `.meta` preparados para versionamento. Correção local ainda não foi
  enviada à `main`.
- `git diff --cached --check`: PASS. `tools/__pycache__` continua ignorado;
  nenhum arquivo gerado indevido rastreável foi encontrado.
- Estado B1: `PASS` para validações locais e correção demonstrada; Unity/CI,
  importação, compilação, PlayMode, dispositivos e assinatura permanecem
  `BLOCKED`/`NOT_RUN`.
