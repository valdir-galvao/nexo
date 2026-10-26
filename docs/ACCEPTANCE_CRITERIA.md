# NEXO — Acceptance Criteria (P0)

## Gate P0

P0 é aceito quando os seis documentos de planejamento estão presentes, sem
segredos, e cada requisito funcional, pedagógico, de acessibilidade, privacidade
e segurança tem um método de validação explícito. Deve existir uma lista de
decisões pendentes sem respostas inventadas.

## Gate P1 — fundação jogável

Aceitar somente se todos os itens forem demonstrados em build de desenvolvimento:

1. Um novo jogador consegue iniciar a missão, aprender o controle, explorar,
   tomar a decisão de água, construir/reparar o reservatório e ver a conclusão.
2. FR-001, FR-007 e NFR-001 passam em modo avião nos dispositivos escolhidos.
3. O save sobrevive a pausa, suspensão, encerramento e reabertura; corrupção é
   rejeitada com recuperação segura.
4. A matriz de eventos prova LR-001/LR-002 sem registrar texto livre infantil.
5. AX-001 a AX-004 passam em revisão de acessibilidade e teste de áudio desligado.
6. CP-001 a CP-005 e SEC-001/SEC-002 passam por revisão e evidência de build.
7. Nenhuma rede, conta, chat, loja, publicidade, economia monetária ou geração
   de conteúdo por IA é necessária para completar a missão.
8. Cada defeito P0/P1 tem dono, severidade e decisão explícita; nenhum bloqueador
   permanece sem aceite formal ou plano de correção.

## Evidências obrigatórias por entrega

- Build identificada por commit/versão e plataforma.
- Vídeo ou roteiro de reprodução do fluxo crítico, quando permitido.
- Relatório de testes com dispositivo, sistema, modo de rede e resultado.
- Matriz de requisitos com links para testes, cenas, scripts ou revisão.
- Registro de decisões e riscos atualizado na mesma alteração.

## Definição de concluído por tarefa

Uma tarefa só é concluída com implementação ou documento no escopo, teste
executado, resultado observável, revisão do responsável e atualização de
`docs/PROGRESSO.md`. “Funciona localmente” sem ambiente e evidência não é aceite.
