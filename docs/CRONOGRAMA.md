# Cronograma por marcos

Atualizado em 26/10/2026, America/Sao_Paulo. Sem datas de entrega prometidas:
estimativas dependem de escopo, plataforma, engine, assets e hardware alvo.

Estados: concluido, parcial, aguardando definicao, planejado, pendente.
Responsavel final de cada marco: gerente. Colaboradores entram sob demanda.

| ID | Marco | Estado | Dependencia | Evidencia de aceite |
| --- | --- | --- | --- | --- |
| P00 | Inspecionar ambiente e credenciais sem expor valores | Concluido | Nenhuma | Diagnostico em AMBIENTE.md |
| P01 | Implementar executor economico e testar protecoes | Concluido | P00 | Executor, configuracao e 9 testes locais aprovados |
| P02 | Validar colaboradores disponiveis | Parcial | P01 | DeepSeek e TypeSafe OK; outros com erros documentados |
| P03 | Preparar documentacao e protocolo de retomada | Concluido | P00–P02 | Relatorio, cronograma, progresso e roteiro consistentes |
| P04 | Produzir contratos P0 do NEXO | Concluido | Briefing NEXO + inventario do ambiente | Seis documentos criados, requisitos rastreaveis e riscos/decisoes revisados |
| P05 | Auditar hardware e fechar baseline tecnico provisório | Concluido | Briefing NEXO | Auditoria, URP/30 FPS baseline, CI iOS e comandos de provisionamento documentados |
| J01 | Definir conceito, genero, plataforma e mecanica central | Concluido para P1 | Briefing + fechamento de escopo | Escopo P1 e ADRs D-003/D-004/D-005 registrados |
| J02 | Selecionar engine e plano visual viavel | Concluido como baseline | Auditoria de hardware + D-001/D-002/D-005 | Unity 6.3 LTS, URP, 3D estilizado e 30 FPS registrados |
| J03 | Instalar/configurar engine e validar projeto minimo | Em execucao remota | J02; secrets Unity para validacao | Scaffold publicado; falta primeiro import/teste GameCI |
| J04 | Construir prototipo da mecanica principal | Planejado | J03 | Ciclo jogavel demonstravel e controles funcionais |
| J05 | Integrar cena representativa, animacao e materiais | Planejado | J04 | Cena jogavel com assets e licencas registrados |
| J06 | Medir e otimizar desempenho e qualidade | Planejado | J05 | Resolucao, FPS e hardware medidos frente a metas definidas |
| J07 | Testar fluxo completo e corrigir defeitos | Planejado | J06 | Criterios do briefing verificados; defeitos restantes descritos |
| J08 | Preparar entrega reproduzivel | Planejado | J07 | Build, instrucoes, versoes e limitacoes documentados |
| J09 | Especificar GDD, narrativa, missao, progressao, UX, dialogos, arte e audio | Concluido | J01/J02 | Oito documentos P1 presentes, regras e criterios implementaveis |
| J10 | Definir arquitetura modular, contratos, persistencia e testes de regras | Concluido parcialmente | J09; Unity Editor para compilacao | Docs, contratos C# e testes EditMode versionados; compilacao GameCI pendente |
| J11 | Implementar gameplay da missão e máquina de estados | Concluido parcialmente | J10; validação Unity | Código, bootstrap de cena e testes versionados; import/build pendente |
| J12 | Integrar direção visual, UI responsiva e áudio configurável | Concluido parcialmente | J11; validação Unity/profiling | Tema, menu/HUD/diário/pausa, áudio e documentação versionados; profiling pendente |
| J13 | Definir pedagogia, avaliação adaptativa e conteúdo versionado | Concluído — entregáveis 100% | J11 | Framework, instrumentos, regras, JSON, validador e testes entregues; validação externa é gate separado |
| J14 | Revisar trust & safety, privacidade e acessibilidade | Concluído — entregáveis 100% | J12/J13 | Threat model, mapa de dados, checklist, política e controles locais entregues; gates de piloto separados |
| J15 | Integrar e executar QA de domínio, fluxo, save e migração | Concluído — QA estática; Unity pendente | J10–J14 | QA report, known issues, testes de integração e scripts locais; execução Unity/dispositivo pendente |
| J16 | Preparar release candidata, manifesto e checklist de distribuição | Concluído — fonte rastreável; builds pendentes | J15 | Versão rc1, notas, checklist, manifesto e limitações publicados |
| O00 | Revalidar gate P0 sob o orquestrador mestre | Concluído | P0 existente | Seis documentos presentes, requisitos únicos, riscos registrados e segredos fora do Git |

Pendencias tecnicas independentes de J01: A01 Mistral HTTP 429;
A02 Gemini HTTP 404; A03 Groq HTTP 403. Nao bloqueiam o briefing.
Retentar somente com motivo documentado, sem apagar ledger ou contornar cache.

Ao iniciar um marco, subdividir em tarefas pequenas com ID (ex.: J04.01), dono,
escopo de arquivos, entrada minima e criterio de aceite. Registrar a subdivisao
aqui antes de distribuir trabalho. Ao concluir cada tarefa, registrar progresso
e atualizar a retomada imediatamente.
J27 — Fase 07 CI — Parcial. Triggers e jobs desbloqueados; execução Unity/GameCI
falhou em todas as modalidades e aguarda logs detalhados para correção.
J26 — Fase 06 push/monitoramento — Bloqueado. Push ocorreu, mas runs remotos
falharam antes dos jobs e dispatch manual retornou 422; revisar permissão e
workflow efetivo antes de repetir.
J25 — Fase 03 CI/GameCI — Parcial. Workflows e script de resolução preparados;
execução Unity, lockfile e testes remotos aguardam runner autorizado.
J24 — Fase 02: blindagem local — Parcial. K-005 e teste PlayMode preparados;
lockfile e execução Unity aguardam Package Manager/GameCI.
J23 — Fase C pré-build — Bloqueado. Manifest/lockfile aguardam Package Manager
Unity; testes Unity e integração K-005 dependem de GameCI.
J22 — Fase B3: primeiro teste Unity GameCI — Bloqueado. Depende de canal
autenticado para `workflow_dispatch` e secrets Unity verificáveis sem exposição;
nenhum runner foi iniciado.

J21 — Fase B2: auditoria GitHub Actions/GameCI — Parcial concluído. YAML,
permissões e gatilhos revisados; Android/iOS ficaram manuais. Depende de secrets
Unity e execução GameCI para validação operacional.

J20 — Fase G: privacidade, segurança infantil e validação pedagógica — Bloqueado.
Depende de uma versão funcional e de revisões técnica, jurídica, pedagógica e
ética; estados e evidências em `docs/PHASE_G_TRUST_SAFETY_REPORT.md`.

J18 — Fase E: builds Android/iOS — Bloqueado. Android e iOS aguardam C/D=`PASS`,
GameCI e, para iOS, runner macOS com Xcode e assinatura autorizada. Relatório:
`docs/PHASE_E_BUILD_REPORT.md`.

J19 — Fase F: QA em dispositivos, desempenho e acessibilidade — Bloqueado.
Depende de builds Android/iOS reais da Fase E e dispositivos ou simuladores;
matriz em `docs/PHASE_F_DEVICE_QA_REPORT.md`.

J17 — Fase D: integração e QA funcional — Bloqueado. Depende de Fase C=`PASS`,
Unity/GameCI e dispositivo ou simulador; matriz e pendências em
`docs/PHASE_D_FUNCTIONAL_REPORT.md`.
