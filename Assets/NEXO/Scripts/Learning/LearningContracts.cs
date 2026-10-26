using System;

namespace Nexo.Learning
{
    [Serializable]
    public sealed class LearningObjective : ILearningObjective
    {
        public string id;
        public string competencyId;
        public string Id { get { return id ?? string.Empty; } }
        public string CompetencyId { get { return competencyId ?? string.Empty; } }
    }

    [Serializable]
    public struct MasteryEvidence : IMasteryEvidence
    {
        public string objectiveId;
        public int attempts;
        public bool demonstrated;
        public string ObjectiveId { get { return objectiveId ?? string.Empty; } }
        public int Attempts { get { return attempts; } }
        public bool Demonstrated { get { return demonstrated; } }
    }

    public interface ILearningObjective
    {
        string Id { get; }
        string CompetencyId { get; }
    }

    public interface IMasteryEvidence
    {
        string ObjectiveId { get; }
        int Attempts { get; }
        bool Demonstrated { get; }
    }
}
