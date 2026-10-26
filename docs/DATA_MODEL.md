# NEXO — Data Model P2

## Dados persistidos

`SaveData` contém `contentVersion`, `schemaVersion`, missões concluídas, estado
de cada missão, evidências de domínio, revisões previstas, configurações de
acessibilidade e um checksum. Não contém nome real, localização, contatos,
biometria, perfil psicológico ou texto livre infantil.

## Identificadores

IDs são strings estáveis em kebab-case (`mission-silent-reservoir`,
`objective-priority`). Conteúdo removido recebe migração ou fallback; nunca se
reusa um ID para outro significado.

## Esquema mínimo

```text
SaveData
  schemaVersion: int
  contentVersion: ContentVersion
  completedMissionIds: string[]
  activeMission: MissionRuntimeState?
  masteryEvidence: MasteryEvidence[]
  scheduledReviews: ReviewItem[]
  accessibility: AccessibilitySettings
  checksum: string

MissionRuntimeState
  missionId, phase, water, discoveredClueIds, allocation, valveIndex
  checkpoint, started, completed
```

Versão 1 aceita apenas valores de enum conhecidos, água entre 0 e 6 e índices de
válvula entre 0 e 2. Falha de parse, checksum ou invariantes restaura o backup
`.bak`; se ambos falharem, oferece reinício seguro sem apagar o arquivo corrompido.

## Armazenamento

O P1 usa JSON versionado em `Application.persistentDataPath` por ser pequeno e
inspecionável. SQLite fica fora do escopo até existir volume de dados que justifique
dependência e validação multiplataforma. Escrita é atômica: arquivo temporário,
flush, replace e backup anterior.
