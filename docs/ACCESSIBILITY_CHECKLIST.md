# NEXO — Accessibility Checklist P6

Legenda: **implementado no design/código**, **pendente de teste físico**.

| Item | Critério | Estado |
|---|---|---|
| Texto | escala ampliada e leitura em safe area | implementado; pendente de teste |
| Contraste | texto claro em fundo escuro e estados por símbolo/texto | implementado; pendente de medição |
| Som | legenda e feedback visual equivalente | implementado no fluxo; pendente de revisão |
| Cor | pistas não dependem só de cor | implementado por marcadores/labels |
| Toque | botões grandes, ações nomeadas e alternativa teclado | implementado no bootstrap; pendente em telas reais |
| Movimento | sem tremor obrigatório; redução de movimento prevista | configuração registrada; integração final pendente |
| Pausa | pausa e retomada sem perda nem punição | implementado |
| Áudio | mute e volumes independentes | implementado para efeitos; música final pendente |
| Texto | legendas e diálogos relíveis | roteiro e labels disponíveis; teste pendente |
| Orientação | 16:9, 19.5:9 e recortes seguros | `Screen.safeArea`; profiling físico pendente |

O teste de aceite deve incluir áudio desligado, escala ampliada, contraste alto,
toque impreciso, controle simplificado, pausa durante cada estado e retomada após
suspensão. Não usar menores em teste sem revisão ética, autorização e consentimentos.
