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
| Auditoria e configuração NEXO | `NEXO_ENVIRONMENT_AUDIT.md`, `NEXO_CONFIGURATION_REPORT.md`, `.gitattributes` | Concluido como baseline |
| Esteira CI/CD GameCI | `.github/workflows/unity-*.yml`, `CICD_REPORT.md` | Estruturada; não executada |
| Verificação de GitHub | API e `git push` | `valdir-galvao/nexo` privado, Actions habilitado, branch `main` sincronizada |
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

P3 adicionou a máquina de estados, regras da missão, save JSON atômico, bootstrap
de ilha temporária, controles de teste, interação com três pistas, puzzle da
válvula, feedback, conclusão e desbloqueio narrativo. A implementação é parcial
até a importação no Unity e o teste em dispositivo.

P4 integrou a paleta compartilhada, menu inicial, HUD, diário, pausa,
configurações de áudio e tons procedurais leves. O greybox continua deliberadamente
temporário até câmera e desempenho serem validados; nenhum pacote de arte pesado
foi adicionado.

P5 está 100% concluído quanto aos entregáveis: framework de competências,
diagnóstico, pós-teste, transferência, revisões configuráveis, regras adaptativas
e conteúdo versionado da missão. O modelo não trata pausa, ajuda ou dificuldade de
compreensão como incapacidade. Revisão por pessoa qualificada e evidência
experimental são gates externos de publicação, não itens faltantes do pacote P5.

P6 está 100% concluído quanto aos entregáveis de revisão: threat model, mapa de
dados, checklist de acessibilidade, política de aceite e controles locais de
exclusão/exportação. O projeto não declara conformidade jurídica certificada;
auditoria de pacote, revisão jurídica/ética e testes físicos são gates de piloto.

P7 adicionou testes de fluxo completo, retomada após reabertura, migração de save
V0, política offline, `QA_REPORT.md`, `KNOWN_ISSUES.md` e `run_static_qa.py`.
A integração está verificada estaticamente; o Editor Unity e dispositivos não
estão disponíveis para executar EditMode/PlayMode, builds ou profiling.

P8 preparou a release candidata `0.1.0-rc1`: versionamento Unity/Android/iOS,
notas de release, checklist interno, manifesto SHA256 de fonte e instruções de
reprodução. Nenhum APK/Xcode foi inventado; os artefatos dependem do GameCI.

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

Atualiza��o P92: o workflow iOS foi ampliado para gerar um arquivo Nexo.ipa sem assinatura ap�s a exporta��o Xcode. O artefato s� comprovar� empacotamento; instala��o exige assinatura Apple posterior.

Atualiza��o P93: run 40852640118 PASS. O workflow agora gera Nexo.ipa sem assinatura via xcodebuild e o artefato foi baixado em C:/Temp/nexo-ios-ipa-40852640118/Nexo.ipa (SHA256 2c92748b8807a6a7fbfc72869fdb0c5135a8049314f7dde000985a1d7fec758c). Reposit�rio restaurado privado. A assinatura Apple n�o foi realizada.

Atualizacao P94: como GraphicsSettings.asset nao existia no projeto, a inclusao de shaders foi implementada por editor bootstrap versionado, evitando GUIDs inventados. O CanvasScaler e configurado no bootstrap da cena e a intro recebeu fade sincrono. Static QA PASS; validacao Unity e dispositivo pendente.

Atualizacao P95: os runs automaticos privados do commit beafb4b falharam sem executar steps, portanto a implementacao mobile ainda nao tem evidencia Unity/GameCI. Static QA continua PASS; o bloqueio e infraestrutura de runner.
