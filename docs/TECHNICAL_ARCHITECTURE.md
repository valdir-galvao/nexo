# NEXO — Technical Architecture P2

Status: baseline de arquitetura e contratos; importação Unity pendente. Data:
26/10/2026.

## Baseline verificado

O repositório declara Unity `6000.3.0f1`, URP `17.3.0`, Input System `1.14.0`
e Test Framework `1.5.1`. O Windows local tem Git/Python, mas não há Unity
Editor, `adb`, SDK Android ou `xcodebuild` verificados no PATH. Android e iOS
serão validados nos runners GameCI; nenhuma compatibilidade de build é declarada
antes do primeiro workflow verde.

## Camadas

`Core` inicializa configuração e ciclo de vida. `Gameplay` contém movimento,
interação e regras puras. `World` expõe objetos e estados sem decidir UI.
`Narrative` publica eventos e diálogos. `Learning` transforma ações em evidências.
`Progression` orquestra missões e desbloqueios. `UI`, `Audio` e apresentação
consomem interfaces. `Persistence` serializa dados versionados. `Safety` aplica
pausa, permissões e padrões infantis. `Analytics` registra somente eventos locais
mínimos, sem rede no protótipo.

Dependências permitidas: UI/Audio dependem de contratos de Gameplay/Narrative;
domínio não depende de MonoBehaviour, cena, rede ou asset visual. Um `CompositionRoot`
é o único ponto que conecta implementações Unity às interfaces do domínio.

## Estrutura

```text
Assets/NEXO/
  Scripts/{Core,Gameplay,World,Narrative,Learning,Progression,UI,Audio,Persistence,Safety,Analytics}
  Scenes/  Prefabs/  Art/  Audio/  Data/  Tests/
```

Dados de missão são assets/JSON declarativos validados por schema e listas
permitidas. Não existe interpretador de código, reflection para executar conteúdo
ou endpoint obrigatório. O estado de apresentação pode ser reconstruído a partir
de `MissionRuntimeState`.

## Ciclo de vida

`Boot → LoadContent → LoadSave → MainMenu/Mission → Pause/Resume → Save → Quit`.
Cada transição é idempotente; salvar falha de forma segura e nunca bloqueia o
retorno ao menu. Configuração de acessibilidade é carregada antes da primeira UI.

## Testabilidade e plataformas

Regras de água, válvula, objetivos e migração ficam em C# sem API Unity e têm
testes EditMode. Adaptadores Unity ficam finos. Android/iOS compartilham domínio;
somente bootstrap, caminho de build e permissões ficam condicionais por plataforma.
