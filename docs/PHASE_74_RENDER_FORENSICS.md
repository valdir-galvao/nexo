# NEXO — Fase 74: diagnóstico forense da renderização visual

## Achados comprovados no código

- A única cena rastreada, `Assets/Scenes/NexoVerticalSlice.unity`, contém apenas o objeto-raiz `NEXO Vertical Slice`. Câmera, luz, Canvas, partículas e mundo são criados pelo bootstrap em runtime.
- A geometria verde é criada em `Assets/NEXO/Scripts/UI/NexoVerticalSliceBootstrap.cs:707` por `MakeCube("Island", ...)`, com a cor `(0.3, 0.45, 0.3)`. O ambiente recebe ainda `NexoVisualTheme.Sky` em `RenderSettings.ambientLight`.
- A porta verde é o objeto `GreenDoor` na fase 1 (`:711-713`), com cor `(0.18, 0.85, 0.42)`. Portanto o fundo verde é parte do cenário procedural, não um objeto órfão da cena.
- A busca por `Color.magenta`, `magenta` e sprites/imagens magenta não encontrou código de produção ou asset intencional. Também não há Shader Graph, textura visual ou prefab que crie quadrados.
- Os quadrados têm a assinatura do `ParticleSystem` procedural `Nexo Intro Floating Lights` (`:541-567`): partículas pequenas, sem material explícito no renderer. Em plataformas móveis, esse caminho dependia do material padrão do módulo de partículas e podia cair em shader ausente/magenta. A correção atribui uma cópia do material mobile-safe `Unlit/Color`/fallback resolvido ao `ParticleSystemRenderer`; se o material não existir, o renderer é desativado e o erro magenta é evitado.
- As partículas de sucesso já recebiam material explícito em `SpawnSuccessParticles`; não foram removidas.

## Pipeline e compatibilidade

Não existem `GraphicsSettings.asset`, `QualitySettings.asset`, Render Pipeline Asset, Renderer Data, Shader Graph ou materiais adicionais versionados em `Assets/`. O projeto declara URP no Package Manager, mas o mundo procedural usa fallback Unlit. `ConfigureMobileGraphics.cs` seleciona OpenGL ES 3 no Android, deixa Vulkan opcional e usa Metal no iOS. A ausência de um asset URP versionado foi mantida como decisão de escopo: o bootstrap não exige URP para renderizar o vertical slice.

O APK anterior confirmou Android 15/API 35, OpenGL ES 3 no SwiftShader e nenhuma exceção Unity ou erro de shader no log. Os avisos `EGL_BAD_CONFIG`, `Gralloc4` e `SurfaceSyncer` pertencem ao backend do emulador e não foram suficientes para atribuir falha ao jogo.

## Diagnóstico instalado

`RuntimeDiagnostics.cs` agora agenda uma auditoria um frame após o carregamento de cada cena e grava:

`[NEXO_RENDER] scene=... camera=... pipeline=... api=... renderers=... materials=... incompatibleShaders=... magentaObjects=... canvases=... debugObjects=...`

O contador de shader incompatível considera material nulo, shader nulo ou `shader.isSupported == false`; o contador magenta considera cores de material com vermelho/azul altos e verde baixo. O log continua local em `Application.persistentDataPath/nexo-runtime.log`, sem rede nem dados pessoais.

## Validação desta correção

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=26`). A contagem C# aumentou de 25 para 26 porque `NexoQualityProfile.cs` foi adicionado na fase anterior.
- `git diff --check`: PASS.
- Unity Editor local: indisponível.
- Nova execução GameCI/AVD para confirmar `magentaObjects=0`: pendente deste commit.
- Appetize, GPU acelerada, Android ARM64 físico e iOS/Metal físico: não disponíveis nesta máquina.

## Conclusão atual

A origem mais provável e tecnicamente sustentada dos quadrados magenta é o material implícito do sistema de partículas de introdução. A geometria verde é intencional e identificada no bootstrap. A correção remove a dependência implícita de shader e instala telemetria local para confirmar o estado real no próximo smoke test.
