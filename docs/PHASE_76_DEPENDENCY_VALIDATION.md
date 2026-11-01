# NEXO — Fase 76: validação de dependências bloqueada com segurança

## Preservação

- Commit da correção visual preservado: `54e67ec`.
- Relatório preservado: `docs/PHASE_75_FORENSIC_ROOT_CAUSE.md`.
- Nenhuma reversão do bootstrap ou da geometria verde foi feita.
- Nenhum novo APK/IPA foi gerado nesta fase.
- Repositório permanece privado.

## Unity e lockfile

A versão exigida pelo projeto é `6000.3.0f1` (`ProjectSettings/ProjectVersion.txt`). Ela não está instalada nesta máquina. O único Editor encontrado é `C:/Program Files/Unity/Hub/Editor/6000.6.7f1/Editor/Unity.exe`. Docker também não está disponível.

A comparação feita por parser JSON confirmou:

- `Packages/manifest.json`: 36 dependências declaradas.
- `Packages/packages-lock.json`: 2 entradas.
- 34 dependências declaradas estão ausentes no lockfile, incluindo `com.unity.textmeshpro`, `com.unity.test-framework`, `com.unity.ugui` e módulos Unity.

O lockfile não foi editado manualmente e não foi regenerado com Unity 6000.6.7f1, pois isso violaria a exigência de resolução com Unity 6000.3.0f1 e poderia introduzir diferenças de pacote.

## TextMeshPro

`EnsureIncludedShaders.cs` procura `TextMeshPro/Distance Field` e registra aviso quando `Shader.Find` retorna nulo. O manifesto declara TMP 3.0.6, mas o lockfile não fixa o pacote; além disso, não existem componentes TMP na cena ou scripts que usem TMP. Portanto o aviso é um risco de importação/dependência, não uma falha de gameplay comprovada.

A confirmação exigida — importação do pacote, presença dos recursos TMP e `Shader.Find` não nulo — permanece BLOCKED até instalar Unity 6000.3.0f1 e resolver o projeto pelo Package Manager.

## Outros erros

- `Soft.psd`: nenhuma ocorrência em arquivos versionados.
- `Default-Skybox.mat`: nenhuma ocorrência em arquivos versionados; a cena tem `m_SkyboxMaterial: {fileID: 0}` e o bootstrap usa Solid Color.
- `EditorBuildSettings.asset`: YAML atual correto, cabeçalho `!u!104`, `EditorBuildSettings`, uma cena habilitada no índice 0.
- `SIGSEGV`: falha nativa histórica do Editor/runner durante reload de assemblies antes do PlayMode; o run posterior `41214944859` passou.
- Partículas: material explícito aplicado no commit `54e67ec`; confirmação dos indicadores `[NEXO_RENDER]` ainda requer execução Android, deliberadamente não realizada.

## QA local

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=26`)
- `git diff --check`: PASS
- Build Android: NOT_RUN nesta fase
- Appetize, Android ARM64 físico e iOS/Metal: NOT_RUN

## Próxima ação necessária

Instalar exatamente Unity 6000.3.0f1 com módulo Android/iOS conforme necessário, abrir o projeto sem alterar o código, deixar o Package Manager resolver as 36 dependências, revisar o diff do lockfile, executar EditMode/PlayMode e só então autorizar um smoke test Android com captura `[NEXO_RENDER]`.
