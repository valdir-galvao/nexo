# PHASE 86 — Diagnóstico estático do runtime do aplicativo NEXO

Data: 2026-11-01 (America/Sao_Paulo)
Branch: `orchestrator/phase-a-baseline`
Versão declarada: Unity `6000.3.0f1`
Escopo: diagnóstico de aplicação por inspeção local. Nenhuma funcionalidade, dependência ou configuração foi alterada; nenhum build foi executado.

## Conclusão executiva

Não há evidência local suficiente para declarar um crash ou falha visual reproduzida nesta sessão. Há, porém, dois riscos concretos do aplicativo:

1. A cena `NexoVerticalSlice.unity` contém apenas um GameObject vazio; câmera, Canvas, EventSystem, mundo e partículas são criados em runtime por `RuntimeInitializeOnLoadMethod`/`NexoVerticalSliceBootstrap`.
2. O Package Manager está inconsistente: manifesto com 36 dependências e lockfile com 2. Isso impede validar TMP, URP e Test Framework com o Editor exigido e pode explicar diferenças entre runners.

A correção do commit `54e67ec` está presente no código: o ParticleSystem `Nexo Intro Floating Lights` recebe material explícito e é desabilitado se o fallback não resolver. Isso elimina a dependência implícita do material de partículas, mas a ausência de execução Unity/Android nesta sessão impede comprovar `magentaObjects=0` em runtime.

## Inicialização

- `ProjectSettings/EditorBuildSettings.asset` registra `Assets/Scenes/NexoVerticalSlice.unity` habilitada no índice 0.
- O `.meta` da cena possui o mesmo GUID registrado no Build Settings.
- A cena não serializa `NexoVerticalSliceBootstrap`, Camera, Canvas ou EventSystem; todos são criados pelo bootstrap em runtime.
- `CreateIfMissing()` usa `RuntimeInitializeOnLoadMethod(AfterSceneLoad)` e cria o bootstrap quando não o encontra.
- `Awake()` injeta EventSystem/StandaloneInputModule.
- `StartCore()` executa qualidade, câmera, luz, Canvas, partículas, missão e `EnsureWorldBuilt()`.
- Falhas em `Start()` são capturadas por `try/catch`, mas um erro de importação/assembly antes da execução não seria tratado por esse bloco.

Risco confirmado: se `CreateIfMissing()` não for executado por falha de importação, assembly ou configuração de script, a cena permanece vazia. Isso requer teste PlayMode real para confirmar.

## Visual e gameplay

`BuildWorld()` cria proceduralmente:

- `Island`: cubo verde intencional (`new Color(0.3, 0.45, 0.3)`).
- `GreenDoor`/portais: cubo verde, azul ou roxo conforme estágio.
- `Source`, `Channel`, `Valve`, `Reservoir`, `ExitBridge` e `Explorer`.

Os objetos usam malha cúbica própria, `MeshRenderer` e material instanciado a partir de `defaultWorldMaterial`; não usam `GameObject.CreatePrimitive`, `CapsuleCollider` ou `CharacterController`. Colisores são `BoxCollider` nos objetos interativos.

A câmera é criada/garantida com tag `MainCamera`, fundo sólido e posição segura. Uma luz direcional é criada se não existir. O Canvas usa `ScreenSpaceCamera`, fallback `ScreenSpaceOverlay`, `CanvasScaler` 1920x1080/Match 0.5, `GraphicRaycaster` e sorting order 10.

## Quadrados magenta e shaders

O código de `54e67ec` está presente em `BuildIntroParticles()`:

- procura `ParticleSystemRenderer`;
- atribui uma cópia de `defaultWorldMaterial` com cor azul clara;
- desabilita o renderer e registra warning se o material/shader não existir.

Não há `Color.magenta` encontrado no código auditado. Os quadrados magenta são, portanto, uma hipótese historicamente consistente com material implícito de partículas, já corrigida no código. A hipótese ainda não está comprovada em um novo APK porque Unity/GameCI não foi executado.

O asset `Assets/Resources/Materials/URP_Default_Gameplay.mat` possui referência de shader serializada com `fileID` embutido e GUID nulo. Sem Unity não é possível confirmar o nome resolvido desse shader. O bootstrap tenta fallback `Unlit/Color`, `UI/Default`, URP Unlit e `Sprites/Default`; essa resolução precisa ser validada no Editor e no dispositivo.

## Dependências

Parser JSON:

- `manifest.json`: 36 dependências.
- `packages-lock.json`: 2 dependências (`com.unity.render-pipelines.universal` e `com.unity.inputsystem`).
- Ausentes no lockfile: TMP 3.0.6, UGUI 2.0.0, Test Framework 1.5.1 e módulos Unity declarados, entre outros.

Isso não prova que os pacotes não existam no registry; prova que a resolução do projeto não está registrada. Não editei o lockfile.

## Erros reproduzidos

Nenhum erro de runtime foi reproduzido nesta máquina: não há Unity `6000.3.0f1`, nem execução PlayMode, Android ou AVD disponível. Não há novo `Editor.log`/logcat desta fase.

Bloqueios observáveis:

- resolução real de pacotes: BLOCKED;
- EditMode/PlayMode: NOT_RUN;
- APK/AVD: NOT_RUN;
- iOS: NOT_RUN.

## Correções mínimas recomendadas

1. Abrir o projeto com Unity `6000.3.0f1` e regenerar `packages-lock.json` pelo Package Manager.
2. Executar EditMode e PlayMode antes de qualquer build.
3. No PlayMode, confirmar que `CreateIfMissing()` instancia o bootstrap e que a cena permanece com câmera/Canvas ativos após um frame.
4. No Android AVD, coletar `[NEXO_RENDER]` e confirmar `incompatibleShaders=0` e `magentaObjects=0`.
5. Se o material `URP_Default_Gameplay.mat` resolver para shader inválido, corrigir o asset pelo Unity, não por edição textual.

## Validação local

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=26`).
- `git diff --check`: PASS.
- Unity runtime: BLOCKED/NOT_RUN.

## Resultado

`FASE 86: PARTIAL`. O código da correção de partículas está presente e a origem da geometria verde é confirmada como intencional. A causa definitiva de qualquer fechamento ou tela magenta em dispositivo permanece não comprovada sem resolução de pacotes e execução Unity/Android observável.
