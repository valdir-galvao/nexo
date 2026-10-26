# NEXO — Game Design Document P1

Status: especificação executável da fatia vertical. Versão 1.0, 26/10/2026.

## Visão

NEXO é uma aventura de exploração, decisão e reconstrução em ilhas suspensas.
Após A Ruptura, as civilizações perderam a conexão. O jogador explora uma ilha,
interpreta evidências e restaura um reservatório sem depender de rede.

## Escopo da fatia

Uma missão de 10–15 minutos, em uma ilha, com prólogo, tutorial, três pistas,
um recurso (água), uma decisão de prioridade, reparo de um reservatório e uma
conclusão que aponta para o mistério maior. Não há combate, morte, moeda, chat,
multiplayer aberto, loot aleatório ou conta obrigatória.

## Loop

- Loop de 1–3 min: observar → mover → inspecionar → escolher uma ação → receber
  feedback → atualizar o estado local.
- Loop de missão: chegar → mapear pistas → formular hipótese → distribuir água →
  reparar → testar fluxo → refletir sobre causa e consequência → salvar.

## Regras essenciais

O avatar se move em terceira pessoa; toque no destino ou controle virtual move,
um botão de interação inspeciona o alvo mais próximo e um botão de mochila mostra
recursos. A água é um inteiro de 0 a 6. O jogador começa com 3 unidades, encontra
mais 3 na fonte alta e pode alocar 2 ao reservatório ou 2 à horta. O reservatório
precisa de 2 unidades e de uma válvula reparada. A horta precisa de 2 unidades.
Ambas as escolhas são válidas: reservatório libera a ponte de saída; horta libera
um atalho e uma fala extra. A escolha pode ser revista antes de confirmar.

## Estados implementáveis

`Boot`: carregar save local, idioma e acessibilidade; se não houver save, criar
missão no estado `Arrival`. `Exploration`: aceitar movimento e inspeção, destacar
objetivos descobertos sem apontar a solução. `Interaction`: congelar movimento,
mostrar contexto e opções confirmáveis. `Feedback`: mostrar causa, efeito e
próxima pista; permitir tentar novamente. `Completion`: registrar evidências,
mostrar resultado e salvar. `Pause`: congelar simulação, permitir áudio,
legendas, tamanho, reinício seguro e retorno; nunca consumir recurso ou tempo.

## Ajuda graduada

1. Contextual: brilho suave no elemento relacionado e pergunta (“O que poderia
   levar água até aqui?”).
2. Explícita: nomeia a relação (“Compare o canal quebrado com a válvula”).
3. Após tentativa: explica causa e efeito, permite desfazer e tentar novamente.
Usar a ajuda não reduz progresso nem mede competência como punição.

## Salvamento

Salvar em chegada, após cada inspeção de pista, confirmação de alocação, reparo,
conclusão e ao pausar. O save contém versão, estado da missão, pistas descobertas,
água, escolha, opções de acessibilidade e checksum. Save inválido é rejeitado,
restaurando o último backup local e oferecendo reinício da missão.

## Critérios de sucesso da missão

O jogador conclui quando repara a válvula, aloca água, aciona o fluxo e escolhe
“seguir o sinal” na ponte. O resultado registra a estratégia e a evidência de que
o jogador relacionou recurso, prioridade e consequência. Falha é apenas estado
temporário: água pode ser redistribuída, pistas podem ser revistas e a missão
pode ser reiniciada sem apagar progressão narrativa já descoberta.

## Dependências

P2 implementa estados, interação, save e regras. P3 implementa feedback,
instrumento pedagógico e acessibilidade. P4 cria ilha, reservatório, UI, animação
e áudio. P5 audita save, privacidade e ausência de segredos. P6 mede em Android/iOS.
