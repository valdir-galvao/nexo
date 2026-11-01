# NEXO — PHASE 80: Free CI investigation

Date: 2026-11-01 02:20 America/Sao_Paulo
Status: BLOCKED — no zero-cost path proven with the required Unity version

## Workflows audited

- .github/workflows/unity-tests.yml: ubuntu-latest, GameCI unity-test-runner@v4, matrix EditMode/PlayMode, Unity 6000.3.0f1.
- .github/workflows/unity-android.yml: ubuntu-latest, GameCI unity-builder@v6, Unity 6000.3.0f1, Android target, emulator job.
- .github/workflows/unity-ios.yml: macos-latest, GameCI builder and unsigned Xcode/IPA packaging.

No workflow was changed or dispatched.

## Free runner limits and storage

GitHub documents standard public-repository runners as free/unlimited; ubuntu-latest is listed with 4 vCPU, 16 GB RAM and 14 GB SSD. Private GitHub Free includes 2,000 minutes/month, 500 MB artifact storage and 10 GB cache storage. Sources: [runner reference](https://docs.github.com/en/actions/reference/runners/github-hosted-runners), [Actions billing](https://docs.github.com/en/billing/concepts/product-billing/github-actions).

The observed public GameCI tag ubuntu-6000.3.27f2-android-3.2.2 is approximately 8.15 GB compressed. The runner must also hold Docker layers, checkout/LFS, Unity Library/package state, Gradle/NDK temporaries and artifacts. Historical NEXO runs already recorded 
o space left on device and Android hangs. Therefore storage fit is **not proven** on the free runner.

## Unity/Linux compatibility

GameCI officially supports Android builds on Linux and provides Unity Docker images. However, the exact required editor version is the gating requirement. Public tags observed include 6000.3.24f1 and 6000.3.27f2; no 6000.3.0f1 tag was found in the queried tag listings. Using those versions would violate the project's locked ProjectVersion.txt, so they are not alternatives.

A custom 6000.3.0f1 image could theoretically be built, but creating/publishing it would require additional image storage and a build environment; this is not a proven R path under the current runner/storage limits.

## Unity Personal activation

GameCI's activation guidance requires a Personal .ulf license in UNITY_LICENSE, with UNITY_EMAIL and UNITY_PASSWORD stored as GitHub Secrets. The current test/build jobs inject email/password only; UNITY_LICENSE is not present in those job environments. The activation workflow only requests an activation artifact and does not complete the license setup automatically. No credentials or license contents were printed or modified.

Source: [GameCI activation](https://game.ci/docs/github/activation/).

## Repository visibility and cost

An unauthenticated GitHub API request for aldir-galvao/nexo returned HTTP 404, consistent with the repository remaining private; visibility was not changed. No paid plan, VM, larger runner or external service was created. Target cost for this investigation: **R**.

## Result

No zero-cost alternative is currently proven that satisfies all constraints simultaneously: exact Unity 6000.3.0f1, Android modules, EditMode/PlayMode, reliable storage and Personal licensing. APK/IPA generation remains NOT_RUN. The safe next step is to provide a verified exact GameCI image/tag and valid UNITY_LICENSE Secret, or explicitly authorize changing the Unity version (which this phase does not do).
