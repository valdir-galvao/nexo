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

J28 ï¿½ Fase 08: Diagnï¿½stico GameCI/licenciamento ï¿½ Bloqueado. Secrets Unity ausentes (`COUNT=0`), portanto testes/builds falham antes de evidenciar C#/packages. Aguardar configuraï¿½ï¿½o segura de `UNITY_LICENSE`, `UNITY_EMAIL` e `UNITY_PASSWORD`; depois repetir somente `unity-tests.yml`.

J29 ï¿½ Fase 10: correï¿½ï¿½o de `buildsPath` e documentaï¿½ï¿½o Unity Personal ï¿½ Parcial/Bloqueado. Workflows validados; ativaï¿½ï¿½o depende de secrets Personal externos.

J30 ï¿½ Fase 13: licenï¿½a automï¿½tica Personal GameCI ï¿½ Bloqueado por incompatibilidade com o mï¿½todo solicitado. A configuraï¿½ï¿½o suportada continua exigindo `UNITY_LICENSE`.

J31 ï¿½ Fase 14: auditoria de `manual.alf` e workflows ï¿½ Bloqueado. Falta converter `.alf` em `.ulf` e cadastrar secrets no GitHub.

J32 ï¿½ Verificar `Unity_lic.ulf` ï¿½ Localmente concluï¿½do; GitHub bloqueado atï¿½ cadastrar `UNITY_LICENSE`.

J33 ï¿½ Verificaï¿½ï¿½o dos Secrets apï¿½s cadastro informado ï¿½ Bloqueado. GitHub ainda retorna zero secrets no repositï¿½rio `valdir-galvao/nexo`.

J34 ï¿½ Corrigir escopo de credenciais ï¿½ Bloqueado atï¿½ migrar Repository variables para Repository secrets e rotacionar valores expostos.

J35 ï¿½ Confirmar Secrets Unity ï¿½ Concluï¿½do. Trï¿½s secrets presentes; remover variables duplicadas antes da execuï¿½ï¿½o GameCI.

J36 ï¿½ Primeiro GameCI com Secrets ï¿½ Falhou em EditMode/PlayMode; diagnï¿½stico detalhado bloqueado por logs inacessï¿½veis.

J37 ï¿½ Diagnï¿½stico GameCI ï¿½ concluï¿½do: falha era espaï¿½o insuficiente do runner Ubuntu; correï¿½ï¿½o de limpeza adicionada aos jobs Ubuntu.

J38 ï¿½ Correï¿½ï¿½o de assemblies Unity ï¿½ concluï¿½do local/push; nova rodada GameCI em execuï¿½ï¿½o.

J39 ï¿½ Fase ML quantitativa 10 ï¿½ Bloqueada por ausï¿½ncia do pipeline/dados/harness no repositï¿½rio Unity.

- Fase 15 ï¿½ runs 40661478289/40661478460/40661478368: FAIL. Bloqueio comum de compilaï¿½ï¿½o C# (BuildFailedException); licenï¿½a PASS, espaï¿½o PASS. Retornar ï¿½ correï¿½ï¿½o de scripts antes de nova rodada.


- Fase 16: correï¿½ï¿½o CS0246 enviada em 3d9ab9a; Unity tests 40663615864 ainda IN_PROGRESS. Gate para Android/iOS permanece pendente.


- Fase 17: cena explï¿½cita corrigida em 74556d0; Unity tests 40665421997 IN_PROGRESS. Gate de builds pendente.


- Fase 18: Unity tests ainda IN_PROGRESS; iOS FAIL por iOS Version invï¿½lida; Android IN_PROGRESS. Gate nï¿½o aprovado.


- Fase 19: PlayerSettings iOS corrigido e enviado; Unity tests 40667016837 IN_PROGRESS. Gate pendente.


- Fase 20: FAIL. Corrigir acesso/versï¿½o do GameCI CLI e ctiveInputHandler; K-005 ainda NOT_RUN; builds mï¿½veis sem artefatos aprovados.

- Fase 21: Input System headless ajustado (5f7adbb); Unity tests 40814474545 IN_PROGRESS. Gate pendente.

- Fase 22: FAIL. Corrigir inclusï¿½o da cena no perfil/lista de build e resolver CLI 403 antes de novo gate; builds ainda sem artefatos.

- Fase 23: garantia programï¿½tica da cena enviada em 3ecbbe0; Unity tests 40815968590 IN_PROGRESS.

- Fase 24: FAIL. CLI 403 superado; EditMode 16/16 mas integraï¿½ï¿½o Checks falhou; PlayMode 0/1 por active build profile; K-005 pendente; APK nï¿½o confirmado.

- Fase 25: permissï¿½es e injeï¿½ï¿½o de cena enviadas em 196920d; Unity tests 40817889017 IN_PROGRESS.

- Fase 26: FAIL. Resolver active build profile Unity 6 e criar teste/evidï¿½ncia K-005; Android/iOS sem artefatos aprovados.

- Fase 27: Build Profile Unity 6 configurado em d838499; testes/builds IN_PROGRESS.

- Fase 28: FAIL. BuildProfile nï¿½o aplicado ao perfil efetivo do runner; K-005 pendente; Android/iOS sem artefatos.

- Fase 29: configuraï¿½ï¿½o estï¿½tica GameCI/Unity 6 enviada em 930945a; testes/builds IN_PROGRESS.

