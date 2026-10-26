# NEXO — Mission Design: O reservatório silencioso

## Objetivo

Restaurar o fluxo de água da ilha e descobrir por que o reservatório ficou
silencioso. Duração-alvo: 10–15 minutos, com checkpoints a cada 2–3 minutos.

## Sequência implementável

| Etapa | Entrada | Ação do jogador | Evidência/saída |
|---|---|---|---|
| Chegada | Boot concluído | seguir Mira até a praça | objetivo “encontrar a fonte” |
| Fonte alta | objetivo ativo | inspecionar poça e coletar 3 água | inventário 6/6; pista do canal |
| Canal quebrado | pista de água | comparar marcas e inspecionar válvula | pista do desvio; reparo habilitado |
| Reservatório | válvula visível | escolher reparar e alocar 2 água ou testar horta | feedback causal; estado salvo |
| Teste | reparo confirmado | acionar alavanca e observar fluxo | reservatório ativo ou horta/atalho ativo |
| Ponte | qualquer estratégia válida | seguir o sinal do eco | conclusão, reflexão e pista maior |

## Puzzles

**P1 — Rota da água.** Solução: seguir marcas de umidade da fonte ao canal.
Pistas: som de gotejamento, musgo molhado e setas de giz. Feedback: a linha de
água anima quando a hipótese está correta; se errar, o jogo pede comparação.

**P2 — Prioridade.** Solução A: 2 água no reservatório, desbloqueia ponte. Solução
B: 2 água na horta, desbloqueia atalho e fala de Mira; o jogador pode então
recolher a água restante e reparar o reservatório. Ambas ensinam trade-off.
Feedback mostra o estoque antes/depois e a consequência observável.

**P3 — Válvula.** Solução: girar a peça até alinhar os dois marcadores de fluxo.
Três posições, apenas uma conecta entrada e saída. Tentativas não quebram a peça;
após duas tentativas a pista explícita mostra os marcadores.

## Falhas recuperáveis

Sem água: voltar à fonte, rever pistas e coletar a unidade restante. Válvula
desalinhada: resetar sem custo. Pausa, suspensão ou fechamento normal retomam o
último checkpoint. Não há morte, cronômetro, perda de save ou punição por ausência.

## Condições de aceite

Cada etapa deve emitir evento identificável (`mission_started`, `clue_found`,
`allocation_confirmed`, `valve_repaired`, `mission_completed`) sem texto livre.
O teste de missão verifica as duas estratégias, reinício e save em cada checkpoint.
