# NEXO — PHASE 82: GitHub Free capacity test

Date: 2026-11-01 02:50 America/Sao_Paulo
Overall status: **BLOCKED** (remote dispatch unavailable)

## Test 1 — isolated runner probe

Created .github/workflows/github-free-capacity.yml with workflow_dispatch only. It runs on ubuntu-latest, has a five-minute timeout, and does not install Unity or Docker. It records:

- runner OS/architecture/name and run ID;
- CPU threads;
- total/available memory from /proc/meminfo;
- root filesystem bytes before checkout;
- checkout, .git, Assets, Packages and ProjectSettings sizes;
- root filesystem and memory after checkout;
- a small artifact containing the measurements.

Workflow definition and local YAML parse: **PASS**.
Remote execution: **BLOCKED/NOT_RUN** because GitHub CLI/API authentication is unavailable in this session. No real runner measurements or quota data were fabricated.

## Test 2 — Unity Personal license

The legal GameCI method requires a Unity Personal .ulf file in the UNITY_LICENSE GitHub Secret, plus UNITY_EMAIL and UNITY_PASSWORD Secrets. Values were not printed, requested or modified. The existing production jobs currently inject email/password but not UNITY_LICENSE, so their Personal activation is not proven. Practical validation is **BLOCKED** until a controlled workflow can run with the already-configured Secrets and logs can be inspected without exposing values.

## Test 3 — exact Editor installation

Gate condition was not met: Test 1 has no remote measurements and Test 2 has no activation evidence. Unity 6000.3.0f1, Android Build Support, SDK/NDK and OpenJDK were therefore **NOT_RUN**. No package resolution or tests were started.

## Preservation and cost

- Production workflows were not modified.
- Unity version and project architecture were not modified.
- Repository visibility was not changed.
- No paid service or larger runner was created.
- No APK or IPA was generated.
- Cost: **R**.

## Next action

From GitHub Actions, manually run **GitHub Free capacity probe**. Save its artifact and confirm actual disk/memory. Only if storage is adequate and UNITY_LICENSE validation is approved should a second, separate installation-proof workflow be considered.
