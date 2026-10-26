using NUnit.Framework;
using Nexo.Core;
using Nexo.Gameplay;
using Nexo.Persistence;
using Nexo.Progression;

namespace Nexo.Tests
{
    internal sealed class MemorySaveStore : ISaveStore
    {
        public SaveData data;
        public SaveData Load() { return data; }
        public bool Save(SaveData value) { data = value; return true; }
    }

    public sealed class VerticalSliceMissionTests
    {
        [Test] public void StateMachineRejectsInvalidCompletionFromExploration()
        {
            MissionStateMachine machine = new MissionStateMachine();
            Assert.IsFalse(machine.TryTransition(GameSignal.MissionFinished));
            Assert.AreEqual(GameState.Initializing, machine.State);
        }

        [Test] public void MissionCannotCompleteTwice()
        {
            MemorySaveStore store = new MemorySaveStore();
            VerticalSliceMission mission = new VerticalSliceMission(store);
            Assert.IsTrue(mission.Initialize());
            Assert.IsTrue(mission.SkipIntro());
            Assert.IsTrue(mission.EvaluateValve(1));
            Assert.IsTrue(mission.Allocate(WaterAllocation.Reservoir));
            Assert.IsTrue(mission.Complete());
            Assert.IsFalse(mission.Complete());
        }

        [Test] public void WrongValveProducesFeedbackWithoutPenalty()
        {
            MemorySaveStore store = new MemorySaveStore();
            VerticalSliceMission mission = new VerticalSliceMission(store);
            mission.Initialize(); mission.SkipIntro();
            Assert.IsTrue(mission.EvaluateValve(0));
            Assert.AreEqual("valve-compare-markers", mission.LastFeedbackId);
            Assert.AreEqual(StartingWaterForTest(), mission.Save.activeMission.water);
        }

        private static int StartingWaterForTest() { return VerticalSliceMission.StartingWater; }
    }
}
