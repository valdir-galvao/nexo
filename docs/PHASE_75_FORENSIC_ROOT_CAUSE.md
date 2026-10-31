# NEXO — Fase 75: auditoria dos erros reportados

## Resultado executivo

A auditoria foi somente leitura sobre os arquivos do projeto. Não foram disparados novos apps e o build Android da repetição pública foi cancelado antes do empacotamento. O repositório está privado novamente.

| Evidência | Estado atual | Diagnóstico |
|---|---|---|
| `TextMeshPro/Distance Field` em `EnsureIncludedShaders.cs:21/44` | risco real de aviso `Shader.Find == null` | O manifesto declara `com.unity.textmeshpro: 3.0.6`, mas o lockfile não contém o pacote. Não há uso de TMP no código ou cena. A resolução final depende do Package Manager Unity. |
| `Soft.psd` | não encontrado no repositório | Nenhuma referência em `Assets`, `Packages` ou `ProjectSettings`; aviso externo de importação/editor, não recurso NEXO rastreado. |
| `Default-Skybox.mat` | não encontrado no repositório | A cena usa `m_SkyboxMaterial: {fileID: 0}` e o bootstrap usa câmera Solid Color; não há referência quebrada versionada. |
| `Type mismatch RenderSettings/EditorBuildSettings` | não reproduzido no arquivo atual | `ProjectSettings/EditorBuildSettings.asset` começa com `--- !u!104` e `EditorBuildSettings:` corretamente, com uma única cena habilitada no índice 0. O erro histórico indica cache/estado de importação ou arquivo transitório do runner. |
| `SIGSEGV signo:11` | falha histórica do Editor/runner | Ocorreu antes do teste PlayMode em `Begin MonoManager ReloadAssembly`; o run posterior `41214944859` passou em tentativa 2. Não foi reproduzido no código runtime. |
| tela verde | origem confirmada | `MakeCube("Island", ...)` usa `(0.3, 0.45, 0.3)` e `GreenDoor` usa verde de portal; são elementos intencionais do bootstrap. |
| quadrados magenta | causa provável fortemente sustentada | `Nexo Intro Floating Lights` criava `ParticleSystem` sem material explícito. A correção `54e67ec` atribui material Unlit mobile-safe ao `ParticleSystemRenderer`. A confirmação runtime foi cancelada junto com o Android. |

## Arquivos auditados

- `Assets/Scripts/Editor/EnsureIncludedShaders.cs`
- `Packages/manifest.json`
- `Packages/packages-lock.json`
- `ProjectSettings/EditorBuildSettings.asset`
- `Assets/Scenes/NexoVerticalSlice.unity`
- `Assets/NEXO/Scripts/UI/NexoVerticalSliceBootstrap.cs`
- `Assets/NEXO/Scripts/Core/RuntimeDiagnostics.cs`
- todos os `.mat` em `Assets/` (1 arquivo; shader não nulo)
- busca global por `Color.magenta`, `Soft.psd`, `Default-Skybox.mat`, Shader Graph, sprites e prefabs

Não existem no repositório `ProjectSettings/GraphicsSettings.asset`, `ProjectSettings/QualitySettings.asset`, Render Pipeline Asset, Renderer Data, Shader Graph, prefabs ou texturas de cena. O pacote URP está no manifesto, mas o mundo é procedural e usa fallback Unlit.

## Correções já aplicadas

1. `54e67ec` atribui explicitamente o material de mundo ao sistema de partículas de introdução e desativa o renderer se o material não resolver, evitando fallback magenta.
2. `RuntimeDiagnostics` registra `[NEXO_RENDER]` no frame seguinte ao carregamento: câmera, pipeline, API, renderers, materiais, shaders incompatíveis, objetos magenta, Canvas e objetos de debug.
3. A seleção gráfica permanece OpenGL ES 3 primeiro no Android, Vulkan opcional e Metal no iOS.

## Validação observável

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=26`)
- `git diff --check`: PASS
- Unity Tests run `41214944859`, tentativa 2: SUCCESS
- iOS run `41214944802`, tentativa 2: SUCCESS
- Android run `41214944817`, tentativa 2: CANCELLED antes do empacotamento, por instrução para não gerar app
- Repositório: `private=true`, `disabled=false`

## Pendências reais

- Reabrir o projeto no Unity 6000.3.0f1 para resolver o manifesto pelo Package Manager e regenerar `packages-lock.json`; edição manual do lockfile foi evitada.
- Executar um novo Android smoke test para capturar `[NEXO_RENDER]` e confirmar `incompatibleShaders=0` e `magentaObjects=0`.
- Validar iOS/Metal e aparelhos físicos. A falha visual não é declarada definitivamente resolvida sem essas evidências.
