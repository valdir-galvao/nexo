# Relatorio do projeto jogo

Atualizacao: 26/10/2026, America/Sao_Paulo. Responsavel: diretor de projeto.

## Objetivo e escopo atual

Desenvolver um jogo dinamico, animado e com direcao grafica estilizada de alta
qualidade, ajustada ao hardware disponivel,
com colaboradores de IA acionados conforme a tarefa e com economia de contexto
e requisicoes. O usuario solicitou preparar a equipe e o ambiente e aguardar
sua instrucao para iniciar o jogo. Solicitou tambem documentacao continua para
retomada independente da sessao.

O briefing NEXO define plataforma Android/iOS, genero, publico, tema e primeira
missao. O escopo P1 e o baseline tecnico foram fechados nesta sessao; a
implementacao funcional e a build ainda aguardam validacao no GameCI.
O executor de APIs esta preparado; os contratos P0 do NEXO foram criados; o ambiente de execucao grafica ainda nao.

## Entregas existentes

| Entrega | Evidencia | Situacao |
| --- | --- | --- |
| Executor HTTP sem dependencias externas | `tools/agents.py` | Implementado |
| Papeis e limites | `agents.json`, `AGENTS.md` | Configurados |
| Protecao contra versionamento de credenciais | `.gitignore` | Configurada |
| Cache, contador, bloqueio de concorrencia e repeticao apos falha | Executor e testes | Verificados por testes locais |
| Diagnostico do ambiente e APIs | `docs/AMBIENTE.md` | Registrado |
| Testes do executor | `tools/test_agents.py` | Ultima execucao registrada: 9 passaram |
| Roteiro de continuidade | `RETOMADA.md`, `CRONOGRAMA.md`, `PROGRESSO.md` | Criado |
| Contratos P0 do NEXO | `PROJECT_CHARTER.md`, `PRODUCT_REQUIREMENTS.md`, `ACCEPTANCE_CRITERIA.md`, `DECISION_LOG.md`, `IMPLEMENTATION_ROADMAP.md`, `RISK_REGISTER.md` | Concluido |
| Auditoria e configura√ß√£o NEXO | `NEXO_ENVIRONMENT_AUDIT.md`, `NEXO_CONFIGURATION_REPORT.md`, `.gitattributes` | Concluido como baseline |
| Esteira CI/CD GameCI | `.github/workflows/unity-*.yml`, `CICD_REPORT.md` | Estruturada; n√£o executada |
| Verifica√ß√£o de GitHub | API e `git push` | `valdir-galvao/nexo` privado, Actions habilitado, branch `main` sincronizada |
| CI/CD GameCI | Workflows remotos | Quatro workflows publicados; secrets Unity ainda vazios |

## Equipe e disponibilidade observada

| Responsavel | Uso previsto | Ultima evidencia |
| --- | --- | --- |
| Gerente | Arquitetura, tarefas, integracao e verificacao | Agente principal da sessao |
| DeepSeek / deepseek-flash | Propostas de codigo e algoritmos delimitados | Inferencia minima OK, 87 tokens totais |
| TypeSafe / jev-latest | Julgamentos estruturados, relevancia de contexto | Noul valido; 284 tokens entrada, 20 saida |
| Mistral / mistral-small-latest | Sintese de contexto e requisitos | Chave atualizada aceita; inferencia HTTP 429 |
| Gemini / gemini-2.5-flash-lite | Especificacoes visuais | Catalogo OK; inferencia HTTP 404 |
| Groq | Revisao localizada e testes | Catalogo HTTP 403; inferencia nao testada |

Disponibilidade e historica, nao garantia futura. Testes minimos validam acesso,
nao competencia em tarefas reais. Colaboradores HTTP nao editam arquivos ou
executam comandos autonomamente. O gerente revisa, aplica e testa as propostas.
Nao existem workers pagos persistentes iniciados por esta preparacao.

## Ambiente e limitacoes

Windows; Python 3.12, Node/npm, Git; Orca 1.4.224 observado conectado.
6 processadores logicos, aproximadamente 16 GB de RAM, Intel UHD Graphics 630.
Engine nao localizada no PATH ou nos caminhos padrao inspecionados; nenhuma
instalacao foi feita. A meta ultrarrealista exige definir a maquina alvo e medir
uma cena real; desempenho local ainda nao foi validado.

## Decisoes registradas

- Acionamento sob demanda, com um dono por tarefa e contexto minimo.
- 12 tentativas de geracao/dia UTC no total; 16000 caracteres de entrada;
  1200 tokens de saida para chat. TypeSafe: 1 a 4 perguntas Noul por tarefa.
- Cache local; sem retry ou troca automatica para modelo mais caro.
- Limites atuais nao constituem teto monetario. Valores gastos nao foram apurados.
- Nenhum segredo em documentacao ou Git. `.env` tem precedencia sobre `.env.txt`.
- Nomes locais corrigidos para `MISTRAL_API_KEY` e `TYPESAFE_API_KEY`.
- Nao reinstalar nem retestar servicos por rotina de retomada.
- Cronograma por marcos; datas e estimativas somente apos definir escopo e engine.

## Pendencias

Prioridade: receber o conceito do jogo e plataforma, depois escolher e validar
a engine. Falhas de provedores sao pendencias paralelas; DeepSeek e TypeSafe
permitem seguir com o planejamento sem depender de todos os servicos.

Mistral: diagnosticar limites/cota/plano antes de nova inferencia; causa especifica
do HTTP 429 ainda nao identificada. Gemini: investigar HTTP 404. Groq: investigar
HTTP 403. Nao atribuir automaticamente essas falhas a saldo ou chave invalida.

O escopo P1 foi autorizado e as premissas D-001 a D-009 estao registradas no
Decision Log. O bloqueio restante e operacional: cadastrar os secrets Unity e
executar o primeiro workflow. Isso valida import, testes e build; nao altera o
escopo aprovado.

O pacote de design P1 foi concluido em oito documentos. Ele cobre o prologo de
90 segundos, a missao completa, estados, puzzles, dialogos, progressao por
competencia, UX, direcao visual e audio. Nenhum codigo de gameplay foi escrito.

