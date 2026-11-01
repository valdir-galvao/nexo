# Diagnóstico de credenciais locais e acesso GitHub

Data: 2026-11-01

## Constatações

- Existe `.env.txt`, mas ele é ignorado por `.gitignore` (`.env` e `.env.*`). Não foi lido nenhum valor.
- As chaves presentes incluem `senha_githup`, mas `tools/agents.py` só carrega chaves listadas em `agents.json` para provedores de IA. `senha_githup` não pertence a esse conjunto e é deliberadamente ignorada.
- O executor local foi projetado para DeepSeek/Gemini/Groq/Mistral/TypeSafe; não implementa autenticação GitHub, Actions, Secrets ou alteração de visibilidade.
- O Git usa `credential.helper manager`, que permite autenticação separada para operações Git, como demonstrado por `git ls-remote`. Isso não cria automaticamente `GITHUB_TOKEN`, `GH_TOKEN` nem uma sessão REST/Actions.
- Não existem variáveis de ambiente `GITHUB_TOKEN` ou `GH_TOKEN` disponíveis. Nenhum valor de `.env.txt` foi exposto.

## Por que a credencial não foi usada

Uma senha ou chave arbitrária em `.env.txt` não é um token OAuth GitHub e não deve ser enviada para a API. A API/Actions exige um mecanismo próprio (GitHub CLI autenticada, OAuth/PAT apropriado, GitHub App ou conector autorizado). O agente não pode inferir escopos nem transformar `senha_githup` em token; fazer isso seria inseguro e poderia vazar segredo.

O Git credential manager serve ao transporte Git e não fornece automaticamente autorização para consultar Actions, Secrets ou mudar visibilidade. Por isso o push/read Git pode funcionar enquanto a API retorna 404 anônimo e o browser mostra `Sign in`.

## Resultado

Nenhuma credencial foi alterada, copiada, exibida ou usada em nova requisição. Para habilitar Actions, é necessária uma integração GitHub autenticada explicitamente no Orca/browser/conector ou uma sessão `gh` já autorizada; não é correto reutilizar a senha armazenada no `.env.txt`.
