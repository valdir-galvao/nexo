# PHASE 99 — Perícia do quadro verde e janelas amarelas

## Conclusão

O quadro verde e as janelas amarelas não são uma falha de renderização. Eles são a aparência real do vertical slice procedural atual.

`NexoVerticalSliceBootstrap.BuildWorld()` cria a cena em runtime com `MakeCube()`:

- `Island`: cor `(0.3, 0.45, 0.3)`;
- `GreenDoor`: verde explícito `(0.18, 0.85, 0.42)`;
- `Reservoir`, `Source`, `Channel`, `Valve`, `ExitBridge` e `Explorer`: cubos com cores de tema;
- UI: Canvas procedural com painel escuro e botão `INICIAR MISSAO`.

Não há arte 3D final, texturas de cenário ou prefabs artísticos sendo carregados nessa fase. Portanto, o resultado esperado do código atual é uma ilha verde com portais coloridos e interface simples.

## Evidências do log fornecido

- `Scripting Backend il2cpp`, `CPU x86_64`: o APK iniciou corretamente.
- `eglCreateContext: EGL_BAD_CONFIG: no ES 3.2 support`: o emulador tentou ES 3.2 e recuou para ES 3.1; não interrompeu a execução.
- `eglMakeCurrent ... ver 3 1`: contexto OpenGL ES 3.1 ativo.
- `SCENE_LOADED mode=2 scene=NexoVerticalSlice`: cena carregada.
- `[NEXO_RENDER] ... pipeline=Built-in/none api=OpenGLES3 renderers=9 materials=9 incompatibleShaders=0 magentaObjects=0 canvases=1`: não há shader incompatível nem objeto magenta no runtime.
- `SurfaceSyncer`, `Gralloc4` e `Unexpected CPU variant`: avisos do Android Emulator/SwiftShader, sem evidência de falha do NEXO.

## Por que a percepção anterior persistiu

As correções anteriores eliminaram o defeito de partículas magenta, estabilizaram o contexto OpenGL ES e validaram boot/interação. Elas não substituíram o conteúdo provisório por uma direção visual final. A tela continua parecendo um protótipo porque o próprio jogo ainda é um vertical slice procedural.

## Estado

- Falha de shader: não reproduzida neste log.
- Falha de inicialização: não reproduzida.
- Falha de input: não reproduzida no AVD; o toque abriu a HUD da missão.
- Problema visual de produto: confirmado; a cena é minimalista/provisória.
- Compatibilidade universal: não comprovada; o teste é somente Appetize/AVD x86_64.

## Correção necessária para mudar o visual

A próxima correção não deve trocar shaders às cegas. Ela deve implementar conteúdo visual real: prefabs/modelos para ilha, reservatório e portais, materiais/texturas mobile-safe, iluminação/câmera art-directed e uma cena serializada. Depois disso, será necessário repetir o AVD e validar screenshots antes/depois.

Nenhuma alteração de código foi feita nesta perícia.
