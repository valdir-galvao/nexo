# NEXO — Product Requirements (P0)

Cada requisito tem ID estável. “Fatia vertical” significa uma experiência
jogável de ponta a ponta, não um conjunto de telas isoladas.

## Funcionais

| ID | Requisito | Validação |
| --- | --- | --- |
| FR-001 | O jogador inicia a missão sem conexão após o primeiro pacote instalado. | Teste em modo avião: iniciar, jogar e concluir em três execuções limpas. |
| FR-002 | A missão apresenta objetivo, estado atual e próximo passo compreensíveis. | Playtest com 5 jogadores; ≥4 identificam o próximo objetivo sem ajuda. |
| FR-003 | O jogador explora a ilha e encontra pistas do reservatório silencioso. | Teste funcional de navegação e checklist de 3 pistas; nenhuma fica inacessível. |
| FR-004 | O jogador coleta e aloca água em uma decisão com pelo menos duas alternativas. | Teste de estados: cada alternativa altera o estado previsto e possui feedback. |
| FR-005 | O jogador constrói ou repara uma estrutura do reservatório usando recursos locais. | Teste de fluxo: recurso insuficiente, recurso suficiente, conclusão e persistência. |
| FR-006 | A missão oferece feedback explicando causa, efeito e conceito aprendido. | Revisão pedagógica e checklist de feedback em cada decisão crítica. |
| FR-007 | O progresso da missão persiste localmente e pode ser retomado. | Encerrar app em cada etapa; reabrir; estado permanece sem rede. |
| FR-008 | O jogador pode pausar, retomar e reiniciar a missão sem corromper o save. | Teste automatizado e manual em 20 ciclos de pausa/reinício. |
| FR-009 | A experiência funciona sem multiplayer aberto, chat ou economia monetária. | Auditoria de escopo e testes sem endpoints de rede durante a missão. |
| FR-010 | O tutorial ensina controles e objetivo por ação progressiva. | Playtest: ≥80% concluem tutorial sem intervenção do moderador. |

## Pedagógicos

| ID | Requisito | Validação |
| --- | --- | --- |
| LR-001 | A missão mede compreensão de relação entre recurso, prioridade e consequência. | Pré/pós-tarefa curta ou rubrica de decisão; ganho e taxa de acerto registrados localmente. |
| LR-002 | Cada objetivo pedagógico é associado a uma ação observável, não apenas ao tempo jogado. | Matriz pedagógica revisada por especialista e teste de eventos emitidos. |
| LR-003 | O feedback permite tentar novamente sem humilhação ou punição predatória. | Revisão editorial e playtest com cenários de erro. |
| LR-004 | Métricas não são apresentadas como nota pública nem usadas para bloquear progresso. | Inspeção de UX, save e regras de progressão. |

## Acessibilidade

| ID | Requisito | Validação |
| --- | --- | --- |
| AX-001 | Texto, ícones e contraste têm tamanho e contraste configuráveis no mínimo em preset ampliado. | Checklist visual em dispositivo-alvo e teste de escala. |
| AX-002 | Informações essenciais não dependem só de cor, som ou vibração. | Auditoria de cada tutorial, alerta e estado de recurso. |
| AX-003 | Controles têm remapeamento ou alternativas equivalentes e pausa acessível. | Teste manual com configuração alternativa e interrupção em qualquer etapa. |
| AX-004 | Legendas para falas e sons relevantes podem ser ativadas. | Teste com áudio silenciado e checklist de cobertura. |

## Privacidade e segurança infantil

| ID | Requisito | Validação |
| --- | --- | --- |
| CP-001 | A missão principal não exige conta, nome real, localização, contatos ou identificadores persistentes. | Revisão de código, permissões e tráfego em modo avião. |
| CP-002 | Eventos pedagógicos são minimizados, anonimizados localmente e sem texto livre infantil. | Inspeção do esquema de eventos e teste de exportação. |
| CP-003 | Qualquer coleta opcional futura tem consentimento verificável do responsável e política compatível com a jurisdição. | Revisão jurídica antes de habilitar coleta; bloqueio por configuração até aprovação. |
| CP-004 | Não há chat aberto, conteúdo gerado por usuário ou contato entre crianças na fatia vertical. | Auditoria de telas, endpoints, pacotes e permissões. |
| CP-005 | Dados locais podem ser apagados pelo usuário sem exigir rede. | Teste de apagar dados e verificar diretórios de save/cache. |
| SEC-001 | Saves e eventos locais rejeitam esquema inválido e não executam conteúdo externo. | Testes de corrupção, fuzzing de JSON/binário e análise de carregamento. |
| SEC-002 | A aplicação não inclui segredos de servidor ou chaves de IA no pacote mobile. | Varredura do build e revisão de configuração de CI. |

## Qualidade e desempenho

| ID | Requisito | Validação |
| --- | --- | --- |
| NFR-001 | A missão completa roda com rede desligada após instalação. | Teste em modo avião nos dispositivos-alvo. |
| NFR-002 | O app não perde progresso após suspensão, retomada ou encerramento normal. | Matriz de ciclo de vida em Android e iOS. |
| NFR-003 | Metas de FPS, memória, tamanho e bateria são definidas após os dispositivos-alvo serem escolhidos. | Benchmark da cena vertical e registro dos limites aprovados em decisão. |
| NFR-004 | Falhas não expõem dados pessoais nem bloqueiam permanentemente a missão. | Testes de erro, logs redigidos e recuperação de save. |
