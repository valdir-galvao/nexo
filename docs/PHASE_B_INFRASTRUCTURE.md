# NEXO — Fase B: infraestrutura, CI e preparação do ambiente

Execution: `NEXO-20261027-A`. Branch: `orchestrator/phase-a-baseline`.
Data: 27/10/2026.

## Resultado final

**BLOCKED** para execução de import/test/build. A configuração está coerente e
validada estaticamente, mas faltam secrets Unity e execução em runners GameCI.
Não foi simulada aprovação.

## Verificações PASS

- `ProjectSettings/ProjectVersion.txt` e os quatro workflows usam Unity
  `6000.3.0f1`.
- Pacotes declarados: URP `17.3.0`, Input System `1.14.0`, Test Framework `1.5.1`.
- YAML dos quatro workflows parseia sem erro.
- Permissão explícita `contents: read` adicionada também ao workflow de ativação.
- Jobs não usam `continue-on-error`; falha de import/test/build impede artifact de
  sucesso. Upload de testes usa `always()` apenas para preservar diagnóstico.
- Cache é separado por runner, Unity, plataforma e hash de Assets/Packages/Settings.
- Artifacts identificam plataforma, versão, commit e `run_id` por nome/metadado.
- Passo de verificação de versão falha cedo se o projeto divergir do CI.
- Não existe publicação automática, assinatura Apple ou alteração de secrets.
- `run_static_qa.py`, `validate_learning_content.py` e `git diff --check` passaram.

## Variáveis necessárias

Configurar somente em **GitHub Settings → Secrets and variables → Actions**:

- `UNITY_LICENSE`
- `UNITY_EMAIL`
- `UNITY_PASSWORD`

Nenhum valor foi lido, impresso ou armazenado. Certificados/provisioning Apple
serão necessários apenas para assinatura futura e não fazem parte desta fase.

## Mudanças realizadas

- workflows revisados sem duplicar pipelines;
- permissões mínimas declaradas;
- verificação de versão adicionada;
- nomes/metadados de artifacts enriquecidos;
- documentação CI/CD corrigida para o projeto Unity atual.

## Bloqueios e próxima ação

Unity Editor, `dotnet`, `adb` e `xcodebuild` não existem neste Windows. Secrets
Unity também não estão configurados. Próxima ação autorizada: cadastrar os três
secrets no GitHub e executar manualmente `Unity tests`; depois, somente se verde,
executar Android e iOS.
