# Fase 02 — estabilização de dependências, K-005 e PlayMode

Commit de trabalho: `f32e74c` antes das alterações. Branch:
`orchestrator/phase-a-baseline`.

## Alterações

- `VerticalSliceMission.RecordTransfer(bool)` registra tentativas no
  `SaveData.masteryEvidence`, com objetivo `transfer-energy-route`, contagem e
  resultado demonstrado.
- `NexoVerticalSliceBootstrap` apresenta uma etapa de transferência após a
  conclusão narrativa e chama o registro persistente para respostas corretas ou
  incorretas.
- Adicionada assembly `Nexo.PlayModeTests` e teste `[UnityTest]`
  `VerticalSliceSceneInitializesIntoIntro`, que carrega a cena e verifica o
  bootstrap no estado `Intro`.
- Adicionado teste NUnit de regressão para persistência da tentativa de
  transferência.

## Validação

- `python tools/run_static_qa.py`: PASS, exit 0 (`json=5`, `cs=24`).
- `python tools/validate_learning_content.py`: PASS, exit 0.
- `python -m unittest discover -s tools -p test_agents.py -v`: PASS, exit 0,
  9 testes.
- `git diff --check`: PASS.
- Unity Editor/GameCI: `NOT_RUN` — não disponível nesta máquina.
- Compilação C# Unity, descoberta PlayMode, carregamento da cena e persistência
  em arquivo: `NOT_RUN`.

## Dependências

`manifest.json` continua com 36 dependências e `packages-lock.json` com 2
entradas. A regeneração correta depende do Unity Package Manager; o lockfile não
foi editado manualmente. Estado: `BLOCKED`.

## Estado final

Correções de código e testes foram preparados em branch isolada, mas a Fase 02
não pode ser considerada totalmente aprovada até o Unity/GameCI confirmar
resolução de pacotes, compilação, EditMode e PlayMode.
