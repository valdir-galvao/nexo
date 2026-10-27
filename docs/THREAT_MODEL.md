# NEXO — Threat Model P6

## Escopo e ativos

Ativos: save local, eventos pedagógicos locais, conteúdo de missão, configuração
de acessibilidade e credenciais de CI. O adversário considerado pode ter acesso
ao dispositivo, ao pacote instalado, a logs de desenvolvimento ou a um save
corrompido. Não há servidor, chat, contas, anúncios, compras ou IA no MVP.

## Riscos priorizados

| ID | Risco | Severidade | Mitigação | Estado |
|---|---|---:|---|---|
| T-01 | Save corrompido perde progresso | Alta | JSON versionado, checksum/backup, validação e reinício seguro | Implementado parcialmente; checksum completo no P7 |
| T-02 | Dados pessoais em logs/eventos | Alta | eventos por IDs/contagens, sem texto livre, revisão de logs | Implementado |
| T-03 | Coleta ou tráfego externo inesperado | Alta | missão offline, sem SDK de analytics/rede, auditoria de dependências | Implementado no código; verificar build |
| T-04 | Contato com desconhecidos/assédio | Crítica | sem chat, mensagens, UGC ou matchmaking | Bloqueado por escopo |
| T-05 | Compra, aposta ou anúncio acidental | Crítica | sem loja, IAP, publicidade ou links externos | Bloqueado por escopo |
| T-06 | Permissões de microfone/localização/câmera | Alta | nenhuma API ou permissão obrigatória; revisar manifestos | Verificar no build |
| T-07 | Prompt injection em IA futura | Alta | IA não participa da missão; conteúdo declarativo sem execução | Mitigado; ADR obrigatório para futuro |
| T-08 | Exclusão/exportação inexistente | Alta | controles locais `LocalDataControls`, instrução de apagar save | Implementado; UX de loja pendente |
| T-09 | Pista sonora ou cor única exclui jogador | Alta | texto, símbolos, legenda, contraste e mute | Implementado no design; teste físico pendente |
| T-10 | Manipulação social ou streak | Alta | sem punição por ausência, sequência, urgência ou recompensa aleatória | Implementado por regra |

Qualquer risco crítico sem mitigação bloqueia piloto. Não executar testes de
penetração ou chamadas contra sistemas externos sem autorização específica.