- Fase 30: IN_PROGRESS; Unity tests e Android ativos, iOS FAIL. Gate pendente.

- Fase 30: FAIL. Cena carregada mas bootstrap nulo no PlayMode; Android ainda ativo e iOS falhou; K-005 pendente.

- Fase 31: FAIL/BLOCKED. Run falhou sem steps/logs apï¿½s push; K-005 e builds nï¿½o validados.

P75 ? 29/10/2026 ? Fase 32: removidos os tr?s steps `actions/cache@v4` que restauravam `Library` nos workflows `unity-tests.yml`, `unity-android.yml` e `unity-ios.yml`, eliminando chaves/restore-keys legadas associadas ao BlobNotFound. A vers?o declarada permanece `6000.3.0f1`. Parser YAML: PASS nos quatro workflows; `python tools/run_static_qa.py`: PASS (`json=5`, `cs=24`); `git diff --check`: PASS. Commit `7e5f4d4` enviado para `orchestrator/phase-a-baseline`. O push disparou runs `40824551082` (tests), `40824551010` (Android) e `40824550999` (iOS), todos encerrados FAIL em ~3?4 s, sem steps executados; logs de job retornam HTTP 404/BlobNotFound ou n?o est?o dispon?veis. N?o h? evid?ncia de Unity, NUnit, K-005, APK ou Xcode. Gate: BLOCKED/FAIL por falha de inicializa??o do runner/logs; repetir ap?s diagnosticar a infraestrutura Actions.

P76 - 29/10/2026 America/Sao_Paulo - Investigacao de causa raiz: workflow minimo `runner-smoke.yml`, sem Unity/GameCI, em `ubuntu-22.04`, tambem falhou em ~3 s com zero steps e `runner_name` vazio (run 40825064094, job 123286108200). Isso confirma falha de provisionamento/capacidade/politica do GitHub-hosted runner antes do primeiro step; cache, Unity, C# e GameCI nao sao a causa primaria. O workflow de smoke foi mantido apenas manual (`workflow_dispatch`) para diagnostico futuro. Fase 32 permanece BLOCKED externamente; nenhuma nova compilacao deve ser repetida ate revisar billing/quota/politica Actions do repositorio.

P77 - 29/10/2026 America/Sao_Paulo - Teste controlado de visibilidade: repositorio alterado para publico, smoke run 40825528099 PASS com runner provisionado; Unity tests 40825567214 e Android 40825569992 iniciaram runners reais, mas ficaram presos no `Free runner disk space` e foram cancelados; iOS 40825572493 iniciou runner, checkout e verificacao de versao, mas falhou no passo GameCI. Repositorio restaurado para privado e confirmado via API. Diagnostico: quota/politica de runners explica zero-step em privado; existe bloqueio adicional no step de limpeza de disco e falha iOS independente. Nenhum APK, NUnit ou K-005 aprovado.

P78 - 29/10/2026 America/Sao_Paulo - Fase 33: removido `jlumbroso/free-disk-space` de unity-tests, unity-android e unity-ios; inserido step shell leve com limpeza tolerante e `df -h`. Todos os workflows YAML passaram no parser; static QA PASS json=5 cs=24; git diff check PASS. Commit local preparado; nao houve push para evitar nova rodada enquanto billing/private runner permanece bloqueado.

P79 - 29/10/2026 America/Sao_Paulo - Fase 34: repositorio ficou publico durante o teste e commit 0ea514d foi enviado. A limpeza shell passou em todos os jobs. Smoke/runner provisionou corretamente. iOS falhou no GameCI; Unity tests 40826804198 e Android 40826804229 chegaram ao GameCI, mas ficaram mais de 8 minutos sem progresso e foram cancelados. Nenhum NUnit/K-005/APK foi confirmado. Repositorio restaurado para privado via API e confirmado.

P80 - 29/10/2026 America/Sao_Paulo - Diagnostico iOS: log do job 123291599782 mostrou `Failed to resolve the latest game-ci CLI release: GitHub API returned 403` em game-ci/unity-builder@v6, antes do Unity. Corrigidos os tres workflows com `cliVersion: v0.1.72` fixo e `gitPrivateToken: ${{ github.token }}`, evitando consulta anonima de release. YAML e static QA PASS; teste remoto pendente.

P81 - 29/10/2026 America/Sao_Paulo - Correcao GameCI: log do iOS confirmou HTTP 403 ao resolver release latest; `cliVersion: v0.1.72` e `gitPrivateToken: ${{ github.token }}` adicionados nos tres workflows. Rodada publica 40827573852/40827573784/40827573928 passou pelo CLI e entrou no Unity/GameCI, demonstrando que o 403 foi eliminado; execucoes ficaram sem progresso por varios minutos e foram canceladas. Repositorio restaurado para privado. Nenhum APK/Xcode/teste formal aprovado.

P82 - 29/10/2026 America/Sao_Paulo - Auditoria profunda dos logs: GameCI CLI fixado funcionou; logs Unity revelaram ULF com machine binding incompat?vel (fallback email/password funcionou), input `buildScenes` invalido no GameCI v6 e ownership root causando Permission denied em metadata apos cancelamento. Removido UNITY_LICENSE dos workflows para usar ativacao por conta, removido buildScenes invalido e adicionado chown seguro antes de metadata. YAML/static QA/git diff check PASS. Commit local; push pendente para nao disparar runner privado bloqueado.