P2 adicionou a arquitetura modular e contratos de dominio em `Assets/NEXO`.
As regras criticas e validacao de save possuem testes EditMode versionados; a
compilacao nao foi executada localmente porque Unity, dotnet, adb e xcodebuild
nao estao disponiveis neste Windows.

P3 adicionou a m√°quina de estados, regras da miss√£o, save JSON at√¥mico, bootstrap
de ilha tempor√°ria, controles de teste, intera√ß√£o com tr√™s pistas, puzzle da
v√°lvula, feedback, conclus√£o e desbloqueio narrativo. A implementa√ß√£o √© parcial
at√© a importa√ß√£o no Unity e o teste em dispositivo.

P4 integrou a paleta compartilhada, menu inicial, HUD, di√°rio, pausa,
configura√ß√µes de √°udio e tons procedurais leves. O greybox continua deliberadamente
tempor√°rio at√© c√¢mera e desempenho serem validados; nenhum pacote de arte pesado
foi adicionado.

P5 est√° 100% conclu√≠do quanto aos entreg√°veis: framework de compet√™ncias,
diagn√≥stico, p√≥s-teste, transfer√™ncia, revis√µes configur√°veis, regras adaptativas
e conte√∫do versionado da miss√£o. O modelo n√£o trata pausa, ajuda ou dificuldade de
compreens√£o como incapacidade. Revis√£o por pessoa qualificada e evid√™ncia
experimental s√£o gates externos de publica√ß√£o, n√£o itens faltantes do pacote P5.

P6 est√° 100% conclu√≠do quanto aos entreg√°veis de revis√£o: threat model, mapa de
dados, checklist de acessibilidade, pol√≠tica de aceite e controles locais de
exclus√£o/exporta√ß√£o. O projeto n√£o declara conformidade jur√≠dica certificada;
auditoria de pacote, revis√£o jur√≠dica/√©tica e testes f√≠sicos s√£o gates de piloto.

P7 adicionou testes de fluxo completo, retomada ap√≥s reabertura, migra√ß√£o de save
V0, pol√≠tica offline, `QA_REPORT.md`, `KNOWN_ISSUES.md` e `run_static_qa.py`.
A integra√ß√£o est√° verificada estaticamente; o Editor Unity e dispositivos n√£o
est√£o dispon√≠veis para executar EditMode/PlayMode, builds ou profiling.

P8 preparou a release candidata `0.1.0-rc1`: versionamento Unity/Android/iOS,
notas de release, checklist interno, manifesto SHA256 de fonte e instru√ß√µes de
reprodu√ß√£o. Nenhum APK/Xcode foi inventado; os artefatos dependem do GameCI.

## Persistencia

Documentos e codigo ficam nesta pasta e sobrevivem a queda da sessao, desde que
o diretorio seja preservado. Isso nao constitui backup externo nem commit Git.
Cache e ledger locais em `.agents-local/` sao ignorados pelo Git: preserva-los
em retomadas na mesma maquina. Ao migrar, manter credenciais por canal seguro
e restaurar registros de uso sem publica-los. Sem ledger, o consumo anterior
e incerto: nao repetir tarefas pagas ate reconciliar o estado.


Estado operacional 29/10/2026: Unity tests PASS no run 40830355578 (16/16 EditMode, 1/1 PlayMode K005). Android e iOS ainda nao sao builds aprovados: Android 40830355608 foi cancelado sem APK; iOS 40830355598 gerou Xcode, mas falhou na validacao GameCI. Repositorio restaurado privado.


Atualizacao P86: builds Android/iOS ajustados localmente com cache actions/cache v4 e buildName estavel; export iOS configurado sem assinatura via AcceptExternalModificationsToPlayer. Validacoes locais PASS; push e execucao remota ainda pendentes.


Atualizacao P87: janela publica da Fase 37 concluida e privacidade restaurada. Cache Android executou, mas o APK de 26 MB permaneceu apenas no workspace do runner porque GameCI falhou na validacao pos-build; iOS teve o mesmo padrao apos exportar Xcode. PlayMode teve SIGSEGV nativo durante recompilacao. Nenhum build movel e considerado aprovado.

Atualizacao P88: causa raiz confirmada no contrato de validacao GameCI e no crash de Code Coverage Unity 6; correcoes locais aplicadas e static QA PASS. Nenhuma build nova declarada antes de execucao remota.

Atualizacao P89: CLI rejeitou noCoverageEnabled antes do Unity; input removido e causa documentada.

Atualizacao P90: causa iOS confirmada como BuildOptions.AcceptExternalModificationsToPlayer em projeto inexistente; alterado para BuildOptions.None.

Atualizacao P91: candidato tecnico aprovado para testes privados; APK e Xcode preservados localmente, repositorio privado.

AtualizaÔøΩÔøΩo P92: o workflow iOS foi ampliado para gerar um arquivo Nexo.ipa sem assinatura apÔøΩs a exportaÔøΩÔøΩo Xcode. O artefato sÔøΩ comprovarÔøΩ empacotamento; instalaÔøΩÔøΩo exige assinatura Apple posterior.

AtualizaÔøΩÔøΩo P93: run 40852640118 PASS. O workflow agora gera Nexo.ipa sem assinatura via xcodebuild e o artefato foi baixado em C:/Temp/nexo-ios-ipa-40852640118/Nexo.ipa (SHA256 2c92748b8807a6a7fbfc72869fdb0c5135a8049314f7dde000985a1d7fec758c). RepositÔøΩrio restaurado privado. A assinatura Apple nÔøΩo foi realizada.

Atualizacao P94: como GraphicsSettings.asset nao existia no projeto, a inclusao de shaders foi implementada por editor bootstrap versionado, evitando GUIDs inventados. O CanvasScaler e configurado no bootstrap da cena e a intro recebeu fade sincrono. Static QA PASS; validacao Unity e dispositivo pendente.

Atualizacao P95: os runs automaticos privados do commit beafb4b falharam sem executar steps, portanto a implementacao mobile ainda nao tem evidencia Unity/GameCI. Static QA continua PASS; o bloqueio e infraestrutura de runner.

Atualizacao P96: a primeira rodada publica da Fase 38 falhou por CS0117 em EnsureIncludedShaders; a correÔøΩÔøΩo agora usa SerializedObject na propriedade m_AlwaysIncludedShaders, sem API inexistente. Android publicou apenas metadata/diagnostico, sem APK.

