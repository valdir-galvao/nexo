# PHASE 89 — Android GameCI timeout root cause

Data: 2026-11-01 (America/Sao_Paulo)

## Evidences

- Unity Tests run `41293713412`: PASS for EditMode and PlayMode on Unity `6000.3.0f1`.
- Android run `41294473685`: FAIL. Preparation, checkout, Unity version/profile checks, cache invalidation and cache step all passed. `Build Android package` remained `in_progress` until the job ended; no APK, metadata or diagnostic artifact was published.
- GitHub job API shows no completed result for the GameCI step and the run log archive contains only an empty ZIP. This is consistent with a runner/action timeout, not a reported C# exception.
- Historical Android run `41211856635` on commit `ce335a6` passed with the same Unity version, target API, ARM64+x86_64 architecture and custom build method in about 19 minutes, including AVD smoke test.
- `ce335a6..7fa50a1` changes affecting runtime are diagnostics and particle material assignment; no Android workflow or build script regression was found. Unity Tests on the current code passed.

## Root cause classification

**Confirmed:** GameCI/IL2CPP Android packaging exceeded the 45-minute job bound and ended without a final Unity log. The available evidence does not identify a code, shader, package or license error.

**Not proven:** the underlying reason for the unusually slow build (cold image/toolchain pull, runner capacity, or transient GameCI infrastructure). No diagnostic log survived the timeout.

## Correction

The Android job timeout was increased from 45 to 60 minutes in `.github/workflows/unity-android.yml`, preserving all build settings and architecture requirements. This prevents a valid cold Unity 6/IL2CPP build from being terminated by the previous bound while retaining a finite upper limit.

No gameplay, package, signing, secret or visibility changes were made. A new Android run is required to validate whether the build completes within the expanded bound.

## Validation

- Static QA: PASS (`json=5`, `cs=26`).
- `git diff --check`: PASS.
- Android rebuild after this correction: NOT_RUN in this step.
- APK/AVD: NOT_RUN because run `41294473685` produced no APK.
