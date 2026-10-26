using System;
using Nexo.Core;

namespace Nexo.Gameplay
{
    [Serializable]
    public struct InteractionContext
    {
        public string actorId;
        public string missionId;
    }

    [Serializable]
    public struct InteractionResult
    {
        public bool accepted;
        public string feedbackId;
    }

    [Serializable]
    public struct PuzzleAttempt
    {
        public string puzzleId;
        public int[] values;
    }

    [Serializable]
    public struct AttemptResult : IAttemptResult
    {
        public bool success;
        public string feedbackId;
        public int attempts;

        public bool Success { get { return success; } }
        public string FeedbackId { get { return feedbackId ?? string.Empty; } }
    }

    public interface IInteractionTarget
    {
        string Id { get; }
        bool CanInteract(InteractionContext context);
        InteractionResult Interact(InteractionContext context);
    }

    public interface IPuzzleDefinition
    {
        string Id { get; }
        AttemptResult Evaluate(PuzzleAttempt attempt);
    }

    public interface IAttemptResult
    {
        bool Success { get; }
        string FeedbackId { get; }
    }
}