AtualizaÔøΩÔøΩo P97: rodada publica 41003465778/41003469023/41003471413 PASS. APK e IPA sem assinatura foram baixados e hashes registrados. RepositÔøΩrio restaurado privado; nenhuma publicacao.


Atualiza??o Fase 39 (30/10/2026 02:09 -03): corrigido o risco de inicializa??o f?sica no bootstrap. `BuildWorld()` n?o ? mais executado durante a tela de Intro; a constru??o com `CreatePrimitive` e `CharacterController` ocorre somente ao iniciar a miss?o, mantendo as intera??es funcionais. O stripping foi definido como Low em todas as plataformas. O pacote `com.unity.modules.physics` j? estava declarado no manifest. Static QA PASS; execu??o Unity/GameCI ainda depende de runner externo.


Atualiza??o Fase 40 (30/10/2026 02:16 -03): auditoria preventiva confirmou a cena principal no ?ndice 0 do EditorBuildSettings, aus?ncia de cenas de teste na build principal e configura??o Android compat?vel com Unity 6 (autorrota??o, API 29/35). N?o existe AndroidManifest customizado nem Build Profile Unity 6 serializado no reposit?rio; esses pontos exigem confirma??o no Unity Editor/GameCI. A UI ? criada pelo bootstrap com CanvasScaler e GraphicRaycaster protegidos contra refer?ncias nulas. Static QA PASS.


Atualiza??o Fase 41 (30/10/2026 02:38 -03): ciclo p?blico autorizado executado. A build Android foi aprovada e o APK foi extra?do localmente; o artefato est? vinculado ao commit c11667a e ao run 41008010979. O gate de testes n?o foi aprovado porque PlayMode falhou no run 41008011072, embora EditMode tenha passado. O iOS ainda n?o tinha encerrado quando o reposit?rio foi restaurado para privado.


Atualiza??o Fase 42 (30/10/2026 02:46 -03): a tela inicial foi reconstru?da no Canvas do bootstrap. O caminho de apresenta??o agora n?o depende de uma c?mera pr?-existente nem de textos OnGUI pequenos; utiliza fundo s?lido, escalonamento por resolu??o, tipografia leg?vel e a??o de in?cio conectada ao fluxo da miss?o. O modo batchmode n?o cria EventSystem, reduzindo risco para testes headless.


Atualiza??o Fase 44 (30/10/2026 02:53 -03): a tela inicial recebeu anima??es e feedback visual nativos, sem depend?ncias externas pesadas. O sistema mant?m o caminho batchmode sem EventSystem e interrompe part?culas ao entrar na miss?o. A valida??o local passou; a confirma??o final depende do GameCI.


Atualiza??o Fase 44 (30/10/2026 02:54 -03): a implementa??o de game juice est? versionada e passou a valida??o est?tica, mas a rodada p?blica n?o chegou ao GameCI devido ? falha de provisionamento/logs `BlobNotFound`. N?o h? APK novo para baixar; o reposit?rio foi restaurado para privado.


Atualiza??o Fase 45 (30/10/2026 02:58 -03): a valida??o local e a presen?a do commit de game juice passaram. O GitHub rejeitou o push por o reposit?rio estar desabilitado, impedindo a cria??o dos runs. O ciclo foi interrompido conforme o gate estrito e a visibilidade foi restaurada para privada.


Atualiza??o Fase 46 (30/10/2026 03:18 -03): o APK com game juice foi compilado pelo GameCI e baixado localmente. O artefato est? vinculado ao commit 1cbe67b e ao run 41011579731. O gate Unity tests n?o foi aprovado porque o run 41011579727 falhou; a visibilidade foi restaurada para privada ap?s a captura.


Atualiza??o Fase 47 (30/10/2026 03:25 -03): corrigida a regress?o estrutural de materiais no bootstrap. A prepara??o gr?fica agora ocorre antes da cria??o procedural, e a transi??o s? libera gameplay depois do fade e do prewarm. A lista de shaders essenciais continua sendo atualizada via SerializedObject do GraphicsSettings, sem GUIDs inventados.


Atualiza??o Fase 47 (30/10/2026 03:44 -03): a blindagem URP e a transi??o segura foram compiladas pelo GameCI. O APK foi obtido e o iOS tamb?m concluiu; o gate de testes Unity falhou separadamente, portanto o artefato m?vel n?o representa aprova??o completa de PlayMode.


Atualiza??o Fase 48 (30/10/2026 06:46 -03): material URP expl?cito e teste PlayMode foram adicionados. A etapa de compila??o/download foi adiada conforme orienta??o; n?o h? novo APK nesta fase.

Atualizacao Fase 49 (30/10/2026): blindagem IL2CPP e particulas aplicada localmente. O link.xml preserva modulos de fisica/UI; curvas x/y/z usam TwoConstants; primitivas e CharacterController tem fallback seguro. Static QA PASS. Nao houve build, APK ou alteracao de visibilidade; gh nao instalado e Unity ausente.

Atualizacao Fase 49 remoto (30/10/2026): link.xml/particulas/fallback foram compilados. Android 41036558774 PASS e APK baixado em C:/Temp/nexo-android-41036558774/Android/Nexo.apk; iOS 41036558638 PASS. Unity tests 41036558837 FAIL no PlayMode; EditMode passou. O repositorio foi restaurado PRIVATE. A causa detalhada do PlayMode exige token com leitura de logs/artefatos.

Atualizacao PlayMode (30/10/2026): logs do runner confirmaram cena ausente no active Build Profile e uso invalido de Arial.ttf. O teste agora registra o perfil quando possivel e evita SceneManager quando nao confirmado, usando bootstrap controlado; UI usa LegacyRuntime.ttf. Run 41044424896 PASS. Repositorio restaurado PRIVATE.

Atualizacao final (30/10/2026): commit 663e32e passou Unity Tests e gerou Android/iOS. APK e IPA foram baixados e validados por hash; repositorio permanece privado.

Atualizacao Fase 50: gameplay usa malha cubica visual e BoxCollider simples apenas em interativos; avatar por Transform. Nenhuma chamada CreatePrimitive permanece no bootstrap. Static QA PASS. Build remoto nao executado porque GitHub recusou push com 403 de repositorio disabled; privacidade PRIVATE restaurada.

