# Phase 124 — Free rig and animation fixtures

## Downloaded content

The KinemIQ CC0 package was downloaded from the public Hugging Face dataset and extracted under `Assets/External/FreeTestAssets/Kinemiq/`. It contains a humanoid mannequin and 35 FBX clips on one 66-bone humanoid skeleton.

Selected acceptance fixtures:

| Purpose | File | SHA-256 |
|---|---|---|
| Humanoid mesh/avatar source | `KinemIQ_Mannequin.fbx` | `658E68891B0F1FC6258B7D62067DB617495761EC23DE85491E60B236A0E2F8B2` |
| Idle | `FBX/Idle_relaxed.fbx` | `4E6581CF8173167ED64A6C69E66394D9299EA00ADDD3118DFD3760BC1E7F8925` |
| Walk | `FBX/Walk_forwards.fbx` | `1B266CD12C96EBCB3DF1F374962621434ED7BEBBA6E157D475ED13984CD86374` |
| Investigation | `FBX/Action_interact.fbx` | `F15E922F6C2C45F70C9C8E101C0FB007E674F5DEC5981A92D32AEEF611E8EA66` |
| Success | `FBX/Action_dance.fbx` | `E39BFBE4F64583C34D0A40672812FF2CE75739EA39A23E96057451314B423E27` |

The source README and a local CC0 notice are preserved in the same directory.

## Unity setup automation

`Assets/Scripts/Editor/ConfigureFreeTestAssets.cs` adds the following menu command after Unity imports the FBX files:

`NEXO > Free Test Assets > Configure Humanoid Character`

The command sets the mannequin to Humanoid, creates `Assets/NEXO/Content/FreeTest/NexoExplorer.controller`, adds Idle/Walk/Investigate/Success states, and saves `NexoExplorer_FreeTest.prefab`.

This script has not been executed in this workspace because Unity `6000.3.0f1` is unavailable. Therefore the prefab, AnimatorController, avatar mapping, material import, IL2CPP build, and device tests remain **BLOCKED**, despite the source files being present.

## Licensing and scope

The asset source describes the package as CC0. These are test fixtures, not a declaration that the NEXO production art direction is complete. Final integration still requires Unity import validation, scene wiring, performance checks and Android/iOS runtime tests.
