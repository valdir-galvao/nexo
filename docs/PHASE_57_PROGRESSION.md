# Fase 57 - Progressao de fases e recordes locais

Data: 31/10/2026 (America/Sao_Paulo)

## Implementacao

- O bootstrap agora mantem `activeStage` entre 1 e 3 usando `PlayerPrefs` (`NEXO_Unlocked_Stage`).
- A Fase 1 usa fluxo de agua; a Fase 2 usa eficiencia energetica; a Fase 3 usa uma sequencia de logica.
- Cada resposta correta desabilita o portal anterior, cria o proximo portal proceduralmente com cor e nome distintos e registra o desbloqueio local.
- O maior score e persistido em `NEXO_High_Score` e aparece no HUD junto da fase atual.
- O reset de dados locais remove score e fase desbloqueada, evitando estado impossivel.
- Opcoes, textos, materiais e componentes novos possuem caminhos defensivos; os portais continuam usando a malha cubica visual e o material seguro ja resolvido pelo bootstrap.

## Validacao

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`).
- `git diff --check`: PASS.
- Unity Editor, PlayMode, dispositivo e builds: NOT_RUN nesta etapa.

## Pendencias

Validar no Unity/GameCI a transicao entre as tres fases, persistencia apos reinicio e o fluxo de reset antes de produzir novos aplicativos.
