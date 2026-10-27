# NEXO — Fase D: Integração da missão e testes funcionais

**Resultado:** `BLOCKED`

## Motivo do bloqueio

A Fase C não foi aprovada. O projeto ainda não foi importado ou executado no
Unity/GameCI, e os testes EditMode/PlayMode permanecem `NOT_RUN`. Nesta máquina
Windows não foram encontrados Unity Editor nem `adb`; os secrets Unity e a
execução GameCI também não foram comprovados. Portanto, não há evidência de
execução do fluxo de jogo que permita aprovar a Fase D.

## Matriz funcional

| Área | Estado | Evidência / observação |
|---|---|---|
| Inicialização e menu | NOT_RUN | Requer execução Unity |
| Prólogo, pular e entrada no jogo | NOT_RUN | Requer execução Unity |
| HUD, diário e estados vazios | NOT_RUN | Implementação estática existente; comportamento não validado |
| Pausa, retorno e saída segura | NOT_RUN | Requer ciclo de execução e retomada |
| Exploração da ilha | NOT_RUN | Requer cena importada e jogável |
| Três objetos investigáveis | NOT_RUN | Regras presentes no código; interação não executada |
| Puzzle do reservatório | NOT_RUN | Regras presentes no código; solução e feedback não executados |
| Erro, pistas graduais e repetição | NOT_RUN | Requer execução funcional |
| Feedback explicativo e transferência | NOT_RUN | Integração pedagógica não executada |
| Conclusão e desbloqueio da área | NOT_RUN | Requer fluxo completo |
| Salvamento, saída e retomada | NOT_RUN | Persistência runtime não executada |
| Interrupção e retorno ao primeiro plano | NOT_RUN | Requer dispositivo ou simulador |
| Proporções de tela e input | NOT_RUN | Requer Editor/dispositivo |

Nenhuma linha foi marcada como `PASS` ou `FAIL`: a matriz registra ausência de
execução, não uma aprovação implícita.

## Pendências visuais e sonoras conhecidas

- O cenário continua em greybox e os assets finais dependem da validação de
  câmera e desempenho.
- O áudio usa tons procedurais leves; a mixagem em dispositivo ainda não foi
  medida.
- Profiling, memória, suspensão e retomada em Android/iOS estão pendentes.
- A tarefa de transferência pedagógica permanece dependente da execução do
  fluxo integrado (item K-005 no registro de problemas conhecidos).

## Próxima ação verificável

1. Configurar no GitHub, pelo canal seguro, `UNITY_LICENSE`, `UNITY_EMAIL` e
   `UNITY_PASSWORD`.
2. Executar o workflow `Unity tests` e obter logs/relatórios reais de import,
   compilação, EditMode e PlayMode para aprovar ou corrigir a Fase C.
3. Somente após C=`PASS`, executar esta matriz em Editor/dispositivo e registrar
   evidências de cada linha.

Nenhum teste funcional ou API externa foi executado nesta fase.
