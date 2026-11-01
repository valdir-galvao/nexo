# PHASE 87 — Diagnóstico de acesso GitHub pelo Orca

Data: 2026-11-01 (America/Sao_Paulo)
Repositório: `github:valdir-galvao/nexo`
Branch local: `orchestrator/phase-a-baseline`

## Mecanismos testados

1. **Orca project host:** `orca repo list --json` reconhece o remote `https://github.com/valdir-galvao/nexo.git` e o projeto `github:valdir-galvao/nexo`. Isso identifica o repositório no Orca, mas não prova autorização GitHub para Actions/API.
2. **Git credential manager:** `git ls-remote origin HEAD refs/heads/orchestrator/phase-a-baseline` retornou refs reais. Há autenticação suficiente para leitura Git do remote.
3. **GitHub CLI:** `gh` não está instalado.
4. **API REST anônima:** `https://api.github.com/repos/valdir-galvao/nexo` retornou HTTP 404.
5. **Browser integrado Orca:** a página mostrou `404 This is not the web page you are looking for` e link `Sign in`; não existe sessão web GitHub autenticada no browser.
6. **Ferramentas MCP:** nenhuma ferramenta GitHub/Actions foi exposta nesta sessão.

## Consequência

A leitura Git do remote funciona, mas não há um canal autenticado para:

- consultar visibilidade real via API;
- listar runs e logs;
- verificar quota;
- verificar nomes/presença dos Secrets;
- dispatchar workflows;
- alterar visibilidade.

Não alterei a visibilidade. Não executei testes ou builds. O repositório permanece sem evidência observável de público nesta sessão; o HTTP 404 anônimo é compatível com privado ou ausência de autorização e não é usado isoladamente como prova.

## Evidência adicional de sincronização

O remote respondeu:

- `HEAD`: `fc12a1ff...`
- `orchestrator/phase-a-baseline`: `54e67ec...`

A branch local está à frente do remote com documentação posterior; isso não foi enviado porque a tarefa pediu diagnóstico e não autorizou push/alteração remota.

## Por que as fases anteriores pararam

As fases 85 e 87 verificaram apenas ausência de `gh` e API anônima. Esta auditoria refinou o diagnóstico: existe credencial Git para leitura, porém não existe token/API/browser autorizado para operações GitHub Actions. Portanto não era correto inferir que o Orca tinha acesso operacional completo.

## Resultado

`ACCESS_DIAGNOSTIC: PARTIAL` — leitura Git remota comprovada; API/Actions/visibilidade não autorizadas ou não expostas. Nenhuma alteração remota foi feita.
