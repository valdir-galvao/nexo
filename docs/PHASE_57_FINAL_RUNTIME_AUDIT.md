# Fase 57 — Auditoria final de inicialização e builds

Data: 30/10/2026 (America/Sao_Paulo)
Commit auditado: `dffaf297a3974fee8d7295ecc2680fb5389f4fe5`

## Achado e correção

A auditoria encontrou três cadeias sem proteção em `BuildWorld()`: o retorno de `MakeCube(...)` era encadeado diretamente com `AddComponent<NexoInteractable>().Initialize(...)`. Se a criação da malha falhasse, a transição para gameplay poderia lançar `NullReferenceException` e fechar o app. A correção introduziu `InitializeInteractable(GameObject, ...)`, que valida o objeto, o componente e captura falhas de inicialização antes de continuar. O restante do bootstrap mantém fallback de câmera, material/shader, luz, persistência e `RuntimeDiagnostics`.

## Validação local

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=25`).
- `python tools/validate_learning_content.py`: PASS.
- `python tools/test_agents.py`: PASS (9 testes).
- `git diff --check`: PASS.
- Unity/Android SDK/Xcode locais: indisponíveis; validação runtime feita no GameCI.

## Evidências GameCI

- Unity Tests `41077497576`: **SUCCESS**; jobs EditMode e PlayMode concluídos com sucesso.
- Android `41077501341`: **SUCCESS**; artefato baixado em `C:/Temp/nexo-android-41077501341/Android/Nexo.apk`, 31.264.343 bytes, SHA256 `6319022DF96CB56B94039CBA388B34A8B09D3DA8C88B5C387601E80B59D772AE`. `BUILD_METADATA.txt` vincula Unity 6000.3.0f1, commit e run.
- iOS `41077505847`: **FAIL/CANCELLED** enquanto o job aguardava runner macOS; nenhum artefato foi produzido.
- Retentativa iOS `41080123981`: permaneceu `queued` sem runner e foi cancelada para encerrar a janela pública; requer nova janela com capacidade macOS.

## Privacidade

O repositório foi restaurado para `private`; API confirmou `disabled=false`. Nenhum segredo foi incluído em logs ou documentação.

## Estado

`CANDIDATO TÉCNICO APROVADO PARA ANDROID`; iOS permanece `BLOCKED` por disponibilidade do runner macOS, não por erro de código demonstrado nesta execução.
