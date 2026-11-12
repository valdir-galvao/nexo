# Phase 117 — Overhaul de apresentação e interatividade

Data: 2026-11-12 (America/Sao_Paulo)

## Correções aplicadas

- O painel `COMO JOGAR` agora explica o fluxo completo e possui o botão `ENTENDI`; também é fechado ao primeiro movimento ou uso de `INVESTIGAR`.
- O cenário ganhou superfície de água azul visível, ondulação contínua, árvores e rochas leves.
- O avatar Explorer recebeu partes rastreadas e animação procedural de caminhada/respiração (corpo, cabeça e pernas).
- A câmera apresenta um enquadramento panorâmico inicial por 3,5 segundos antes de acompanhar o avatar.
- O marcador de objetivo, portal, canal, água e luz continuam animados no loop existente.
- A posição inicial do avatar foi aproximada dos objetivos para tornar a primeira missão visível e compreensível.

## Verificação

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=26`, exit 0).
- `git diff --check`: PASS.
- Nenhuma build ou alteração de visibilidade foi executada nesta etapa.

## Pendências

A validação visual final ainda exige nova execução Unity/GameCI e smoke no AVD. O APK anterior não contém estas alterações.
