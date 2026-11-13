# PHASE 120 — Runtime content and rendering audit

Data: 2026-11-13 (America/Sao_Paulo)

## Causa confirmada

A cena `Assets/Scenes/NexoVerticalSlice.unity` contém somente um GameObject raiz vazio. Câmera, Canvas, luz, cenário, água, portal e avatar são criados em runtime por `NexoVerticalSliceBootstrap`.

`BuildWorld()` usa `MakeCube`/`CreateVisualPart` para montar todos os elementos. As cores verde, amarelo, azul e laranja são valores explícitos de `NexoVisualTheme` e das chamadas de `MakeCube`; elas não indicam falha de shader. Os logs recentes confirmam `incompatibleShaders=0` e `magentaObjects=0`.

## O que existe no APK

- Ilha: cubo verde.
- Portal: cubo verde/amarelo, dependendo da fase.
- Água/canal: cubos azuis.
- Avatar: seis cubos (`Explorer Body`, `Head`, `Visor`, `Backpack`, `Left Leg`, `Right Leg`).
- Animação: `Mathf.Sin` alterando escala/rotação/posição; não há Animator, rig ou clips.
- Áudio: síntese procedural; não há arquivos de música ou efeitos.
- Interação: HUD Canvas, botões, raycast e fluxo de investigação.

## Evidência de ausência de conteúdo final

A árvore `Assets` não contém `.fbx`, `.obj`, `.prefab` de gameplay, `AnimationClip`, `AnimatorController`, `SkinnedMeshRenderer`, texturas de cenário ou assets de áudio. O único material de gameplay é `Assets/Resources/Materials/URP_Default_Gameplay.mat`.

## Conclusão

O quadro verde com elementos amarelos é a aparência esperada do blockout procedural atual. Não há evidência de falha de renderização que esteja ocultando um cenário 3D final. Para obter a experiência visual proposta, é necessário adicionar conteúdo de jogo real (prefabs/modelos, materiais/texturas, animações, VFX e áudio) e ligá-lo ao bootstrap; ajustes de EGL, shader stripping ou permissões não criam esses assets.

Nenhum código foi alterado nesta auditoria. O repositório permanece privado e os artefatos da última build continuam em `C:/Temp/nexo-release-final`.
