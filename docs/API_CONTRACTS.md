# NEXO — API Contracts P2

Os contratos abaixo são agnósticos de apresentação. Implementações Unity podem
usar `ScriptableObject` ou `MonoBehaviour`, mas o domínio consome as interfaces.

```csharp
public interface IMissionDefinition { string Id { get; } IReadOnlyList<string> ObjectiveIds { get; } }
public interface IMissionRuntimeState { string MissionId { get; } int Water { get; } }
public interface IInteractionTarget { string Id { get; } bool CanInteract(InteractionContext context); InteractionResult Interact(InteractionContext context); }
public interface IPuzzleDefinition { string Id { get; } AttemptResult Evaluate(PuzzleAttempt attempt); }
public interface ILearningObjective { string Id { get; } string CompetencyId { get; } }
public interface IAttemptResult { bool Success { get; } string FeedbackId { get; } }
public interface IMasteryEvidence { string ObjectiveId { get; } int Attempts { get; } bool Demonstrated { get; } }
public interface ISaveData { int SchemaVersion { get; } ContentVersion ContentVersion { get; } }
public interface IContentVersion { int Major { get; } int Minor { get; } string Build { get; } }
```

Os tipos concretos e serializáveis estão em `Assets/NEXO/Scripts`. Entradas de
conteúdo passam por `ContentValidator`: IDs não vazios, referências existentes,
limites numéricos e enums permitidos. Conteúdo inválido impede o carregamento da
missão e produz log técnico sem dados pessoais.

Eventos mínimos: `mission_started`, `clue_found`, `allocation_confirmed`,
`valve_repaired`, `objective_attempted`, `mission_completed`. O payload contém
IDs, contagens e resultado; não contém texto digitado por criança.