Atualizacao final Fase 50: BuildWorld sem CreatePrimitive gerou Android e iOS com gate Unity PASS. APK/IPA baixados e verificados; repositorio privado.


## Atualiza??o Fase 51

A tela rosa ap?s ?Iniciar Miss?o? tinha dois riscos estruturais al?m da ilumina??o: o material em `Resources` estava serializado sem shader (`fileID: 0`) e n?o havia `ProjectSettings/GraphicsSettings.asset` rastreado para garantir inclus?o de shaders. O bootstrap foi endurecido para usar URP Unlit primeiro, com fallbacks nativos, e para criar luz direcional apenas quando a cena n?o oferece uma. O resolvedor de Editor continua incluindo URP Lit/Unlit, UI/Default e TMP quando a propriedade serializada existe, mas tamb?m corrige o material sem exigir a exist?ncia pr?via de GraphicsSettings. A valida??o local passou; a confirma??o em Unity/GameCI permanece pendente.


A auditoria complementar confirmou que a aus?ncia de um Render Pipeline Asset ativo tamb?m podia causar magenta mesmo com o shader URP dispon?vel. A sele??o agora ? condicional ao pipeline efetivo.


## Fase 52 ? auditoria forense

A varredura de materiais encontrou um ?nico `.mat`, com `m_Shader: {fileID: 0}`. A refer?ncia foi corrigida para Built-in Unlit e o runtime/editor passaram a substituir materiais incompat?veis com o pipeline ativo. Nenhum shader nulo permanece; a valida??o Unity/GameCI ainda ? necess?ria para confirmar a renderiza??o no APK.


## Fase 53 ? auditoria preventiva

IL2CPP, PlayerSettings, bootstrap e manifesto Android foram revisados e blindados. A configura??o t?cnica passou em valida??es locais; importa??o Unity e execu??o em dispositivo ainda precisam de evid?ncia remota.


## Fase 54 ? compatibilidade mobile

O bootstrap agora limita o frame rate a 60, evita suspens?o durante a miss?o e preserva o estado de tempo em pausas do sistema. N?o existem CanvasScaler serializados fora do runtime controlado pelo bootstrap.


P120 - 30/10/2026 America/Sao_Paulo - Valida??o e builds nativas conclu?das no commit `d5c1af98e17c934c405aba1b1c259199fa50b7c3`. Unity Tests run `41059686100`: PASS em EditMode e PlayMode. Android run `41059686028`: SUCCESS; APK em `C:/Temp/nexo-android-41059686028/Android/Nexo.apk`, 31.253.715 bytes, SHA256 `a32ce8d42cf1e294089f9fddc9f9840fd574808c20ed47614d81cfadf9b4089e`; `BUILD_METADATA.txt` presente. iOS run `41059686091`: SUCCESS; IPA em `C:/Temp/nexo-ios-41059686091/Nexo.ipa`, 40.222.535 bytes, SHA256 `c8aa06ce737df79eede565486f96ed02cf78d9ee2e50853ee3d6a4a31c8648ae`; projeto Xcode exportado em `C:/Temp/nexo-ios-41059686091/iOS`. Reposit?rio restaurado PRIVATE e `disabled=false`.


P121 - 30/10/2026 America/Sao_Paulo - Fase 55: diagn?stico forense do fechamento no boot aplicado. Bootstrap agora captura exce??es de inicializa??o, cria c?mera segura, protege callbacks de pausa/foco e valida componentes/recursos antes do uso. EditorBuildSettings confirma somente `NexoVerticalSlice.unity` habilitada no ?ndice 0. Static QA e `git diff --check` PASS. Relat?rio: `docs/PHASE_55_CRASH_FORENSICS.md`. A reprodu??o em aparelho/logcat ainda ? pendente.


P122 - 30/10/2026 America/Sao_Paulo - Sistema de diagn?stico persistente adicionado ao app. O runtime registra sess?o, ambiente, cenas e erros com stack trace em `Application.persistentDataPath/nexo-runtime.log`, com limite de 2 MiB e aviso visual local. N?o h? rede nem segredos. Static QA PASS (`json=5`, `cs=25`), `git diff --check` PASS. Relat?rio: `docs/PHASE_56_RUNTIME_DIAGNOSTICS.md`.

P123 / 30-10-2026: final audit runtime PASS; Android artifact validated; iOS blocked by macOS runner capacity. See `docs/PHASE_57_FINAL_RUNTIME_AUDIT.md`.

P124 / 30-10-2026: Android emulator smoke workflow prepared; remote dispatch blocked by workflow branch visibility (404), no runtime result claimed.

P125 / 30-10-2026: Android emulator job integrated; run cancelled before emulator due Unity build stall; no runtime conclusion.

P126 / 31-10-2026: Android boot crash fixed; emulator smoke SUCCESS; root cause was missing launcher Activity from custom manifest.

P127 - 31/10/2026: escopo atualizado com uma porta verde interativa e desafio pedagogico curto no vertical slice. Implementacao local concluida; sem builds nesta etapa. QA estatico PASS.

P128 - 31/10/2026: escopo ampliado para tres desafios sequenciais e recorde local PlayerPrefs. Implementacao local concluida; sem builds. QA estatico PASS.

P129 - 31/10/2026: audio procedural sintetizado adicionado ao gameplay e ao desbloqueio de portais; builds permanecem NOT_RUN.

P130 - 31/10/2026: release candidata 0.1.0-rc1 preparada com preflight de perfil Android no GameCI; nenhum artefato e considerado aprovado antes do run remoto.

P131 - 31/10/2026 America/Sao_Paulo - Release candidate executada no commit `404947660f932631110cb238dbbdc91b5fb2506b`. Unity Tests `41171249209`: SUCCESS. Android `41171249169`: build Release SUCCESS e smoke test AVD API 35 SUCCESS; APK em `C:/Temp/nexo-release-4049476/android/Android/Nexo.apk`, 31.274.715 bytes, SHA256 `5075FDFFC3FC7F65C24B354EA5B5DF9A655EAA76458D79B899F358875862B312`. iOS `41171249180`: SUCCESS; IPA em `C:/Temp/nexo-release-4049476/ios-xcode3/Nexo.ipa`, SHA256 `879E864EE7404BF95E4DD6F9C5728E9CA78455F8F6561C83F221E2F1C7AAACB0`; projeto Xcode no mesmo diretorio. Repositorio restaurado PRIVATE (`disabled=false`).

