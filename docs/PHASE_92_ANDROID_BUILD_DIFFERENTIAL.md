# NEXO — Perícia diferencial do hang no Build Android

Data: 2026-11-01 (America/Sao_Paulo)

## Comparação executada

| Item | Run aprovado | Runs com timeout |
|---|---|---|
| Run | 41211856635 | 41297554044, 41299957549, 41299986311 |
| Unity | 6000.3.0f1 | 6000.3.0f1 |
| GameCI | unity-builder@v6 / CLI v0.1.72 | igual |
| Imagem | unityci/editor:ubuntu-6000.3.0f1-android-3 | igual configurada |
| Arquitetura | ARM64 + x86_64 | igual |
| Build method | Nexo.Editor.Build.AndroidBuild.PerformBuild | igual |
| Parâmetros | batchmode, nographics, single-instance, disable-assembly-reload | iguais |
| Perfil | API 35 / min 29 / IL2CPP | igual |
| Resultado | APK 57 MB e AVD PASS | timeout sem artefato |

## Evidência de tempo

No run `41211856635`, o log Unity registrou `PlayerBuildInfo` com `Postprocess built Player` em 780.659 ms, seguido de `Build succeeded!`, APK de 57 MB e upload concluído. O job inteiro durou aproximadamente 20 minutos.

Nos runs `41297554044`, `41299957549` e `41299986311`, o passo `Build Android package` permaneceu `in_progress` até o encerramento do job entre 40 e 54 minutos. Não houve `Build succeeded!`, não houve logs finais do job (`log not found`) e nenhum artefato foi publicado.

## O que foi descartado

- Não há mudança no script `AndroidBuild.cs`, manifesto, lockfile, PlayerSettings ou workflow além do aumento do timeout de 45 para 60 minutos.
- Unity Tests passou no mesmo projeto, descartando erro básico de compilação C#.
- A falha ocorre antes do AVD e sem APK, portanto não é falha de shader, Canvas, câmera ou toque.
- A geometria verde e o material das partículas não participam do empacotamento IL2CPP de forma que explique esse padrão; o material magenta é problema de runtime do APK antigo.

## Causa mais precisa possível

**Hang não observável no container GameCI durante a fase Unity/IL2CPP/Gradle de empacotamento Android, dependente do estado/recursos do runner.** A causa interna (processo IL2CPP, Docker, disco, Gradle ou imagem) não pode ser isolada porque o runner não preservou `Editor.log` nem logs do processo quando expirou.

O histórico comprova que a configuração é capaz de concluir; os três timeouts consecutivos comprovam instabilidade/reprodução do bloqueio no ambiente atual. Não há evidência suficiente para atribuir a falha a um arquivo do jogo.

## Correção recomendada

Manter o smoke test corrigido (`52ce0ce`) e executar uma tentativa limpa com diagnóstico persistente do processo. Se o timeout repetir, tratar como bloqueio do runner/GameCI e trocar apenas a estratégia de infraestrutura (imagem/tag ou runner), sem reescrever a lógica do aplicativo.

Estado: `BLOCKED` para nova geração do APK até uma execução concluir.
