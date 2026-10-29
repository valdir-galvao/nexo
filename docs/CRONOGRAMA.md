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
| P03 | Preparar documentacao e protocolo de retomada | Concluido | P00â€“P02 | Relatorio, cronograma, progresso e roteiro consistentes |
| P04 | Produzir contratos P0 do NEXO | Concluido | Briefing NEXO + inventario do ambiente | Seis documentos criados, requisitos rastreaveis e riscos/decisoes revisados |
| P05 | Auditar hardware e fechar baseline tecnico provisÃ³rio | Concluido | Briefing NEXO | Auditoria, URP/30 FPS baseline, CI iOS e comandos de provisionamento documentados |
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
| J11 | Implementar gameplay da missÃ£o e mÃ¡quina de estados | Concluido parcialmente | J10; validaÃ§Ã£o Unity | CÃ³digo, bootstrap de cena e testes versionados; import/build pendente |
| J12 | Integrar direÃ§Ã£o visual, UI responsiva e Ã¡udio configurÃ¡vel | Concluido parcialmente | J11; validaÃ§Ã£o Unity/profiling | Tema, menu/HUD/diÃ¡rio/pausa, Ã¡udio e documentaÃ§Ã£o versionados; profiling pendente |
| J13 | Definir pedagogia, avaliaÃ§Ã£o adaptativa e conteÃºdo versionado | ConcluÃ­do â€” entregÃ¡veis 100% | J11 | Framework, instrumentos, regras, JSON, validador e testes entregues; validaÃ§Ã£o externa Ã© gate separado |
| J14 | Revisar trust & safety, privacidade e acessibilidade | ConcluÃ­do â€” entregÃ¡veis 100% | J12/J13 | Threat model, mapa de dados, checklist, polÃ­tica e controles locais entregues; gates de piloto separados |
| J15 | Integrar e executar QA de domÃ­nio, fluxo, save e migraÃ§Ã£o | ConcluÃ­do â€” QA estÃ¡tica; Unity pendente | J10â€“J14 | QA report, known issues, testes de integraÃ§Ã£o e scripts locais; execuÃ§Ã£o Unity/dispositivo pendente |
| J16 | Preparar release candidata, manifesto e checklist de distribuiÃ§Ã£o | ConcluÃ­do â€” fonte rastreÃ¡vel; builds pendentes | J15 | VersÃ£o rc1, notas, checklist, manifesto e limitaÃ§Ãµes publicados |
| O00 | Revalidar gate P0 sob o orquestrador mestre | ConcluÃ­do | P0 existente | Seis documentos presentes, requisitos Ãºnicos, riscos registrados e segredos fora do Git |

Pendencias tecnicas independentes de J01: A01 Mistral HTTP 429;
A02 Gemini HTTP 404; A03 Groq HTTP 403. Nao bloqueiam o briefing.
Retentar somente com motivo documentado, sem apagar ledger ou contornar cache.

Ao iniciar um marco, subdividir em tarefas pequenas com ID (ex.: J04.01), dono,
escopo de arquivos, entrada minima e criterio de aceite. Registrar a subdivisao
aqui antes de distribuir trabalho. Ao concluir cada tarefa, registrar progresso
e atualizar a retomada imediatamente.
J27 â€” Fase 07 CI â€” Parcial. Triggers e jobs desbloqueados; execuÃ§Ã£o Unity/GameCI
falhou em todas as modalidades e aguarda logs detalhados para correÃ§Ã£o.
J26 â€” Fase 06 push/monitoramento â€” Bloqueado. Push ocorreu, mas runs remotos
falharam antes dos jobs e dispatch manual retornou 422; revisar permissÃ£o e
workflow efetivo antes de repetir.
J25 â€” Fase 03 CI/GameCI â€” Parcial. Workflows e script de resoluÃ§Ã£o preparados;
execuÃ§Ã£o Unity, lockfile e testes remotos aguardam runner autorizado.
J24 â€” Fase 02: blindagem local â€” Parcial. K-005 e teste PlayMode preparados;
lockfile e execuÃ§Ã£o Unity aguardam Package Manager/GameCI.
J23 â€” Fase C prÃ©-build â€” Bloqueado. Manifest/lockfile aguardam Package Manager
Unity; testes Unity e integraÃ§Ã£o K-005 dependem de GameCI.
J22 â€” Fase B3: primeiro teste Unity GameCI â€” Bloqueado. Depende de canal
autenticado para `workflow_dispatch` e secrets Unity verificÃ¡veis sem exposiÃ§Ã£o;
nenhum runner foi iniciado.

J21 â€” Fase B2: auditoria GitHub Actions/GameCI â€” Parcial concluÃ­do. YAML,
permissÃµes e gatilhos revisados; Android/iOS ficaram manuais. Depende de secrets
Unity e execuÃ§Ã£o GameCI para validaÃ§Ã£o operacional.

