# Phase 257 — Avatar, UI e áudio em runtime

Data: 2026-11-14 (America/Sao_Paulo)

## Auditoria

`NexoVerticalSliceBootstrap.StartCore()` chama `EnsureWorldBuilt()` antes de
`NexoScenePresentation.Initialize()`. Em `BuildWorld()`, o avatar é criado por
`CreateExplorerAvatar(new Vector3(-4, 1, -1))`, usando primeiro
`Resources.Load<GameObject>("Characters/CapBuddy")`; o prefab é instanciado
com `Instantiate`. Se o prefab não existir, o código registra o fallback
procedural explicitamente.

## Correções aplicadas

- O avatar autorado agora recebe `Animator` caso o prefab não o contenha,
  carrega `Characters/CapBuddy` como `RuntimeAnimatorController` quando o
  controller não estiver serializado, chama `Rebind/Update/Play("Idle")`,
  habilita o Animator, ativa todos os `Renderer` filhos e registra o estado
  final com `[NEXO_AVATAR]`.
- `EnsureAvatarVisible` calcula os bounds dos renderers e reposiciona a câmera
  para enquadrar o avatar à frente dela. O log informa alvo, quantidade de
  renderers e estado do Animator.
- O painel `COMO JOGAR` teve sua área de texto deslocada para cima do botão
  `ENTENDI`, removendo a sobreposição observada no Canvas.
- `OnGUI` legado deixa de desenhar quando o Canvas moderno está pronto. Isso
  elimina a segunda HUD que sobrepunha textos e botões após `INICIAR MISSÃO`.
- Todos os botões criados por `CreateHudButton`, incluindo controles e guia,
  reproduzem `NexoAudioDirector.PlayClick()` antes da ação. As respostas e o
  fechamento do desafio também reproduzem o clique; acerto, erro e desbloqueio
  continuam usando `PlayFeedback`, `PlayDiscovery` e `PlayPortalUnlock` nos
  pontos de conclusão existentes.

## Validação

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=27`).
- `git diff --check`: PASS.
- Unity Editor local 6000.3.0f1: indisponível neste ambiente; compilação e
  execução local permanecem BLOCKED. A confirmação runtime depende de novo run
  GameCI/AVD.

## Limitação

O prefab CapBuddy e seu controller são gerados no CI pelo `CapBuddySetup`; esta
alteração não inventa assets locais ausentes. O fallback procedural só aparece
quando o recurso não foi gerado/importado, e o log identifica essa condição.
