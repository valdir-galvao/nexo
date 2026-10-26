# NEXO — checklist final de ativação

Verificado em 26/10/2026, America/Sao_Paulo.

## Estado remoto

- Repositório privado `valdir-galvao/nexo`: acessível.
- GitHub Actions: habilitado; ações permitidas: `all`.
- Workflows publicados: ativação, testes, Android e iOS.
- Secrets configurados: nenhum. A API retorna apenas nomes, nunca valores.
- Projeto Unity: scaffold P1 publicado; primeiro import/build ainda não executado.

## Ação manual obrigatória

No GitHub, abrir `Settings → Secrets and variables → Actions → New repository secret`
e adicionar `UNITY_LICENSE`, `UNITY_EMAIL` e `UNITY_PASSWORD`. O conteúdo da
licença deve ser o `.ulf` aceito pelo método de ativação da conta; nunca publicar
esses valores em arquivo, issue, log ou chat.

Depois executar `Actions → Unity tests → Run workflow`. O primeiro run é o teste
de importação e compilação do scaffold. Corrigir apenas erros produzidos pelo
Editor/GameCI e registrar cada mudança em `docs/PROGRESSO.md`.

## Aceite do primeiro run

O P1 só pode ser marcado como aceito quando o workflow de testes terminar verde,
o artifact de resultados existir e Android produzir um APK. iOS inicialmente
produz um projeto Xcode não assinado; assinatura requer certificados Apple em
secrets/environment protegido. Até lá, o estado correto é “infraestrutura pronta,
build ainda não validada”.
