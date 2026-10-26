# Preparacao do projeto

Estado em 26/10/2026: infraestrutura de colaboracao preparada; jogo nao iniciado.

## Maquina

Windows, Python 3.12, Node/npm e Git disponiveis. Orca 1.4.224 conectado e pronto.
6 processadores logicos, aproximadamente 16 GB de RAM, Intel UHD Graphics 630.
UnrealEditor e Godot ausentes no PATH; nenhuma instalacao encontrada nos caminhos
padrao verificados de Epic Games, Unity Hub e Godot. Isso nao exclui instalacao
em outro diretorio. Nenhuma engine foi instalada nesta preparacao.

Graficos ultrarrealistas em tempo real nao foram validados neste hardware.
A escolha da engine e da maquina alvo precisa considerar esse limite.

## Executor de colaboradores

Sem dependencias externas: `python tools/agents.py --help`.
Credenciais lidas de `.env`, ou `.env.txt` quando o primeiro nao existe.
Ultima entrada nao vazia prevalece em duplicatas; arquivo original preservado.
Ha duplicatas de GEMINI_API_KEY e GROQ_API_KEY. Nenhum segredo foi impresso.

Consulta de catalogos: `python tools/agents.py probe`.
Teste minimo de inferencia: `python tools/agents.py smoke --provider deepseek`.
Repetir teste concluido le o cache, sem nova chamada. Erros exigem revisao.

Resultados reais da primeira verificacao:

| Provedor | Catalogo | Inferencia | Estado de convocacao |
| --- | --- | --- | --- |
| DeepSeek | OK, deepseek-flash selecionado | OK; resposta OK, 87 tokens totais | Disponivel |
| Gemini | OK, gemini-2.5-flash-lite listado | HTTP 404 | Pendente de diagnostico; nao convocar |
| Groq | HTTP 403 | Nao tentada | Pendente de diagnostico; nao convocar |
| Mistral | OK apos atualizacao da chave; mistral-small-latest selecionado | HTTP 429 no teste minimo | Autenticacao resolvida; inferencia ainda indisponivel |
| TypeSafe | OK, jev-latest selecionado | OK; Noul valido, 284 tokens entrada e 20 saida | Disponivel para decisoes estruturadas |

Nao ha repeticao automatica, fallback pago ou agentes em segundo plano.
Mistral integrada ao executor como colaboradora de contexto com
`mistral-small-latest` como modelo preferido, ainda sem disponibilidade validada.
Apos a atualizacao da chave pelo usuario, a consulta de catalogo foi aceita.
Um unico teste minimo de inferencia retornou HTTP 429. Nao houve retry automatico.
Esse codigo nao permite distinguir limite temporario, cota ou restricao do plano
sem diagnostico adicional; verificar limites da conta antes de novo teste.
Historico anterior a substituicao da chave: consulta direta a `https://api.mistral.ai/v1/models`
com `Authorization: Bearer` retornou HTTP 401 e `{"detail":"Invalid API Key"}`.
Verificados: chave carregada, sem espacos, aspas ou prefixo Bearer duplicado;
sem proxy configurado; diferente das demais chaves de IA. O erro de rotulo local
ja foi corrigido. A API rejeitava a credencial anterior. Nenhuma geracao foi
solicitada naquele diagnostico.
Para consultar so esse provedor: `python tools/agents.py probe --provider mistral`.
Os codigos HTTP, isoladamente, nao provam saldo insuficiente nem chave invalida.
O teste DeepSeek confirma resposta minima, nao qualidade de codigo ou de assets.

Limites e papeis em `agents.json`; regras em `AGENTS.md`.
Registros locais em `.agents-local/availability.json`, `calls.json` e `cache/`.
Nao existe ainda limite monetario verificado, apenas limites de chamadas e tamanho.

## Referencias de integracao

- Gemini: https://ai.google.dev/api/models
- Groq: https://console.groq.com/docs/models
- DeepSeek: https://api-docs.deepseek.com/api/list-models/
- DeepSeek geracao: https://api-docs.deepseek.com/api/create-chat-completion/
- Mistral: https://docs.mistral.ai/api

## Proximo passo

TypeSafe: corrigido o rotulo `TYPESAVE_API-KEY` para `TYPESAFE_API_KEY`.
Uma consulta de catalogo e uma inferencia minima foram bem-sucedidas.
Resposta do teste de reconhecimento de movimentos de personagem: Noul 0.97.
Isso valida conectividade e formato; nao avalia qualidade em tarefas reais.
Integracao HTTP propria conforme https://docs.typesafe.ai/api/system-one,
com cache, contador diario compartilhado e sem retries automaticos.
Entrada de tarefa TypeSafe: JSON contendo somente `state` e `questions`,
com 1 a 4 perguntas do tipo `noul`, cada uma com `instructions` e `criteria`
opcional. Limite de 16000 caracteres inclui todo esse JSON. Nao usa o parametro
de limite de saida dos modelos de chat; saida limitada pela quantidade e tipo
de perguntas. Nao ativados contexto longo ou imagens.
Exemplo: `{"state":"O heroi deve saltar.","questions":{"movimento":{"type":"noul","instructions":"O texto descreve movimentos de personagem?"}}}`.
Uso: `python tools/agents.py task --provider typesafe --file tarefa.json`.
TypeSafe auxilia a selecionar contexto; nao substitui um gerador de codigo ou texto.

Aguardar a instrucao de inicio e o conceito do jogo. Definir a engine com base
na plataforma alvo e no hardware; instalar e validar o editor nessa etapa.
