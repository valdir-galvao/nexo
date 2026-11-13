# Phase 127 — GameCI license diagnostic

Run `43348490067` reached the GameCI builder and failed before Unity compilation. The first relevant log was:

`[Licensing::Module] Error: "manualLicenseFile" must be provided with the path to a valid ULF file: "UnityLicenseFile.ulf" not found`

The subsequent image lookup failure was a cascade. Unity Tests `43348489786` passed after the C# newline fix, proving the compiler error was resolved. The Android/iOS jobs had `UNITY_LICENSE` injected, but GameCI did not receive a valid ULF file. Historical successful runs used the account activation path with `UNITY_EMAIL` and `UNITY_PASSWORD` only. The invalid/empty ULF injection was therefore removed from the three production workflows; no Secret value was read or changed.

The workflows retain `NEXO_CONFIGURE_FREE_ASSETS=1`, so the CC0 fixture setup remains enabled during the next valid Unity import.
