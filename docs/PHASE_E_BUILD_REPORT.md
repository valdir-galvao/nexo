# NEXO — Fase E: compilações Android e iOS

**Resultado geral:** `BLOCKED`

As Fases C e D não estão aprovadas. A execução Unity/GameCI não foi comprovada,
portanto nenhum build foi iniciado e nenhum artefato foi inventado.

## Estados por plataforma

| Plataforma | Estado | Evidência | Próximo requisito |
|---|---|---|---|
| Android | `BLOCKED` | Unity, módulo Android, `adb` e secrets não disponíveis localmente; C/D pendentes | C=`PASS`, D=`PASS`, workflow GameCI, APK real e teste em dispositivo/emulador |
| iOS | `BLOCKED` | `xcodebuild`/runner macOS não disponível nesta máquina; C/D pendentes | C=`PASS`, D=`PASS`, runner macOS, Xcode, assinatura autorizada e artefato real |

## Verificações realizadas

- `where.exe Unity`: não localizado.
- `where.exe adb`: não localizado.
- `where.exe xcodebuild`: não localizado (Windows).
- Fase C: `BLOCKED`, com importação, compilação e testes Unity `NOT_RUN`.
- Fase D: `BLOCKED`, com fluxo da missão e persistência funcional `NOT_RUN`.

Não foram gerados APK, AAB, projeto Xcode ou hashes de artefatos. Não foram
verificados instalação, inicialização, navegação, controles ou persistência em
dispositivo real.

## Sequência de desbloqueio

1. Configurar os secrets Unity no GitHub por canal seguro.
2. Obter C=`PASS` com logs de importação, compilação e testes.
3. Obter D=`PASS` com evidências do fluxo funcional.
4. Executar Android em `ubuntu-latest`, gerar APK de teste e testar em ambiente
   compatível; gerar AAB somente se a distribuição interna exigir.
5. Executar iOS em `macos-latest`, validar assinatura e gerar o artefato sem
   inventar certificados ou perfis.

Nenhuma API externa foi chamada nesta fase.
