# NEXO — Narrative Bible P1

## Cânone

A Ruptura interrompeu os caminhos de energia e água entre as ilhas suspensas.
Ninguém sabe se foi acidente, decisão política ou sabotagem. O arquipélago não
está destruído: seus sistemas estão desconectados e podem ser compreendidos.

## Protagonista

O Explorador é um papel customizável sem nome, voz ou biografia fixa. Sua função
é observar, fazer perguntas e testar hipóteses. O jogo nunca presume idade,
identidade ou conhecimento prévio do jogador.

## Personagens

- **Mira**, guardiã local: conhece as pessoas e a rotina da ilha, mas não entende
  a tecnologia antiga. Objetivo: manter a comunidade segura.
- **Téo**, aprendiz de manutenção: deixou marcas de giz e diagramas. Objetivo:
  aprender a reparar sem repetir os erros da geração anterior.
- **O eco**: registro sonoro automático da rede NEXO, sem personalidade humana.
  Entrega dados incompletos, nunca respostas mágicas.

## Arco de mistério

Local: o reservatório parou porque a válvula travou e o canal foi desviado.
Camada 1: a distribuição desigual de água foi uma decisão, não um acidente.
Camada 2: o diagrama mostra o mesmo símbolo em outras ilhas. Revelação futura:
A Ruptura afetou uma rede compartilhada. Nenhuma pista exige informação fora do
jogo; cada revelação deriva de mapa, marcas, som ou consequência observável.

## Tom e linguagem

Esperançoso, curioso e concreto. Conflitos são resolvidos por observação e
cooperação; não há vilão caricatural. Falas curtas, sem sarcasmo que esconda
instruções, e sempre com opção de reler.

## Prólogo de 90 segundos

| Tempo | Ação/câmera | Áudio | Fala/evento disparador |
|---|---|---|---|
| 0–15 s | Plano aberto das ilhas partidas; câmera desce até o avatar. | Vento, madeira distante. | Eco: “Conexão perdida.” Dispara ao terminar o plano. |
| 15–30 s | Travelling acompanha o avatar na ponte curta; o horizonte mostra sinais apagados. | Três notas graves; fade de música. | Mira entra: “A Ruptura levou o caminho da água.” |
| 30–45 s | Close em uma placa com o símbolo NEXO e corte para o reservatório sem som. | Gotejamento interrompido, silêncio de 2 s. | Téo: “Se o símbolo ainda acende, a rede não morreu.” |
| 45–60 s | Câmera gira para a fonte alta; um fio de água ilumina marcas no chão. | Gotejamento volta, pulso sutil de UI. | Mira: “Encontre de onde ela vem.” Define objetivo `find_source`. |
| 60–75 s | Controle passa ao jogador; tutorial mostra mover e inspecionar. | Passos e ambiente, legenda opcional. | Ao primeiro movimento, `tutorial_move`; ao primeiro foco, `tutorial_inspect`. |
| 75–90 s | A câmera enquadra fonte, canal e reservatório no mesmo eixo; volta ao avatar. | Tema de descoberta em volume baixo. | Eco: “Uma pista local. Um sinal distante.” Transição para `Exploration`. |

O prólogo não pode ser pulado na primeira execução, mas pode ser revisto no
diário. Cada transição ocorre por tempo ou evento nomeado, nunca por uma
resposta implícita do jogador.
