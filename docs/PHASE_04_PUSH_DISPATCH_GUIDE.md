# Fase 04 — guia de push e disparo remoto

## Estado confirmado

- Branch local: `orchestrator/phase-a-baseline`.
- HEAD: `2d0cf2d`.
- Árvore de trabalho: limpa.
- `9499032` contém K-005, assembly/teste PlayMode e metadados `.meta`.
- `2d0cf2d` contém o script de resolução Unity e o relatório CI.
- Arquivos relevantes e `.meta` estão rastreados.

## Push seguro da branch

```powershell
git fetch origin
git status --short
git log --oneline -3
git push --set-upstream origin orchestrator/phase-a-baseline
```

O comando publica somente a branch isolada. Não fazer force push nem alterar
`main` diretamente.

## Pré-requisitos no GitHub

Em **Settings → Secrets and variables → Actions**, configurar sem imprimir os
valores:

- `UNITY_LICENSE`
- `UNITY_EMAIL`
- `UNITY_PASSWORD`

## Ordem de execução

1. Abrir **Actions → Unity tests → Run workflow**.
2. Selecionar a branch `orchestrator/phase-a-baseline`.
3. Aguardar o job e baixar logs, JUnit e artefatos.
4. Confirmar importação, resolução de pacotes e compilação antes de considerar
   os testes.
5. Só se os testes passarem, executar manualmente **Unity Android build**.
6. Baixar e verificar `Nexo.apk` e `BUILD_METADATA.txt`.
7. Depois, executar **Unity iOS Xcode project**; o resultado é Xcode não
   assinado, não um pacote instalável em iPhone.

## Limitação importante

O workflow atualmente configurado executa apenas `testMode: editmode`. O teste
`Nexo.PlayModeTests` foi versionado, mas não será executado até existir um job
PlayMode explícito no workflow. Não relatar cobertura PlayMode como aprovada.

Nenhum push ou dispatch foi executado nesta sessão.
