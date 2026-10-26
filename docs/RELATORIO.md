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

## Persistencia

Documentos e codigo ficam nesta pasta e sobrevivem a queda da sessao, desde que
o diretorio seja preservado. Isso nao constitui backup externo nem commit Git.
Cache e ledger locais em `.agents-local/` sao ignorados pelo Git: preserva-los
em retomadas na mesma maquina. Ao migrar, manter credenciais por canal seguro
e restaurar registros de uso sem publica-los. Sem ledger, o consumo anterior
e incerto: nao repetir tarefas pagas ate reconciliar o estado.
