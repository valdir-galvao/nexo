# Fase 52 ? Auditoria forense de shaders e materiais

Data: 30/10/2026 (America/Sao_Paulo)
Branch: `orchestrator/phase-a-baseline`
Commit de inspe??o: `7a60f50`

## Escopo e m?todo

- Varredura program?tica de todos os `Assets/**/*.mat` procurando `m_Shader` ausente ou `fileID: 0`.
- Inspe??o de `ProjectSettings/EditorBuildSettings.asset` e `ProjectSettings/ProjectSettings.asset`.
- Revis?o de `Assets/NEXO/Scripts/UI/NexoVerticalSliceBootstrap.cs` e `Assets/Scripts/Editor/EnsureIncludedShaders.cs`.
- Valida??o com `python tools/run_static_qa.py` e `git diff --check`.

## Materiais encontrados

| Arquivo | Antes | Depois | Estado |
| --- | --- | --- | --- |
| `Assets/Resources/Materials/URP_Default_Gameplay.mat` | `m_Shader: {fileID: 0}` | shader Built-in Unlit (`fileID: 10751`) | corrigido; refer?ncia n?o nula |

Quantidade: 1 material rastreado; nulos de shader ap?s corre??o: 0; campos `m_Shader` ausentes: 0. Os demais `fileID: 0` encontrados s?o refer?ncias Unity leg?timas (prefab, cubemap, pai etc.), n?o shaders.

## Configura??es verificadas

- `ProjectSettings/EditorBuildSettings.asset`: `Assets/Scenes/NexoVerticalSlice.unity` presente e habilitada.
- `ProjectSettings/ProjectSettings.asset`: `activeInputHandler: 0`, `bundleVersion: 0.1.0-rc1`, Android Min API 29 e Target API 35.
- `ProjectSettings/GraphicsSettings.asset`: n?o existe/rastreado; o c?digo n?o assume sua presen?a.

## Blindagem aplicada

- O bootstrap substitui o shader do material de `Resources` quando ele ? nulo ou incompat?vel com o pipeline efetivo.
- O resolvedor de Editor escolhe URP Unlit quando h? Render Pipeline ativo e `Unlit/Color` quando o renderer ? Built-in, evitando magenta por incompatibilidade de pipeline.
- Fallbacks `Sprites/Default` e `UI/Default` continuam dispon?veis.

## Verifica??o

- `python tools/run_static_qa.py`: PASS, exit 0 (`json=5`, `cs=24`).
- `git diff --check`: PASS, exit 0.
- Unity Editor/GameCI: NOT_RUN nesta fase; a confirma??o visual em dispositivo permanece pendente.
