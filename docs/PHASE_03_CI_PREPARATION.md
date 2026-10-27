# Fase 03 — preparação e validação de CI/GameCI

## Estado

**Resultado:** `BLOCKED` para execução remota; preparação estática `PASS`.

- Branch: `orchestrator/phase-a-baseline`
- Commit local: `9499032` antes desta preparação
- Unity declarada no projeto e workflows: `6000.3.0f1`
- `unity-tests.yml`: EditMode manual/Pull Request; não gera pacote móvel.
- `unity-android.yml`: workflow manual, APK em `build/Android/Nexo.apk`.
- `unity-ios.yml`: workflow manual, projeto Xcode não assinado em `build/iOS`.
- `gh` e Unity não estão disponíveis nesta máquina; nenhum workflow foi
  disparado e nenhum commit foi enviado ao remoto nesta etapa.

## Resolução do lockfile

`manifest.json` declara 36 dependências e o lockfile atual contém 2 entradas.
Foi criado `tools/resolve_unity_packages.ps1`. Ele exige o caminho de um Unity
real, executa:

```powershell
powershell -ExecutionPolicy Bypass -File tools/resolve_unity_packages.ps1 `
  -UnityPath 'C:\Program Files\Unity\Hub\Editor\6000.3.0f1\Editor\Unity.exe'
```

O script abre o projeto em `-batchmode -nographics -quit`, deixa o Package
Manager resolver o projeto, verifica a existência do lockfile e falha se alguma
dependência direta do manifest continuar ausente. Não edita versões nem fabrica
entradas. Não foi executado por falta do Unity Editor.

## Builds editoriais

- `AndroidBuild.PerformBuild` usa as cenas habilitadas, `BuildTarget.Android`,
  `BuildOptions.None` e o caminho `BUILD_PATH`/APK padrão.
- `IosBuild.PerformBuild` usa as cenas habilitadas, `BuildTarget.iOS`,
  `BuildOptions.None` e gera apenas projeto Xcode; não assina nem instala.

## Procedimento remoto seguro

1. Enviar a branch/commit para o remoto por canal autorizado, se necessário.
2. Configurar `UNITY_LICENSE`, `UNITY_EMAIL` e `UNITY_PASSWORD` como secrets,
   sem registrá-los em logs.
3. Executar manualmente `unity-tests.yml` no commit exato.
4. Confirmar resolução de pacotes, compilação de assemblies, EditMode e
   PlayMode, preservando JUnit/logs/artifacts.
5. Só após os testes, considerar workflows Android ou iOS.

Nenhum build móvel ou instalação foi iniciado nesta fase.
