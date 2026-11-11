# PHASE 104 — Comparação do log Android API 37

O log confirma o mesmo estado funcional observado anteriormente:

- Unity `6000.3.0f1`, IL2CPP, x86_64 inicializa normalmente.
- `NexoVerticalSliceBootstrap.Awake()` e `Start()` executam.
- `[NEXO_RENDER]` registra `api=OpenGLES3`, `incompatibleShaders=0`, `magentaObjects=0`, `canvases=1`.
- Portanto, não há evidência de shader ausente, objeto magenta ou falha de boot.

Os erros `EGL_BAD_CONFIG` para ES 3.2/3.1 e `Bad attribute idx` pertencem ao GFXSTREAM do emulador API 37. O Unity ainda cria um contexto OpenGLES3 e prossegue; são incompatibilidades de negociação do backend do emulador, não a causa comprovada do quadro verde.

`liblibswappywrapper.so` falha na primeira tentativa por nome incorreto e logo depois `libswappywrapper.so` é carregada com sucesso; isso também não interrompe a execução.

Este trecho termina antes de qualquer evento de toque e não contém `OpenChallenge`, `NexoChallengeDoor` ou `InteractAtScreenPoint`. Assim, ele não prova a correção do clique. O diagnóstico do código permanece: o APK anterior usava raycast de 4 unidades e não alcançava o portal; a correção local está no commit `37abd91`.

Conclusão: o log é consistente com o diagnóstico anterior, mas a validação definitiva do clique exige uma captura após o toque e o log correspondente ao evento de interação.