P132 - 31/10/2026 America/Sao_Paulo - Fase 59 visual corrigiu Canvas overlay/sorting, EventSystem, StandaloneInputModule, PhysicsRaycaster, toque direto e quiz Canvas com Buttons/Images. Static QA PASS (json=5, cs=25) e git diff --check PASS. Nao houve build nesta correcao. Relatorio: docs/PHASE_59_VISUAL_FIX.md.

## Fase 60 - pre-build checklist

A auditoria local confirmou ARM64/IL2CPP, APIs Android 29/35, cena principal no indice 0, orientacao paisagem e anchors de Safe Area no Canvas runtime. O manifesto customizado permanece ausente por design apos a correcao da Activity launcher; o manifesto gerado pelo Unity foi exercitado no smoke test Android anterior. Static QA e diff check passaram. A nova Safe Area requer validacao em Unity/GameCI antes de ser considerada verificada em runtime.

## Fase 61 - release end-to-end

Preflight local aprovado. Game Juice, progressao, UI/raycasting e Safe Area estao consolidados. Builds Android/iOS desta fase ainda dependem da execucao remota e de artefatos verificaveis vinculados ao commit.

## Fase 61 - resultado remoto

Runs 41174760334 (Android), 41174760342 (tests) e 41174760333 (iOS) falharam sem steps e sem logs uteis, antes do Unity/GameCI. O estado global e `BLOQUEADO POR DEPENDENCIA EXTERNA`; a visibilidade voltou para privado e nao foram gerados novos pacotes.

A retentativa confirmou o mesmo bloqueio de infraestrutura: jobs falhando em aproximadamente 3 segundos, sem runner e sem steps. O repositorio permanece privado e nenhum novo artefato foi produzido.

## Diagnostico Fase 61

O runner publico iniciou corretamente. A falha Android e falta de espaco no Docker/Unity; a falha PlayMode e um SIGSEGV nativo durante reload de assemblies no Unity 6000.3.0f1. EditMode passou. Nao declarar release ate resolver esses dois pontos e obter artefatos verificaveis.

## Estado apos correcao de espaco

A limpeza do runner foi corrigida e validada. O Unity Test gate passou e o IPA foi gerado. O Android ainda tem um bloqueio separado: o builder n√£o concluiu nem reportou artefato dentro da janela operacional.


Estado operacional P140: build Android/iOS bloqueado por runner GameCI sem progresso observ?vel; nenhum APK novo foi obtido. A visibilidade foi restaurada para privado.


Estado P141: SIGSEGV de PlayMode isolado em reload de assemblies; correcao aplicada sem nova execucao remota.


Estado P142: Unity Tests e Android aprovados; APK capturado e smoke test AVD concluido; iOS pendente/cancelado.


Estado P143: maquina de estados e HUD inicializadas no primeiro frame; validacao Unity remota desta alteracao ainda pendente.


Estado P144: input universal e fallback de shaders aplicados; validacao Unity remota desta alteracao ainda pendente.


Estado P145: builds Android e iOS aprovados e artefatos capturados; repositorio privado restaurado.


Estado P146: build Android multi-arquitetura e tracing de boot configurados; nova compilacao ainda pendente.


Estado P147: build Android/iOS e testes aprovados; artefatos capturados e visibilidade privada restaurada.

## Fase 68 - c‚mera, luz e Canvas

A inicializaÁ„o do bootstrap foi endurecida para criar/ativar uma Main Camera posicionada em (0,0,-10), garantir uma luz direcional antes do mundo procedural e vincular o Canvas runtime ‡ c‚mera em ScreenSpaceCamera. A validaÁ„o local passou; a confirmaÁ„o em dispositivo permanece pendente.

## Fase 69 - auditoria e build Android

A auditoria local confirmou static_qa_ok: json=5 cs=25 e git diff --check PASS. A execuÁ„o p˙blica foi restaurada para privado apÛs os runs autom·ticos falharem sem steps e a retentativa Android permanecer sem progresso no empacotamento Unity/IL2CPP. Nenhum artefato novo foi declarado.

## Fase 70 - endurecimento CI Android

A workflow passou a invalidar caches Unity/Gradle potencialmente corrompidos, usa cache v2 versionado, amplia o timeout para 45 minutos, configura heap/logging Gradle e adiciona flags headless de inst‚ncia ˙nica. Nenhum requisito de arquitetura Android foi alterado.

## Fase 71 - release Android/iOS

A build remota apÛs as correÁıes de infraestrutura passou em testes, Android e iOS. Os artefatos foram baixados e verificados por SHA-256; a visibilidade voltou para privada.

## Fase 72 - EventSystem e entrada de UI

O bootstrap agora garante EventSystem e StandaloneInputModule no primeiro ciclo de carregamento, alÈm de manter CanvasScaler e GraphicRaycaster ativos. A alteraÁ„o foi validada estaticamente; teste runtime permanece pendente.

## Fase 73 - auditoria forense de compatibilidade

A an·lise separou o tema de cores do jogo dos erros de backend EGL do emulador. O projeto n„o tinha GraphicsSettings/QualitySettings/URP Asset versionados; a renderizaÁ„o procedural usa fallback Unlit. Foi criado um bootstrap de APIs mÛveis, perfis Low/Medium/High e diagnÛstico tÈcnico local. A confirmaÁ„o final depende de importaÁ„o Unity e testes por plataforma.

## Fase 73 - fechamento t√©cnico

A auditoria de compatibilidade foi aplicada no commit `ce335a6`: perfis Low/Medium/High baseados em mem√≥ria/GPU/CPU, sele√ß√£o OpenGLES3 com Vulkan opcional no Android, Metal no iOS, fallback Canvas Overlay e diagn√≥stico de GPU/API/mem√≥ria/resolu√ß√£o. Os tr√™s workflows foram executados com sucesso: Unity Tests `41211856704`, Android `41211856635` e iOS `41211856610`. O APK foi instalado e iniciado em AVD API 35 x86_64 usando OpenGL ES 3; o log confirmou a cena `NexoVerticalSlice` e os marcadores `[NEXO_BOOT]`, sem exce√ß√£o Unity ou erro de shader. A paleta verde/amarela observada √© coerente com o tema visual atual; os avisos EGL/Gralloc s√£o do backend do emulador e n√£o foram reproduzidos como falha do app. APK e IPA est√£o em `C:/Temp/nexo-release-final`; o reposit√≥rio foi restaurado para privado. A compatibilidade f√≠sica ampla ainda requer matriz de aparelhos reais e simulador iOS.

