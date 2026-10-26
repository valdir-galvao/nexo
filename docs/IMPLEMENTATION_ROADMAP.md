# NEXO — Implementation Roadmap (P0)

Estimativas são faixas de esforço de uma equipe pequena, não datas prometidas.
Elas assumem acesso aos dispositivos, ferramentas instaladas e decisões rápidas.
Nenhuma instalação ou compra está autorizada por este documento.

| Fase | Estimativa | Entregável | Dependências | Critério de saída | Responsável primário |
| --- | --- | --- | --- | --- | --- |
| P0 Planejamento | 1–2 dias | Charter, requisitos, aceite, decisões, riscos e roadmap | Briefing recebido | Gate P0 aprovado | Diretor |
| P1 Fundação técnica | 2–5 dias | Projeto Unity mínimo, cenas, input, save local, build de teste | D-001/D-002; Unity/SDKs/dispositivos | App abre, salva e roda offline no mínimo definido | Engenharia |
| P2 Núcleo da missão | 5–10 dias | Exploração, pistas, recurso de água, decisão e reparo | P1; D-003/D-004 | Fluxo crítico jogável de início a conclusão | Engenharia + design |
| P3 Aprendizagem e UX | 3–6 dias | Tutorial, feedback, eventos pedagógicos, acessibilidade | P2; D-004/D-005 | LR/AX passam sua matriz de testes | Pedagogia + UX |
| P4 Arte e áudio verticais | 5–10 dias | Ilha, reservatório, UI, animações e áudio licenciados | P2; D-005/D-007 | Cena coerente, assets rastreados, áudio opcional | Arte |
| P5 Segurança e hardening | 2–4 dias | Save robusto, privacidade, logs redigidos, build sem segredos | P1–P4; D-006 | CP/SEC passam auditorias e testes | Engenharia + segurança |
| P6 Validação | 3–7 dias | Testes em dispositivos, métricas, defeitos priorizados | P1–P5; D-002 | Gate P1 aceito ou plano corretivo aprovado | QA + diretor |
| P7 Empacotamento | 1–3 dias | Build reproduzível e documentação de instalação | P6 | Artefatos identificados e retomáveis | Release |

## Dependências de ferramenta verificadas em P0

No diretório e PATH inspecionados há Git e Python. Não foram encontrados Unity,
`dotnet`, `adb` ou `xcodebuild`; nenhum dispositivo de teste foi confirmado.
P1 não pode começar como implementação até D-001/D-002 e a disponibilidade
verificada do toolchain. A ausência no PATH não prova ausência em todo o disco.

## Sequenciamento mínimo

P0 → decisões D-001/D-002/D-004 → P1 → P2 → P3 e P4 em paralelo controlado →
P5 → P6 → P7. P4 não pode bloquear o protótipo por assets finais; usar placeholders
com licença rastreada. P5 começa cedo para revisar save e dados, mas seu gate é
após a integração.
