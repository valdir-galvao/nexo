# NEXO — PHASE 81: technical proof of free Unity Linux CI

Date: 2026-11-01 02:35 America/Sao_Paulo
Result: **BLOCKED** (remote proof not executed)

## Official Unity availability

Unity's official release page for 6000.3.0f1 lists **Linux** under manual installation operating systems and provides a Linux Download Assistant option. This confirms the exact Editor distribution exists; it does not prove that a GitHub runner has enough space or that the installation succeeds there.

Source: https://unity.com/releases/editor/whats-new/6000.3.0f1

## Current workflow audit

- unity-tests.yml: Ubuntu GameCI test runner, EditMode/PlayMode matrix, 6000.3.0f1.
- unity-android.yml: Ubuntu GameCI Android builder, 6000.3.0f1.
- unity-ios.yml: macOS GameCI builder; outside this Linux proof.
- Existing workflows were preserved and not dispatched.

## Storage and measurements

GitHub documents public ubuntu-latest standard runners as 4 vCPU, 16 GB RAM and 14 GB SSD. The actual extracted size of Unity 6000.3.0f1, Android Build Support, SDK/NDK and OpenJDK was **not measured**, because no remote runner was started. Local repository sizes are only a lower bound: Assets ~0.13 MB, Packages ~0.002 MB, ProjectSettings ~0.001 MB; they exclude Unity's generated Library, Docker layers and toolchains.

A direct install could avoid Docker overhead in theory, but this phase did not create a new workflow or execute a remote installation. The prior history of GameCI disk failures remains a real blocker.

## License investigation

GameCI's Personal activation procedure requires a Unity .ulf license stored as UNITY_LICENSE, plus UNITY_EMAIL and UNITY_PASSWORD in GitHub Secrets. The current test/build jobs inject email/password only; they do not inject UNITY_LICENSE. No secret values were printed, changed or requested in chat.

Source: https://game.ci/docs/github/activation/

## Dependency and tests state

- Manifest dependencies: 36.
- Lockfile dependencies: 2.
- Missing lockfile entries: 34, including TextMeshPro 3.0.6.
- Static QA: **PASS**, json=5, cs=26.
- Workflow YAML parse: **PASS**.
- git diff --check: **PASS**.
- Unity Editor installation: **NOT_RUN** remotely.
- Package Manager resolution: **BLOCKED**.
- EditMode/PlayMode: **BLOCKED/NOT_RUN**.
- APK/IPA: **NOT_RUN by scope**.

## Verdict

The official Linux distribution is available, but a free GitHub Ubuntu path is not technically proven. A PASS requires a dedicated proof workflow that measures disk, installs the exact Editor and modules, verifies UNITY_LICENSE, resolves packages and runs EditMode/PlayMode before any build job. This phase intentionally did not generate APK/IPA or alter workflow architecture.
