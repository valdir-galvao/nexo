# NEXO — Fase B3: primeiro teste Unity controlado no GameCI

**Resultado:** `BLOCKED`

## Pré-condições verificadas

- Branch: `orchestrator/phase-a-baseline`.
- Commit fixado: `8cf86c711b80f3d8dbdc938d9654809bf390b29c`.
- Árvore de trabalho: limpa antes desta documentação.
- `unity-tests.yml`: sintaxe e configuração estática validadas na Fase B2.
- Escopo: somente EditMode; não há build Android/iOS neste workflow.
- Artefatos: relatório de testes e metadata com SHA/run estão configurados.
- Licenciamento: nomes dos secrets estão referenciados, mas a presença no
  GitHub é `NÃO VERIFICÁVEL` localmente.

## Bloqueio de execução

O executável `gh` não está instalado e não há nesta sessão um mecanismo
autenticado de disparo do GitHub Actions. Sem esse mecanismo não é possível
iniciar `workflow_dispatch`, obter o identificador da execução ou preservar os
logs reais. Não tentei contornar a autenticação nem li valores de secrets.

## Resultado de testes

| Item | Estado | Evidência |
|---|---|---|
| Workflow disparado | `NOT_RUN` | Nenhum identificador de execução |
| Importação Unity | `NOT_RUN` | Runner não iniciado |
| Resolução de pacotes | `NOT_RUN` | Runner não iniciado |
| Compilação de assemblies | `NOT_RUN` | Runner não iniciado |
| Descoberta de testes | `NOT_RUN` | Sem relatório JUnit |
| EditMode | `NOT_RUN` | Sem relatório JUnit |
| PlayMode | `NOT_APPLICABLE` nesta execução | Workflow não possui job PlayMode |
| Android/iOS/device | `NOT_APPLICABLE` | Proibidos nesta fase |

Não há contagem de testes descobertos, executados, aprovados ou reprovados,
porque o runner não foi iniciado.

## Ação necessária

Disponibilizar um método autenticado para `workflow_dispatch` no repositório
`valdir-galvao/nexo`, confirmar os três secrets Unity sem expor seus valores e
fornecer o URL/ID da execução. Depois, executar somente `unity-tests.yml` e
registrar logs, relatório e commit efetivamente testado.

Nenhuma API externa, build ou instalação foi executada nesta fase.
