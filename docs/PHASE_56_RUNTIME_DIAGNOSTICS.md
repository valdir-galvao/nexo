# Fase 56 ? Diagn?stico persistente de runtime

Data: 30/10/2026 (America/Sao_Paulo)

Foi criado `Assets/NEXO/Scripts/Core/RuntimeDiagnostics.cs`, instalado antes do carregamento da primeira cena e mantido com `DontDestroyOnLoad`.

## Dados registrados

- in?cio/fim da sess?o;
- vers?o Unity, plataforma, modelo do dispositivo, sistema operacional, GPU e mem?ria;
- caminho de `persistentDataPath`;
- cenas carregadas;
- logs `Warning`, `Error`, `Assert` e `Exception`, com timestamp, cena e stack trace;
- falhas de inicializa??o e de grava??o do pr?prio diagn?stico.

O arquivo local ? `Application.persistentDataPath/nexo-runtime.log`, limitado a 2 MiB. N?o h? envio de rede nem registro de tokens, senhas ou dados pessoais. Em caso de erro, uma mensagem informa na tela que o diagn?stico foi salvo.

A captura usa `logMessageReceivedThreaded`; o callback n?o acessa APIs Unity de cena diretamente. O nome da cena ? atualizado no thread principal.

## Valida??o

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`, exit 0).
- `git diff --check`: PASS.
- Dispositivo Android/logcat: NOT_RUN nesta etapa.
