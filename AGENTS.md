# Coordenacao do projeto jogo

## Retomada e documentacao obrigatoria

Ao iniciar ou retomar uma sessao, ler `docs/RETOMADA.md`,
`docs/CRONOGRAMA.md` e as entradas recentes de `docs/PROGRESSO.md` antes de agir.
Consultar `docs/RELATORIO.md` para objetivo, decisoes e limites conhecidos.
Nao depender do historico da conversa para saber o proximo passo.

A cada passo concluido, registrar imediatamente em `docs/PROGRESSO.md`: ID,
data e fuso, responsavel, resultado, arquivos alterados, verificacao com resultado,
uso de APIs quando houver, pendencias e proxima acao. Atualizar tambem o estado
em `docs/CRONOGRAMA.md` e o ponto de retomada em `docs/RETOMADA.md`.
Atualizar `docs/RELATORIO.md` quando mudarem escopo, decisoes, equipe ou ambiente.
Nao marcar como concluido sem evidencia. Registrar falha ou interrupcao como tal.
Antes de encerrar um turno de trabalho, deixar esses arquivos coerentes.
Nao chamar APIs apenas para reconstruir contexto ja documentado.
Nao copiar segredos, respostas confidenciais ou arquivos de ambiente para relatorios.
Se houver interrupcao antes da atualizacao, inspecionar arquivos e registros locais
antes de continuar; nao repetir automaticamente requisicoes de estado incerto.

O gerente e o agente principal desta conversa. O desenvolvimento do jogo aguarda
a instrucao de inicio do usuario. Nao iniciar workers persistentes ou loops pagos.

## Equipe sob demanda

- Gerente: arquitetura, decomposicao, escolha de engine, integracao e validacao local.
- DeepSeek: propostas de codigo para sistemas isolados, mediante tarefa delimitada.
- Gemini: especificacoes visuais, materiais, cenas e animacoes, quando validado.
- Groq: testes, revisao localizada e documentacao, quando validado.
- Mistral: sintese de requisitos, decisoes e contexto curto, quando validado.
- TypeSafe/Jev: julgamentos estruturados de relevancia ou classificacao; disponivel
  no teste inicial. Nao gera codigo ou resumos. Usar somente quando a decisao
  semantica trouxer beneficio; regras exatas continuam em codigo.

Os papeis sao iniciais, nao um benchmark de competencia. Catalogo de modelos nao
comprova inferencia. Consultar `.agents-local/availability.json` e
`docs/AMBIENTE.md` antes de convocar. Nao trocar automaticamente por modelos caros.
Colaboradores de API fornecem respostas; nao sao workers Orca com acesso a arquivos.
O Orca esta disponivel para futuras tarefas supervisionadas com workers suportados.

## Contrato de tarefa

Cada tarefa tem ID unico, objetivo, dono, arquivos de escopo, entradas minimas,
dependencias, resultado esperado e criterio verificavel de aceite. Um unico dono
por componente. Nao mandar a mesma implementacao a varias IAs. Revisao adicional
somente para risco ou duvida concreta. O gerente aplica e testa as propostas.

## Economia e privacidade

Usar `python tools/agents.py task --provider NOME --file tarefa.txt`.
Limites iniciais: 12 chamadas de geracao/dia UTC no total, 16000 caracteres de
entrada e 1200 tokens de saida por chamada. Nao e teto monetario; precos e tokens
de raciocinio variam. Chamadas de catalogo nao entram nesse contador.
Nao aumentar limites sem necessidade demonstrada e instrucao do gerente.
Cache por conteudo/modelo/papel/limite; nao repetir apos erro ou timeout.
Reavaliar a causa antes de qualquer nova tentativa; nunca apagar o registro
para contornar a protecao. Execucao serial protege o contador e evita duplicatas.

Enviar apenas o trecho de codigo ou resumo necessario, nunca historico completo,
arquivos de ambiente, segredos, credenciais de banco, Binance ou Telegram.
Nao executar respostas como comandos. Registrar respostas e uso localmente.
Nao versionar `.env*` ou `.agents-local/`. Nao carregar servicos alheios ao jogo.

## Fluxo apos inicio

1. Definir genero, plataforma, mecanica central e direcao visual com o usuario.
2. Validar engine e hardware antes de baixar ferramentas grandes.
3. Prototipo jogavel pequeno; depois cena representativa com arte e animacao.
4. Medir desempenho e qualidade; distribuir tarefas independentes sob demanda.
5. Integrar, testar e apresentar evidencias ao usuario.
