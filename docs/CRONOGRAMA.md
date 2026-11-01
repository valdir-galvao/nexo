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

J28 � Fase 08: Diagn�stico GameCI/licenciamento � Bloqueado. Secrets Unity ausentes (`COUNT=0`), portanto testes/builds falham antes de evidenciar C#/packages. Aguardar configura��o segura de `UNITY_LICENSE`, `UNITY_EMAIL` e `UNITY_PASSWORD`; depois repetir somente `unity-tests.yml`.

J29 � Fase 10: corre��o de `buildsPath` e documenta��o Unity Personal � Parcial/Bloqueado. Workflows validados; ativa��o depende de secrets Personal externos.

J30 � Fase 13: licen�a autom�tica Personal GameCI � Bloqueado por incompatibilidade com o m�todo solicitado. A configura��o suportada continua exigindo `UNITY_LICENSE`.

J31 � Fase 14: auditoria de `manual.alf` e workflows � Bloqueado. Falta converter `.alf` em `.ulf` e cadastrar secrets no GitHub.

J32 � Verificar `Unity_lic.ulf` � Localmente conclu�do; GitHub bloqueado at� cadastrar `UNITY_LICENSE`.

J33 � Verifica��o dos Secrets ap�s cadastro informado � Bloqueado. GitHub ainda retorna zero secrets no reposit�rio `valdir-galvao/nexo`.

J34 � Corrigir escopo de credenciais � Bloqueado at� migrar Repository variables para Repository secrets e rotacionar valores expostos.

J35 � Confirmar Secrets Unity � Conclu�do. Tr�s secrets presentes; remover variables duplicadas antes da execu��o GameCI.

J36 � Primeiro GameCI com Secrets � Falhou em EditMode/PlayMode; diagn�stico detalhado bloqueado por logs inacess�veis.

J37 � Diagn�stico GameCI � conclu�do: falha era espa�o insuficiente do runner Ubuntu; corre��o de limpeza adicionada aos jobs Ubuntu.

J38 � Corre��o de assemblies Unity � conclu�do local/push; nova rodada GameCI em execu��o.

J39 � Fase ML quantitativa 10 � Bloqueada por aus�ncia do pipeline/dados/harness no reposit�rio Unity.

- Fase 15 � runs 40661478289/40661478460/40661478368: FAIL. Bloqueio comum de compila��o C# (BuildFailedException); licen�a PASS, espa�o PASS. Retornar � corre��o de scripts antes de nova rodada.


- Fase 16: corre��o CS0246 enviada em 3d9ab9a; Unity tests 40663615864 ainda IN_PROGRESS. Gate para Android/iOS permanece pendente.


- Fase 17: cena expl�cita corrigida em 74556d0; Unity tests 40665421997 IN_PROGRESS. Gate de builds pendente.


- Fase 18: Unity tests ainda IN_PROGRESS; iOS FAIL por iOS Version inv�lida; Android IN_PROGRESS. Gate n�o aprovado.


- Fase 19: PlayerSettings iOS corrigido e enviado; Unity tests 40667016837 IN_PROGRESS. Gate pendente.


- Fase 20: FAIL. Corrigir acesso/vers�o do GameCI CLI e ctiveInputHandler; K-005 ainda NOT_RUN; builds m�veis sem artefatos aprovados.

- Fase 21: Input System headless ajustado (5f7adbb); Unity tests 40814474545 IN_PROGRESS. Gate pendente.

- Fase 22: FAIL. Corrigir inclus�o da cena no perfil/lista de build e resolver CLI 403 antes de novo gate; builds ainda sem artefatos.

- Fase 23: garantia program�tica da cena enviada em 3ecbbe0; Unity tests 40815968590 IN_PROGRESS.

- Fase 24: FAIL. CLI 403 superado; EditMode 16/16 mas integra��o Checks falhou; PlayMode 0/1 por active build profile; K-005 pendente; APK n�o confirmado.

- Fase 25: permiss�es e inje��o de cena enviadas em 196920d; Unity tests 40817889017 IN_PROGRESS.

- Fase 26: FAIL. Resolver active build profile Unity 6 e criar teste/evid�ncia K-005; Android/iOS sem artefatos aprovados.

- Fase 27: Build Profile Unity 6 configurado em d838499; testes/builds IN_PROGRESS.

- Fase 28: FAIL. BuildProfile n�o aplicado ao perfil efetivo do runner; K-005 pendente; Android/iOS sem artefatos.

- Fase 29: configura��o est�tica GameCI/Unity 6 enviada em 930945a; testes/builds IN_PROGRESS.

