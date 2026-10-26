# NEXO — Decision Log (P0)

Formato: decisão, estado, motivo, consequência, responsável e data.

| ID | Decisão | Estado | Consequência / verificação |
| --- | --- | --- | --- |
| ADR-001 | O primeiro produto é uma fatia vertical da missão “O reservatório silencioso”. | Aprovada pelo briefing | Limita arte, sistemas e testes a uma missão completa; validação no Gate P1. |
| ADR-002 | Android e iOS são plataformas-alvo. | Aprovada pelo briefing | Exige escolher dispositivos mínimos e pipeline de build antes de P1. |
| ADR-003 | Unity com C# é a recomendação inicial de motor. | Provisória | Portabilidade e ecossistema favorecem a escolha; Unity não está instalada/verificada. J02 deve confirmar versão, licenças, SDKs e hardware. |
| ADR-004 | A missão principal funciona offline e não depende de backend. | Aprovada pelo briefing | Saves e eventos mínimos são locais; qualquer serviço futuro será opcional e separado. |
| ADR-005 | Não haverá multiplayer aberto, economia complexa, chat, publicidade obrigatória ou IA generativa na fatia vertical. | Aprovada pelo escopo P0 | Reduz risco de segurança, custo e dependências; cooperação é representada por sistemas locais/narrativos nesta fatia. |
| ADR-006 | Privacidade infantil e minimização de dados são requisitos de aceite, não trabalho posterior. | Aprovada | Bloqueia contas, texto livre e identificadores persistentes até decisão jurídica. |
| ADR-007 | A métrica pedagógica será baseada em ações e decisões observáveis. | Aprovada em princípio | Requer matriz pedagógica e revisão de especialista; não define ainda instrumento psicométrico. |
| ADR-008 | O executor de IA não participa da lógica necessária para completar a missão. | Aprovada | Agentes podem apoiar planejamento/revisão; o jogo permanece determinístico e offline. |
| ADR-009 | Baseline técnico provisório: Unity 6.3 LTS + URP baixa/média, 30 FPS e Android 10+/4 GB. | Provisória, baseada na auditoria | i5-9500T, 16 GiB e UHD 630 favorecem iteração leve; confirmar com Unity Hub, dispositivo mínimo e benchmark. |
| ADR-010 | iOS será compilado em CI macOS; `xcodebuild` local é indisponível no Windows. | Aprovada pela restrição do SO | Depende de repositório/runner e credenciais protegidas; não bloqueia protótipo Android. |
| ADR-011 | Git LFS e estrutura Unity de arquivos serão preparados antes de adicionar assets binários. | Aprovada | `.gitignore` e `.gitattributes` foram criados; instalação/configuração de LFS ainda não verificada. |
| ADR-012 | Direção visual da fatia: 3D estilizado, iluminação baked/simple e câmera Unity; Cinemachine fica opcional. | Provisória | Mantém iteração possível na UHD 630; validar leitura visual e conforto em playtest. |

## Decisões pendentes — registro histórico

As decisões D-001 a D-005 e D-007 a D-009 abaixo foram resolvidas para o
protótipo na seção de fechamento. D-006 continua pendente porque depende de
jurisdição e coleta online futura; isso não bloqueia a missão offline.