J30 - Fase publica controlada: Unity tests PASS no run 40830355578 (EditMode 16/16, PlayMode 1/1 K005). Android bloqueado por build longa/cancelada no run 40830355608; iOS bloqueado por validacao GameCI pos-export no run 40830355598. Repositorio restaurado privado.


J31 - Otimizacao de builds: cache Unity/Library e nomes de build estaveis configurados localmente; export iOS sem assinatura explicita. Aguardando push e novo teste controlado com repositorio privado.


J32 - Fase 37 encerrada: cache executado; APK gerado no workspace do runner mas nao publicado por falha pos-build; iOS Xcode exportado mas validacao GameCI falhou; PlayMode bloqueado por SIGSEGV. Repositorio privado novamente.

J33 - Fase 38: correcoes de marcador GameCI, Code Coverage headless, cache e upload; validacao local PASS; aguardar push/build controlado.

J34 - Fase 38b: corrigir argumento coverageEnabled incompatï¿½vel e aguardar rodada publica.

J35 - Fase 38c: corrigir exportacao iOS append-only e repetir build.

J36 - Fase 38 concluida tecnicamente: Unity tests, Android APK e iOS Xcode PASS; QA fisico pendente.

J37 - Configuraï¿½ï¿½o de empacotamento IPA sem assinatura no runner macOS concluï¿½da localmente; aguarda execuï¿½ï¿½o remota e download.

J38 - IPA iOS sem assinatura gerado no runner macOS e baixado localmente; gate tï¿½cnico de empacotamento PASS, instalaï¿½ï¿½o fï¿½sica pendente.

J39 - Correcao de shaders URP e escala de UI aplicada localmente; aguarda validacao Unity/GameCI e teste em dispositivo.

J40 - Gate publico PASS: Unity tests, APK Android e IPA iOS empacotado baixados. Assinatura/instalacao iOS pendente; privacidade restaurada.


Fase 39 ? Bootstrap e stripping (conclu?da localmente em 30/10/2026 02:09 -03)
- Bootstrap UI-only durante Intro; f?sica procedural constru?da sob demanda ap?s entrada.
- Managed Stripping Low (1) para Standalone/Android/iPhone; Physics/Physics2D j? declarados.
- Static QA e diff check PASS.
- Depend?ncia: validar compila??o Unity/GameCI e runtime m?vel.


Fase 40 ? Auditoria pr?-compila??o (30/10/2026 02:16 -03)
- EditorBuildSettings: cena principal habilitada no ?ndice 0; cenas de teste ausentes.
- Build Profile Unity 6: n?o verific?vel sem Editor/asset serializado.
- Android: autorrota??o, Min API 29, Target API 35; sem AndroidManifest customizado.
- UI: Canvas runtime defensivo; nenhum script nulo detectado na cena.
- Static QA PASS; compila??o/runtimes pendentes.


Fase 41 ? ciclo p?blico/privado (30/10/2026 02:38 -03)
- Android run 41008010979: PASS; APK e BUILD_METADATA baixados e hash registrado.
- Unity tests 41008011072: EditMode PASS; PlayMode FAIL.
- iOS run 41008010970: IN_PROGRESS no momento da restaura??o de privacidade.
- Reposit?rio restaurado para PRIVATE.


Fase 42 ? UI inicial responsiva (30/10/2026 02:46 -03)
- Main Camera/fundo s?lido garantidos antes do gameplay.
- CanvasScaler, GraphicRaycaster, painel, textos grandes e bot?o funcional implementados.
- Intro usa CanvasGroup para transi??o suave; f?sica continua adiada.
- Static QA PASS; revalida??o PlayMode/Android pendente.


Fase 44 ? game juice e transi??es (30/10/2026 02:53 -03)
- Punch el?stico, estados visuais do bot?o e pulso de t?tulo.
- Part?culas leves em c?mera, limitadas a 28.
- Fade de entrada/sa?da com EaseInOut e limpeza ao iniciar gameplay.
- Static QA PASS; build m?vel pendente.


Fase 44 ? resultado remoto (30/10/2026 02:54 -03)
- Commit 7315dae enviado; static QA local PASS.
- Unity tests/Android/iOS falharam antes dos steps; logs BlobNotFound.
- Nenhum APK novo; privacidade restaurada PRIVATE.


Fase 45 ? gate estrito (30/10/2026 02:58 -03)
- Portas 1 e 2: PASS.
- Porta 3: BLOCKED por 403 `Your repository is disabled` no push.
- Portas 4?6: NOT_RUN; sem build/APK.
- Reposit?rio restaurado PRIVATE.


Fase 46 ? resultado (30/10/2026 03:18 -03)
- Reposit?rio habilitado; push aceito e janela p?blica conclu?da.
- Android 41011579731: PASS, APK baixado e hash registrado.
- Unity tests 41011579727: FAIL; iOS 41011579762 permaneceu em execu??o durante a captura.
- Privacidade restaurada PRIVATE.


Fase 47 ? blindagem gr?fica (30/10/2026 03:25 -03)
- Always Included Shaders: Lit, Unlit, UI/Default e TextMeshPro Distance Field.
- Preload de shaders/material fallback antes de BuildWorld.
- Fade bloqueia intera??o, aguarda frame e constr?i gameplay com materiais v?lidos.
- Static QA PASS; build remota pendente.


