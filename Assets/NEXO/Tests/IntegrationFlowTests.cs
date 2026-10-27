using NUnit.Framework;
using Nexo.Core;
using Nexo.Gameplay;
using Nexo.Learning;
using Nexo.Persistence;
using Nexo.Progression;
using Nexo.Safety;

namespace Nexo.Tests
{
    internal sealed class IntegrationMemorySaveStore : ISaveStore
    {
        public SaveData data;
        public SaveData Load() { return data; }
        public bool Save(SaveData value) { data = value; return true; }
    }

    public sealed class IntegrationFlowTests
    {
        [Test] public void FullMissionFlowPersistsAfterExitAndReopen()
        {
            IntegrationMemorySaveStore store = new IntegrationMemorySaveStore();
            VerticalSliceMission mission = new VerticalSliceMission(store);
            Assert.IsTrue(mission.Initialize());
            Assert.AreEqual(GameState.Intro, mission.StateMachine.State);
            Assert.IsTrue(mission.SkipIntro());
            Assert.IsTrue(mission.RecordClue(ClueId.Source));
            Assert.IsTrue(mission.RecordClue(ClueId.Channel));
            Assert.IsTrue(mission.RecordClue(ClueId.Valve));
            Assert.IsTrue(mission.EvaluateValve(0));
            Assert.AreEqual("valve-compare-markers", mission.LastFeedbackId);
            Assert.IsTrue(mission.Pause());
            Assert.IsTrue(mission.Resume());
            Assert.IsTrue(mission.EvaluateValve(1));
            Assert.IsTrue(mission.Allocate(WaterAllocation.Reservoir));
            Assert.IsTrue(mission.Complete());

            VerticalSliceMission reopened = new VerticalSliceMission(store);
            Assert.IsTrue(reopened.Initialize());
            Assert.IsTrue(reopened.Save.activeMission.completed);
            Assert.AreEqual(GameState.MissionComplete, reopened.StateMachine.State);
        }

        [Test] public void LegacySaveMigratesToCurrentSchema()
        {
            SaveData migrated = SaveMigration.FromV0(new LegacySaveDataV0 {
                missionId = VerticalSliceMission.MissionId, water = 4, checkpoint = "exploration"
            });
            Assert.IsTrue(SaveValidation.IsValid(migrated));
            Assert.AreEqual(1, migrated.schemaVersion);
            Assert.AreEqual(4, migrated.activeMission.water);
        }

        [Test] public void OfflineSafetyPolicyIsPartOfIntegrationContract()
        {
            Assert.IsTrue(SafetyPolicy.IsMvpSafeConfiguration());
            AdaptiveDecision decision = LearningRules.ChooseSupport(1, false, false);
            Assert.IsTrue(decision.repeatWithoutPenalty);
        }
    }
}
