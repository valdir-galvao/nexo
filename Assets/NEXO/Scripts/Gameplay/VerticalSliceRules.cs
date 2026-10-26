using System;
using Nexo.Progression;

namespace Nexo.Gameplay
{
    public enum ClueId { Source, Channel, Valve }

    [Serializable]
    public struct WaterAttempt
    {
        public WaterAllocation allocation;
        public int valveIndex;
    }

    [Serializable]
    public struct RuleFeedback
    {
        public bool success;
        public string feedbackId;
        public string explanation;
        public bool unlocksArea;
    }

    public static class VerticalSliceRules
    {
        public static RuleFeedback EvaluateValve(WaterAttempt attempt)
        {
            bool aligned = MissionRuleEngine.IsValveAligned(attempt.valveIndex);
            return new RuleFeedback {
                success = aligned,
                feedbackId = aligned ? "valve-aligned" : "valve-compare-markers",
                explanation = aligned ? "Entrada e saída agora formam um caminho." : "Compare os marcadores de entrada e saída.",
                unlocksArea = false
            };
        }

        public static RuleFeedback EvaluateWater(WaterAttempt attempt)
        {
            bool valid = attempt.allocation == WaterAllocation.Reservoir || attempt.allocation == WaterAllocation.Garden;
            return new RuleFeedback {
                success = valid,
                feedbackId = valid ? "allocation-confirmed" : "allocation-choose-target",
                explanation = valid ? "A escolha muda o caminho da água e pode ser revisada." : "Escolha o reservatório ou a horta.",
                unlocksArea = attempt.allocation == WaterAllocation.Reservoir
            };
        }
    }
}
