# NEXO — Fase B2: auditoria de GitHub Actions e GameCI

Data: 27/10/2026, America/Sao_Paulo. Branch: `orchestrator/phase-a-baseline`.
Commit inicial da auditoria: `e59d126`. Nenhum workflow foi disparado.

## Matriz por workflow

| Workflow | YAML | Gatilho | Runner | Secrets | Testes/build | Artefatos | Estado |
|---|---|---|---|---|---|---|---|
| `unity-activation.yml` | PASS, PyYAML | manual | `ubuntu-latest` | nenhum | solicita `.alf` manual | activation + `run_id` | PASS estático |
| `unity-tests.yml` | PASS, PyYAML | manual e `pull_request` | `ubuntu-latest` | três Unity | EditMode apenas | resultados sempre; metadata com SHA/run | PASS estático; PlayMode ausente |
| `unity-android.yml` | PASS, PyYAML | manual | `ubuntu-latest` | três Unity | APK via builder v6 | diretório Android + SHA/run | PASS estático; execução bloqueada |
| `unity-ios.yml` | PASS, PyYAML | manual | `macos-latest` | três Unity | projeto Xcode não assinado | diretório iOS + SHA/run | PASS estático; execução bloqueada |

Os gatilhos automáticos `push` dos workflows Android e iOS foram removidos
nesta fase. Builds de plataforma agora exigem `workflow_dispatch` explícito.

## Verificações

- PyYAML `6.0.3` parseou os quatro arquivos sem erro.
- Todos usam permissões `contents: read`.
- Actions encontradas: checkout v4, cache v4, upload-artifact v4,
  `unity-test-runner@v4`, `unity-builder@v6` e activation action v2.
- Unity está fixada em `6000.3.0f1`, coerente com
  `ProjectSettings/ProjectVersion.txt`.
- Falha de uma etapa interrompe as etapas seguintes por comportamento padrão do
  GitHub Actions; não há `continue-on-error`.
- O job de testes não possui dependências entre jobs: existe somente EditMode.
- Testes PlayMode não são executados por nenhum workflow atual. O projeto não
  possui job PlayMode configurado; isso requer Unity Editor/GameCI e uma decisão
  de cobertura, não apenas uma troca textual.
- iOS chama `BuildTarget.iOS` e faz upload do projeto Xcode. Não há `xcodebuild`,
  assinatura, provisionamento, instalação ou distribuição.
- Não há publicação em lojas, deploy externo ou operação destrutiva.
- Android e iOS usam `BUILD_METADATA.txt` com plataforma, Unity, SHA e `run_id`.
  Retenção explícita não foi configurada; aplica-se o padrão do GitHub Actions.

## Secrets e ativação

Os nomes `UNITY_LICENSE`, `UNITY_EMAIL` e `UNITY_PASSWORD` aparecem nos três
jobs que executam Unity (testes, Android e iOS). A action de ativação apenas gera
um arquivo `.alf` e não cria nem altera secrets. A presença dos secrets no
GitHub não é verificável por inspeção local: estado `NÃO VERIFICÁVEL`.

Na configuração atual, os três são necessários para o método de autenticação
declarado pelos jobs GameCI. Nenhum valor foi lido, impresso, copiado ou
registrado.

## Riscos restantes

1. Execução real ainda depende de secrets e runners GameCI.
2. EditMode/PlayMode não foram executados; PlayMode não está coberto pelo CI.
3. Retenção de artefatos não está explícita.
4. Metadata de build só é escrita após sucesso; falhas de build não geram
   metadata própria.
5. O workflow iOS não é um pacote instalável e não valida assinatura.

## Resultado

**Pipeline pronto para teste de diagnóstico Unity:** `SIM, ESTATICAMENTE` — o
workflow manual `unity-tests.yml` está sintaticamente coerente e não dispara
build de plataforma.

**Pipeline pronto para execução real:** `BLOCKED` — secrets não verificáveis,
Unity/GameCI não executados e PlayMode ausente. Nenhum build Android/iOS foi
disparado nesta fase.
