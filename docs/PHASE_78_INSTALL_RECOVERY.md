# NEXO — PHASE 78: Unity 6000.3.0f1 installation recovery

Date: 2026-11-01 01:32 America/Sao_Paulo
Status: BLOCKED (storage/location constraint)

## Preservation

- 54e67ec preserved and remains an ancestor of the current branch.
- 1500265 preserved as the Phase 77 environment report commit.
- docs/PHASE_75_FORENSIC_ROOT_CAUSE.md and docs/PHASE_76_DEPENDENCY_VALIDATION.md preserved.
- No gameplay logic, manifest, lockfile, workflow, or signing configuration was changed.
- No APK or IPA was generated.

## Evidence from previous attempts

Unity Hub/CLI log: C:\Users\valdir.galvao\AppData\Roaming\UnityHub\logs\cli-log.json.

The relevant failure is:

- EditorInstaller: UnitySetup64-6000.3.0f1 (1).exe /S /D=P:\Unity\Hub\Editor\6000.3.0f1.
- InstallDriver: Module 6000.3.0f1-x86_64 failed to install: Access to the path 'P:\Unity\Hub\Editor\6000.3.0f1' is denied.
- CLI result: INSTALL_FAILED, exit code 6.

The downloaded official installer is complete:

- Size: 4,131,048,704 bytes.
- SHA-256: 1629B8E701AD1B4F5576D6F912FCC1E82AAACEE56BFBAFA75A4D3EB62A1190AC.
- A second identical download has the same hash.

A direct silent installer attempt in P:\NexoUnityEditors\6000.3.0f1 ended with exit code 2 and produced no files. This is consistent with the network filesystem restriction rather than a corrupt download.

## Destination audit

- P: free space: approximately 1.228 TB.
- P: display root: \\arquivoscorp.segplan.goias.intra\\ArquivosNFS\\FINBATCH\\prod\\ITAU\\Retorno\\Processar.
- P: is a mapped NFS/network share, not local NTFS. Ordinary file writes succeed, but Unity Editor extraction/installation is rejected.
- C: free space: approximately 126 MB, insufficient for the ~4.13 GB installer plus the extracted Unity Editor. Existing 6000.6.7f1 installation was not removed or modified.
- No incomplete Editor tree was found under P:\Unity\Hub\Editor or P:\NexoUnityEditors.

## Conclusion and recovery path

The exact version is available through the official Unity distribution channel, and the download is intact. Installation is blocked because the only spacious destination is an NFS share and the local SSD is full. The safe recovery is to provide a local NTFS volume/runner with sufficient space (preferably at least 15 GB free), configure Unity Hub's install path there, and rerun the install. Do not use 6000.6.7f1 as a substitute and do not delete existing installations to make room.

## Tests and dependencies

- Static QA: previously PASS (json=5, cs=26).
- git diff --check: PASS.
- Unity Package Manager resolution: BLOCKED until exact Editor is installed.
- EditMode/PlayMode: BLOCKED/NOT_RUN.
- TextMeshPro shader verification: BLOCKED until packages resolve.
- Android/iOS builds: NOT_RUN by scope.
