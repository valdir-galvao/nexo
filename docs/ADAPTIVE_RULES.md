# NEXO — Adaptive Rules P5

## Regras determinísticas

1. Se o jogador não encontra evidência após 45 s em faixa de tempo, mostrar pista
   contextual; nunca revelar a resposta.
2. Após uma tentativa incorreta, repetir a pergunta com explicação causal curta.
3. Após duas tentativas incorretas, oferecer pista explícita e permitir reinício.
4. Se a pista for usada, manter o resultado como válido, reduzindo a força da
   evidência de independência; não reduzir progresso narrativo.
5. Pausa, suspensão, saída ou abandono não contam como erro. Ao retornar, oferecer
   resumo e repetir o item sem penalidade.
6. Se houver acerto em duas situações de dificuldade diferente, marcar
   `InitialEvidence`; só a transferência consistente pode marcar domínio confirmado.
7. Divergência entre situações coloca `NeedsReview`, não “fracasso”.

## Idade

8–11: frases curtas, diagrama e até duas variáveis visíveis. 12–14: duas
evidências e previsão explícita. 15–18: hipótese, justificativa e transferência.
Presets podem avançar por competência; idade nunca bloqueia desafio demonstrado.

## Privacidade

Regras operam em IDs, contagens e estados locais. Não inferem perfil psicológico,
motivação, inteligência ou risco. Analytics de rede permanece desligado.
