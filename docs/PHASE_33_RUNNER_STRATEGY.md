# Fase 33 - Estrategia de runner

Data: 29/10/2026 America/Sao_Paulo

## Alteracao

Removido `jlumbroso/free-disk-space@v1.3.1` dos workflows Unity. Os tres workflows agora executam um step bash curto:

```bash
set -euo pipefail
sudo rm -rf /usr/share/dotnet /torch /usr/local/lib/android/sdk/sample-data 2>/dev/null || true
df -h
```

A limpeza e tolerante a caminhos ausentes e nao falha por arquivos opcionais inexistentes. O step foi adicionado tambem ao iOS para manter o comportamento uniforme.

## Validacao

- YAML parser: PASS nos cinco workflows.
- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=24`).
- `git diff --check`: PASS.
- Nenhuma execucao remota iniciada; o repositorio permanece privado.

## Recomendacao

A estrategia remove o gargalo observado no step de limpeza de disco. Antes de uma nova rodada GameCI, liberar billing/quota de runners privados ou configurar runner self-hosted. Depois executar smoke manual, Unity tests e somente entao Android/iOS.
