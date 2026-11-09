# PHASE 96 — Diagnóstico de gráficos Android/emulador

## Evidências verificadas

- A captura histórica `artifacts/run-41211856635/.../screenshot.png` mostra a cena carregada, Canvas visível e botão `INICIAR MISSAO`; portanto o problema não é ausência total de boot.
- O log de runtime registra `scene=NexoVerticalSlice`, Unity `6000.3.0f1`, Android 15/API 35, `OpenGLES3`, `Google SwiftShader`, resolução `2400x1080`.
- O mesmo AVD registra `EGL_BAD_CONFIG` para tentativas ES 3.1/3.2 e avisos `Gralloc4`/`SurfaceSyncer`. Esses avisos pertencem ao backend do emulador; o contexto ES 3 foi criado e a cena abriu.
- Os quadrados magenta aparecem na captura do APK histórico. A origem está no `ParticleSystem` `Nexo Intro Floating Lights`, criado em `Assets/NEXO/Scripts/UI/NexoVerticalSliceBootstrap.cs` e originalmente sem material explícito. O commit `54e67ec` atribui material mobile-safe ao `ParticleSystemRenderer` e desativa o renderer quando o fallback não resolve.

## Causas separadas

### 1. Aparência verde/amarela

Não é fallback de shader por si só. `BuildWorld()` cria `Island` com cor verde e o portal inicial com cor verde; a UI usa painel escuro e cores de sinalização. O projeto é um vertical slice procedural, não uma cena artística completa com modelos/texturas. A aparência é intencional no código atual.

### 2. Quadrados magenta

São um defeito do APK antigo: partículas pequenas dependiam do material implícito do módulo de partículas. Em Android/SwiftShader esse material podia resolver para shader ausente/incompatível. A correção está no código, mas ainda não foi comprovada em um APK novo porque os runs recentes foram bloqueados por espaço/billing.

### 3. Pipeline gráfico incompleto

`Packages/manifest.json` declara URP 17.3.0, porém não existem no repositório `ProjectSettings/GraphicsSettings.asset`, `ProjectSettings/QualitySettings.asset` nem um URP Render Pipeline Asset. `EnsureIncludedShaders` encontra `GraphicsSettings.asset` ausente e apenas registra aviso. Consequentemente, o runtime usa o fallback Built-in (`Unlit/Color`) em vez de uma configuração URP serializada. Isso é uma fragilidade real para variantes de GPU/emulador, embora o fallback Built-in tenha permitido o boot histórico.

### 4. Dependências Unity

O manifesto possui 36 entradas e `Packages/packages-lock.json` contém apenas URP e Input System. A resolução real pelo Unity Package Manager 6000.3.0f1 ainda não foi executada nesta máquina/cloud; não é correto declarar que TextMeshPro ou UGUI estão resolvidos no ambiente de build.

## O que resolve para emuladores

1. Usar OpenGL ES 3 como API Android única no perfil de smoke test; deixar Vulkan fora dessa execução reduz caminhos de inicialização que o SwiftShader não suporta de forma consistente.
2. Gerar pelo Unity 6000.3.0f1 um `GraphicsSettings.asset`/`QualitySettings.asset` válido e um URP asset/renderer versionados, ou assumir explicitamente Built-in e remover a dependência operacional de URP. Não editar `packages-lock.json` manualmente.
3. Manter o material explícito das partículas e registrar no runtime `[NEXO_RENDER]` os contadores `incompatibleShaders` e `magentaObjects`.
4. Reexecutar o APK com AVD API 35, x86_64, SwiftShader e só considerar a correção visual comprovada quando o log registrar `incompatibleShaders=0` e `magentaObjects=0`, além de uma captura após a tela inicial e outra após `INICIAR MISSAO`.
5. Resolver primeiro o bloqueio externo do GitHub Actions (`recent account payments have failed or your spending limit needs to be increased`). Sem um runner iniciado não há como gerar o APK corrigido nem separar novos erros do emulador.

## Estado atual

- Código: correção de partículas presente; fallback de câmera/Canvas/material presente.
- Cena: índice 0, mas conteúdo é criado em runtime pelo bootstrap.
- Build Android novo: não comprovado; runs recentes bloqueados por disco e depois billing/quota.
- Compatibilidade do emulador: boot histórico comprovado em OpenGL ES 3/SwiftShader; warnings EGL/Gralloc não impediram a execução.
- Estado: `BLOCKED` para validação final da renderização, não `PASS`.
