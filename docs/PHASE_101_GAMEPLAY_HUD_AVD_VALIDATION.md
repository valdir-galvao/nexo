# PHASE 101 — Validação do HUD de gameplay após Iniciar Missão

Data: 2026-11-10 (America/Sao_Paulo)
Commit testado: `ed13e2b0a72c7159a4589ccf79f8dafc5eef8824`

## Diagnóstico

O quadro estático observado anteriormente não era um congelamento do Unity. O vertical slice usa geometria procedural verde/amarela (`BuildWorld`/`MakeCube`) e o deslocamento do jogador depende de entrada (`Input.GetAxis`/toque). A informação pós-clique dependia principalmente de `OnGUI`, que era pequena e pouco confiável em alguns caminhos de renderização móvel.

## Correção aplicada

`NexoVerticalSliceBootstrap.cs` passou a criar uma HUD explícita em Canvas, com `CanvasScaler` 1920x1080, `GraphicRaycaster`, área segura e textos legíveis para estado, água, pontuação, objetivo e prompt. A HUD é ativada quando `showMenu == false && showIntro == false`; `OnGUI` permanece como fallback. O movimento continua corretamente condicionado a input do jogador.

## Validação remota

- Run Android ARM64 automático: `42744224501`, SUCCESS.
- Run Android X86_64 + AVD: `42747250853`, commit `ed13e2b`, job Android SUCCESS e job `android-emulator` SUCCESS conforme a página autenticada do run.
- Artefatos remotos do run X86_64: `android-emulator-diagnostics-42747250853` (305 KB, digest `sha256:6d10b4c1bb3103609bed3fecb24ea4a12c064d3d52975334fae6e532161341a6`) e `nexo-android-X86_64-ed13e2b0a72c7159a4589ccf79f8dafc5eef8824` (55.4 MB, digest `sha256:f05f0418588879115215fae3af38f60f27b00e732715184af922879de0fccb6c`).
- A API REST atingiu rate limit HTTP 403 durante a tentativa de download local dos artefatos; por isso não foi possível inspecionar a nova screenshot/log neste ambiente. O sucesso remoto do job foi confirmado pela página do workflow, mas a evidência visual pós-correção permanece pendente de download.

## QA local

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=26`).
- `git diff --check`: PASS.
- Repositório restaurado para `PRIVATE` após a validação.

## Estado

`PARTIAL`: a correção está commitada e o build/smoke remoto concluíram com sucesso, mas a captura visual pós-correção não foi baixada devido ao limite da API. Não declarar compatibilidade universal sem screenshot/log baixados e teste físico ARM64.
