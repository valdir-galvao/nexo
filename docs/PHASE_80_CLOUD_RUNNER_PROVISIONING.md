# NEXO — PHASE 80: Windows cloud runner provisioning plan

Date: 2026-11-01 02:00 America/Sao_Paulo
Status: BLOCKED — awaiting provider and cost approval

## Decision and sizing

Use a dedicated Windows x64 VM with a local NTFS SSD, not an NFS/SMB share:

- Recommended target: 8 vCPU, 32 GiB RAM, 150 GB SSD.
- Minimum acceptable target: 4 vCPU, 16 GiB RAM, 100 GB SSD.
- Windows license and internet access must be documented.
- Stop the VM outside CI windows.

## Provider comparison (estimate)

| Provider | Reference VM | Published compute reference | Windows/disk treatment | Assessment |
|---|---|---:|---|---|
| AWS EC2 | 	3.xlarge | 4 vCPU/16 GiB, about **US.167/h** (~US/month if on 730 h) | Windows license and EBS SSD additional | Lowest-cost reference; burstable CPU may slow IL2CPP |
| Azure | D4s v5 | 4 vCPU/16 GiB; regional quote required | Windows and managed SSD additional | Good if an Azure agreement exists |
| Google Cloud | 
2-standard-4 | 4 vCPU/16 GiB, about **US.1814/h** compute | Windows image/license and disk additional | Viable after regional quote |

Prices are USD estimates and can change by region, tax and reservation. No paid resource was created. Sources: https://aws.amazon.com/ec2/instance-types/t3/ and https://cloud.google.com/products/compute/pricing/general-purpose.

## Security design

1. Register the runner only at repository or controlled organization scope.
2. Use a label such as 
exo-windows-unity and protected branches.
3. Store Unity credentials/license and runner registration token only in GitHub Secrets.
4. Do not expose Secrets to forked pull requests or untrusted workflows.
5. Patch Windows, Unity and Android tools on a maintenance schedule; reimage if compromised.

## Installation contract

Install exactly Unity 6000.3.0f1, Android Build Support, Android SDK & NDK Tools and OpenJDK. Use a local path such as C:\Unity\6000.3.0f1; never a mapped network drive. Regenerate Packages/packages-lock.json through Unity Package Manager only.

## Separate workflow after approval

Add a new workflow using uns-on: [self-hosted, windows, x64, nexo-windows-unity] without changing the existing workflows. Gates:

1. Verify OS, free disk, Unity path/version, modules and license.
2. Checkout the protected commit and run Static QA/diff check.
3. Open the project in Unity batch mode to resolve packages; upload lockfile/logs.
4. Run EditMode, then PlayMode; upload JUnit/XML and Editor logs.
5. Stop before APK/IPA builds; build jobs depend on test success.

## Current status

- Provider/cost approval: pending.
- VM and runner: not provisioned/registered.
- Unity 6000.3.0f1: not installed in cloud.
- Package resolution, EditMode and PlayMode: BLOCKED.
- APK/IPA: not generated.
- Commits 54e67ec, 1500265 and d847108 preserved.
