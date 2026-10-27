# NEXO — QA Report P7

Data: 27/10/2026. Commit sob teste: preencher após o workflow GameCI.

## Resultado executivo

Integração de domínio e contratos verificada estaticamente. Testes de fluxo,
migração e offline foram adicionados, mas não executados pelo Unity porque o
Editor não está instalado neste Windows. Android/iOS e dispositivos físicos ficam
pendentes; não declarar compatibilidade completa.

## Matriz de execução

| Área | Método | Resultado |
|---|---|---|
| JSON/ASMDEF | `tools/run_static_qa.py` | PASS local |
| Conteúdo pedagógico | `tools/validate_learning_content.py` | PASS local |
| Regras unitárias | Unity EditMode | PENDENTE: Unity ausente |
| Fluxo ponta a ponta | `IntegrationFlowTests` | PENDENTE: Unity ausente |
| Save/retomada | `IntegrationFlowTests` + migração V0 | PENDENTE: Unity ausente |
| Offline/rede | auditoria de dependências + `SafetyPolicyTests` | auditoria PASS; teste Unity pendente |
| Telas/safe area/input | Android/iOS físico | PENDENTE |
| FPS/memória/draw calls | Unity Profiler em dispositivo | PENDENTE |

## Fluxo obrigatório especificado

O teste automatizado cobre iniciar, pular prólogo, investigar três pistas,
errar válvula com feedback, pausar/retomar, resolver, concluir, salvar e reabrir.
A demonstração de aprendizagem é representada pelo contrato de transferência e
regras P5; sua execução em UI precisa ser validada no Unity.

## Procedimento de execução futura

1. Configurar os secrets Unity no GitHub.
2. Executar `Actions → Unity tests` e guardar artifact.
3. Registrar cada teste EditMode/PlayMode, commit, runner e duração.
4. Executar Android e iOS; testar rede desligada, suspensão, exclusão e safe area.
5. Corrigir defeitos, repetir a matriz e atualizar este relatório.

Nenhum teste com menores ou coleta de dados reais foi realizado.
