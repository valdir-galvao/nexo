using System;
using Nexo.Core;
using Nexo.Progression;

namespace Nexo.Persistence
{
    [Serializable]
    public sealed class LegacySaveDataV0
    {
        public string missionId;
        public int water;
        public string checkpoint;
    }

    public static class SaveMigration
    {
        public static SaveData FromV0(LegacySaveDataV0 legacy)
        {
            if (legacy == null) return null;
            return new SaveData
            {
                schemaVersion = 1,
                contentVersion = new ContentVersion(),
                activeMission = new MissionRuntimeState
                {
                    missionId = legacy.missionId,
                    water = legacy.water,
                    checkpoint = legacy.checkpoint,
                    started = !string.IsNullOrEmpty(legacy.missionId)
                }
            };
        }
    }
}