- Fase 30: IN_PROGRESS; Unity tests e Android ativos, iOS FAIL. Gate pendente.

- Fase 30: FAIL. Cena carregada mas bootstrap nulo no PlayMode; Android ainda ativo e iOS falhou; K-005 pendente.

- Fase 31: FAIL/BLOCKED. Run falhou sem steps/logs ap�s push; K-005 e builds n�o validados.

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

J34 - Fase 38b: corrigir argumento coverageEnabled incompat�vel e aguardar rodada publica.

J35 - Fase 38c: corrigir exportacao iOS append-only e repetir build.

J36 - Fase 38 concluida tecnicamente: Unity tests, Android APK e iOS Xcode PASS; QA fisico pendente.

J37 - Configura��o de empacotamento IPA sem assinatura no runner macOS conclu�da localmente; aguarda execu��o remota e download.

J38 - IPA iOS sem assinatura gerado no runner macOS e baixado localmente; gate t�cnico de empacotamento PASS, instala��o f�sica pendente.

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

P148 - Fase 68 conclu�da: corre��o de c�mera, luz direcional e v�nculo Canvas/c�mera aplicada e validada localmente. Pr�ximo passo: validar em Unity/GameCI quando autorizado.

P149 - Fase 69: auditoria e push conclu�dos; build Android bloqueada por hang do GameCI/Unity IL2CPP sem artefato. Reposit�rio privado restaurado. Pr�ximo passo: runner com mais capacidade ou diagn�stico de build com timeout/log persistente.

P150 - Fase 70 conclu�da: infraestrutura CI Android ajustada e validada localmente. Pr�ximo passo: disparar nova build somente em janela autorizada e verificar se IL2CPP conclui.

P151 - Fase 71 conclu�da: testes e builds Android/iOS aprovados, artefatos baixados e visibilidade privada restaurada. Pr�ximo passo: valida��o local em dispositivos, sem publica��o.

P152 - Fase 72 conclu�da: EventSystem/InputModule injetados no primeiro ciclo e Canvas de UI validado. Pr�ximo passo: teste Unity/GameCI visual quando autorizado.

P153 - Auditoria visual/multiplataforma e corre��es locais conclu�das. Pr�ximo passo: importar/testar no GameCI, gerar APK/IPA e validar smoke test Android; testes f�sicos e Appetize continuam dependentes de infraestrutura.

P154 | Fase 73 | PASS técnico remoto | commit ce335a6 | Unity Tests 41211856704 SUCCESS; Android 41211856635 SUCCESS + AVD smoke PASS; iOS 41211856610 SUCCESS | APK e IPA baixados e hash verificado | testes físicos/Appetize/iOS Simulator pendentes | validar em dispositivos próprios e revisar splash/tutorial fullscreen do emulador

P155 | Fase 74 | correção visual aplicada | commit pendente | material explícito para partículas + auditoria `[NEXO_RENDER]`; Static QA/diff PASS | nova execução GameCI ainda não realizada | commitar, executar Unity Tests/Android smoke e confirmar `magentaObjects=0`

P156 | Fase 75 | auditoria PASS, correção visual ainda não comprovada | 54e67ec | EditorBuildSettings íntegro; pacote TMP/lockfile inconsistente; SIGSEGV histórico; partículas corrigidas e auditoria `[NEXO_RENDER]` instalada | Android smoke cancelado antes do build | resolver lockfile no Unity e executar smoke sem gerar distribuição pública

P157 | Fase 76 | BLOCKED por ferramenta exata ausente | Unity 6000.3.0f1/Package Manager | manifest=36, lock=2; 34 dependências ausentes confirmadas; sem edição manual | nenhum build iniciado | instalar Unity 6000.3.0f1, resolver lockfile e executar QA antes do smoke Android

| P158 | Fase 77 | BLOCKED por ambiente | Unity 6000.3.0f1 ausente; CLI dry-run reconhece pacote, instala��o n�o registrada | manifest=36, lock=2, TMP 3.0.6 ausente no lockfile; QA/diff PASS; sem builds | instalar exatamente 6000.3.0f1 em local com espa�o, resolver Package Manager e executar EditMode/PlayMode |

| P159 | Fase 78 | BLOCKED por armazenamento/local de instala��o | P: � NFS e rejeita extra��o do Unity Installer; C: ~126 MB livres | instalador oficial �ntegro, causa comprovada; sem altera��es de c�digo/build | disponibilizar SSD/local NTFS com espa�o, instalar 6000.3.0f1, resolver Package Manager e testar |
