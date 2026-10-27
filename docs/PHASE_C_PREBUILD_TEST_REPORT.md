# NEXO — Fase C: testes completos antes dos pacotes móveis

**Resultado:** `BLOCKED`

Commit auditado: `37b4246fc46da294e8aca3a636a73ebc27f929be` na branch
`orchestrator/phase-a-baseline`. Nenhum pacote Android/iOS foi gerado.

## Dependências

`Packages/manifest.json` declara 36 dependências; `packages-lock.json` contém
somente 2 entradas (`com.unity.render-pipelines.universal` e
`com.unity.inputsystem`). Não há entradas extras no lockfile. A diferença é
anormal para uma resolução final, mas não foi corrigida manualmente: Unity
Package Manager deve regenerar/confirmar o lockfile em ambiente compatível.

Versões declaradas: Unity `6000.3.0f1`, URP `17.3.0`, Input System `1.14.0`,
Test Framework `1.5.1`. Resolução real: `BLOCKED` porque Unity Editor/GameCI não
foi executado.

## Assemblies e cobertura

- `Nexo.Runtime`: assembly runtime sem referências explícitas adicionais.
- `Nexo.Tests`: `includePlatforms: ["Editor"]`, referências a
  `UnityEngine.TestRunner`/`UnityEditor.TestRunner`.
- Foram encontrados 15 testes NUnit `[Test]` em cinco classes.
- Não foram encontrados `[UnityTest]` nem testes PlayMode.
- O workflow atual executa somente EditMode; PlayMode não está configurado.

Assim, a assembly Editor não é uma prova de bloqueio de lógica, mas confirma
que a suíte existente não cobre runtime/PlayMode. Nenhum teste Unity foi
descoberto ou executado nesta sessão.

## Persistência e fluxo

O código e os testes estáticos cobrem save em memória, migração V0, validação de
limites, fluxo da missão e retomada. Gravação real, corrupção de arquivo,
interrupção, reinicialização, JSON inválido e Package Manager permanecem
`NOT_RUN` sem Unity/runtime.

A cena `NexoVerticalSlice.unity` existe e seu GUID corresponde ao `.meta` e ao
`EditorBuildSettings.asset`; isso não comprova que todas as referências sejam
resolvidas pelo Unity.

## K-005 — transferência pedagógica

Origem: `docs/KNOWN_ISSUES.md`, item K-005; a mesma lacuna é explicitada em
`docs/QA_REPORT.md` (seção “Fluxo obrigatório especificado”). Requisito:
demonstrar aprendizagem em uma tarefa de transferência e registrar a resposta
localmente. Implementado: contrato/conteúdo de transferência em
`Assets/NEXO/Data/mission-silent-reservoir-learning.json` e regras de domínio em
`Assets/NEXO/Scripts/Learning/LearningAssessment.cs`. Ausente: tela/fluxo UI que
apresente a tarefa, aceite a resposta e registre `TransferCorrect` ou
`TransferIncorrect` durante a missão.

Impacto: a missão pode concluir narrativamente sem comprovar a demonstração de
aprendizagem exigida. Correção mínima proposta: integrar uma etapa de UI após a
conclusão do puzzle, persistir a evidência de transferência e cobrir o fluxo com
teste Unity. Isso fica pendente até haver Unity/GameCI para validar a integração;
nenhuma correção foi aplicada nesta fase.

## Validadores locais

- `python tools/validate_learning_content.py`: PASS, conteúdo válido.
- `python tools/run_static_qa.py`: PASS (`json=4`, `cs=23`).
- EditMode: `NOT_RUN` — Unity/GameCI bloqueado.
- PlayMode: `NOT_RUN` — sem Unity e sem testes PlayMode configurados.
- Persistência runtime: `NOT_RUN`.
- Fluxo em cena/dispositivo: `NOT_RUN`.

## Próxima ação

Disponibilizar dispatch autenticado e secrets Unity; executar `unity-tests.yml`
com resolução real de pacotes e relatórios separados. Depois corrigir K-005,
adicionar teste de integração adequado e repetir a suíte antes de qualquer build.
