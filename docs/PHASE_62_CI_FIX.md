# Fase 62 - correcao de PlayMode e build headless

Data: 31/10/2026 (America/Sao_Paulo)

## Diagnostico

O artefato de diagnostico do run `41193417344` mostrou `Caught fatal signal - signo:11` em `Begin MonoManager ReloadAssembly`, depois de `Executing IPrebuildSetup for Unity.PerformanceTesting.Editor.TestRunBuilder`. A falha ocorreu antes da execucao do teste e nao foi uma assertiva de gameplay. EditMode terminou com sucesso. O PlayMode continha referencias `UnityEditor` e reflexao para BuildProfile/AssetDatabase, desnecessarias no assembly de runtime de testes.

## Correcoes

- `Nexo.PlayModeTests` agora usa apenas APIs runtime (`SceneManager.LoadSceneAsync`) e fallback por GameObject; removida a dependencia de `UnityEditor`, `EditorSceneManager`, `AssetDatabase` e reflexao.
- Mantidos os `yield return null` e timeout de prontidao para permitir `Awake`/`Start`.
- `unity-tests.yml` passou a declarar `-batchmode -nographics -quit` explicitamente.
- `unity-android.yml` passou a declarar `-batchmode -nographics -quit -buildTarget Android -customBuildTarget Android`.
- Nao foi removido nenhum teste nem desabilitado o gate.

## Verificacao local

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`).
- `git diff --check`: PASS.
- YAML dos dois workflows validado com PyYAML: PASS.

## Limites

Unity Editor nao esta instalado localmente. A confirmacao de PlayMode e a compilacao Android permanecem pendentes de um novo run GameCI. Nenhum build foi disparado nesta fase e nenhum APK novo foi declarado.
