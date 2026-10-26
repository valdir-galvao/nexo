using NUnit.Framework;
using Nexo.Gameplay;
using Nexo.Progression;
using Nexo.Persistence;

namespace Nexo.Tests
{
    public sealed class MissionRuleEngineTests
    {
        [Test] public void AllocationConsumesTwoWater() {
            Assert.AreEqual(4, MissionRuleEngine.ApplyAllocation(6, WaterAllocation.Reservoir));
        }

        [Test] public void AllocationWithoutEnoughWaterIsRecoverable() {
            Assert.AreEqual(1, MissionRuleEngine.ApplyAllocation(1, WaterAllocation.Garden));
        }

        [Test] public void OnlyMiddleValvePositionIsAligned() {
            Assert.IsFalse(MissionRuleEngine.IsValveAligned(0));
            Assert.IsTrue(MissionRuleEngine.IsValveAligned(1));
            Assert.IsFalse(MissionRuleEngine.IsValveAligned(2));
        }

        [Test] public void InvalidSaveIsRejected() {
            SaveData save = new SaveData();
            save.activeMission = new MissionRuntimeState { water = 9, valveIndex = 1 };
            Assert.IsFalse(SaveValidation.IsValid(save));
        }
    }
}