Fase 47 ? resultado remoto (30/10/2026 03:44 -03)
- Android 41013991314: PASS, APK baixado e hash registrado.
- iOS 41013991290: PASS.
- Unity tests 41013991357: FAIL; PlayMode ainda bloqueia aprova??o global.
- Reposit?rio restaurado PRIVATE.


Fase 48 ? material Resources (30/10/2026 06:46 -03)
- URP_Default_Gameplay.mat versionado em Assets/Resources/Materials.
- Bootstrap carrega material por Resources e resolve shader se necess?rio.
- PlayMode verifica material/shader.
- Static QA PASS; build remoto NOT_RUN por orienta??o.

P109 | Fase 49 | concluido localmente / remoto bloqueado | Unity/GameCI e canal GitHub autenticado | link.xml, curva de particulas e fallback seguros; QA PASS |

P110 | Fase 49 remoto | parcial | PlayMode FAIL e logs detalhados bloqueados por 401 | Android/iOS PASS; APK preservado; gate global nao aprovado |

P111 | Gate PlayMode | concluido | Unity 6 Build Profile e fonte LegacyRuntime | Run 41044424896 PASS; gate de testes aprovado |

P112 | Release artefatos corrigidos | concluido | Runs GameCI 41044424896/41044424987/41044424886 | APK e IPA capturados e hashes registrados |

P113 | Fase 50 | concluido localmente / remoto bloqueado | reativacao administrativa do GitHub | BuildWorld sem CreatePrimitive; QA PASS |

P114 | Fase 50 release | concluido | runs 41050092394/41050092233/41050092372 | gate e artefatos PASS; hashes registrados |


| P115 | Corrigir fallback magenta no bootstrap e auditar causas adicionais | Conclu?do localmente | Unity/GameCI para confirma??o visual | Unlit-first, luz direcional condicional, resolu??o de material desacoplada de GraphicsSettings; Static QA PASS |
| P117 | Auditoria forense de materiais e shaders nulos | Conclu?do localmente | Unity/GameCI para confirma??o visual | 0 shaders nulos; configura??o e bootstrap auditados; QA PASS |
| P118 | Auditoria preventiva IL2CPP, PlayerSettings, bootstrap e manifesto Android | Conclu?do localmente | Unity/GameCI para confirma??o de build | link.xml ampliado, paisagem/API 29-35, null checks e manifesto m?nimo; QA PASS |
| P119 | Blindagem de FPS, energia, pausa/foco e UI mobile | Conclu?do localmente | Unity/GameCI/dispositivo para medi??o | 60 FPS, NeverSleep, callbacks seguros e scaler auditado; QA PASS |
| P120 | Gate Unity e builds nativas Android/iOS | Conclu?do | GameCI runs 41059686100/41059686028/41059686091 | Testes PASS; APK e IPA baixados e hash verificado; reposit?rio privado |
| P121 | Diagn?stico forense de crash no boot e blindagem de inicializa??o | Conclu?do localmente | Dispositivo Android/logcat | Bootstrap protegido, cena ?ndice 0 confirmada, QA PASS; reprodu??o f?sica pendente |
| P122 | Diagn?stico persistente de erros e crashes no app | Conclu?do localmente | Build e reprodu??o em dispositivo | Log local com stack trace, contexto e aviso visual; QA PASS |

P123 concluida: Android aprovado; iOS bloqueado por capacidade externa do runner macOS. Proxima acao autorizada: nova janela iOS somente apos runner disponivel.

P124 concluida: infraestrutura de emulador pronta; execucao depende de disponibilizar o workflow na branch padrao ou de push controlado.

P125 concluida: smoke integrado, execucao pendente por travamento da compilacao Unity no runner.

P126 concluida: Activity launcher restaurada e abertura Android validada em AVD API 35. Proxima acao: testar fluxo de toque/Inicio da missao no dispositivo real.

| P127 | Gameplay interativo e Game Juice | concluido localmente | Unity/GameCI e dispositivo | porta verde, quiz, score, feedback, pulse/tremor e particulas implementados; QA local PASS; builds NOT_RUN |

| P128 | Progressao multi-fase e recorde local | concluido localmente | Unity/GameCI/dispositivo | tres temas, portais procedurais, PlayerPrefs e HUD implementados; QA PASS; builds NOT_RUN |

| P129 | Audio procedural e polimento | concluido localmente | Unity/GameCI/dispositivo | tons runtime, feedback de eventos e silencio em lifecycle implementados; QA PASS; builds NOT_RUN |

| P130 | Preparacao da release Android | concluido localmente | GameCI/Unity Builder | perfil Release validado e workflow com preflight; APK remoto pendente |

P131 - 31/10/2026 America/Sao_Paulo - Release candidate executada no commit `404947660f932631110cb238dbbdc91b5fb2506b`. Unity Tests `41171249209`: SUCCESS. Android `41171249169`: build Release SUCCESS e smoke test AVD API 35 SUCCESS; APK em `C:/Temp/nexo-release-4049476/android/Android/Nexo.apk`, 31.274.715 bytes, SHA256 `5075FDFFC3FC7F65C24B354EA5B5DF9A655EAA76458D79B899F358875862B312`. iOS `41171249180`: SUCCESS; IPA em `C:/Temp/nexo-release-4049476/ios-xcode3/Nexo.ipa`, SHA256 `879E864EE7404BF95E4DD6F9C5728E9CA78455F8F6561C83F221E2F1C7AAACB0`; projeto Xcode no mesmo diretorio. Repositorio restaurado PRIVATE (`disabled=false`).

