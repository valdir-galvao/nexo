# Fase 34 - Diagnostico e correcao iOS

O log do run 40826804213/job 123291599782 registrou:

`Failed to resolve the latest game-ci CLI release: GitHub API returned 403.`

A falha ocorria no `game-ci/unity-builder@v6` antes da abertura do Unity. O workflow dependia da descoberta dinamica da release mais recente do CLI, sujeita a rate limit/permissao da API GitHub.

Correcao aplicada em `unity-tests.yml`, `unity-android.yml` e `unity-ios.yml`:

```yaml
cliVersion: v0.1.72
gitPrivateToken: ${{ github.token }}
```

A versao `v0.1.72` existe no repositorio oficial `game-ci/cli`. O token nativo do workflow evita acesso anonimo ao GitHub quando a action precisar consultar o repositorio.

Validacao local: YAML PASS; `python tools/run_static_qa.py` PASS (`json=5`, `cs=24`); `git diff --check` PASS.

Validacao remota P81: iOS passou pela fase de resolucao do CLI e entrou no GameCI apos a correcao; o erro 403 nao reapareceu. A execucao foi cancelada por falta de progresso prolongada antes de artefato.
