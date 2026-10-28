# Fase 14 — auditoria local de licença e workflows

Data: 28/10/2026, America/Sao_Paulo  
Estado: `BLOCKED` por configuração externa ausente.

## Achados

- `manual.alf` existe na raiz, tem assinatura XML `.alf` e não é uma licença `.ulf`. Ele deve ser enviado ao portal de ativação da Unity para obter o conteúdo da licença Personal.
- O arquivo estava não rastreado e sem regra de exclusão; `.gitignore` agora ignora `*.alf`, `*.ulf` e `manual.alf`.
- `unity-android.yml` e `unity-ios.yml` usam `game-ci/unity-builder@v6`, `buildsPath` e `BUILD_PATH` compatíveis com os métodos customizados do projeto.
- `unity-tests.yml` usa `game-ci/unity-test-runner@v4` e referencia as mesmas credenciais.
- `unity-activation.yml` continua compatível com o fluxo documentado de ativação manual; não foi removido.
- A API de Secrets do repositório retornou `COUNT=0`; portanto `UNITY_EMAIL`/`UNITY_PASSWORD` não estão configurados no GitHub, apesar de existirem localmente.

## Próximo passo externo

Enviar `manual.alf` ao portal de licença Unity, obter o `.ulf`, cadastrar no GitHub Actions os secrets `UNITY_LICENSE`, `UNITY_EMAIL` e `UNITY_PASSWORD`, e só então executar `unity-tests.yml`.
