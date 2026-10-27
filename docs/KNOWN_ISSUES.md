# NEXO — Known Issues P7

| ID | Severidade | Reprodução | Esperado | Observado | Evidência/ação |
|---|---|---|---|---|---|
| K-001 | Alta | Executar workflow sem secrets Unity | Editor importa e testa | Ativação não pode ocorrer | Cadastrar secrets e repetir workflow |
| K-002 | Alta | Tentar build Android/iOS neste Windows | Build reproduzível | Unity/SDKs não disponíveis | Usar GameCI; testar em dispositivos |
| K-003 | Média | Abrir cena com proporção não validada | UI respeita recorte | Ainda não medido | Testar 16:9/19.5:9 e registrar |
| K-004 | Média | Jogar com áudio/arte finais | assets licenciados e perfilado | Greybox/tons procedurais | Substituir após validar câmera/performance |
| K-005 | Média | Pedir demonstração de transferência na UI | resposta é registrada localmente | contrato existe; UI P5 ainda não integrada | Integrar no P3/P5 de gameplay |
| K-006 | Baixa | Inspecionar logs de desenvolvimento | nenhum dado pessoal | auditoria estática limpa | repetir varredura no build |

Nenhum defeito crítico de segurança foi observado na auditoria estática. Isso não
substitui teste de pacote, dispositivo ou revisão jurídica/ética.
