# PHASE 83 — Auditoria forense da infraestrutura original

Data: 2026-11-01 (America/Sao_Paulo)
Branch auditada: `orchestrator/phase-a-baseline`
Commit atual: `47f56a7`
Escopo: somente leitura e documentação. Nenhum workflow foi disparado; nenhum código, secret, visibilidade, dependência ou configuração de build foi alterado.

## 1. Arquitetura encontrada

O projeto é Unity 6, com versão declarada em `ProjectSettings/ProjectVersion.txt` como `6000.3.0f1`. A estrutura contém cena vertical slice (`Assets/Scenes/NexoVerticalSlice.unity`), scripts runtime/editor, testes EditMode/PlayMode, dados pedagógicos, persistência, diagnóstico de renderização e workflows GitHub Actions. Os scripts de build são `Assets/Scripts/Editor/Build/AndroidBuild.cs` e `IosBuild.cs`; a validação local é feita por `tools/run_static_qa.py`, `tools/validate_learning_content.py` e `tools/test_agents.py`.

## 2. Workflows e integração

| Workflow | Gatilhos | Runner | Execução | Artefatos/integrações | Estado auditado |
|---|---|---|---|---|---|
| `unity-tests.yml` | `workflow_dispatch`, push na branch de trabalho, PR filtrado | `ubuntu-latest` | GameCI `unity-test-runner@v4`, matriz EditMode/PlayMode | JUnit/logs e diagnósticos de falha | Configurado; execução nova não verificável nesta sessão |
| `unity-android.yml` | `workflow_dispatch`, push na branch de trabalho | `ubuntu-latest` | GameCI `unity-builder@v6`, Android, cleanup/cache, custom headless flags | APK, `BUILD_METADATA.txt`, diagnósticos; job AVD dependente | Configurado; histórico misto de sucesso, falta de espaço e hang |
| `unity-ios.yml` | `workflow_dispatch`, push na branch de trabalho | `macos-latest` | GameCI `unity-builder@v6`, exportação Xcode/IPA não assinado | IPA/Xcode, metadata, diagnósticos | Configurado; sucessos históricos e cancelamentos por runner |
| `android-emulator-smoke.yml` | dispatch ou `workflow_run` após Android SUCCESS | `ubuntu-latest` | `reactivecircus/android-emulator-runner@v2`, API 35 x86_64/SwiftShader | logcat, dumpsys, screenshot, runtime log | Integração existente e historicamente executada |
| `runner-smoke.yml` | dispatch | Ubuntu selecionável | health check sem Unity | relatório de runner | Isolado/manual |
| `unity-activation.yml` | dispatch | Ubuntu | apenas solicita `.alf` via `unity-request-activation-file@v2` | `.alf` | Não completa a ativação ULF |
| `github-free-capacity.yml` | dispatch בלבד | Ubuntu | mede disco/RAM/CPU e checkout; não instala Unity | relatório pequeno | Criado em `47f56a7`; não disparado |

Os workflows de teste/build usam `6000.3.0f1`, mas atualmente injetam somente `UNITY_EMAIL`/`UNITY_PASSWORD`; não há `UNITY_LICENSE` no ambiente do job. A ativação Personal documentada pelo GameCI requer ULF em `UNITY_LICENSE` junto com email/senha. Portanto, a configuração atual não prova ativação válida.

Não foram encontradas referências a Appetize.io, BrowserStack, Genymotion ou Appium no repositório. A integração original de emulação é o AVD headless via `reactivecircus/android-emulator-runner`, não um serviço Appetize.

## 3. Emulador e APK

`unity-android.yml` constrói o APK e possui job dependente que baixa o artefato do mesmo run, inicia AVD API 35 x86_64 com SwiftShader e executa `tools/android_smoke_test.sh`. O workflow separado `android-emulator-smoke.yml` repete esse modelo para um artefato de um run anterior. Os documentos `docs/PHASE_73_RELEASE_VALIDATION.md` e `docs/EXECUTION_REGISTER.md` registram execução histórica real: instalação ADB SUCCESS, cena `NexoVerticalSlice` carregada, OpenGL ES 3/SwiftShader e ausência de exceções Unity/shader no log. Isso comprova a integração AVD, mas não comprova Appetize nem aparelhos físicos.

## 4. Dependências Unity

`manifest.json` declara 36 dependências, incluindo URP, Input System, UGUI, Test Framework e TextMeshPro 3.0.6. `packages-lock.json` contém apenas 2 entradas observadas. A diferença de 34 entradas é uma inconsistência de resolução/lockfile; não prova que 34 bibliotecas estejam ausentes do registry. O lockfile não foi editado manualmente. A correção segura requer abrir o projeto com Unity 6000.3.0f1 e deixar o Package Manager regenerá-lo.

TextMeshPro 3.0.6 está declarado, mas não resolvido no lockfile atual; por isso `Shader.Find("TextMeshPro/Distance Field")` não pode ser validado nesta máquina. UGUI e Test Framework também estão declarados, porém a resolução real não foi executada. A única instalação local utilizável é Unity 6000.6.7f1, que não foi usada como substituta.

