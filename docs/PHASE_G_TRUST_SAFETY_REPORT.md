# NEXO — Fase G: privacidade, segurança infantil e validação pedagógica

**Resultado geral:** `BLOCKED`

Esta fase exige uma versão funcional para revisão. As Fases C, D, E e F ainda
estão bloqueadas; não há build, execução em dispositivo ou evidência vinculada a
uma versão jogável. A documentação existente foi considerada como preparação,
não como aprovação final.

## Estados obrigatórios

| Revisão | Estado | Justificativa | Responsável necessário |
|---|---|---|---|
| Segurança técnica e privacidade | `PENDENTE` | Threat model e mapa de dados existem, mas não houve auditoria do build nem verificação de segredos/permissões em pacote executado | Gerente + segurança |
| Jurídica (LGPD, ECA, ECA Digital e lojas) | `PENDENTE` | Requer interpretação e validação profissional; nenhum parecer foi produzido | Assessoria jurídica |
| Pedagógica | `PENDENTE` | Conteúdo e instrumentos estão documentados, mas não foram revisados em versão funcional por profissional qualificado | Especialista pedagógico |
| Ética e testes com menores | `PENDENTE` | Nenhum estudo foi iniciado; consentimentos, salvaguardas e avaliação ética ainda precisam ser definidos | Comitê/revisor ético |

Nenhum estado acima pode ser convertido em `PASS` por análise documental
isolada. Não foram coletados dados reais de menores nem iniciados testes com
participantes.

## Escopo de verificação preparado

- Confirmar no artefato final a ausência de chat aberto, publicidade
  comportamental, compras e permissões desnecessárias.
- Conferir que telemetria é local/desativada por padrão e que não há segredos no
  cliente.
- Revisar exportação, exclusão, dados inválidos e retenção do save.
- Comparar a missão com objetivos, evidências, feedback e transferência para as
  faixas 8–11, 12–14 e 15–18 anos.
- Encaminhar política de privacidade, consentimento e fluxos de dados para
  revisão jurídica antes de qualquer piloto.

## Dependências para desbloqueio

1. Fases C e D aprovadas com evidências de execução.
2. Builds E gerados e Fase F concluída em dispositivos/simuladores.
3. Dossiê técnico revisado no pacote exato da release candidata.
4. Pareceres jurídico, pedagógico e ético registrados com responsáveis.

Nenhuma API externa foi chamada e nenhuma conclusão de conformidade jurídica ou
eficácia educacional foi declarada.