J20 â€” Fase G: privacidade, seguranÃ§a infantil e validaÃ§Ã£o pedagÃ³gica â€” Bloqueado.
Depende de uma versÃ£o funcional e de revisÃµes tÃ©cnica, jurÃ­dica, pedagÃ³gica e
Ã©tica; estados e evidÃªncias em `docs/PHASE_G_TRUST_SAFETY_REPORT.md`.

J18 â€” Fase E: builds Android/iOS â€” Bloqueado. Android e iOS aguardam C/D=`PASS`,
GameCI e, para iOS, runner macOS com Xcode e assinatura autorizada. RelatÃ³rio:
`docs/PHASE_E_BUILD_REPORT.md`.

J19 â€” Fase F: QA em dispositivos, desempenho e acessibilidade â€” Bloqueado.
Depende de builds Android/iOS reais da Fase E e dispositivos ou simuladores;
matriz em `docs/PHASE_F_DEVICE_QA_REPORT.md`.

J17 â€” Fase D: integraÃ§Ã£o e QA funcional â€” Bloqueado. Depende de Fase C=`PASS`,
Unity/GameCI e dispositivo ou simulador; matriz e pendÃªncias em
`docs/PHASE_D_FUNCTIONAL_REPORT.md`.

J28 — Fase 08: Diagnóstico GameCI/licenciamento — Bloqueado. Secrets Unity ausentes (`COUNT=0`), portanto testes/builds falham antes de evidenciar C#/packages. Aguardar configuração segura de `UNITY_LICENSE`, `UNITY_EMAIL` e `UNITY_PASSWORD`; depois repetir somente `unity-tests.yml`.

J29 — Fase 10: correção de `buildsPath` e documentação Unity Personal — Parcial/Bloqueado. Workflows validados; ativação depende de secrets Personal externos.

J30 — Fase 13: licença automática Personal GameCI — Bloqueado por incompatibilidade com o método solicitado. A configuração suportada continua exigindo `UNITY_LICENSE`.

J31 — Fase 14: auditoria de `manual.alf` e workflows — Bloqueado. Falta converter `.alf` em `.ulf` e cadastrar secrets no GitHub.

J32 — Verificar `Unity_lic.ulf` — Localmente concluído; GitHub bloqueado até cadastrar `UNITY_LICENSE`.

J33 — Verificação dos Secrets após cadastro informado — Bloqueado. GitHub ainda retorna zero secrets no repositório `valdir-galvao/nexo`.

J34 — Corrigir escopo de credenciais — Bloqueado até migrar Repository variables para Repository secrets e rotacionar valores expostos.

J35 — Confirmar Secrets Unity — Concluído. Três secrets presentes; remover variables duplicadas antes da execução GameCI.

J36 — Primeiro GameCI com Secrets — Falhou em EditMode/PlayMode; diagnóstico detalhado bloqueado por logs inacessíveis.

J37 — Diagnóstico GameCI — concluído: falha era espaço insuficiente do runner Ubuntu; correção de limpeza adicionada aos jobs Ubuntu.

J38 — Correção de assemblies Unity — concluído local/push; nova rodada GameCI em execução.

J39 — Fase ML quantitativa 10 — Bloqueada por ausência do pipeline/dados/harness no repositório Unity.

- Fase 15 — runs 40661478289/40661478460/40661478368: FAIL. Bloqueio comum de compilação C# (BuildFailedException); licença PASS, espaço PASS. Retornar à correção de scripts antes de nova rodada.


- Fase 16: correção CS0246 enviada em 3d9ab9a; Unity tests 40663615864 ainda IN_PROGRESS. Gate para Android/iOS permanece pendente.


- Fase 17: cena explícita corrigida em 74556d0; Unity tests 40665421997 IN_PROGRESS. Gate de builds pendente.


- Fase 18: Unity tests ainda IN_PROGRESS; iOS FAIL por iOS Version inválida; Android IN_PROGRESS. Gate não aprovado.


- Fase 19: PlayerSettings iOS corrigido e enviado; Unity tests 40667016837 IN_PROGRESS. Gate pendente.


- Fase 20: FAIL. Corrigir acesso/versão do GameCI CLI e ctiveInputHandler; K-005 ainda NOT_RUN; builds móveis sem artefatos aprovados.

- Fase 21: Input System headless ajustado (5f7adbb); Unity tests 40814474545 IN_PROGRESS. Gate pendente.

- Fase 22: FAIL. Corrigir inclusão da cena no perfil/lista de build e resolver CLI 403 antes de novo gate; builds ainda sem artefatos.

- Fase 23: garantia programática da cena enviada em 3ecbbe0; Unity tests 40815968590 IN_PROGRESS.
