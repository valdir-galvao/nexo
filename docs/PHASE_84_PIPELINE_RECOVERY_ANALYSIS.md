# PHASE 84 — Análise de recuperação do pipeline original

Data: 2026-11-01 (America/Sao_Paulo)
Branch: `orchestrator/phase-a-baseline`
Commit auditado: `abdf2b0` (relatório anterior) / código de referência funcional `ce335a6`
Escopo: análise histórica e comparação local. Não houve dispatch, alteração de workflow, mudança de secret/visibilidade, resolução de pacotes ou build.

## 1. Últimas execuções aprovadas documentadas

O acesso autenticado ao histórico atual do GitHub não está disponível nesta sessão (`gh` não instalado e API sem autenticação). Os registros abaixo são as últimas evidências históricas versionadas no repositório, não uma consulta nova à nuvem:

| Fluxo | Run ID | Commit | Resultado | Evidência |
|---|---:|---|---|---|
| Unity Tests | `41211856704` | `ce335a6` | SUCCESS; EditMode/PlayMode | `docs/PHASE_73_RELEASE_VALIDATION.md` |
| Android Build | `41211856635` | `ce335a6` | SUCCESS; APK + AVD smoke | `docs/PHASE_73_RELEASE_VALIDATION.md` |
| iOS Build | `41211856610` | `ce335a6` | SUCCESS; IPA/Xcode | `docs/PHASE_73_RELEASE_VALIDATION.md` |
| Android AVD smoke | job dependente do Android `41211856635` | `ce335a6` | SUCCESS; instalação ADB, cena carregada, sem erros Unity/shader | `docs/PHASE_73_RELEASE_VALIDATION.md` |

Configuração comum registrada: Unity `6000.3.0f1`; Android AVD API 35, x86_64, `google_apis`, SwiftShader/OpenGL ES 3. A imagem exata do container GameCI não foi registrada nos artefatos/documentos disponíveis; não é seguro inventá-la.

Outras evidências posteriores também registram PASS (`41196547722`/`41196547899`, `41199267466`/`41199267494`/`41199267475`, `41059686100`/`41059686028`/`41059686091`), mas `412118...` é a última sequência completa documentada antes da auditoria visual/dependências.

## 2. Comparação funcional x atual

A comparação `git diff ce335a6 HEAD -- .github/workflows Packages Assets/Scripts/Editor/Build` encontrou apenas o novo workflow isolado `github-free-capacity.yml`. Portanto, os workflows de produção atuais são essencialmente os mesmos do commit funcional `ce335a6`; não há evidência de que uma alteração posterior de YAML tenha quebrado diretamente a configuração aprovada.

A configuração funcional preservada inclui:

- `unity-tests.yml`: matriz EditMode/PlayMode com `game-ci/unity-test-runner@v4`.
- `unity-android.yml`: `ubuntu-latest`, `game-ci/unity-builder@v6`, Unity `6000.3.0f1`, build customizado `Nexo.Editor.Build.AndroidBuild.PerformBuild`, cache, metadata e job AVD dependente.
- `unity-ios.yml`: `macos-latest`, `game-ci/unity-builder@v6`, exportação Xcode e empacotamento IPA não assinado.
- AVD: `reactivecircus/android-emulator-runner@v2`, API 35, x86_64, perfil Pixel 6, `-gpu swiftshader_indirect`, sem janela/snapshot/áudio/animações.

## 3. Primeiro ponto de ruptura identificado

O primeiro ponto de ruptura operacional comprovado não é uma mudança única de código entre `ce335a6` e o estado atual. A sequência posterior documentada mostra:

1. runs iniciais sem jobs/BlobNotFound e falhas de provisionamento;
2. falta de espaço no runner Android e hang durante `Build Android package`;
3. SIGSEGV nativo no carregamento PlayMode em um runner específico;
4. falhas de cena/perfil ativo e CLI 403 em rodadas intermediárias;
5. depois, correções de workflow e código voltaram a produzir runs PASS, inclusive `412118...`.

A partir da Fase 75, o bloqueio mudou para o ambiente de validação: Unity `6000.3.0f1` não está instalado localmente, a instalação falha em P: por NFS/Access denied e o `packages-lock.json` ficou com 2 entradas contra 36 declaradas. Assim, a causa atual é de reprodutibilidade/ambiente, não uma regressão demonstrada nos workflows de produção.

## 4. Unity, GameCI e licença

Todos os workflows e a configuração de projeto continuam declarando `6000.3.0f1`. Não há autorização para trocar a versão. A imagem GameCI histórica exata não está registrada localmente e não foi comprovada nesta sessão como ainda disponível.

Os documentos de fases anteriores indicam que os jobs atuais passam `UNITY_EMAIL` e `UNITY_PASSWORD`, mas não há prova local de que `UNITY_LICENSE` esteja disponível no job atual. A ativação Personal com GameCI requer ULF em `UNITY_LICENSE` além das credenciais. A existência/validade dos Secrets não pode ser consultada sem acesso autenticado; nenhum valor foi lido ou exposto.

## 5. Dependências e lockfile

Não houve mudança histórica comprovada que remova as 34 dependências. O manifesto atual declara 36 pacotes; o lockfile contém 2. O estado pode ser um lockfile incompleto/stale, não necessariamente bibliotecas inexistentes. A comparação histórica não autoriza edição manual: a restauração correta é executar o Unity Package Manager com `6000.3.0f1` e registrar o resultado.

## 6. Menor plano de recuperação sem custo

1. Obter acesso GitHub autenticado e consultar os runs/Secrets apenas por metadados.
2. Executar manualmente `github-free-capacity.yml`; confirmar disco, memória e quota reais.
3. Confirmar que `UNITY_LICENSE` contém ULF Personal válida, sem revelar conteúdo.
4. Confirmar que a imagem GameCI ou instalação Linux disponível suporta exatamente `6000.3.0f1`.
5. Rodar somente `unity-tests.yml`; resolver primeiro falhas de importação/licença/Package Manager.
6. Com EditMode e PlayMode PASS, executar Android e reutilizar o job AVD existente.
7. Só depois executar iOS.

Nenhuma alteração de workflow é necessária como primeiro passo; o diagnóstico deve validar o ambiente e os Secrets antes de editar infraestrutura.

## 7. Arquivos que poderão exigir alteração na próxima fase

Somente se os testes demonstrarem necessidade:

- `Packages/packages-lock.json`: regenerado pelo Unity Package Manager, nunca manualmente.
- `.github/workflows/unity-tests.yml`: apenas para corrigir uma incompatibilidade comprovada de licença/runner.
- `.github/workflows/unity-android.yml` e `unity-ios.yml`: apenas diante de falha reproduzida nos passos atuais.
- `docs/EXECUTION_REGISTER.md` e relatórios de execução: sempre que houver run real.

## Resultado

`FASE 84: PASS` para a análise histórica local, com evidências versionadas. Recuperação operacional: `BLOCKED` até haver acesso autenticado ao GitHub, medição real do runner, confirmação da licença ULF e execução com Unity `6000.3.0f1`. Nenhum workflow, secret, visibilidade, dependência ou build foi alterado.