P132 - 31/10/2026 America/Sao_Paulo - Fase 59 visual corrigiu Canvas overlay/sorting, EventSystem, StandaloneInputModule, PhysicsRaycaster, toque direto e quiz Canvas com Buttons/Images. Static QA PASS (json=5, cs=25) e git diff --check PASS. Nao houve build nesta correcao. Relatorio: docs/PHASE_59_VISUAL_FIX.md.

P133 | Fase 60 | concluido localmente | Unity/GameCI para validacao visual posterior | ARM64+IL2CPP, APIs 29/35, cena indice 0 e Safe Area auditados; Static QA e diff check PASS | sem build nesta fase; nova Safe Area ainda sem teste runtime | importar/testar no Unity/GameCI em janela controlada

P134 | Fase 61 | preflight concluido | GameCI Android/iOS | Static QA e diff check PASS; release report criado | builds ainda nao executados | commitar e disparar ciclo controlado publico/build/privado

P135 | Fase 61 | bloqueado por dependencia externa | runs 41174760334/41174760342/41174760333 | jobs sem steps e logs vazios; visibilidade restaurada private | nenhum artefato novo | revisar quota/capacidade GitHub Actions antes de repetir

P136 | Fase 61 | bloqueio confirmado apos retentativa | GitHub-hosted runners | attempt 2 falhou antes dos steps em Android/testes/iOS | nenhum artefato novo | usar janela de runner disponivel ou self-hosted autorizado

P137 | Fase 61 | diagnostico parcial confirmado | runs 41174760334/41174760342/41174760333 | Android disco cheio; PlayMode SIGSEGV nativo; EditMode PASS; iOS em andamento | APK nao gerado; IPA pendente | corrigir estrategia de espaco e investigar crash Unity PlayMode

P138 | Fase 61 | parcial | commit e676e59 | cleanup passou; testes PASS; iOS IPA capturado; Android build pendente/cancelado por travamento | sem APK novo | investigar hang do builder Android ou usar runner com mais recursos

P139 | Fase 61 | Android ainda bloqueado | GameCI Unity/IL2CPP | cleanup PASS; iOS e testes PASS; Android sem heartbeat/artefato | APK nao gerado | usar runner com mais recursos ou diagnostico de build Unity com timeout/log persistente


P140 (31/10/2026): Android/iOS GameCI bloqueados por execu??o sem progresso; sem artefatos. EditMode passou, PlayMode falhou. Reposit?rio privado restaurado.

P148 - Fase 68 concluída: correção de câmera, luz direcional e vínculo Canvas/câmera aplicada e validada localmente. Próximo passo: validar em Unity/GameCI quando autorizado.

P149 - Fase 69: auditoria e push concluídos; build Android bloqueada por hang do GameCI/Unity IL2CPP sem artefato. Repositório privado restaurado. Próximo passo: runner com mais capacidade ou diagnóstico de build com timeout/log persistente.

P150 - Fase 70 concluída: infraestrutura CI Android ajustada e validada localmente. Próximo passo: disparar nova build somente em janela autorizada e verificar se IL2CPP conclui.

P151 - Fase 71 concluída: testes e builds Android/iOS aprovados, artefatos baixados e visibilidade privada restaurada. Próximo passo: validação local em dispositivos, sem publicação.

P152 - Fase 72 concluída: EventSystem/InputModule injetados no primeiro ciclo e Canvas de UI validado. Próximo passo: teste Unity/GameCI visual quando autorizado.

P153 - Auditoria visual/multiplataforma e correções locais concluídas. Próximo passo: importar/testar no GameCI, gerar APK/IPA e validar smoke test Android; testes físicos e Appetize continuam dependentes de infraestrutura.

P154 | Fase 73 | PASS tÃ©cnico remoto | commit ce335a6 | Unity Tests 41211856704 SUCCESS; Android 41211856635 SUCCESS + AVD smoke PASS; iOS 41211856610 SUCCESS | APK e IPA baixados e hash verificado | testes fÃ­sicos/Appetize/iOS Simulator pendentes | validar em dispositivos prÃ³prios e revisar splash/tutorial fullscreen do emulador

P155 | Fase 74 | correÃ§Ã£o visual aplicada | commit pendente | material explÃ­cito para partÃ­culas + auditoria `[NEXO_RENDER]`; Static QA/diff PASS | nova execuÃ§Ã£o GameCI ainda nÃ£o realizada | commitar, executar Unity Tests/Android smoke e confirmar `magentaObjects=0`

P156 | Fase 75 | auditoria PASS, correÃ§Ã£o visual ainda nÃ£o comprovada | 54e67ec | EditorBuildSettings Ã­ntegro; pacote TMP/lockfile inconsistente; SIGSEGV histÃ³rico; partÃ­culas corrigidas e auditoria `[NEXO_RENDER]` instalada | Android smoke cancelado antes do build | resolver lockfile no Unity e executar smoke sem gerar distribuiÃ§Ã£o pÃºblica

P157 | Fase 76 | BLOCKED por ferramenta exata ausente | Unity 6000.3.0f1/Package Manager | manifest=36, lock=2; 34 dependÃªncias ausentes confirmadas; sem ediÃ§Ã£o manual | nenhum build iniciado | instalar Unity 6000.3.0f1, resolver lockfile e executar QA antes do smoke Android

