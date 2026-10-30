# Fase 59 — Execução do smoke Android

Foi integrado o job `android-emulator` diretamente em `.github/workflows/unity-android.yml`, evitando o problema anterior de workflow não reconhecido na branch padrão. O job depende do build Android, baixa o APK do mesmo run e executa o AVD headless API 35.

Commit de infraestrutura: `719a223`.

Validação local: YAML válido, Static QA PASS (`json=5`, `cs=25`), `git diff --check` PASS.

Run remoto `41081847271`: permaneceu no passo `Build Android package` por aproximadamente 20 minutos sem publicar logs de erro ou chegar ao job do emulador. Foi cancelado para evitar consumo indefinido. Nenhum resultado de instalação/emulador foi declarado.

O repositório foi restaurado para PRIVATE (`disabled=false`). O APK anterior `41077501341` continua válido como artefato, mas não possui diagnóstico de execução em AVD.
