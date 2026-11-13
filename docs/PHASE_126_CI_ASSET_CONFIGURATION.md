# Phase 126 — CI asset configuration

The GameCI test and Android jobs now pass the already-configured `UNITY_LICENSE` Secret together with `UNITY_EMAIL` and `UNITY_PASSWORD`; no secret value is stored or printed. They also set `NEXO_CONFIGURE_FREE_ASSETS=1`.

When Unity imports the project in those jobs, `ConfigureFreeTestAssets.cs` uses `InitializeOnLoadMethod` and a one-shot `SessionState` guard to configure the CC0 mannequin, Humanoid avatar, AnimatorController and four test states. The same command remains available manually from `NEXO > Free Test Assets > Configure Humanoid Character`.

This change is prepared but not remotely executed in this session. A successful GameCI run must still prove that FBX import completed, the prefab/controller were generated, and EditMode/PlayMode/IL2CPP completed without errors.
