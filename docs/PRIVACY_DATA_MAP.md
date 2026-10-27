# NEXO — Privacy Data Map P6

Este mapa descreve o MVP offline e não é certificação jurídica. LGPD, ECA
Digital, políticas de lojas e bases legais devem ser avaliados por assessoria
jurídica antes de piloto com menores.

| Dado | Finalidade | Retenção | Acesso | Exclusão/exportação | Base legal |
|---|---|---|---|---|---|
| `schemaVersion`, conteúdo e estado da missão | retomar jogo e migrar save | até o usuário apagar app/save | somente dispositivo | `LocalDataControls.DeleteAll`; exportação local | validar com jurídico |
| IDs de pistas e escolhas | reconstruir narrativa e feedback | junto do save | somente dispositivo | mesma exclusão | validar com jurídico |
| evidências agregadas por competência | revisão pedagógica local | até apagar save; sem envio | somente dispositivo | mesma exclusão | validar com jurídico |
| preferências de acessibilidade/áudio | manter configuração | até apagar save | somente dispositivo | mesma exclusão | validar com jurídico |
| logs técnicos de desenvolvimento | corrigir falhas sem dados pessoais | sessão/arquivo local temporário | equipe técnica em dispositivo de teste | apagar junto do save; não coletar de menores | base e procedimento a validar |

Não coletar nome real, localização precisa, contatos, câmera, microfone, saúde,
biometria, texto livre infantil ou inferência emocional. Não há consentimento
online no MVP; qualquer função futura precisa de fluxo verificável para
responsável, transparência, minimização e bloqueio por padrão.

## Fluxo de direitos

Configurações → “Apagar dados locais” mostra confirmação clara e oferece
cancelamento. Após confirmar, remove save e backup, reinicia a missão e informa
que não há servidor para apagar. Exportação é arquivo local escolhido pelo
usuário/responsável; não há upload automático.
