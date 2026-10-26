# NEXO — Risk Register (P0)

Escala: Probabilidade (P) e impacto (I) de 1 a 5. Score = P×I.
Mitigação precisa produzir evidência; risco não é encerrado por intenção.

| ID | Risco | P | I | Score | Mitigação / gatilho | Dono | Estado |
| --- | --- | ---: | ---: | ---: | --- | --- | --- |
| R-001 | Unity/SDKs não instalados ou incompatíveis com a máquina. | 4 | 5 | 20 | Fechar D-001/D-002, instalar somente após aprovação, validar build mínima. Gatilho: falha de editor/build. | Engenharia | Aberto |
| R-002 | Intel UHD 630/16 GB não atende metas visuais ou FPS. | 4 | 5 | 20 | Definir dispositivo mínimo, usar perfil de qualidade e benchmark cedo. Gatilho: frame/memória fora da meta. | Engenharia + arte | Aberto |
| R-003 | Escopo educativo cresce além da fatia vertical. | 3 | 4 | 12 | Gate de mudança, P1 sem múltiplas ilhas/multiplayer/economia. Gatilho: requisito sem teste ou dono. | Diretor | Aberto |
| R-004 | Aprendizagem não é mensurável ou confunde tempo com domínio. | 3 | 5 | 15 | Mapa pedagógico, eventos de decisão e rubrica revisada. Gatilho: LR-001 sem instrumento. | Pedagogia | Aberto |
| R-005 | Coleta inadequada de dados de menores. | 2 | 5 | 10 | Offline, minimização, sem texto livre/conta, revisão jurídica antes de online. Gatilho: novo dado ou endpoint. | Segurança + produto | Aberto |
| R-006 | Save corrompido perde progresso infantil. | 3 | 4 | 12 | Escrita atômica/versionada, validação de esquema, backup local limitado e testes de interrupção. | Engenharia | Aberto |
| R-007 | Acessibilidade fica para o fim e exige retrabalho. | 3 | 4 | 12 | AX nos contratos, preset ampliado, legendas e alternativas desde P1. | UX | Aberto |
| R-008 | Dependência de IA ou rede quebra a missão offline. | 2 | 5 | 10 | IA apenas no processo de desenvolvimento; conteúdo e lógica determinísticos no app. | Diretor | Mitigado por ADR-008 |
| R-009 | Assets têm licença incerta ou custo inesperado. | 3 | 4 | 12 | Inventário de origem/licença, placeholders e aprovação antes de incorporar. | Arte | Aberto |
| R-010 | Falta de dispositivos iOS/Android impede validação real. | 4 | 4 | 16 | Confirmar D-002 e reservar dispositivos antes de P6. Gatilho: nenhum dispositivo acessível. | QA | Aberto |
| R-011 | Cooperação é interpretada como multiplayer online e invade escopo. | 3 | 4 | 12 | D-003 define cooperação local/NPC; bloquear chat/matchmaking na fatia. | Produto | Aberto |
| R-012 | APIs de colaboradores têm limites, falhas ou custo não observado. | 3 | 3 | 9 | Cache, ledger, limites e revisão humana; nenhuma API na lógica do jogo. | Diretor | Mitigado |

## Critérios de escalonamento

Qualquer risco com score ≥15 exige plano de mitigação antes do próximo gate.
Qualquer incidente de privacidade, segredo em build ou perda de save é bloqueador
imediato e deve ser registrado em `docs/PROGRESSO.md` e no decision log.
