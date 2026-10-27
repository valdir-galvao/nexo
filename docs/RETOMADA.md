# Ponto de retomada

Ultima atualizacao: 26/10/2026, America/Sao_Paulo.
Ultimo passo concluido: P30, Fase C bloqueada corretamente; execução Unity ainda não iniciada.
Tarefa de jogo em execucao: nenhuma. Chamadas externas em andamento: nenhuma
iniciada por este passo. Nao ha engine ou build validada.

## Como retomar
Atualização P42: workflow de testes ajustado para matriz EditMode/PlayMode e
validado localmente. Nenhum dispatch foi executado; próximo passo é push e
execução GameCI.
Atualização P41: commits e rastreamento confirmados; guia de push/dispatch criado.
Nenhum push foi executado. Antes de builds, executar `unity-tests.yml` e conferir
que PlayMode ainda requer job explícito.
Atualização P40: Fase 03 preparada. Script de resolução Unity criado e validado,
mas Unity/gh ausentes; nenhum dispatch ou build ocorreu. Relatório:
`docs/PHASE_03_CI_PREPARATION.md`.
Atualização P39: correção local de K-005 e teste PlayMode foram adicionados;
validadores locais passaram. Lockfile e testes Unity ainda aguardam Unity/GameCI.
Relatório: `docs/PHASE_02_HARDENING_REPORT.md`.
Atualização P38: tentativa de continuidade encerrada sem chamada externa;
`gh` e dispatch autenticado continuam indisponíveis. Fase C permanece
`BLOCKED`. Executar somente `unity-tests.yml` quando o canal seguro existir.
Atualização P37: Fase C `BLOCKED`. Lockfile aguarda resolução Unity; EditMode,
PlayMode e persistência runtime não foram executados. K-005 continua sem UI de
transferência. Relatório: `docs/PHASE_C_PREBUILD_TEST_REPORT.md`.
Atualização P36: Fase B3 `BLOCKED`; `gh` não está instalado e nenhum canal
autenticado de dispatch está disponível. Executar somente `unity-tests.yml`
quando o método seguro estiver disponível. Relatório:
`docs/PHASE_B3_GAMECI_TEST_REPORT.md`.

Atualização P35: Fase B2 auditada. Workflows parseiam e builds Android/iOS agora
exigem disparo manual; GameCI ainda não executado, secrets permanecem não
verificáveis e PlayMode não está coberto. Relatório:
`docs/PHASE_B2_CI_AUDIT.md`.

Atualização P34: a Fase G está `BLOCKED`; não existe versão funcional para
revisão. Revisões técnica, jurídica, pedagógica e ética permanecem `PENDENTE`.
Relatório: `docs/PHASE_G_TRUST_SAFETY_REPORT.md`.

Atualização P33: a Fase F está `BLOCKED` porque a Fase E não produziu builds e
nenhum dispositivo ou simulador foi disponibilizado. Relatório:
`docs/PHASE_F_DEVICE_QA_REPORT.md`.

Atualização P32: a Fase E está `BLOCKED` para Android e iOS. Nenhum build foi
iniciado; depende da aprovação de C e D e dos workflows GameCI.

Atualização P31: a Fase D foi registrada como `BLOCKED` porque a Fase C não foi
aprovada. A matriz funcional está em `docs/PHASE_D_FUNCTIONAL_REPORT.md`; não
há evidência runtime. Próxima dependência: secrets Unity e workflow GameCI.

1. Ler `AGENTS.md`, este arquivo, `CRONOGRAMA.md` e as entradas recentes de
   `PROGRESSO.md`. Consultar `RELATORIO.md` e `AMBIENTE.md` quando necessario.
2. Inspecionar `git status --short` e os arquivos antes de alterar; preservar
   trabalho do usuario. Nao presumir que os arquivos foram commitados.
3. Conferir, sem exibir segredos, se `.env` ou `.env.txt` e os registros locais
   `.agents-local/availability.json`, `calls.json` e `cache/` estao presentes.
   Nao chamar APIs apenas para verificar se a sessao foi restaurada.
4. Se houver `.agents-local/executor.lock`, verificar se ha executor ativo.
   Nao remover o lock nem repetir uma tentativa incerta automaticamente.
5. Se codigo ou ambiente mudaram e houver necessidade de verificar o executor,
   executar `python -m unittest discover -s tools -p test_agents.py -v`.
   Esse comando usa mocks e nao consulta provedores externos.
6. Executar a proxima acao abaixo dentro da autorizacao do usuario. Registrar
   o resultado em PROGRESSO.md e atualizar cronograma e este ponto de retomada.

## Proxima acao

J01: registrar o briefing NEXO recebido e fechar D-003/D-004/D-005. Antes de
P1, confirmar ADR-009/ADR-010 e decidir D-001/D-002/D-008/D-009: Unity/SDKs,
dispositivos mínimos, espaço e CI iOS. Não iniciar implementação ou instalações grandes antes
dessas decisoes e de uma verificacao de toolchain.

Atualizacao P23: o pacote de design P1, a arquitetura P2, o gameplay P3, a
integração visual/UI/áudio P4 e a pedagogia P5 foram
versionados; as decisoes D-001 a D-009
continuam fechadas como baseline do
prototipo. A proxima acao e cadastrar os secrets Unity e executar `Unity tests`;
o P1 so sera aceito apos evidencia do import e dos testes no GameCI.

## Equipe utilizavel no ultimo teste

Gerente, DeepSeek e TypeSafe. Mistral: autenticacao OK, inferencia HTTP 429.
Gemini: HTTP 404 na geracao. Groq: HTTP 403 na consulta. Nao tentar desbloquear
todos como requisito para planejar o jogo. Ultima suite local: 9 testes passaram.

## Recuperacao de trabalho interrompido

Se o ultimo registro indicar tarefa em andamento ou os arquivos divergirem do
relatorio, inspecionar o resultado e as verificacoes existentes antes de refazer.
Reservas no ledger sem conclusao representam tentativa incerta, nao chamada
seguramente nao enviada. Cache existente deve ser reaproveitado. Se for preciso
novo pedido, registrar a causa e a decisao do gerente; respeitar os limites.
Se os registros locais estiverem ausentes, nao presumir consumo zero.

## Contexto curto para uma nova sessao

> Retome este projeto lendo AGENTS.md e docs/RETOMADA.md. O executor de APIs foi
> preparado; DeepSeek e TypeSafe responderam aos testes. O jogo nao foi iniciado,
> a engine nao foi escolhida e falta o briefing do usuario. Siga CRONOGRAMA.md,
> preserve o ledger/cache e documente cada passo concluido em PROGRESSO.md.
