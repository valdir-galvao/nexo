# NEXO — Relatório de configuração de projeto (P0)

Data: 26/10/2026. Este é o baseline recomendado, condicionado às decisões
pendentes e à validação em dispositivos reais.

## Matriz de decisões

| Área | Configuração proposta | Justificativa e limite |
| --- | --- | --- |
| Unity | Unity 6.3 LTS, projeto 3D URP | É a LTS atual consultada no arquivo oficial da Unity; URP reduz custo de iteração e escala para Android/iOS. A versão só será fixada após verificar Unity Hub/licença e espaço. |
| Licença | Unity Personal, se a elegibilidade da equipe permitir | Não foi possível identificar conta/plano local; confirmar no Hub antes de criar projeto. |
| Renderização | URP baixa/média; sem HDRP; iluminação baked/simple real-time | GPU integrada e 16 GiB de RAM tornam HDRP e perfil ultrarrealista impróprios para iteração local. |
| Android mínimo | Android 10+, 4 GB RAM, ARM64, Vulkan 1.1 ou OpenGL ES 3.1 | Perfil inicial conservador para a faixa 8–18; confirmar cobertura de dispositivos e APIs em P1. |
| FPS | 30 FPS sustentados; orçamento de 33.3 ms/frame | Prioriza estabilidade e bateria no dispositivo mínimo. 60 FPS fica como modo opcional após benchmark. |
| Budget de cena | inicial: ≤100 draw calls, ≤500k triângulos visíveis, textura e memória definidas por benchmark | São limites de trabalho para o protótipo, não garantia universal; ajustar após profiling. |
| iOS | Build local indisponível; CI macOS (ex.: GitHub Actions macOS runner) | SO é Windows. Assinatura, certificados e provisioning permanecem decisões protegidas e pendentes. |
| Controle de versão | Git + `.gitignore` Unity + `.gitattributes`; Git LFS para binários grandes | Evita versionar Library/Build e reduz conflitos de assets. LFS deve ser instalado/configurado antes de adicionar binários. |
| Cenas/código | Prefabs isolados; ScriptableObjects para dados; cenas aditivas somente quando profiling justificar | Reduz conflitos de merge e mantém a fatia pequena; uma cena principal e subcenas de conteúdo no P1. |
| Save | Arquivo local versionado/atômico, schema explícito e validação | Sustenta offline, recuperação e SEC-001; sem backend obrigatório. |
| Câmera | Unity Camera System inicialmente; Cinemachine só após confirmar compatibilidade e necessidade | Evita adicionar dependência antes do protótipo; câmera terceira pessoa suave, zoom limitado e sem motion sickness. |
| Direção visual | 3D estilizado com materiais simples, paleta de fantasia e iluminação baked; “ultrarrealismo” reservado a referências e composição | Compatível com GPU local e mobile; assets realistas de alta qualidade podem ser usados em baixa complexidade após benchmark. |

## Marcos mensuráveis da fatia vertical

| Marco | Evidência de conclusão |
| --- | --- |
| M1 — Mecânica | Em build Android, jogador explora, encontra 3 pistas, aloca água em 2 alternativas e repara o reservatório offline. |
| M2 — Pipeline | Projeto reproduz a build Android em máquina limpa documentada; iOS produz artefato em CI macOS quando credenciais forem aprovadas. |
| M3 — Usabilidade | 5 playtests; ≥4 identificam o objetivo e ≥4 completam tutorial sem intervenção. |
| M4 — Aprendizagem | Rubrica/pré-pós-tarefa mede recurso-prioridade-consequência; eventos locais correspondem aos objetivos LR-001/LR-002. |
| M5 — Profiling | Dispositivo mínimo sustenta 30 FPS na cena crítica, sem perda de save, e orçamento de draw calls/triângulos é registrado. |

## Plano de ação para o PATH e instalação

Não executar automaticamente. Primeiro liberar espaço para uma margem operacional
de pelo menos 40–60 GB e confirmar D-001. Os comandos abaixo são verificações e
instalações orientativas; o instalador Unity Hub requer licença/conta e o CI iOS
requer um repositório remoto e segredos configurados separadamente.

### Verificações PowerShell

```powershell
Get-Command winget, git, python, java, adb, unity, UnityHub -ErrorAction SilentlyContinue
Get-PhysicalDisk | Select FriendlyName,MediaType,BusType,HealthStatus,Size
Get-PSDrive C | Select Used,Free
```

### Unity Hub e editor

```powershell
winget search Unity.Hub
winget install --id Unity.UnityHub --exact
```

Depois, no Unity Hub, instalar Unity 6.3 LTS com módulos Android Build Support,
Android SDK & NDK Tools e OpenJDK. Não existe um comando seguro universal para
aceitar a licença Unity nem para selecionar a conta; fazer essa etapa na UI do
Hub e registrar a versão instalada em `DECISION_LOG.md`.

### Git LFS

```powershell
winget install --id GitHub.GitLFS --exact
git lfs install
git check-attr filter -- Assets 2>$null
```

### Android fora do Hub

Só usar se o módulo do Hub não puder ser instalado. Instalar Android Studio pela
fonte oficial, abrir SDK Manager e instalar platform/build-tools aprovados por
D-001. Não misturar SDKs sem registrar caminho e versão; validar com `adb version`
e `adb devices` após conectar um dispositivo de teste autorizado.

### iOS

Não há comando local nesta máquina: `xcodebuild` é INDISPONÍVEL no Windows.
Após o projeto existir e o repositório remoto ser aprovado, configurar workflow
CI com runner macOS, Unity license secret, certificados e provisioning protegidos.
Nenhum segredo deve entrar no repositório ou neste relatório.

## Bloqueios antes de P1

1. Liberar espaço no disco primário ou indicar volume alternativo.
2. Confirmar Unity 6.3 LTS/Unity Personal ou decisão diferente.
3. Definir modelo de dispositivo Android mínimo e obter pelo menos um aparelho.
4. Definir acesso a runner macOS para a futura build iOS.
5. Registrar os quatro primeiros itens como decisões antes da instalação.
