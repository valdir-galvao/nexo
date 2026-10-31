# Fase 60 - Checklist pre-build Android

Data: 31/10/2026 (America/Sao_Paulo)
Branch auditada: `orchestrator/phase-a-baseline`
Escopo: inspeção local de configuração, sem build e sem execução Unity nesta fase.

## 1. Arquitetura Android

`ProjectSettings/ProjectSettings.asset` foi auditado por leitura estruturada:

- `scriptingBackend.Android: 1`: IL2CPP.
- `androidTargetArchitectures: 3`: ARMv7 + ARM64, incluindo ARM64.
- `AndroidMinSdkVersion: 29`.
- `AndroidTargetSdkVersion: 35`.
- `defaultScreenOrientation: 3`: paisagem, coerente com o perfil definido.

Os valores foram conferidos com busca por linhas exatas; uma tentativa anterior com regex ampla produziu falsos negativos e foi descartada.

## 2. Cenas de build

`ProjectSettings/EditorBuildSettings.asset` contém `Assets/Scenes/NexoVerticalSlice.unity` como primeira entrada (`m_Scenes`, índice 0) e com `enabled: 1`. Não foi encontrada outra cena na primeira posição. A configuração permanece compatível com o smoke test Android já aprovado no run `41171249169`.

## 3. UI, Safe Area e interação

O bootstrap `Assets/NEXO/Scripts/UI/NexoVerticalSliceBootstrap.cs` foi ajustado para criar uma raiz `Nexo Safe Area UI` sob o Canvas e recalcular os anchors a partir de `Screen.safeArea`. Intro e painel de desafio são filhos dessa raiz, mantendo controles dentro do recorte seguro. Também foram confirmados:

- Canvas em `ScreenSpaceOverlay`, `sortingOrder = 10`.
- CanvasScaler em `ScaleWithScreenSize`, resolução `1920x1080`, `MatchWidthOrHeight`, `match = 0.5`.
- EventSystem, GraphicRaycaster e PhysicsRaycaster presentes no caminho de inicialização.
- Botões do quiz criados com Image, Button e target graphic.

A raiz safe area é recalculada no `Update` para acomodar rotação ou mudanças de viewport.

## 4. Manifesto e permissões

`Assets/Plugins/Android/AndroidManifest.xml` não está presente. Isso é intencional: o manifesto customizado incompleto foi removido anteriormente porque impedia a criação da Activity launcher; Unity gera o manifesto padrão. O APK do run `41171249169` passou pelo smoke test no AVD API 35, confirmando a Activity de entrada.

## 5. Validações

- `python tools/run_static_qa.py`: PASS (`static_qa_ok: json=5 cs=25`).
- `git diff --check`: PASS. O Git emitiu apenas o aviso normal de conversão CRLF/LF.
- Unity Editor, importação, teste PlayMode e nova build: NOT_RUN nesta fase. A evidência de runtime anterior permanece vinculada ao commit `4049476`.

## 6. Resultado e pendências

Resultado da auditoria local: PASS para as configurações verificáveis por arquivo e código. A alteração de Safe Area ainda requer uma execução Unity/GameCI posterior para validação visual em múltiplas proporções; não há alegação de validação física nova neste passo.
