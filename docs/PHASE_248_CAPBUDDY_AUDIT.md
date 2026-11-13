# Auditoria do pacote CapBuddy

## Encontrado

`CapBuddy/` contém:

- `CapBuddy.fbx` (1,027,500 bytes), com esqueleto e animações exportadas;
- `CapBuddy.glb` (1,362,376 bytes), alternativa com materiais incorporados;
- `CapBuddy.blend` (5,643,692 bytes), fonte editável;
- `Textures/Fur_BaseColor.png`;
- clips de demonstração `Idle.gif`, `Walk.gif`, `Investigate.gif` e `Success.gif`;
- `CapBuddy/Unity/Editor/CapBuddySetup.cs`, que gera prefab e AnimatorController;
- `Validation.json`, indicando 17 ossos, 2 meshes e quatro actions.

## O que não existe

Não há ainda dentro de `CapBuddy/`:

- `.prefab` gerado pelo Unity;
- `.controller` gerado pelo Unity;
- `.anim` separados;
- arquivos `.meta` do Unity;
- collider, controlador de movimento ou integração com o bootstrap;
- áudio associado ao personagem.

## Limitação técnica

O próprio `README_PT.txt` informa que o rig é `Generic`, não um Avatar Humanoid validado. O `Validation.json` também marca `Unity_tested: false`. Portanto, o pacote contém uma fonte 3D animada utilizável, mas ainda requer importação no Unity 6000.3.0f1, configuração `ModelImporter`, criação de prefab/controller e conexão com `NexoVerticalSliceBootstrap`.

## Conclusão

CapBuddy é suficiente como base visual de avatar e já possui os quatro movimentos necessários. Não é suficiente, sozinho, para aparecer no APK. A etapa pendente é executar `Tools > CapBuddy > Create Prefab and Animator` no Unity e substituir o fallback procedural pelo prefab gerado, validando materiais, escala, colisores e animações em PlayMode/AVD.
