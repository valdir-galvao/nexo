# PHASE 119 — Unity Tests dispatch diagnostic

Data: 2026-11-12 (America/Sao_Paulo)

## Resultado

- Workflow: `Unity tests`
- Run: `43330830350`
- Commit remoto testado: `ee8ec51ea60bd7e2f1e36e14cb1ba9ea6947c406`
- URL: https://github.com/valdir-galvao/nexo/actions/runs/43330830350
- Conclusão: `FAIL`
- Duração observada: aproximadamente 4 segundos
- Jobs: `editmode tests` e `playmode tests`, ambos `failure`, sem steps iniciados
- Logs: indisponíveis (`gh run view --log-failed` retornou `log not found`)

## Interpretação

O workflow foi aceito pelo dispatch, mas falhou antes de alocar/executar o runner. Portanto, este run não executou Unity, não descobriu testes e não produziu artefatos. A evidência não aponta para erro C# ou PlayMode; confirma o bloqueio externo de execução GameCI/conta já observado nos runs Android/iOS.

Nenhum build Android ou iOS foi disparado nesta rodada. O repositório permaneceu privado.