## Fase 74 - diagn√≥stico visual forense

A origem da geometria verde foi comprovada no bootstrap: `Island` usa cor verde escura e `GreenDoor` usa verde de portal. A busca n√£o encontrou `Color.magenta`, sprites magenta ou shader graph. Os quadrados magenta foram associados ao sistema de part√≠culas procedural da introdu√ß√£o, que n√£o atribu√≠a material ao renderer. A corre√ß√£o usa material Unlit mobile-safe expl√≠cito e desativa o renderer caso o fallback n√£o exista. O runtime passou a registrar uma auditoria `[NEXO_RENDER]` ap√≥s cada cena. A confirma√ß√£o final exige nova execu√ß√£o Unity/Android; testes f√≠sicos e Appetize continuam pendentes.

## Fase 75 - auditoria forense dos erros reportados

A auditoria n√£o encontrou corrup√ß√£o atual em `EditorBuildSettings.asset`, nem refer√™ncias versionadas a `Soft.psd` ou `Default-Skybox.mat`. O aviso de TextMeshPro √© plaus√≠vel porque o manifesto declara o pacote, enquanto o lockfile cont√©m apenas URP e Input System; a corre√ß√£o segura √© regenerar o lockfile com Unity Package Manager, n√£o edit√°-lo manualmente. A tela verde vem de `Island`/`GreenDoor` intencionais. Os quadrados magenta v√™m provavelmente do material impl√≠cito do ParticleSystem de introdu√ß√£o, j√° corrigido com material Unlit expl√≠cito. O diagn√≥stico `[NEXO_RENDER]` est√° instalado, mas a valida√ß√£o Android foi cancelada antes do build; a falha visual permanece pendente de evid√™ncia runtime.

## Fase 76 - depend√™ncias e valida√ß√£o Android

A corre√ß√£o visual de `54e67ec` foi preservada. A resolu√ß√£o do lockfile n√£o foi realizada porque o Editor exato Unity 6000.3.0f1 est√° ausente; somente 6000.6.7f1 est√° dispon√≠vel. A compara√ß√£o objetiva mostrou 36 depend√™ncias no manifesto e apenas 2 no lockfile. N√£o houve edi√ß√£o manual, build ou download. O diagn√≥stico permanece pendente de instala√ß√£o da vers√£o correta do Unity e de um smoke test Android que registre `[NEXO_RENDER]`.

## Fase 77 - estado do ambiente Unity

A vers„o declarada permanece 6000.3.0f1. O Unity CLI instalado reconheceu essa vers„o em --dry-run e estimou 4.131.048.448 bytes, mas as tentativas reais de instalaÁ„o no caminho P:\Unity\Hub\Editor n„o registraram o Editor nem criaram uma ·rvore instal·vel. O Editor 6000.6.7f1 existente n„o foi usado como substituto. A comparaÁ„o por parser mostrou 36 dependÍncias no manifesto e apenas 2 no lockfile; com.unity.textmeshpro 3.0.6 est· declarado, mas n„o est· resolvido no lockfile. Sem o Editor exato, a resoluÁ„o Unity Package Manager e os testes EditMode/PlayMode permanecem bloqueados. Nenhum APK ou IPA foi gerado nesta fase.

## Fase 78 - recuperaÁ„o da instalaÁ„o Unity 6000.3.0f1

A causa foi confirmada nos logs do Unity Hub: EditorInstaller tentou /S /D=P:\Unity\Hub\Editor\6000.3.0f1 e o InstallDriver retornou Access to the path ... is denied, cÛdigo INSTALL_FAILED (exit 6). A unidade P: È um compartilhamento NFS/rede; escrita comum funciona, mas a extraÁ„o/execuÁ„o do instalador Unity n„o È suportada nesse destino. O instalador oficial foi baixado integralmente, com 4.131.048.704 bytes e hash SHA-256 1629B8E701AD1B4F5576D6F912FCC1E82AAACEE56BFBAFA75A4D3EB62A1190AC. A tentativa direta em outro caminho P: terminou exit 2 sem arquivos. C: possui cerca de 126 MB livres, portanto n„o È alternativa sem remover instalaÁıes existentes; nenhuma instalaÁ„o foi apagada. O bloqueio exige volume local NTFS/runner com espaÁo suficiente. Nenhum APK/IPA foi gerado.

## Fase 79 - migraÁ„o cloud CI/CD

A auditoria dos workflows mostra que o pipeline existente ainda depende do GameCI em ubuntu-latest para testes/Android e macos-latest para iOS. N„o h· configuraÁ„o de instalaÁ„o do Unity 6000.3.0f1 em Windows nem runner self-hosted Windows confirmado. A documentaÁ„o oficial do GitHub lista windows-latest e windows-2022, mas runners padr„o de repositÛrios privados possuem armazenamento limitado; isso n„o garante espaÁo para o Editor Unity, Android Build Support, SDK/NDK e OpenJDK. Os YAMLs existentes passaram no parser; Static QA e git diff --check passaram. A fase permanece bloqueada atÈ provisionar a infraestrutura Windows e executar os testes com a vers„o exata. Nenhuma build foi iniciada.

## Fase 80 - planejamento do runner Windows cloud

A migraÁ„o foi planejada sem criar recursos pagos. A arquitetura recomendada È uma VM Windows x64 com armazenamento local NTFS (150 GB), 8 vCPU e 32 GiB RAM, usando runner self-hosted restrito ao repositÛrio. AWS EC2 t3.xlarge oferece referÍncia de 4 vCPU/16 GiB a cerca de US,167/h de compute; Azure D4s v5 e Google n2-standard-4 atendem 4 vCPU/16 GiB, mas exigem cotaÁ„o regional e encargos de Windows/disco. A VM deve ser parada fora das janelas de CI. O runner sÛ deve aceitar workflows confi·veis da branch protegida e nunca executar pull requests n„o confi·veis com acesso a Secrets. Provisionamento, registro do runner e instalaÁ„o Unity aguardam aprovaÁ„o de custo/provedor.

