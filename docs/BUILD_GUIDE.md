# NEXO — Build Guide P2

## Ambiente atual

`ProjectVersion.txt` e os quatro workflows usam Unity `6000.3.0f1`. O Windows
local deve permanecer como editor Git leve enquanto o disco tiver cerca de 15 GiB
livres. Não instalar SDKs grandes sem nova decisão.

## Validação obrigatória

1. Cadastrar `UNITY_LICENSE`, `UNITY_EMAIL` e `UNITY_PASSWORD` no repositório.
2. Executar manualmente **Unity tests** e guardar o artifact.
3. Executar **Unity Android** e inspecionar o APK em dispositivo Android 10+.
4. Executar **Unity iOS**; o resultado inicial é projeto Xcode não assinado.
5. Registrar versão, commit, runner, dispositivo, rede offline e resultado.

## Comandos locais leves

```powershell
git status --short
git diff --check
git log -1 --oneline
```

Não há comando local de compilação Unity disponível. O workflow é a fonte de
verdade até o Editor ser instalado em ambiente com espaço aprovado. Nenhum build
Android/iOS é declarado suportado antes de evidência dos respectivos runners.

## Regras de release

Builds são identificados por commit e versão de conteúdo. Segredos ficam somente
em GitHub Actions Secrets; certificados Apple nunca entram no repositório. A
primeira missão deve completar em modo avião após a instalação.
