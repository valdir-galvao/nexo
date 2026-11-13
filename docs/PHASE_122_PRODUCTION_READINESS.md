# PHASE 122 — Production readiness criteria and environment limits

Data: 2026-11-13 (America/Sao_Paulo)

## Acceptance criteria

A production release is accepted only when all items below have evidence linked to a commit:

1. At least one versioned gameplay character prefab with an AnimatorController and clips for idle, walk, investigate and success.
2. Versioned environment prefabs for the island, water source, channel, valve, reservoir and portal, with explicit materials and no missing references.
3. Licensed or owned music and SFX assets, imported with mobile compression settings and tested through pause/resume.
4. EditMode and PlayMode tests pass in Unity 6000.3.0f1.
5. Android ARM64 and x86_64 smoke tests pass on API 35; iOS simulator and physical-device validation pass where signing permits.
6. Memory, FPS, startup, rotation, pause and resume measurements meet documented budgets on low, medium and high profiles.
7. No shader/material errors, missing assets, unhandled exceptions, ANR or crash in the device matrix.
8. Release artifacts identify the exact commit, Unity version and build configuration.

## Current evidence

The repository currently contains no `.fbx`, `.prefab`, `.anim`, `.controller`, `.wav`, `.mp3` or `.ogg` gameplay assets. No Unity Editor, Blender, `adb` or physical Android/iOS device is available in this environment. Therefore models, rig/clips, final audio and real-device validation cannot be honestly marked complete here.

The current code has a runtime presentation layer and procedural audio/VFX, but those are fallback content and do not satisfy the production-asset criteria above.

## Status

`BLOCKED` for production completion. The next implementation step requires importing or creating owned art/audio assets in a Unity-capable environment, then running the acceptance matrix against the resulting commit. No release build should be labeled production until those gates pass.
