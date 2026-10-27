using NUnit.Framework;
using Nexo.Learning;

namespace Nexo.Tests
{
    public sealed class LearningRulesTests
    {
        [Test] public void PauseDoesNotBecomeFailure()
        {
            AdaptiveDecision decision = LearningRules.ChooseSupport(2, true, false);
            Assert.IsTrue(decision.repeatWithoutPenalty);
            Assert.AreEqual("resume-summary", decision.supportId);
        }

        [Test] public void TwoIndependentContextsAndTransferConfirmMastery()
        {
            CompetencyState state = new CompetencyState { independentCorrect = 2, transferCorrect = 1, transferAttempts = 1 };
            Assert.AreEqual(MasteryLevel.ConfirmedMultipleContexts, LearningRules.Classify(state));
        }

        [Test] public void TransferDivergenceRequestsReview()
        {
            CompetencyState state = new CompetencyState { independentCorrect = 2, transferAttempts = 1, transferCorrect = 0 };
            Assert.AreEqual(MasteryLevel.NeedsReview, LearningRules.Classify(state));
        }

        [Test] public void HintsProvideSupportWithoutBlockingProgress()
        {
            AdaptiveDecision decision = LearningRules.ChooseSupport(1, false, false);
            Assert.IsTrue(decision.repeatWithoutPenalty);
            Assert.IsTrue(decision.showExplanation);
        }
    }
}
