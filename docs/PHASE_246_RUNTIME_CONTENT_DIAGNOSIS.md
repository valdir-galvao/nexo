# NEXO — Diagnóstico de conteúdo visual em runtime

## Resultado

O APK estava iniciando corretamente, mas apresentava um vertical slice de blockout. O log `[NEXO_RENDER]` reportava `incompatibleShaders=0` e `magentaObjects=0`; portanto os avisos EGL do emulador não explicam a ausência de conteúdo. A aparência observada era produzida pelo próprio código procedural.

## Evidências

- `Assets/NEXO/Scripts/UI/NexoVerticalSliceBootstrap.cs`: `BuildWorld()` cria `Island`, `Water Surface`, `Source`, `Channel`, `Valve`, `Reservoir`, `ExitBridge`, árvores, rochas e o `Explorer` com `MakeCube`/`CreateVisualPart`.
- `CreateExplorerAvatar()` formava corpo, cabeça, visor, mochila e pernas com cubos; não havia prefab humanoide conectado ao player.
- `Assets/NEXO/Scripts/UI/NexoScenePresentation.cs`: cristais/diamantes, pirâmides, anel do portal e faixa de água são malhas procedurais (`CreateDiamondMesh`, `CreatePyramidMesh`, `CreateRibbonMesh`).
- `Assets/NEXO/Scripts/Audio/NexoAudioDirector.cs`: áudio sintetizado em runtime; não é trilha/efeito final importado.
- Os fixtures gratuitos existiam em `Assets/External/FreeTestAssets`, mas não havia prefab/controller gerado no repositório.

## Correção aplicada

O setup do Editor agora grava o prefab humanoide em `Assets/Resources/Characters/NexoExplorer_FreeTest.prefab`. O bootstrap tenta carregar esse prefab e seu `AnimatorController` antes do fallback procedural, registrando explicitamente qual caminho foi usado. A correção não remove os fixtures nem as dependências; ela conecta a saída do importador ao runtime.

## Limite de validação

O Unity Editor 6000.3.0f1 não está disponível nesta máquina, portanto ainda não é possível comprovar a importação FBX, a geração do prefab/controller ou a aparência em um APK novo. O APK anterior não pode ser considerado uma build com avatar humanoide de produção.