## Fase 80 - investigaÁ„o Free CI

A alternativa Ubuntu GitHub-hosted È gratuita somente sob as condiÁıes de cobranÁa do GitHub (p˙blica: runners padr„o sem cobranÁa; privada Free: 2.000 minutos/mÍs, 500 MB de artefatos e 10 GB de cache). O runner Ubuntu padr„o oferece 14 GB SSD e 16 GB RAM em repositÛrio p˙blico. A imagem unityci/editor:ubuntu-6000.3.27f2-android-3.2.2 observada possui cerca de 8,15 GB comprimida; somando Docker, workspace, Library e tempor·rios, n„o h· margem segura, coerente com os erros histÛricos de espaÁo/hang. O tag exato 6000.3.0f1 n„o foi comprovado nas tags p˙blicas consultadas. AlÈm disso, os workflows atuais passam apenas UNITY_EMAIL/UNITY_PASSWORD; a ativaÁ„o Personal exige UNITY_LICENSE (ULF) em Secret. Portanto, nenhuma alternativa integralmente gratuita foi comprovada para este projeto mantendo Unity 6000.3.0f1. Workflows e vers„o foram preservados, sem build ou alteraÁ„o de visibilidade.

## Fase 81 - prova tÈcnica Unity Linux gratuito

A Unity confirma oficialmente que a vers„o 6000.3.0f1 possui instalador manual para Linux, mas a prova pr·tica em GitHub Actions n„o foi executada. O runner Ubuntu p˙blico documentado oferece 14 GB SSD/16 GB RAM; o tamanho efetivamente extraÌdo do Editor, Android Build Support, SDK/NDK e OpenJDK precisa ser medido dentro do runner. O projeto versionado È pequeno, porÈm o Package Manager ainda precisa resolver 34 entradas ausentes no lockfile. A ativaÁ„o Personal depende de UNITY_LICENSE (ULF), UNITY_EMAIL e UNITY_PASSWORD protegidos; os workflows atuais n„o passam UNITY_LICENSE. Estado: BLOCKED, sem APK/IPA.

## Fase 82 - teste controlado de capacidade GitHub Free

Foi criado um workflow isolado de mediÁ„o, acionado apenas por workflow_dispatch, sem instalar Unity, Docker ou mÛdulos. Ele registra disco, memÛria, CPU e tamanho do checkout antes/depois e gera um artefato textual pequeno. O YAML e os validadores locais passaram, mas o dispatch remoto n„o foi executado porque a CLI/API autenticada do GitHub n„o est· disponÌvel nesta sess„o; n„o h· evidÍncia real de capacidade/quota. A licenÁa Personal tambÈm n„o foi validada na pr·tica e a instalaÁ„o fica bloqueada por gate. Workflows de produÁ„o e visibilidade permanecem inalterados.

## Fase 83 - auditoria forense da infraestrutura original

A auditoria somente leitura recuperou a arquitetura existente: Unity 6000.3.0f1, GameCI em `ubuntu-latest` para testes/Android, `macos-latest` para iOS e AVD headless API 35 via `reactivecircus/android-emulator-runner`. N√£o h√° Appetize.io, BrowserStack, Genymotion ou Appium no reposit√≥rio. O hist√≥rico registra runs remotos PASS e falhas de espa√ßo, licen√ßa, SIGSEGV e hang.

A causa raiz atual n√£o √© um √∫nico defeito de jogo. O Unity exato n√£o est√° instalado localmente porque o InstallDriver falhou ao extrair em P: (NFS/Access denied); o lockfile tem 2 entradas para 36 depend√™ncias declaradas e s√≥ pode ser regenerado pelo Package Manager do Unity 6000.3.0f1; os workflows de teste/build n√£o injetam `UNITY_LICENSE`, e a disponibilidade de imagem/tag GameCI exata e quota atuais n√£o foi comprovada. O probe `github-free-capacity.yml` existe, mas n√£o foi executado por falta de CLI/API GitHub autenticada.

Estado: auditoria documental PASS; execu√ß√£o Unity/Package Manager/novos builds BLOCKED. Pr√≥xima a√ß√£o: executar o probe autenticado, verificar ULF Personal sem expor segredo e confirmar o Editor exato antes de resolver depend√™ncias e rodar testes.

## Fase 84 - recupera√ß√£o do pipeline original

A √∫ltima sequ√™ncia completa documentada com PASS foi no commit `ce335a6`: Unity Tests `41211856704`, Android `41211856635` com smoke AVD, e iOS `41211856610`; Unity `6000.3.0f1`. O pipeline usa GameCI em Ubuntu/macOS e `reactivecircus/android-emulator-runner` API 35/x86_64/SwiftShader. A compara√ß√£o local `ce335a6..HEAD` encontrou apenas o probe isolado `github-free-capacity.yml`; n√£o existe uma quebra posterior demonstrada nos workflows de produ√ß√£o.

Os bloqueios atuais s√£o ambientais: o Editor exato n√£o est√° instalado localmente, a instala√ß√£o em P: falha por NFS/Access denied, o lockfile tem 2 de 36 depend√™ncias e licen√ßa ULF/quotas n√£o s√£o verific√°veis sem GitHub autenticado. N√£o houve altera√ß√µes nem builds nesta fase. Pr√≥ximo passo: executar o probe autenticado e confirmar licen√ßa/Editor exato antes de recuperar o gate Unity.

## Fase 85 - recupera√ß√£o operacional

A recupera√ß√£o remota ficou bloqueada: `gh` n√£o existe no ambiente e a API an√¥nima do reposit√≥rio privado retornou HTTP 404. N√£o foi poss√≠vel consultar execu√ß√µes, quota ou Secrets, nem disparar o probe de capacidade. Os runs hist√≥ricos PASS permanecem documentados, mas n√£o foram revalidados. Nenhuma configura√ß√£o foi alterada; o pr√≥ximo passo √© obter acesso autenticado e executar somente o probe antes do gate Unity.

## Fase 86 - diagn√≥stico do runtime

