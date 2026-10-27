# Fase 08 — Diagnóstico GameCI e licenciamento

Data: 27/10/2026, America/Sao_Paulo  
Branch: `orchestrator/phase-a-baseline`  
Commits diagnosticados: `7920de8` (captura inicial) e `bb06877` (log montado no workspace).

## Evidências

- YAML dos quatro workflows parseado com PyYAML; `git diff --check` passou.
- Push de `bb06877` concluído sem alterar `main`.
- Runs disparados pelo push: testes `40459086676`, Android `40459086685`, iOS `40459086672`.
- Testes: EditMode e PlayMode falharam na etapa `Run Unity tests` após importação do job; não houve JUnit/testes descobertos. Artefatos de resultado contêm apenas `BUILD_METADATA.txt`.
- Android: falhou em `Build Android package`; não houve APK. Diagnóstico `12712639643` não contém `Editor.log`.
- iOS: run `40459086672` permanecia em execução na última consulta; build não aprovado.
- Diagnósticos anteriores e atuais não encontraram log Unity no host. A segunda tentativa passou `-logFile /github/workspace/...`, mas o artefato permaneceu sem log, indicando falha antes da inicialização útil do Editor ou incapacidade do contêiner em persistir o arquivo.
- API de Actions listou `COUNT=0` secrets no repositório. Os workflows referenciam `UNITY_LICENSE`, `UNITY_EMAIL` e `UNITY_PASSWORD`.

## Causa e classificação

Causa primária comprovada: **licenciamento bloqueado por ausência dos três secrets no repositório**. Sem eles, GameCI não consegue ativar o Unity Personal. Não há evidência de que a execução tenha chegado à compilação C# ou à resolução final de pacotes; portanto C#/packages ficam `NOT_DETERMINED`, não aprovados nem reprovados.

Estado: **BLOCKED — dependência externa de credenciais Unity**.

## Correção aplicada

Foi adicionada captura pós-falha e `customParameters` de `-logFile` aos workflows de testes, Android e iOS. A correção é diagnóstica e não altera código de jogo, dependências ou assinatura. Secrets não foram criados nem modificados.

## Próximo passo seguro

Configurar, no repositório GitHub e fora do código, os secrets exigidos usando um método de licença Unity válido e autorizado; depois repetir primeiro `Unity tests`. Só classificar C#/packages após uma execução que ultrapasse a ativação/importação do Editor. Não iniciar builds de dispositivo antes desse gate.
