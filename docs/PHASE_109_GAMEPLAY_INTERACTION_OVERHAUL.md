# PHASE 109 — GAMEPLAY INTERACTION OVERHAUL

Data: 2026-11-11 21:43:30 -03:00
Escopo: transformar o vertical slice procedural em um fluxo de gameplay compreensível em dispositivos móveis.

## Causa corrigida
O mundo era criado por MakeCube e a interação dependia de raycast/OnGUI. Após o clique inicial, o jogador não tinha controles persistentes nem um alvo explícito para abrir o quiz; as animações eram limitadas ao botão de entrada e a uma pulsação quase imperceptível da porta.

## Alterações
- HUD de gameplay agora inclui controles direcionais móveis (Nexo Mobile Controls).
- Botão INVESTIGAR chama Interact() diretamente, sem depender de acertar o raycast.
- Indicador FASE x/3 permanece visível.
- Portal pulsa com amplitude maior e modula a cor durante a exploração.
- Canal de água oscila, avatar recebe movimento vertical sutil e a luz direcional pulsa.
- Partículas introdutórias continuam em baixa emissão durante o gameplay.
- A lógica existente de quiz, pontuação, progressão e persistência foi preservada.

## Arquivo alterado
- Assets/NEXO/Scripts/UI/NexoVerticalSliceBootstrap.cs

## Verificação
- python tools/run_static_qa.py — PASS (json=5, cs=26, exit 0).
- git diff --check — PASS.
- Build Unity/Android ainda não executado nesta alteração; requer runner Unity/GameCI.

## Limitações conhecidas
O cenário continua usando geometria procedural simples. A alteração torna o fluxo jogável e visível, mas arte 3D final, personagem animado, câmera cinematográfica e efeitos de produção ainda exigem assets/autoria adicionais.

P211 (2026-11-11 21:47:18 -03:00): auditoria UX aprofundada confirmou ausência de alvo visual de interação; adicionados efeito energético do portal, luz pulsante e partículas persistentes ao gameplay.

P212 (2026-11-11 21:47:43 -03:00): corrigido o cálculo de brilho do portal para sempre usar a cor-base, evitando escurecimento acumulado por frame.
