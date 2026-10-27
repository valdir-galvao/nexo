# Fase 13 — auditoria do pedido de licença automática GameCI

Data: 27/10/2026, America/Sao_Paulo  
Estado: `BLOCKED`.

A documentação oficial atual do GameCI informa que Unity Personal exige ativação manual única e secret `UNITY_LICENSE` com o conteúdo do `.ulf`; `UNITY_EMAIL` e `UNITY_PASSWORD` também são usados na ativação. `unityLicensingServer` é destinado a servidor de licenças próprio, não à licença Personal gratuita.

Por isso, não removi `UNITY_LICENSE`, não excluí `unity-activation.yml`, não adicionei `unityLicensingServer` fictício e não fiz push de uma configuração inválida. Os workflows já usam `game-ci/unity-builder@v6` e `game-ci/unity-test-runner@v4`.

Evidência: https://game.ci/docs/github/activation/ e https://game.ci/docs/github/builder/.

Próxima ação: configurar uma licença Personal válida no fluxo suportado ou fornecer uma decisão técnica autorizando outro método real de licenciamento.
