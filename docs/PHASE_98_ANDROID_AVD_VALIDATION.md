# PHASE 98 — Validação Android no AVD e diagnóstico visual

Data: 2026-11-10 (America/Sao_Paulo)
Commit testado: `03be89764e439445576995b940643599c566d9c7`

## Execuções reais

- Run ARM64 `42731883239`: SUCCESS; build Android em 26m, APK gerado.
- Run X86_64 `42735484609`: SUCCESS; build em 23m27s e smoke test AVD SUCCESS.
- AVD: `google_apis`, API 35, x86_64, Pixel 6, SwiftShader, OpenGL ES 3.
- Instalação: `Success`.
- Activity em foreground: `com.nexo.educational/com.unity3d.player.UnityPlayerActivity`.

## Evidência de renderização e interação

O smoke test capturou `screenshot.png` antes da interação e `screenshot-after-start.png` após `adb shell input tap 1080 700`.

- A tela inicial mostra NEXO, texto introdutório e o botão `INICIAR MISSAO` em tela cheia.
- Após o toque, a tela muda para o HUD da missão, com `Pontuacao: 0 | Recorde: 0 | Fase: 1/3`, ilha verde, portal amarelo e controles `Pausa`, `Diário`, `Interagir` e `Ajuda`.
- Não há quadrados magenta nas capturas.
- Não houve diálogo do Pixel Launcher sobre ANR nesta execução.

Arquivos: `artifacts/run-42735484609/screenshot.png` e `artifacts/run-42735484609/screenshot-after-start.png`.

## Logs técnicos

`nexo-runtime-external.log` registrou:

`SCENE_LOADED mode=2 scene=NexoVerticalSlice`

`[NEXO_RENDER] scene=NexoVerticalSlice camera=Nexo Main Camera pipeline=Built-in/none api=OpenGLES3 renderers=9 materials=9 incompatibleShaders=0 magentaObjects=0 canvases=1 debugObjects=0`

O logcat confirmou `NEXO_BOOT`, `NEXO_RENDER`, OpenGL ES 3 e ausência de exceções Unity/FATAL/Shader/SIGSEGV associadas ao aplicativo.

## Artefatos

- APK ARM64: `artifacts/run-42731883239/Android/Nexo.apk`
- SHA-256 ARM64: `A1C0D46FCC45234C8976C4F8625D4771C6B6851B2CA8AB935832412B820D10A3`
- APK X86_64: disponível no artefato `nexo-android-X86_64-03be89764e439445576995b940643599c566d9c7` do run `42735484609`.
- Metadata: `artifacts/run-42731883239/BUILD_METADATA.txt`.

Observação: o `BUILD_METADATA.txt` declara `Mono2x` por causa do modo de homologação, mas o log do player reporta `Scripting Backend il2cpp` para o APK executado no AVD. Essa divergência documental deve ser corrigida no workflow antes de usar o metadata como prova de backend.

## Estado

- Boot, renderização, UI e toque: PASS no AVD x86_64/SwiftShader.
- `incompatibleShaders=0` e `magentaObjects=0`: PASS neste runtime.
- Compatibilidade universal Android/iOS: NÃO CONCLUÍDA; só o AVD x86_64 foi testado.
- Unity Package Manager/lockfile: ainda requer resolução oficial no Unity 6000.3.0f1.
- Testes ARM64 físico: NOT_RUN.

## Próxima ação

Corrigir a geração de `BUILD_METADATA.txt` para refletir o backend efetivo e, depois, repetir testes em dispositivo ARM64 quando houver equipamento. O repositório será restaurado para privado após esta auditoria.
