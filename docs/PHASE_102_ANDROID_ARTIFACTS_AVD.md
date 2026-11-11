# PHASE 102 — Builds Android e validação AVD

Data: 2026-11-11 (America/Sao_Paulo)
Commit remoto: e3bcd1e6aae8371ddb8225f052fadfb75331f209

## Execuções

- ARM64: run 42932164802, SUCCESS.
- X86_64 + AVD: run 42932184190, SUCCESS.
- Build Android X86_64: 24m46s.
- Smoke AVD API 35 / x86_64 / SwiftShader: 2m19s, SUCCESS.

## Artefatos

- ARM64: C:/Temp/nexo-release-final/Android/Nexo-ARM64.apk, SHA-256 $ha.
- X86_64: C:/Temp/nexo-release-final/Android/Nexo-X86_64.apk, SHA-256 $hx.
- Metadados: BUILD_METADATA-ARM64.txt e BUILD_METADATA-X86_64.txt.
- Diagnósticos: C:/Temp/nexo-release-final/Android/AVD-diagnostics-42932184190/.

## Evidência visual

screenshot.png mostra a tela inicial com título, instruções e botão INICIAR MISSAO. screenshot-after-start.png mostra o estado Exploring, água, pontuação, recorde, objetivo e prompt Toque em Interagir para investigar pistas.. A HUD Canvas corrigiu o problema de ausência de texto após o clique.

O log externo confirmou:

- SCENE_LOADED ... NexoVerticalSlice
- NEXO_RENDER ... api=OpenGLES3
- incompatibleShaders=0
- magentaObjects=0
- canvases=1

Os avisos EGL/SwiftShader são do emulador e não impediram a renderização.

## Segurança e QA

O repositório foi tornado público apenas durante o push/build e retornado imediatamente para PRIVATE após os downloads. python tools/run_static_qa.py permanece PASS (json=5, cs=26) e git diff --check PASS.

Estado: PASS para boot, transição pós-clique, renderização e smoke test no AVD X86_64. Isso não comprova compatibilidade universal nem substitui teste físico ARM64.