| P158 | Fase 77 | BLOCKED por ambiente | Unity 6000.3.0f1 ausente; CLI dry-run reconhece pacote, instalação não registrada | manifest=36, lock=2, TMP 3.0.6 ausente no lockfile; QA/diff PASS; sem builds | instalar exatamente 6000.3.0f1 em local com espaço, resolver Package Manager e executar EditMode/PlayMode |

| P159 | Fase 78 | BLOCKED por armazenamento/local de instalação | P: é NFS e rejeita extração do Unity Installer; C: ~126 MB livres | instalador oficial íntegro, causa comprovada; sem alterações de código/build | disponibilizar SSD/local NTFS com espaço, instalar 6000.3.0f1, resolver Package Manager e testar |

| P160 | Fase 79 | BLOCKED por infraestrutura cloud não configurada | workflows atuais Ubuntu/macOS; sem job/runner Windows validado; limite padrão de disco | YAML/QA/diff PASS; sem dispatch/build/testes Unity | provisionar Windows cloud/self-hosted com disco local suficiente, instalar 6000.3.0f1 + Android modules e executar gate de testes |

| P161 | Fase 80 | planejamento concluído; provisionamento pendente de aprovação | AWS/Azure/GCP comparados; recomendação Windows 8 vCPU/32 GiB/150 GB NTFS | nenhum recurso pago criado; sem runner/testes/builds | aprovar provedor e orçamento, provisionar VM, instalar runner/Unity exato e executar gate Unity |

| P162 | Investigação Free CI | BLOCKED tecnicamente | Ubuntu público gratuito tem 14 GB SSD; imagem Android Unity 6 ~8,15 GB comprimida; tag exata 6000.3.0f1 não comprovada; UNITY_LICENSE ausente nos jobs | workflows preservados, sem builds | disponibilizar imagem/tag exata e licença Personal válida em Secrets, ou aprovar mudança de versão (não autorizada) |

| P163 | Fase 81 | BLOCKED | Linux 6000.3.0f1 oficialmente disponível, mas runner GitHub não executado; tamanho extraído/módulos/licença não medidos na nuvem | static/YAML/diff PASS; sem alteração de workflows/build | executar prova em workflow Ubuntu isolado com espaço/licença, ou registrar bloqueio de quota/segredo |

| P164 | Fase 82 | PARCIAL/BLOCKED | Workflow de capacidade criado e validado; dispatch remoto indisponível | YAML/QA/diff PASS; sem medição real, licença ou instalação | executar manualmente GitHub Free capacity probe, registrar artefato e só então avaliar Teste 2/3 |

P165 | Fase 83 | PASS documental / BLOCKED execuÃ§Ã£o | commit 47f56a7 | infraestrutura original, AVD e histÃ³rico auditados; manifest=36, lock=2; sem Appetize; Unity/CI atual nÃ£o executados | nenhum workflow/build/secret/visibilidade alterado | executar probe autenticado e confirmar Unity 6000.3.0f1 + UNITY_LICENSE antes de restaurar Package Manager/testes | docs/PHASE_83_FORENSIC_AUDIT_ORIGINAL_INFRASTRUCTURE.md

P166 | Fase 84 | PASS anÃ¡lise / BLOCKED recuperaÃ§Ã£o | referÃªncia funcional ce335a6; HEAD abdf2b0 | runs histÃ³ricos 41211856704/56635/56610 SUCCESS; AVD API35 x86_64; produÃ§Ã£o atual sem diff relevante | histÃ³rico GitHub/Secrets atuais nÃ£o verificÃ¡veis sem autenticaÃ§Ã£o; sem builds | executar probe autenticado, confirmar ULF e Unity 6000.3.0f1 antes de qualquer alteraÃ§Ã£o | docs/PHASE_84_PIPELINE_RECOVERY_ANALYSIS.md

P167 | Fase 85 | BLOCKED | commit d66353c | sem gh/API autenticada; runs atuais, quota e Secrets nÃ£o verificÃ¡veis; probe/Unity/testes/builds NOT_RUN | nenhum workflow ou configuraÃ§Ã£o alterado | fornecer sessÃ£o GitHub autenticada e executar probe de capacidade antes do gate Unity | docs/PHASE_85_OPERATIONAL_RECOVERY.md

P168 | Fase 86 | PARTIAL diagnÃ³stico estÃ¡tico | commit d66353c | bootstrap e cena auditados; verde intencional; partÃ­culas corrigidas em 54e67ec; manifest=36/lock=2 | Unity/GameCI/AVD nÃ£o executados; causa runtime definitiva pendente | resolver pacotes no Unity 6000.3.0f1 e executar PlayMode + AVD com `[NEXO_RENDER]` | docs/PHASE_86_APP_RUNTIME_DIAGNOSIS.md

P169 | Fase 87 | BLOCKED | commit 2c5d956 | AVD workflow original auditado; sem adb/emulator/gh/Unity; sem visibilidade ou dispatch | nenhum APK, logcat ou screenshot | executar workflow Android autenticado e reutilizar AVD existente | docs/PHASE_87_ANDROID_EMULATOR_ROOT_CAUSE.md

P170 | Acesso GitHub | PARTIAL | leitura Git via credential manager PASS; API/Actions/browser autenticado ausentes | sem mudanÃ§a de visibilidade, secrets ou workflows | fornecer/ativar integraÃ§Ã£o GitHub autenticada no Orca antes de dispatch | docs/PHASE_87_GITHUB_ACCESS_DIAGNOSTIC.md

