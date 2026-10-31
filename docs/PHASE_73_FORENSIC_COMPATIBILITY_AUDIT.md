# NEXO — Fase 73: auditoria forense visual e compatibilidade

## Causa provável da tela verde/amarela

A auditoria não encontrou shader nulo restante no material de `Resources`: `URP_Default_Gameplay.mat` referencia o shader Built-in Unlit por fileID. As cores verde/amarela vêm de `NexoVisualTheme` e da UI de desafio (`Water`, `Signal`, feedback de resposta), portanto são aparência intencional do vertical slice, não evidência isolada de fallback magenta.

O projeto declara o pacote URP, mas não versiona `ProjectSettings/GraphicsSettings.asset`, `QualitySettings.asset` nem um Render Pipeline Asset. Assim, o editor pode executar com renderer padrão e o bootstrap seleciona `Unlit/Color`/`UI/Default` para o mundo procedural. Essa ausência era uma fonte de variação entre Editor, Android e iOS.

Os avisos `EGL_BAD_CONFIG`, `SurfaceSyncer` e `Gralloc4` são compatíveis com limitações do backend gráfico do emulador/Appetize. Eles não provam, sozinhos, um shader inválido do projeto, mas exigem uma API Android mais conservadora.

## Correções implementadas

- `ConfigureMobileGraphics.cs`: OpenGL ES 3 como primeira API Android, Vulkan opcional e Metal no iOS; API mínima 29, alvo 35 e ARM64+x86_64 reforçados pelo Editor.
- `NexoQualityProfile.cs`: perfis Low, Medium e High selecionados por RAM, memória gráfica e núcleos; override manual persistido em `NEXO_Quality_Override`.
- `RuntimeDiagnostics.cs`: registra API gráfica, GPU, memória gráfica, RAM, resolução, perfil de qualidade e processadores; warnings/errors de shader e cena continuam capturados no log local.
- `NexoVerticalSliceBootstrap.cs`: aplica perfil antes da criação visual, diferencia logs Awake/Start e usa Overlay como fallback se a câmera não puder ser vinculada ao Canvas.
- Mantidos EventSystem, StandaloneInputModule, GraphicRaycaster, câmera, luz direcional, shaders Unlit e Safe Area existentes.

## Compatibilidade alvo

- Android: API 29+, OpenGL ES 3 como baseline, Vulkan opcional, IL2CPP, ARM64 e x86_64.
- iOS: Metal e ARM64; o IPA continua sujeito à assinatura para instalação física.
- Low: 30 FPS, sem sombras e sem anti-aliasing.
- Medium: sombras simples e até 60 FPS.
- High: sombras, reflexão e anti-aliasing moderados, até 60 FPS.

## Limitações e validação

A máquina local não possui Unity Editor nem dispositivos físicos. A validação runtime deve ocorrer nos workflows GameCI e no smoke test Android existente. Appetize, emuladores Vulkan/OpenGL separados, aparelhos Android de entrada/intermediário/alto desempenho e iPhones físicos permanecem pendentes até haver infraestrutura observável. Nenhuma compatibilidade universal é declarada antes desses testes.
