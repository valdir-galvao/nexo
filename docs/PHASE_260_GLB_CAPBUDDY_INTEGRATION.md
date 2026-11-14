# Phase 260 — Integração do CapBuddy GLB

Data: 2026-11-14 (America/Sao_Paulo)

## Alterações

- `Assets/External/FreeTestAssets/CapBuddy/CapBuddy.glb` foi substituído pelo
  modelo novo `CapBuddy_Animated.glb` (hash SHA-256
  `6AC086D36E44C82AC85907AE9E45C095DD1CC593CEF154977294762B00D833A8`). O
  `CapBuddy.fbx` foi preservado para a origem dos clips.
- `com.unity.cloud.gltfast` `6.21.0` foi adicionado ao manifesto. O lockfile não
  foi editado manualmente; ele deve ser regenerado pelo Unity 6000.3.0f1 no
  runner.
- `CapBuddySetup` agora importa o GLB como fonte visual, instancia sua malha e
  materiais no prefab, preserva o FBX como fonte de animação e atribui o
  `AnimatorController` com `Idle`, `Walk`, `Investigate` e `Success`.
- O setup registra a fonte visual e a contagem de materiais e mantém o gate
  `[NEXO_CAPBUDDY] SUCCESS` após validar prefab/controller.

## Validação local

- Static QA: PASS (`json=5`, `cs=27`).
- `git diff --check`: PASS.
- Unity local 6000.3.0f1: ausente; importação efetiva do GLB e regeneração do
  lockfile dependem do GameCI.

## Estado

Nenhum build foi considerado concluído nesta etapa até o push e a confirmação
dos logs remotos de importação glTFast, mapeamento dos quatro clips e artefatos.