P171 | Credenciais locais | PASS diagnÃ³stico / BLOCKED integraÃ§Ã£o GitHub | `.env.txt` ignorado; senha_githup fora do escopo de tools/agents.py; Git credential manager separado | sem leitura/uso de valores e sem API Actions | habilitar conector GitHub autenticado prÃ³prio, sem reutilizar senha local | docs/PHASE_87_ENV_CREDENTIAL_DIAGNOSIS.md

P172 | SeguranÃ§a de credenciais | NOT_RUN | nÃ£o usar senha local como token GitHub | nenhuma API/Actions executada | autenticar por mecanismo GitHub prÃ³prio | docs/PHASE_87_ENV_CREDENTIAL_DIAGNOSIS.md

P173 | GitHub CLI | PARTIAL | CLI instalada; autenticaÃ§Ã£o ainda ausente | `gh auth status`: nÃ£o logado; sem API/Actions | executar login GitHub autorizado em nova sessÃ£o, sem usar senha do `.env.txt` | docs/PHASE_87_ENV_CREDENTIAL_DIAGNOSIS.md

P174 | Rechecagem GitHub | PARTIAL | Git read PASS; gh/API/browser auth FAIL | sem consulta Actions/Secrets/visibilidade | autenticaÃ§Ã£o API prÃ³pria necessÃ¡ria | docs/PHASE_87_GITHUB_ACCESS_DIAGNOSTIC.md

P175 | Fase 88 | BLOCKED gate operacional | branch local 3c7c4cf; remote branch 54e67ec | gh/API nÃ£o autenticada; adb/emulator/Unity ausentes; sem runs/APK | autenticar GitHub no ambiente e executar gate Unity antes do Android AVD | docs/PHASE_88_OPERATIONAL_RECOVERY.md

P176 | Visibilidade pÃºblica | BLOCKED | `gh repo view/edit` exit 4 sem autenticaÃ§Ã£o | repositÃ³rio nÃ£o alterado; teste nÃ£o iniciado | autenticar CLI/API GitHub e repetir consulta antes de mudar visibilidade | docs/PHASE_88_OPERATIONAL_RECOVERY.md

P177 | Login GitHub | PENDING | `gh auth login --web` aguardando confirmaÃ§Ã£o no navegador | sem API/Actions ainda | concluir autorizaÃ§Ã£o interativa e verificar `gh auth status` | docs/PHASE_87_GITHUB_ACCESS_DIAGNOSTIC.md

P178 | GitHub API reauthenticated | IN_PROGRESS | gh auth status/api user/repo view PASS; Unity Tests 41293713412 running | wait and inspect EditMode/PlayMode results before Android gate |

P179 | Gate Unity + Android | PARTIAL | Unity Tests 41293713412 PASS; Android 41294473685 FAIL por timeout em Build Android package; sem APK | sem log final e sem artefato | revisar causa do hang e reduzir/reproduzir o build antes de repetir |

P180 | Diagnostico Android GameCI | PASS parcial | causa de encerramento por timeout confirmada; timeout ajustado 45->60 min; causa interna do atraso nao comprovada | static/diff PASS; novo build ainda nao executado | disparar novo Android somente apos revisao/autorizacao |

P181 | Validacao da correcao de timeout | IN_PROGRESS | Android run 41299986311 em Build Android package; sem artefato ainda | aguardar encerramento real |

P182 (2026-11-01): diagnostico Android visual parcial; aguardar encerramento do run 41299986311 antes de validar screenshot/interacao do APK corrigido.

P183 (2026-11-01): correção do diagnóstico AVD aplicada no smoke test; aguardando APK novo após encerramento do run Android atual.

P184 (2026-11-01): build Android remoto falhou por timeout sem logs/artifact; proxima acao e execucao limpa observavel do GameCI.

P185 (2026-11-01): comparacao historica descarta diferenca de projeto; hang segue ambiental/GameCI. Run 41302431361 pendente.

P186 (2026-11-01): watchdog do build Android preparado; proxima acao e push e monitoramento do run pericial.

P187 (2026-11-01): erro do debug corrigido; causa era arvore Git suja criada pelo proprio watchdog antes do GameCI.

P188 - Run 41305244451: FAIL no Gradle assembleRelease após Unity/IL2CPP concluírem. Watchdog comprovou disco do runner em 100% (505 MB livres). APK não gerado; AVD não executado. Próximo gate: corrigir espaço/cache e repetir o build.

P189 - Watchdog Android ajustado para não capturar argumentos de processo; pronto para próxima execução após limpeza de espaço.

P190 - Workflow Android preparado para build controlado sem cache Unity restaurado e com gate de espaço antes do GameCI. Próximo: push, dispatch e monitoramento.

P191 - Timeout Android ampliado para permitir conclusão do IL2CPP multi-arquitetura; próximo run será monitorado até sucesso ou diagnóstico de espaço.

P192 - Build Android dividido por ABI: ARM64 padrão para APK de dispositivo; X86_64 opcional para AVD. Próximo: push e validar APK ARM64.

P193 - Build Android de homologação ajustado para Mono2x+ARM64 para caber no runner gratuito; próxima execução deve validar APK e depois AVD x86_64 separado.

P194 (2026-11-01): Run 41324298511 falhou por disco cheio do runner antes da coleta; limpeza pre-GameCI ampliada, aguardando novo run Android.

