# Fase 10 — Unity Personal e parâmetros GameCI

Data: 27/10/2026, America/Sao_Paulo  
Branch: `orchestrator/phase-a-baseline`.

## Alterações

- `unity-android.yml`: substituído `buildPath` por `buildsPath: build/Android`.
- `unity-ios.yml`: substituído `buildPath` por `buildsPath: build/iOS`.
- Mantida a captura de falha e acrescentado o dump solicitado para `~/Library/Logs/Unity/Editor.log` e `~/.config/unity3d/Unity/Editor/Editor.log`.
- `unity-activation.yml`: documentado o fluxo de ativação **Unity Personal gratuito** por arquivo `.alf`/`.ulf`, sem serial Pro/Plus.

## Licenciamento

Os workflows de testes e builds continuam usando `UNITY_LICENSE`, `UNITY_EMAIL` e `UNITY_PASSWORD`. A configuração desses valores não pode ser feita no repositório nem inventada pelo agente. A API havia confirmado zero secrets; portanto a ativação ainda está `BLOCKED` até o responsável configurar uma licença Personal válida no GitHub Secrets.

## Validação

- PyYAML parseou os quatro workflows.
- `git diff --check` passou.
- Nenhum build foi disparado por esta validação local.

Estado: `BLOCKED` por secrets externos ausentes; correções de workflow concluídas.
