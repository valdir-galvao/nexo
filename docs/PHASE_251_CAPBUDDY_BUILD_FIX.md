# PHASE 251 — Correção da geração do CapBuddy antes da build

## Evidência
O run Android 43520318437 foi SUCCESS, porém o log runtime registrou fallback procedural. A causa foi o setup depender somente de InitializeOnLoad/delayCall; o builder podia iniciar antes da geração do prefab.

## Correção
- CapBuddySetup importa/atualiza explicitamente Assets/External/FreeTestAssets/CapBuddy/CapBuddy.fbx.
- EnsureGeneratedForBuild é chamado antes de BuildPipeline nos builders Android e iOS quando NEXO_CONFIGURE_CAPBUDDY=1.
- O bootstrap mantém fallback defensivo e agora registra avatar authored sem alegar humanoide.

## Validação local
- python tools/run_static_qa.py: PASS (json=5, cs=27).
- git diff --check: PASS.

## Pendente
Executar novo Unity Tests/Android público e confirmar no log [NEXO_AVATAR] Authored avatar prefab loaded. Não declarar a integração concluída antes dessa evidência.

## Tentativa local subsequente
Unity 6000.3.0f1 nao foi encontrado nos caminhos locais nem no PATH; importacao, geração do prefab/controller e validação LoadAssetAtPath permanecem BLOCKED localmente.

## Gate obrigatório de build
AndroidBuild e IosBuild chamam CapBuddySetup.Build antes do BuildPipeline e interrompem a build se LastBuildSucceeded for falso. Logs [NEXO_CAPBUDDY] registram início, mapeamento de cada clip, falha e sucesso.
