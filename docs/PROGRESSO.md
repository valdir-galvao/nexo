# Registro de progresso

Historico em ordem de ocorrencia. Nao apagar falhas ou substituir evidencias
antigas por resultados novos; adicionar uma entrada de atualizacao.
Datas desta reconstrucao: 26/10/2026, America/Sao_Paulo; horarios individuais
anteriores nao foram registrados aqui e nao sao presumidos.

## P00 — Inspecao inicial — concluido

- Responsavel: gerente.
- Resultado: identificados arquivo `.env.txt`, ferramentas, hardware e Orca ativo.
- Evidencia: comandos locais de inventario; resumo em `AMBIENTE.md`.
- Limitacoes: engine nao localizada nos caminhos verificados; sem prova de ausencia
  em outros diretorios. Nenhum teste de desempenho grafico realizado.
- Proxima acao executada: preparar executor.

## P01 — Executor e controles — concluido

- Responsavel: gerente.
- Arquivos: `tools/agents.py`, `tools/test_agents.py`, `agents.json`, `.gitignore`,
  `AGENTS.md` e `docs/AMBIENTE.md`.
- Resultado: chamadas HTTP explicitas, cache, contador diario, bloqueio de
  repeticao apos tentativa incerta, exclusao mutua e validacao de entrada.
- Verificacao mais recente: 9 testes unitarios passaram, sem chamadas reais;
  `git diff --check` sem erros na verificacao anterior.
- Limite conhecido: controle por chamadas/tamanho, sem teto monetario apurado.
- Proxima acao executada: validacao dos provedores.

## P02 — Disponibilidade dos colaboradores — parcial

- Responsavel: gerente.
- DeepSeek: catalogo e inferencia minima OK; 87 tokens totais registrados.
- Gemini: catalogo OK; inferencia HTTP 404, sem retry automatico.
- Groq: catalogo HTTP 403; inferencia nao solicitada.
- Mistral: rotulo corrigido; chave anterior rejeitada com Invalid API Key.
  Apos substituicao pelo usuario, catalogo OK; inferencia minima HTTP 429.
- TypeSafe: rotulo corrigido; catalogo e inferencia estruturada OK; 284 tokens
  de entrada e 20 de saida. Integracao e testes locais acrescentados.
- Uso: 4 tentativas de inferencia presentes no ledger no inventario desta etapa
  documental. Consultas de catalogo/diagnostico nao entram nesse numero.
  Nao ha custo monetario total verificado.
- Evidencias: `.agents-local/availability.json`, `calls.json`, `cache/` e
  `AMBIENTE.md`. Registros locais ficam fora do Git e nao contem relatorio publico.
- Pendencias: A01/A02/A03 em CRONOGRAMA.md; nao impedem J01.

## P03 — Documentacao de continuidade — concluido

- Responsavel: gerente; data: 26/10/2026, America/Sao_Paulo.
- Solicitacao: registrar cada passo para retomada apos queda ou troca de sessao.
- Arquivos criados: `RELATORIO.md`, `CRONOGRAMA.md`, `RETOMADA.md` e este
  `PROGRESSO.md`, todos em `docs/`. Regra persistente adicionada a `AGENTS.md`.
- Resultado: estado atual, marcos, pendencias, criterios de aceite e roteiro de
  recuperacao documentados; cronograma sem prazos inventados.
- Verificacao: revisao de consistencia dos arquivos e links locais; conferir
  a entrada de verificacao ao final deste registro.
- Uso de APIs: nenhuma chamada externa nesta etapa documental.
- Proxima acao: J01, aguardar inicio e definir conceito/plataforma do jogo.

## P03 — Verificacao da documentacao — concluida

- Data: 26/10/2026, America/Sao_Paulo; responsavel: gerente.
- Verificacao local: seis documentos presentes e preenchidos; referencias aos
  quatro novos documentos em AGENTS.md e marcos P03/J01 conferidos.
- Verificacao de privacidade: nenhuma das chaves de IA configuradas encontrada
  nos textos dos documentos. Nenhum valor de credencial foi exibido.
