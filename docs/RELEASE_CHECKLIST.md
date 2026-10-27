# NEXO — Release Checklist 0.1.0-rc1

## Preparação técnica

- [x] Unity `6000.3.0f1`, URP `17.3.0`, Input System `1.14.0` e Test Framework `1.5.1` registrados.
- [x] `bundleVersion` definido como `0.1.0-rc1`.
- [x] Android version code `2`; iOS build number `1`.
- [x] Builds usam `BuildOptions.None`; não são Development Build.
- [x] `submitAnalytics: 0`; nenhum endpoint ou segredo no pacote fonte.
- [x] `.env*`, `Library`, builds e logs ignorados pelo Git.
- [x] Workflows Android/iOS usam GameCI e secrets do GitHub.
- [x] Manifesto de conteúdo e origem do commit documentados.

## Execução obrigatória antes de teste interno

- [ ] Cadastrar secrets Unity sem os publicar.
- [ ] Executar Unity tests e guardar artifact.
- [ ] Executar Android; instalar APK em Android 10+/4 GB e testar offline.
- [ ] Executar iOS; inspecionar projeto Xcode não assinado.
- [ ] Medir FPS, memória, draw calls, tamanho e carregamento.
- [ ] Testar save, migração, exclusão, suspensão e retorno.
- [ ] Auditar permissões e SDKs do pacote.

## Distribuição interna

Distribuir somente para testadores autorizados, com commit, SHA256 do artefato,
dispositivo, versão do SO, rede e instruções de remoção do save. Não publicar em
lojas, aceitar contratos, criar contas ou assinar builds neste marco.

## Bloqueio atual

APK Android e projeto Xcode ainda não foram gerados porque o Unity Editor/GameCI
não foi executado. A release é uma candidata rastreável de fonte, não uma build
aprovada para piloto.
