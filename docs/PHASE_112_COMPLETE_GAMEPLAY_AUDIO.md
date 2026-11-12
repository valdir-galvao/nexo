# PHASE 112 — COMPLETE GUIDED GAMEPLAY AND AUDIO

Data: 2026-11-11 22:46:27 -03:00

## Implementado
- Sequência obrigatória de investigação: Fonte -> Canal -> Válvula -> Portal.
- Marcador visual giratório e pulsante do objetivo atual.
- Feedback textual 1/3, 2/3, 3/3 na HUD.
- Interação contextual: INVESTIGAR avança somente o objetivo atual.
- Válvula muda de intensidade enquanto aguarda investigação.
- Água recebeu três ripples animados sobre o canal.
- Avatar Explorer composto por peças visuais e movimento de respiração.
- Portal mantém partículas, luz e pulsação.
- Trilha ambiente procedural em loop adicionada ao NexoAudioDirector.
- Efeitos de clique, descoberta, acerto, erro e desbloqueio preservados.

## Verificação
- python tools/run_static_qa.py: PASS (json=5, cs=26).
- git diff --check: PASS.
- Unity/GameCI e APK desta revisão: ainda NOT_RUN.