- `git diff --check`: sem erros. Documentos novos ainda nao foram commitados.
- Nao foi necessario repetir testes de codigo, que nao mudou nesta etapa.
- Uso de APIs: zero. Cronograma e ponto de retomada atualizados.
- Proxima acao: J01, definir o briefing apos instrucao do usuario.

## P04 — NEXO P0: contratos de planejamento — concluido

- Data: 26/10/2026, America/Sao_Paulo; responsavel: diretor de projeto.
- Entrada: briefing NEXO fornecido pelo usuario e inventario local do repositorio.
- Arquivos criados: `PROJECT_CHARTER.md`, `PRODUCT_REQUIREMENTS.md`,
  `ACCEPTANCE_CRITERIA.md`, `DECISION_LOG.md`, `IMPLEMENTATION_ROADMAP.md` e
  `RISK_REGISTER.md`.
- Resultado: escopo minimo da fatia vertical, requisitos rastreaveis, metodos de
  validacao, gates, dependencias, riscos e decisoes pendentes documentados.
- Verificacao: cada requisito possui ID e metodo de validacao; Unity/C# ficou
  como recomendacao provisoria; Unity, dotnet, adb e xcodebuild nao foram
  encontrados no PATH inspecionado; nenhum dispositivo foi confirmado.
- Uso de APIs: zero. Nenhum segredo copiado. Nenhuma instalacao ou operacao externa.
- Bloqueadores para P1: D-001/D-002 e confirmacao de toolchain/dispositivos.
- Proxima acao: decidir engine/template, dispositivos, cooperacao, objetivos
  pedagogicos e direcao visual; depois iniciar P1.

## P04.1 — Verificacao final dos contratos — concluida

- Data: 26/10/2026, America/Sao_Paulo; responsavel: diretor de projeto.
- Verificacao local: os seis arquivos P0 existem e não estão vazios; 30 requisitos
  rastreáveis foram conferidos por busca; todos possuem coluna de validação;
  `git diff --check` não encontrou erros.
- Privacidade: nenhuma sequência de chave conhecida ou padrão de segredo foi
  encontrada nos seis documentos. Nenhuma API foi chamada.
- Estado: P0 aceito. P1 permanece bloqueado até as decisões D-001/D-002 e a
  confirmação do toolchain/dispositivos; isso é uma dependência explícita, não
  uma funcionalidade incompleta declarada como pronta.
- Próxima ação: fechar decisões pendentes com o usuário e, então, validar a
  instalação Unity/SDK e um dispositivo antes de criar o projeto.

## P05 — Auditoria de hardware e baseline técnico — concluído como configuração provisória

- Data: 26/10/2026, America/Sao_Paulo; responsável: diretor de projeto.
- Evidência: Windows 11 Pro 64-bit; i5-9500T, 6 cores/6 threads; 16 GiB RAM;
  pagefile de 16,158 MB; Intel UHD 630 integrada; DirectX 12/Feature Level
  12_1; Vulkan 1.3.215; NVMe saudável de 256 GB; cerca de 15 GiB livres.
- Ferramentas: Git, Python, Vulkan e winget encontrados; Unity/Unity Hub,
  dotnet, adb, xcodebuild e dispositivo de teste não confirmados.
- Decisão provisória: Unity 6.3 LTS + URP baixa/média, Android 10+/4 GB,
  30 FPS/33.3 ms; iOS via CI macOS. Motivos e limites em
  `NEXO_CONFIGURATION_REPORT.md` e ADR-009/010/011/012.
- Arquivos: `NEXO_ENVIRONMENT_AUDIT.md`, `NEXO_CONFIGURATION_REPORT.md`,
  `.gitattributes`, `.gitignore`, `DECISION_LOG.md`, `RELATORIO.md`,
  `CRONOGRAMA.md`, `RETOMADA.md`.
- Verificação: oito documentos de NEXO presentes e não vazios; 29 requisitos
  encontrados; `git diff --check` sem erros; nenhuma instalação ou API externa.
- Bloqueios: apenas 15 GiB livres, D-001/D-002/D-008/D-009 ainda precisam de
  confirmação; não declarar Unity ou build como disponível.
- Próxima ação: liberar espaço/selecionar volume, confirmar baseline e obter
  dispositivo Android antes do provisionamento.