## 5. Histórico e ponto de quebra

- `54e67ec`: material Unlit explícito para partículas e diagnóstico `[NEXO_RENDER]`; correção visual preservada.
- `1500265`: registra bloqueio do ambiente Unity exato e lockfile manifest/lock inconsistente.
- `d847108`: comprova falha de instalação do Unity 6000.3.0f1 em P: com `Access denied`.
- `ab3c6e0`: documenta que a migração cloud Windows não estava provisionada; workflows existentes permanecem Ubuntu/macOS.
- `439c73d`: investiga custo zero; identifica limites de disco e licença/imagem.
- `2e625d7`: confirma disponibilidade oficial de Linux 6000.3.0f1, sem prova de execução em runner.
- `47f56a7`: adiciona probe isolado de capacidade GitHub Free, ainda não despachado.

O histórico mostra dois bloqueios independentes: (1) instalação/resolução local impossibilitada por Unity exato ausente e destino P: em NFS; (2) CI GameCI sujeito a licença ULF ausente no job, disponibilidade não comprovada de imagem/tag exata e pressão de armazenamento/hangs no runner. Houve runs remotos históricos bem-sucedidos, mas também falhas de licença/CLI, SIGSEGV do Editor, falta de espaço e cancelamentos de build.

## 6. Matriz de causa raiz

| Componente | Existência | Estado atual | Causa do problema | Ação necessária |
|---|---|---|---|---|
| GitHub Actions | Existe | Configurado, não verificável agora | Sem CLI/API autenticada; quota/runner não medidos nesta sessão | Executar probe e consultar runs com acesso GitHub autenticado |
| Unity Editor | Existe apenas outra versão | `6000.3.0f1` não instalado localmente | P: é NFS e InstallDriver falha; C: sem espaço | Disponibilizar volume NTFS/runner e instalar versão exata |
| Package Manager | Manifest e lock existem | Inconsistente (36 vs 2) | Lockfile não regenerado pelo Editor exigido | Abrir Unity 6000.3.0f1 e regenerar via Package Manager |
| Android Build | Workflow e script existem | Histórico misto | imagem/licença/espaço/hang; não revalidado | Após dependências e licença, rodar testes; só depois build |
| Emulador | AVD workflow existe | Historicamente validado | Appetize não existe; execução nova não feita | Reusar AVD existente e preservar artefatos/logcat |
| Integração APK | Download/upload existe | Historicamente validada | depende de run Android SUCCESS | Baixar artefato do mesmo run e executar smoke |
| EditMode | Matriz e testes existem | Histórico PASS em runs remotos | atual não executado com dependências resolvidas | Executar GameCI após gate de licença/Editor |
| PlayMode | Matriz, assembly e cena existem | Histórico passou após correções, mas houve SIGSEGV e falha de cena | ambiente/perfil/carga nativa | Executar com Unity exato e relatório JUnit/Editor.log |

## 7. O que funciona e o que não está comprovado

Funciona/validado historicamente: workflows versionados e parseáveis; Static QA/YAML/diff check; build Android e IPA em runs anteriores; AVD API35 x86_64/SwiftShader; upload/download de artefatos; diagnóstico de renderização e material explícito de partículas.

Não comprovado nesta auditoria: dispatch do `github-free-capacity.yml`; capacidade/quota atual do GitHub; ativação Unity Personal no job; instalação/resolução do Unity 6000.3.0f1; regeneração do lockfile; EditMode/PlayMode atuais; novo APK/IPA; Appetize, simulador iOS e dispositivos físicos.

## 8. Plano mínimo de recuperação sem custo

1. Usar acesso GitHub autenticado no repositório privado e executar apenas `github-free-capacity.yml`; guardar o artefato de medição.
2. Confirmar que `UNITY_LICENSE` contém um ULF Personal válido e que `UNITY_EMAIL`/`UNITY_PASSWORD` existem, sem expor valores.
3. Confirmar uma imagem GameCI ou instalação Linux que suporte exatamente Unity 6000.3.0f1; não substituir por outra versão.
4. Se o runner gratuito não comportar Editor+módulos, manter o bloqueio explícito; não editar o lockfile nem iniciar build.
5. Quando o Editor exato estiver disponível, resolver o Package Manager, executar EditMode e PlayMode, e só com ambos PASS rodar Android e AVD smoke.
6. Manter o repositório privado e usar somente recursos gratuitos efetivamente disponíveis.

## Resultado

`FASE 83: PASS` para a auditoria documental/forense somente leitura. `BLOCKED` para execução Unity/Package Manager/novos builds por ausência de Editor 6000.3.0f1 local, falta de execução autenticada do probe/CI e licença ULF não verificável no ambiente. Nenhum workflow, secret, visibilidade, código ou dependência foi alterado; nenhum APK/IPA foi gerado nesta fase.
