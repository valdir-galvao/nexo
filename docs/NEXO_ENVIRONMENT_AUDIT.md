# NEXO — Auditoria de hardware e ambiente local

Data: 26/10/2026, America/Sao_Paulo. Auditoria somente leitura; não houve
instalação, compra, publicação ou alteração de conta externa.

## Sistema operacional e ferramentas

| Item | Evidência | Resultado |
| --- | --- | --- |
| Sistema operacional | Windows 11 Pro, build 26200, 64-bit; `dxdiag` confirmou Windows 11 Pro 64-bit | Suporta ferramentas Windows; não é macOS |
| Arquitetura | x64-based PC | Compatível com editor Windows e Android tooling |
| iOS local | `xcodebuild` ausente e Xcode só é suportado em macOS | INDISPONÍVEL localmente; usar CI/CD com runner macOS para iOS |
| Ferramentas encontradas | Git; Python 3.12; `vulkaninfo` | Git/Python/Vulkan disponíveis |
| Ferramentas não encontradas | Unity/Unity Hub, `dotnet`, `adb`, `xcodebuild` | P1 não pode começar sem provisionamento verificado |

## CPU, RAM e paginação

| Item | Medição |
| --- | --- |
| CPU | Intel Core i5-9500T @ 2.20 GHz, 6 cores / 6 threads, turbo reportado 2.208 GHz |
| RAM física | 16,943,194,112 bytes (aprox. 15.8 GiB; `dxdiag` arredonda para 16,384 MB) |
| Pagefile | `C:\pagefile.sys`, alocado 16,158 MB; uso atual 1,008 MB; pico 1,401 MB |
| Virtualização firmware | Não reportada como habilitada |

O orçamento de memória é limitado para Unity, Android tooling e navegador
simultâneos. Usar uma única instância do Editor, fechar processos pesados e
manter cache em disco; não aumentar instâncias ou importar assets grandes sem
liberar espaço.

## GPU e APIs gráficas

| Item | Medição |
| --- | --- |
| GPU | Intel UHD Graphics 630, integrada; 128 MB dedicada e até 8,079 MB compartilhada reportada pelo dxdiag |
| Driver | 31.0.101.2135, 05/03/2025 |
| DirectX | DirectX 12; Feature Levels 12_1, 12_0, 11_1, 11_0, 10_1, 10_0, 9_3, 9_2, 9_1 |
| WDDM | 3.1 |
| Vulkan | Instance 1.3.301; GPU API 1.3.215; dispositivo integrado Intel UHD 630 |
| OpenGL ES | Não é API nativa de execução Windows verificada. Será validada no Android-alvo; o perfil mínimo proposto exige OpenGL ES 3.1 ou Vulkan 1.1. |

O editor e a cena devem usar URP com qualidade baixa/média, iluminação baked
ou simples e limite de resolução. A GPU local não sustenta validar a promessa
de “ultrarrealismo”; a qualidade final deve ser medida em dispositivos-alvo.

## Disco e E/S

| Item | Medição |
| --- | --- |
| Disco primário | SSSTC CL1-8D256-HP, NVMe, GPT, saudável |
| Capacidade | 256,060,514,304 bytes (aprox. 238.5 GiB) |
| Livre em C: | 16,136,536,064 bytes (aprox. 15.0 GiB) |
| Sistema de arquivos | NTFS |
| E/S | Tipo NVMe identificado. `winsat` não produziu métricas utilizáveis nesta execução; não inventar throughput. |

O espaço livre é insuficiente para instalar com folga Unity, módulos Android,
cache e builds. Liberar espaço ou usar outro volume antes do provisionamento.

## Conclusão operacional

Desenvolvimento local Android/Desktop é tecnicamente plausível após liberar
disco e instalar o toolchain. Compilação iOS local é indisponível neste SO;
planejar assinatura e build em macOS CI, com segredos fornecidos apenas no
ambiente protegido do CI. Não executar instalação antes da decisão D-001 e da
confirmação de espaço/dispositivo.