P195 (2026-11-01): Run 41328802090 bloqueado antes do runner por falha de pagamento/limite de gastos; nenhuma compilacao Android executada.

P196 (2026-11-09): diagnóstico visual Android concluído; correção de partículas preservada, configuração URP serializada ausente e validação final pendente de runner/billing.

P197 (2026-11-09): builds ARM64/X86_64 PASS; smoke AVD iniciou Activity Unity, mas evidência visual contaminada por ANR do launcher. Diagnóstico corrigido; repetir X86_64 para screenshot/log render válidos.

P198 (2026-11-09): smoke test agora valida boot + toque no botao + screenshot pos-transicao; aguardar run X86_64.

P199 (2026-11-10): Android AVD PASS para boot, renderização e toque no X86_64; APK ARM64 também compilado. Corrigir divergência de metadata/backend e manter teste ARM64 físico pendente.

P202 - 2026-11-10: HUD gameplay commitada e validada remotamente em build Android X86_64 + AVD (run 42747250853 SUCCESS). Download local de screenshots/logs bloqueado por rate limit HTTP 403; próxima ação é baixar após reset da quota e inspecionar a captura pós-clique.

P203 - 2026-11-11: builds ARM64/X86_64 e AVD smoke concluídos SUCCESS; artefatos baixados e privacidade restaurada. Próximo: teste físico ARM64 e correção da divergência metadata Mono2x versus player IL2CPP.

P204 (2026-11-11): correção local da interação do portal concluída e validada estaticamente; build/AVD pós-correção ainda NOT_RUN.

P205: log API 37 analisado; boot/render PASS, interação pós-toque não observável neste trecho.

P206: geração de artefatos BLOCKED antes do runner por billing/quota GitHub; correção de gameplay permanece não validada em novo APK.

P207: ciclo de visibilidade concluído e restaurado PRIVATE; builds BLOCKED antes do runner por billing/quota, mesmo com repositório público.

P208: bloqueio de billing confirmado no nível da conta; detalhe interno (pagamento, budget ou lock) permanece não exposto pela API autorizada.

P209: gate Unity PASS; APK ARM64 e IPA sem assinatura Apple baixados com hashes; visibilidade restaurada PRIVATE.

P210 | Overhaul gameplay/UI | PASS local | controles móveis e feedback contínuo implementados; build Unity pendente | executar EditMode/PlayMode e smoke Android após janela CI

P211 | Polimento de feedback | PASS local | aura de portal, luz e partículas de gameplay adicionadas | validar em Unity/GameCI

P213 | Build pós-gameplay | PASS | testes, APK ARM64 e IPA concluídos; visibilidade privada restaurada | executar smoke visual no APK quando necessário

P214 | UX guiada | PASS local | avatar, tutorial pós-início e alvo explícito de interação implementados | validar visualmente em novo APK

P215 | Loop de missão completo | PASS local | objetivos obrigatórios, feedback visual e áudio ambiente implementados | executar Unity tests e smoke Android

P216 (2026-11-12): build remoto do gameplay guiado concluído; próxima ação é smoke AVD X86_64 e inspeção visual antes de declarar validação técnica completa.

P217: separar validação visual de build. Próxima ação é executar APK X86_64 com toque automatizado e logs explícitos de transição/interação.

P219: correção de entrada/transição aplicada; próximo passo é executar Unity Tests e smoke X86_64 antes de gerar novos artefatos.

P220: interatividade validada no AVD X86_64 após correção do harness; APKs ARM64/X86_64 e IPA capturados. Próxima etapa opcional: teste físico ARM64.

P221 | Auditoria debug/HAR | DIAGNÃ“STICO | sem crash/shader; transiÃ§Ã£o confirmada, investigaÃ§Ã£o nÃ£o confirmada; UX do guia persistente e toque fora de alvo sÃ£o bloqueios de clareza | implementar fechamento do guia e feedback de interaÃ§Ã£o antes de novo build

P222 | Overhaul visual/gameplay | PASS local | Ã¡gua, paisagem, avatar animado, cÃ¢mera de revelaÃ§Ã£o e guia dispensÃ¡vel implementados | executar Unity Tests e smoke AVD antes de gerar release

P223 | Disparo pÃ³s-overhaul | BLOCKED | GitHub recusou push: repository disabled (HTTP 403); visibilidade restaurada PRIVATE | reativar repositÃ³rio no GitHub antes de novo ciclo

P224 | Visibilidade pÃºblica | PASS | repositÃ³rio confirmado PUBLIC via API; sem build disparado | manter pÃºblico somente durante o ciclo autorizado e restaurar PRIVATE ao concluir

P225 | Build pÃ³s-overhaul | BLOCKED | Unity Tests PASS; Android/iOS repetidamente presos no container GameCI e cancelados apÃ³s exceder timeout | investigar infraestrutura/licenciamento antes de novo dispatch

P226 registrado: Unity Tests 43330830350 foi recusado antes do runner, sem steps/logs. Builds Android/iOS continuam bloqueadas por congelamentos GameCI anteriores.

P227 concluÃ­do: gate Unity PASS e APK/IPA capturados; privacidade restaurada PRIVATE.

P228: diagnÃ³stico visual concluÃ­do; bloqueio Ã© falta de conteÃºdo final, nÃ£o pipeline grÃ¡fico.

P229: camada de apresentaÃ§Ã£o visual implementada e ligada ao bootstrap; prÃ³xima etapa Ã© teste Unity/GameCI e smoke visual.