## P06 — Esteira GitHub Actions/GameCI — concluído como infraestrutura não executada

- Data: 26/10/2026, America/Sao_Paulo; responsável: diretor de projeto.
- Arquivos: `.github/workflows/unity-activation.yml`, `unity-tests.yml`,
  `unity-android.yml`, `unity-ios.yml`, `.gitattributes` e `docs/CICD_REPORT.md`.
- Resultado: ativação manual separada; testes e Android em Ubuntu; Xcode project
  iOS em macOS; cache de Library, LFS e artifacts configurados; jobs condicionados
  à presença de `ProjectSettings/ProjectVersion.txt`.
- Verificação: revisão local e `git diff --check`; nenhum workflow remoto executado.
- Limitação: `Nexo.Editor.Build.*` ainda não existe; deve ser criado no P1 ou
  substituído pelo método de build escolhido.

## P07 — Verificação de credencial GitHub — concluído, sem mutação remota

- Data: 26/10/2026, America/Sao_Paulo; responsável: diretor de projeto.
- Resultado: o valor atualizado em `.env.txt` tem 93 caracteres e formato de
  token GitHub; consulta somente leitura a `GET /user` retornou HTTP 200 e login
  `valdir-galvao`. O token não foi impresso nem copiado para arquivos.
- Limitações: GitHub CLI (`gh`) continua ausente; nenhum `git remote` está
  configurado; a busca de repositórios por nome contendo `nexo`, `jogo` ou
  `game` não encontrou candidato nos 100 mais recentes.
- Uso externo: uma chamada somente leitura à API GitHub; nenhuma criação,
  alteração, push, secret ou workflow remoto foi executado.
- Próxima ação: receber a URL exata do repositório GitHub e configurar o remote;
  depois revisar os workflows localmente antes de qualquer push.

## P08 — Tentativa de criação do repositório GitHub — bloqueada por permissão

- Data: 26/10/2026, America/Sao_Paulo; responsável: diretor de projeto.
- Alvo solicitado: repositório privado `valdir-galvao/nexo`.
- Resultado: `GET /user` continua autenticando `valdir-galvao`, mas `POST /user/repos`
  retornou HTTP 403 `Resource not accessible by personal access token`.
- Segurança: nenhuma repetição, contorno, push ou alteração remota foi executado;
  token não foi impresso. O diretório local e seus arquivos permanecem intactos.
- Limitações: `gh` não está instalado e não há remote Git configurado.
- Próxima ação: criar o repositório manualmente no GitHub com a sessão do usuário
  ou substituir o token por um PAT/fine-grained token com permissão de criação de
  repositórios; depois fornecer a URL exata para configurar o remote.

## P09 — Verificação da URL GitHub — repositório não encontrado

- Data: 26/10/2026, America/Sao_Paulo; responsável: diretor de projeto.
- Alvo: `https://github.com/valdir-galvao/nexo.git`.
- Resultado: API autenticada retornou HTTP 404 para `GET /repos/valdir-galvao/nexo`;
  `git ls-remote` não obteve HEAD ou branch. O repositório não está acessível
  com a conta/token atual ou ainda não foi criado.
- Segurança: apenas leitura; nenhum remote local, arquivo, push ou secret alterado.
- Observação: a mensagem `8483-CF99` recebida em seguida não foi interpretada
  nem usada, pois não há contexto seguro para tratá-la como credencial/autorização.
- Próxima ação: criar o repositório na conta correta e fornecer uma URL verificável;
  então repetir apenas GET/ls-remote antes de configurar o remote.

## Modelo para cada novo passo

Copiar e preencher ao concluir uma tarefa; registrar interrupcoes tambem.

```text
ID e titulo:
Data/hora e fuso:
Responsavel:
Estado: concluido / parcial / interrompido / falhou
Objetivo e resultado:
Arquivos alterados:
Verificacao executada e resultado observavel:
APIs: provedor/modelo, cache ou nova chamada, tokens e referencia local (sem segredos):
Decisoes e pendencias:
Proxima acao e dependencias:
Cronograma e RETOMADA.md atualizados: sim / motivo da pendencia
```
