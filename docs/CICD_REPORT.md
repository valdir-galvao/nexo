# NEXO — Relatório de CI/CD GameCI

Status: estrutura criada localmente; nenhum workflow foi publicado ou executado
no GitHub remoto. O repositório ainda não contém um projeto Unity (`ProjectSettings/
ProjectVersion.txt` não existe), portanto os jobs de teste/build ficam pulados até
que a fundação Unity seja adicionada.

## Arquivos criados

- `.github/workflows/unity-activation.yml`: acionamento manual para produzir um
  artefato `.alf` usando `game-ci/unity-request-activation-file@v2`.
- `.github/workflows/unity-tests.yml`: EditMode via `game-ci/unity-test-runner@v4`.
- `.github/workflows/unity-android.yml`: Android em `ubuntu-latest` via
  `game-ci/unity-builder@v6`.
- `.github/workflows/unity-ios.yml`: projeto Xcode em `macos-latest` via
  `game-ci/unity-builder@v6`.

## Licença e segredos

Os workflows nunca imprimem nem versionam credenciais. Configurar no repositório,
em Settings → Secrets and variables → Actions:

- `UNITY_LICENSE`: conteúdo do arquivo `.ulf` quando o fluxo de licença estiver
  aprovado;
- `UNITY_EMAIL` e `UNITY_PASSWORD`: credenciais Unity necessárias pela ativação
  GameCI; usar apenas secrets, nunca variáveis públicas;
- futuramente, certificados/provisioning iOS em secrets ou environment protegido.

O workflow `.alf` gera somente o arquivo de solicitação e faz upload como artifact;
ele não registra secret automaticamente. A documentação atual do GameCI indica
que Unity Personal deve preferencialmente ser ativada pelo Unity Hub e que a
licença `.ulf` deve ser adicionada manualmente aos secrets. A página de ativação
mais antiga ainda descreve o fluxo `.alf`; por isso o job está separado, manual e
não é dependência silenciosa dos builds. Confirmar a política vigente da conta
antes de executar.

## Decisões técnicas

- Runners: Ubuntu para Android/testes e macOS para gerar o projeto Xcode iOS.
- `UNITY_VERSION` está centralizado como `6000.3.0f1`, um baseline Unity 6.3 LTS;
  confirmar a versão exata disponível no Unity Hub/GameCI antes do primeiro run.
- Cache de `Library/` usa hash de `Assets`, `Packages` e `ProjectSettings`; os
  caches são por SO, versão e plataforma e não são versionados.
- Git LFS é solicitado no checkout; `.gitattributes` já classifica assets binários.
- Os jobs têm `if` para não falhar enquanto não existir um projeto Unity.
- O método `Nexo.Editor.Build.*` exige scripts de build no futuro P1; até lá os
  workflows são infraestrutura preparada, não prova de build.
- Os artifacts são saídas de CI e não publicação em lojas.

## Plano de execução seguro

1. Criar/confirmar o projeto Unity e `ProjectSettings/ProjectVersion.txt` com URP.
2. Confirmar versão Unity 6.3 LTS e atualizar `UNITY_VERSION` se necessário.
3. Configurar Git LFS no repositório e verificar que assets grandes não são ponteiros
   quebrados.
4. Ativar Unity Personal no Hub ou seguir o método de licença autorizado pela conta;
   salvar somente secrets no GitHub.
5. Executar manualmente o workflow de testes em um PR pequeno.
6. Executar Android e inspecionar o APK artifact em um dispositivo de teste.
7. Executar iOS no macOS runner; assinatura/distribuição ficam separadas e exigem
   certificados aprovados. O artifact inicial é um projeto Xcode não assinado.
8. Só depois adicionar gates de branch protection e qualquer publicação interna.

## Limitações e riscos

- Sem projeto Unity, os workflows não foram executados nem validados por um runner.
- Os métodos `Nexo.Editor.Build.AndroidBuild.PerformBuild` e
  `Nexo.Editor.Build.IosBuild.PerformBuild` ainda não existem; devem ser criados
  no P1 ou removidos dos workflows em favor do método padrão do projeto.
- A configuração de assinatura iOS não foi inventada. Não há certificados,
  provisioning profile, Team ID ou App Store Connect configurados.
- GitHub Actions consome minutos e pode exigir plano/runner compatível; nenhum
  gasto foi autorizado ou estimado aqui.
- Unity Personal, GameCI e a forma de ativação podem mudar; revisar a documentação
  oficial antes de executar o job de licença.

## Referências consultadas

- GameCI activation: https://game.ci/docs/github/activation/
- GameCI builder: https://game.ci/docs/github/builder/
- GameCI action de ativação: https://github.com/game-ci/unity-request-activation-file
- Unity manual de licenças: https://docs.unity.com/en-us/engine/6000.3/manual/get-started/install-and-upgrade/licenses-and-activation/license-activation-methods
