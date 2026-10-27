using System;

namespace Nexo.Learning
{
    public enum MasteryLevel { NotAssessed, Developing, InitialEvidence, ConfirmedMultipleContexts, NeedsReview }
    public enum LearningEventKind { Correct, Incorrect, HintRequested, Paused, AbandonedByPause, ComprehensionDifficulty, TransferCorrect, TransferIncorrect }

    [Serializable]
    public struct LearningObservation
    {
        public string competencyId;
        public int difficulty;
        public LearningEventKind kind;
        public bool usedHint;
        public int attemptNumber;
    }

    [Serializable]
    public sealed class CompetencyState
    {
        public string competencyId;
        public MasteryLevel level;
        public int independentCorrect;
        public int supportedCorrect;
        public int incorrect;
        public int transferCorrect;
        public int transferAttempts;
        public int reviewDueInDays;
    }

    public struct AdaptiveDecision
    {
        public string supportId;
        public bool repeatWithoutPenalty;
        public bool showExplanation;
    }

    public static class LearningRules
    {
        public static AdaptiveDecision ChooseSupport(int incorrectAttempts, bool paused, bool comprehensionDifficulty)
        {
            if (paused) return new AdaptiveDecision { supportId = "resume-summary", repeatWithoutPenalty = true, showExplanation = false };
            if (comprehensionDifficulty) return new AdaptiveDecision { supportId = "rephrase-and-diagram", repeatWithoutPenalty = true, showExplanation = true };
            if (incorrectAttempts >= 2) return new AdaptiveDecision { supportId = "explicit-relation", repeatWithoutPenalty = true, showExplanation = true };
            if (incorrectAttempts == 1) return new AdaptiveDecision { supportId = "causal-feedback", repeatWithoutPenalty = true, showExplanation = true };
            return new AdaptiveDecision { supportId = "contextual-question", repeatWithoutPenalty = true, showExplanation = false };
        }

        public static MasteryLevel Classify(CompetencyState state)
        {
            if (state == null) return MasteryLevel.NotAssessed;
            if (state.transferAttempts > 0 && state.transferCorrect == 0 && state.independentCorrect > 0)
                return MasteryLevel.NeedsReview;
            if (state.transferCorrect >= 1 && state.transferAttempts >= 1 && state.independentCorrect >= 2)
                return MasteryLevel.ConfirmedMultipleContexts;
            if (state.independentCorrect >= 2 || state.supportedCorrect >= 2)
                return MasteryLevel.InitialEvidence;
            if (state.incorrect > 0 || state.supportedCorrect > 0)
                return MasteryLevel.Developing;
            return MasteryLevel.NotAssessed;
        }

        public static int ReviewIntervalDays(MasteryLevel level)
        {
            switch (level)
            {
                case MasteryLevel.ConfirmedMultipleContexts: return 30;
                case MasteryLevel.InitialEvidence: return 7;
                case MasteryLevel.NeedsReview: return 1;
                default: return 7;
            }
        }
    }
}
