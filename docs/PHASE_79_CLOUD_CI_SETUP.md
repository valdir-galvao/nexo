# NEXO — PHASE 79: cloud CI/CD environment audit

Date: 2026-11-01 01:45 America/Sao_Paulo
Status: BLOCKED (Windows cloud runner not provisioned/validated)

## Current architecture

- unity-tests.yml: ubuntu-latest, GameCI unity-test-runner@v4, Unity 6000.3.0f1.
- unity-android.yml: ubuntu-latest, GameCI unity-builder@v6, Unity 6000.3.0f1.
- unity-ios.yml: macos-latest, GameCI unity-builder@v6, Unity 6000.3.0f1.
- No workflow currently installs Unity on a Windows runner.
- No self-hosted Windows runner was confirmed from the available local tooling; gh CLI is not installed, so runner API enumeration was not possible in this session.

## Hosted Windows availability

GitHub documents windows-latest and windows-2022 hosted labels. Standard private-repository runners are ephemeral and have limited local storage (the GitHub reference lists approximately 14 GB SSD and 8 GB RAM for the standard tier). This is insufficient to assume a reliable installation of Unity 6000.3.0f1 plus Android Build Support, SDK/NDK and OpenJDK without a measured preflight or a larger runner.

The current Ubuntu GameCI jobs cannot satisfy the requirement that Unity installation, dependency resolution and Android tests occur on Windows cloud infrastructure. They were not dispatched.

## Required cloud design

1. Provision an authorized Windows cloud runner (windows-2022/windows-latest or self-hosted Windows) with local NTFS storage preferably >= 40 GB free, 8+ GB RAM, and administrator rights.
2. Configure secure Unity Personal credentials/license through GitHub Secrets; do not commit them.
3. Install exact 6000.3.0f1 with Android Build Support, Android SDK & NDK Tools and OpenJDK.
4. Add a dedicated Windows workflow that preflights disk, Unity version, modules, license and project version before opening the project.
5. Run Unity Package Manager resolution, then EditMode and PlayMode; upload logs and the regenerated lockfile as review artifacts.
6. Only after those gates pass may the Android build workflow be authorized.

## Validation performed in this phase

- Parsed all six workflow YAML files: **PASS**.
- python tools/run_static_qa.py: **PASS**, json=5, cs=26.
- git diff --check: **PASS**.
- Cloud workflow dispatch: **NOT_RUN** (current jobs are not Windows and the required Windows environment is not provisioned).
- Unity dependency resolution/EditMode/PlayMode: **BLOCKED**.
- Android/iOS builds: **NOT_RUN by scope**.

## Preserved state

Commits 54e67ec, 1500265 and d847108 remain in history. No project code, package manifest or lockfile was changed. No APK/IPA was generated.
