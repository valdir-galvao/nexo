# NEXO — P1 foundation

Status: scaffold versionável criado; importação pelo Unity e build GameCI ainda
pendentes. Data: 26/10/2026, America/Sao_Paulo.

## Entregas

- `ProjectSettings/ProjectVersion.txt`: Unity `6000.3.0f1` (baseline Unity 6.3 LTS).
- `Packages/manifest.json` e `packages-lock.json`: Input System, Test Framework e
  URP `17.3.0`, compatível com a família Unity 6.3 conforme a matriz oficial.
- `ProjectSettings/EditorBuildSettings.asset`: cena `NexoVerticalSlice` habilitada.
- `ProjectSettings/ProjectSettings.asset`: identificadores NEXO, Android API 29–35,
  ARM64 e IL2CPP como baseline.
- `Assets/Scenes/NexoVerticalSlice.unity`: cena mínima placeholder versionável.
- `Assets/Scripts/Editor/Build/AndroidBuild.cs` e `IosBuild.cs`: métodos usados
  pelos workflows GameCI para gerar APK e projeto Xcode.
- Workflows Android/iOS receberam `BUILD_PATH` explícito para os métodos customizados.

## O que ainda precisa do Unity Editor

O pacote URP está declarado, mas o pipeline ativo, renderer asset, iluminação,
câmera e input precisam ser criados/confirmados pelo Unity Editor. A cena é um
placeholder sem gameplay. A compilação não foi executada localmente nem no GitHub.

## Validação estática P1

- JSON de `Packages/manifest.json` e `packages-lock.json` deve ser válido.
- `ProjectVersion.txt` precisa coincidir com `UNITY_VERSION` nos quatro workflows.
- A cena listada em `EditorBuildSettings.asset` e o `.meta` devem ter o mesmo GUID.
- Os métodos customizados precisam existir exatamente nos namespaces configurados
  pelos workflows.
- `.env*`, `Library/`, builds e `.agents-local/` devem permanecer ignorados.

## Próximo passo verificável

Após configurar os secrets Unity no GitHub, executar primeiro `Unity tests` por
workflow manual. Se o import falhar, ajustar a versão do pacote/ProjectSettings
com a saída do Editor; não mascarar erro alterando o workflow. Depois executar
Android e inspecionar o artifact APK. iOS gera primeiro projeto Xcode não assinado.
