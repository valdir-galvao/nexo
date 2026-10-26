# NEXO — Project Charter (P0)

Status: P0 concluído como planejamento; implementação não iniciada.
Data: 26/10/2026, America/Sao_Paulo.

## Propósito

NEXO é um jogo educativo mobile sobre reconstruir civilizações em um arquipélago
de ilhas suspensas. A primeira experiência será a missão “O reservatório
silencioso”, em uma fatia vertical funcional para Android e iOS.

## Visão e resultados

O jogador deve investigar, decidir, construir e cooperar em um ciclo curto que
ensina conceitos verificáveis sem transformar a aprendizagem em uma prova. A
fatia vertical deverá demonstrar uma missão completa, progresso saudável,
acessibilidade básica, funcionamento offline e coleta mínima de evidências de
aprendizagem, sem depender de multiplayer aberto, economia complexa ou IA
generativa.

## Princípios de produto

- Curiosidade e descoberta orientam a exploração.
- Agência: escolhas têm consequências compreensíveis.
- Domínio: regras e habilidades tornam-se mais claras por prática e feedback.
- Progressão saudável: sessões curtas, sem pressão predatória, sem loot boxes,
  anúncios obrigatórios ou punição por ausência.
- Aprendizagem mensurável: cada objetivo pedagógico tem uma evidência observável.
- Privacidade por padrão: coleta mínima, sem chat aberto na fatia vertical.

## Escopo P0/P1 da fatia vertical

Incluído: uma ilha jogável, a missão completa, exploração em terceira pessoa ou
visão equivalente definida em J02, um recurso de água, um reservatório, uma
decisão de priorização, construção/reparo de uma estrutura, feedback pedagógico,
progresso local, pausa, acessibilidade essencial e tutorial offline.

Excluído: mundo completo, múltiplas ilhas, matchmaking, chat entre usuários,
economia monetária, loja, publicidade, contas obrigatórias, geração de conteúdo
por IA, backend obrigatório e publicação nas lojas.

## Plataformas e tecnologia

Plataformas-alvo: Android e iOS. Unity com C# é a recomendação inicial por
portabilidade e disponibilidade de ferramentas, mas a decisão final depende de
verificar versão suportada, hardware de desenvolvimento, pipeline de assets,
licenças e dispositivos de teste. No ambiente inspecionado não há Unity, SDK
Android, Xcode ou dispositivo de teste verificado.

## Critério de sucesso do P0

Os contratos de produto, aceite, decisões, roadmap e riscos existem; cada
requisito possui rastreabilidade e método de validação; decisões pendentes estão
explícitas; nenhuma funcionalidade é declarada pronta sem evidência.

## Governança

O diretor de projeto mantém escopo, contratos e decisões. Cada tarefa terá um
único responsável, arquivos de escopo, dependências e teste de aceite. A equipe
de arte, engenharia, pedagogia, segurança e QA trabalha contra este charter.
Mudanças de escopo exigem entrada no decision log e atualização do roadmap.
