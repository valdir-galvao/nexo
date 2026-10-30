# Fase 53 ? Auditoria preventiva de IL2CPP, PlayerSettings, Bootstrap e Android

Data: 30/10/2026 (America/Sao_Paulo)
Branch: `orchestrator/phase-a-baseline`

## 1. IL2CPP/link.xml

`Assets/link.xml` agora preserva explicitamente `UnityEngine.CoreModule`, `UnityEngine.UI`, `UnityEngine.PhysicsModule`, `UnityEngine.Physics2DModule`, `UnityEngine.TextRenderingModule`, `UnityEngine.IMGUIModule` e `UnityEngine.AnimationModule`. O XML foi validado por parser real.

## 2. PlayerSettings e orienta??o

`ProjectSettings/ProjectSettings.asset` foi verificado: Android Min API 29, Target API 35, bundle `0.1.0-rc1`. Como a UI usa refer?ncia 1920x1080, a orienta??o foi fixada em paisagem (`defaultScreenOrientation: 3`) e a rota??o autom?tica do SO desabilitada (`useOSAutorotation: 0`) para evitar troca de layout durante a miss?o.

## 3. Bootstrap

`NexoVerticalSliceBootstrap.cs` recebeu checagens defensivas para avatar, luz, `MeshFilter`, `MeshRenderer`, malha, `BoxCollider`, `NexoInteractable`, c?mera e collider de raycast. Intera??es agora s?o inicializadas por helper seguro; falhas registram erro expl?cito e n?o derrubam o boot.

## 4. AndroidManifest

Criados `Assets/Plugins/Android/AndroidManifest.xml` e metadados Unity. O manifesto n?o solicita permiss?es de hardware, armazenamento, c?mera, microfone ou localiza??o; define `usesCleartextTraffic=false` e `allowBackup=false`. API 35 ? controlada pelo PlayerSettings/Unity, sem `uses-sdk` duplicado no manifesto. XML validado por `xml.etree.ElementTree`.

## Valida??o

- `python tools/run_static_qa.py`: PASS (`json=5`, `cs=24`, exit 0).
- `git diff --check`: PASS (exit 0).
- Unity Editor/GameCI: NOT_RUN; a confirma??o de importa??o e execu??o no APK continua pendente.