| ID | Pergunta | Bloqueia | Dono da decisão | Evidência necessária |
| --- | --- | --- | --- | --- |
| D-001 | Qual é a versão Unity, template e política de licenças? | J03 | Diretor + engenharia | Verificação local e documentação Unity vigente. |
| D-002 | Quais dispositivos mínimos Android/iOS e metas de FPS/memória? | J02/J06 | Produto + engenharia | Inventário de dispositivos e benchmark. |
| D-003 | A cooperação da fatia será assíncrona local, NPC ou outra forma? | J01/J04 | Produto + narrativa | Decisão de design testável, sem multiplayer aberto. |
| D-004 | Quais conceitos curriculares e instrumento de medição? | J01/J05 | Pedagogia + produto | Mapa curricular, faixa etária e rubrica revisados. |
| D-005 | Perspectiva, estilo visual, câmera e direção de arte? | J02/J05 | Arte + produto | Moodboard, protótipo e critérios de acessibilidade. |
| D-006 | Jurisdições de privacidade e processo de consentimento para futuras funções online? | Qualquer coleta online | Jurídico/produto | Parecer e política aprovada; online permanece desligado até lá. |
| D-007 | Quais idiomas entram na primeira fatia? | J05/J08 | Produto | Lista de idiomas e orçamento de localização. |
| D-008 | O disco atual tem apenas cerca de 15 GiB livres; liberar espaço ou usar volume alternativo? | P1 | Produto/infra | Espaço livre ≥40–60 GiB ou volume aprovado. |
| D-009 | Qual runner macOS e política de assinatura serão usados para iOS? | P6/P7 | Release/segurança | CI aprovado, certificados fora do Git e build de teste. |

## Fechamento do escopo P1 (26/10/2026)

O usuário autorizou a conclusão do planejamento e o início do P1 sob as
seguintes premissas verificáveis. Elas valem para o protótipo e podem ser
alteradas somente por novo ADR antes de adicionar dependências ou assets finais:

| ID | Decisão de baseline | Estado | Verificação/limite |
| --- | --- | --- | --- |
| D-001 | Unity 6.3 LTS (`6000.3.0f1`) + URP; Personal em CI; sem instalação local pesada. | Fechada para P1 | Confirmar import no GameCI; secrets permanecem fora do Git. |
| D-002 | Android 10+, 4 GB RAM, ARM64, Vulkan/OpenGL ES 3.1; meta inicial 30 FPS/33,3 ms. iOS equivalente de entrada definido no primeiro dispositivo disponível. | Fechada para protótipo | Benchmark real ainda é obrigatório no P6. |
| D-003 | Cooperação representada por NPC/sistemas locais assíncronos; sem rede, matchmaking ou chat. | Fechada para P1 | Deve ser jogável integralmente offline. |
| D-004 | Objetivo curricular: relacionar recurso, prioridade e consequência; instrumento: rubrica de decisão e eventos locais minimizados. | Fechada para P1 | Revisão pedagógica e teste de eventos no P3/P6. |
| D-005 | 3D estilizado, iluminação baked/simple, câmera Unity em terceira pessoa; Cinemachine opcional. | Fechada para protótipo | Validar leitura, conforto e acessibilidade em playtest. |
| D-007 | Português do Brasil na primeira fatia; localização adicional fora do P1. | Fechada para P1 | Textos devem ser externalizáveis. |
| D-008 | Não instalar Unity/SDK localmente enquanto houver cerca de 15 GiB livres; builds usam GitHub Actions. | Fechada | Reavaliar somente se o espaço livre atingir 40–60 GiB. |
| D-009 | `macos-latest` para projeto Xcode iOS não assinado; certificados ficam em secrets protegidos quando o release exigir assinatura. | Fechada para CI | Assinatura de loja não faz parte do P1. |

| ADR-013 | Domínio em C# puro, adaptadores Unity na borda e contratos por interfaces. | Aprovada para P2 | Permite testes EditMode, reduz acoplamento de cena e compartilha regras Android/iOS. |
| ADR-014 | Persistência inicial em JSON versionado com escrita atômica; SQLite não entra sem necessidade medida. | Aprovada para P2 | Estado pequeno, migrações explícitas e nenhuma dependência de terceiro. |
| ADR-015 | Analytics apenas local e mínimo no protótipo; sem rede obrigatória. | Aprovada para P2 | Eventos têm IDs e contagens, sem texto livre ou dados pessoais. |

## Regra de mudança

Uma mudança que altere plataformas, dados infantis, dependências online, escopo
da missão ou motor deve criar novo ADR, atualizar requisitos/risco e registrar
impacto no roadmap antes de implementação.
