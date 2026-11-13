# Phase 123 — Free test assets

Date: 2026-11-13 (America/Sao_Paulo)

## Scope

Downloaded a small, license-safe fixture set for future Unity import tests. This does not claim production art completion and does not generate an APK/IPA.

## Assets

| File | Size | SHA-256 | License/source |
|---|---:|---|---|
| `Assets/External/FreeTestAssets/Kenney/BlockyCharacters/character-a.glb` | 113,596 B | `8EE5DAE167EC589863F6BBA222467EB90ACE8BE357A4C5ABFCAB289290181616` | Kenney mirror, CC0 1.0, `shorepine/kenney` |
| `Assets/External/FreeTestAssets/Kenney/BlockyCharacters/Textures/texture-a.png` | 20,171 B | `257E944C582CE7CDA206FBD8CEB717BE648F9721756BAF377A36478C11C0059A` | Kenney mirror, CC0 1.0 |
| `Assets/External/FreeTestAssets/Kenney/InterfaceSounds/click_001.wav` | 10,190 B | `EA67C9D78750E5A356BF53EF8C2063B52CC3CCDAE6AE8AFEEC87F97F79AE692D` | Kenney Interface Sounds, CC0 1.0 |
| `Assets/External/FreeTestAssets/Kenney/InterfaceSounds/bong_001.wav` | 11,726 B | `F5D5F83B13EDC5321352C2BA57972D1400151E3F27F1C18FCDA07D261358511A` | Kenney Interface Sounds, CC0 1.0 |

License texts are included in `Assets/External/FreeTestAssets/Kenney/LICENSE.txt` and `.../InterfaceSounds/LICENSE.txt`. The source pages state the Kenney assets are CC0; see the repository README and license files.

## Validation

- Downloaded successfully with HTTPS and recorded hashes above.
- No existing gameplay files were overwritten.
- No Unity Editor is available in this workspace, so `.glb` import, texture/material assignment, audio import settings, rigging, AnimatorController generation, and device runtime tests are **BLOCKED**.
- `python tools/run_static_qa.py` and `git diff --check` must be run after the documentation update; passing them does not prove Unity import or device compatibility.

## Next step

Open the project with Unity `6000.3.0f1`, install a glTF importer only if the project policy permits it, and run an isolated import test. Do not connect these fixtures to the bootstrap until import and licensing checks pass.
