using Nexo.Progression;

namespace Nexo.Persistence
{
    public static class SaveValidation
    {
        public static bool IsValid(SaveData save)
        {
            if (save == null || save.schemaVersion != 1 || save.contentVersion == null)
                return false;
            if (save.activeMission == null)
                return true;
            return save.activeMission.water >= 0 && save.activeMission.water <= 6 &&
                   save.activeMission.valveIndex >= 0 && save.activeMission.valveIndex <= 2;
        }
    }
}