A auditoria local confirmou que `NexoVerticalSlice.unity` √© uma cena m√≠nima: o bootstrap √© injetado por `RuntimeInitializeOnLoadMethod`, e cria c√¢mera, luz, Canvas, EventSystem, part√≠culas e mundo. A geometria verde pertence ao cen√°rio procedural e √© intencional. O commit `54e67ec` corrigiu o material impl√≠cito do ParticleSystem, mas `magentaObjects=0` ainda n√£o foi observado em novo runtime.

O risco t√©cnico confirmado √© a resolu√ß√£o incompleta do Package Manager (manifest=36, lock=2). Sem Unity `6000.3.0f1`, EditMode/PlayMode, Android/AVD e iOS permanecem NOT_RUN. Estado: PARTIAL; n√£o declarar o app corrigido definitivamente.

## Fase 87 - diagn√≥stico Android com AVD existente

O AVD original est√° corretamente definido nos workflows: API 35, `google_apis`, x86_64, Pixel 6 e SwiftShader. O script `tools/android_smoke_test.sh` usa o package `com.nexo.educational`, instala o APK, inicia a Activity via `monkey`, coleta logcat, dumpsys, screenshot e runtime log. A execu√ß√£o real n√£o foi poss√≠vel porque `adb`, `emulator`, Unity 6000.3.0f1 e autentica√ß√£o GitHub n√£o est√£o dispon√≠veis. Nenhuma visibilidade foi alterada e nenhum app foi gerado. Estado: BLOCKED por ambiente.

## Diagn√≥stico adicional de acesso GitHub pelo Orca

O Orca reconhece o projeto `github:valdir-galvao/nexo` e o Git credential manager permite `git ls-remote`. Isso √© somente leitura do remote Git. A API GitHub an√¥nima retorna 404, o browser integrado mostra Sign in e n√£o h√° ferramenta GitHub/Actions exposta. Portanto n√£o h√° autoriza√ß√£o operacional verific√°vel para consultar runs, Secrets, quota ou visibilidade. Nenhuma altera√ß√£o remota foi realizada.

## Credenciais locais e GitHub

O arquivo `.env.txt` √© ignorado e usado apenas pelo executor de agentes. Embora contenha uma chave chamada `senha_githup`, `tools/agents.py` a ignora porque s√≥ aceita chaves definidas em `agents.json`. O Git usa Credential Manager separadamente, permitindo `git ls-remote`, mas isso n√£o autoriza API GitHub, Actions, Secrets ou visibilidade. Nenhum segredo foi lido ou exposto.

## Corre√ß√£o do acesso local

A GitHub CLI 2.103.3 foi instalada em `C:\Program Files\GitHub CLI\gh.exe`, corrigindo a aus√™ncia da ferramenta. A sess√£o atual precisa ser reiniciada para atualizar o PATH. `gh auth status` ainda retorna que nenhum host est√° autenticado. O pr√≥ximo passo √© login GitHub autorizado; n√£o usar a senha do `.env.txt`.

## Rechecagem de autentica√ß√£o GitHub

A GitHub CLI est√° instalada, mas n√£o autenticada. O Git Credential Manager possui uma entrada GitHub e permite leitura de refs remotas; isso n√£o concede API/Actions. `cmdkey` confirmou apenas a exist√™ncia do alvo, sem ler o segredo. O browser Orca continua sem sess√£o autenticada. Nenhum workflow ou visibilidade foi alterado.

## Fase 88 - tentativa operacional

A fase deixou de ser documental e tentou executar o pipeline real. O gate falhou antes do workflow porque a GitHub CLI n√£o est√° autenticada; o Git Credential Manager s√≥ permite leitura de refs. Unity 6000.3.0f1, adb e emulator tamb√©m n√£o est√£o dispon√≠veis localmente. N√£o houve build, AVD ou mudan√ßa remota. A retomada depende de autentica√ß√£o GitHub pr√≥pria no ambiente.

## Reautenticacao GitHub e gate atual
A CLI autenticada foi confirmada sem expor tokens. O repositorio permanece privado. O run Unity Tests 41293713412 foi disparado na branch orchestrator/phase-a-baseline, commit 3315b64, com EditMode e PlayMode em execucao. O Android historico 41214944817 foi cancelado durante Build Android package; iOS 41214944802 concluiu com sucesso.

## Resultado operacional P179
Autenticacao GitHub operacional. Unity Tests 41293713412: PASS em EditMode e PlayMode. Android 41294473685: FAIL por timeout/hang em Build Android package; nenhum artefato foi publicado e o AVD n„o executou. O estado global permanece VALIDACAO INCOMPLETA.

## P180 - Diagnostico e correcao do timeout Android
O hang ocorreu no container GameCI durante Build Android package; o job expirou sem logs finais. Unity Tests passou no mesmo codigo e o build historico 41211856635 passou com as mesmas configuracoes. Aumentado o limite do job Android de 45 para 60 minutos como correcao de infraestrutura; a causa interna do atraso segue nao comprovada. Novo build ainda nao executado.

## P181 - Validacao do timeout ampliado
O run Android 41299986311 foi disparado com timeout de 60 minutos. Preparacao passou; Build Android package continua em execucao apos cerca de 30 minutos. Nenhum APK deve ser considerado existente ate o encerramento.

## Fase 94 - perÌcia do hang Gradle Android

O run `41305244451` confirmou que a correÁ„o do watchdog resolveu o erro de branch suja e permitiu que Unity/IL2CPP chegassem ao `assembleRelease`. O bloqueio seguinte È de espaÁo: o runner registrou 100% do volume, com 505 MB livres, enquanto Gradle e o container ainda estavam ativos. O timeout encerrou o job; n„o houve APK e o AVD foi skipped. O estado global permanece `BLOCKED` por infraestrutura de empacotamento. PrÛxima aÁ„o: corrigir limpeza/invalidaÁ„o de caches e repetir o build somente com espaÁo validado.

## SeguranÁa do watchdog
ApÛs a perÌcia do run 41305244451, o watchdog foi ajustado para n„o registrar argumentos de processos. Isso evita que futuras capturas de diagnÛstico incluam credenciais passadas ao container.

## PreparaÁ„o P190
ApÛs a causa comprovada de disco cheio, o job Android deixou de restaurar o cache Unity de projeto e ganhou gate de espaÁo antes do GameCI. A alteraÁ„o È de infraestrutura e aguarda validaÁ„o remota.
