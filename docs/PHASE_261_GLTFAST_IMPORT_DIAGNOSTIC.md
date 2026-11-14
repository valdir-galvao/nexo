# PHASE 261 — Diagnóstico de importação GLB e estado dos runs

Data: 2026-11-14 (America/Sao_Paulo)

## Resultado

- Static QA local: PASS (`json=5`, `cs=27`).
- `git diff --check`: PASS.
- Unity Tests run `43546295823`: SUCCESS (EditMode e PlayMode).
- CapBuddy GLB: importação concluída no log (`Assets/External/FreeTestAssets/CapBuddy/CapBuddy.glb`), seguida de `[NEXO_CAPBUDDY] SUCCESS` no build iOS.
- iOS run `43546296733`: SUCCESS; geração do Xcode/IPA concluída.
- Android run `43546295830`: ainda `in_progress` no passo `Build Android package` desde 02:28:51Z; sem artefato ou conclusão para validar.

## Erros/avisos reais encontrados

1. As fixtures `Assets/External/FreeTestAssets/Kenney/World/*.glb` falham no `GLTFast.Editor.GltfImporter` durante a importação. A inspeção dos contêineres GLB mostrou URIs externas sem arquivos correspondentes:
   - `character-a.glb` referencia `Textures/texture-a.png`, inexistente nessa pasta.
   - `sand-rocks.glb`, `stone-rocks.glb`, `unit-tree.glb`, `water-island.glb`, `water-rocks.glb` e `water.glb` referenciam `Textures/colormap.png`, inexistente.
   Esses arquivos não são referenciados pelo código de runtime atual; portanto, os erros não bloquearam os testes nem a geração do CapBuddy.

2. `Assets/External/FreeTestAssets/CapBuddy/CapBuddy.blend` registra `Blender could not be found` no runner Linux. O mesmo conjunto contém FBX/GLB usado pelo setup; a ausência do Blender não impediu a criação do prefab/controller nem a validação dos quatro clips.

3. Os erros `EGL_BAD_CONFIG`, `Bad attribute idx` e mensagens de `SurfaceSyncer/Gralloc` pertencem ao backend gráfico do emulador, não aparecem no Unity Tests e não invalidam o import do CapBuddy. Eles não provam compatibilidade visual em dispositivo físico.

## Conclusão

Não está correto declarar “sem erros”. A integração principal do CapBuddy está validada no Unity/GameCI, mas o repositório contém fixtures GLB quebradas por arquivos auxiliares ausentes e o build Android ainda não terminou. Nenhuma correção de código foi aplicada neste diagnóstico para evitar alterar assets sem decidir se essas fixtures serão mantidas. O próximo passo seguro é obter as texturas Kenney originais ou mover as fixtures não utilizadas para fora de `Assets/` antes de um novo ciclo de importação.
