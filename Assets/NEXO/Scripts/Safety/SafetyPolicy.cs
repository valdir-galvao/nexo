namespace Nexo.Safety
{
    public static class SafetyPolicy
    {
        public const bool NetworkRequiredForMission = false;
        public const bool OpenChatEnabled = false;
        public const bool PrivateMessagingEnabled = false;
        public const bool PurchasesEnabled = false;
        public const bool AdvertisingEnabled = false;
        public const bool PreciseLocationRequired = false;
        public const bool MicrophoneRequired = false;
        public const bool CameraRequired = false;
        public const bool PersistentNotificationsEnabled = false;

        public static bool IsMvpSafeConfiguration()
        {
            return !NetworkRequiredForMission && !OpenChatEnabled && !PrivateMessagingEnabled &&
                   !PurchasesEnabled && !AdvertisingEnabled && !PreciseLocationRequired &&
                   !MicrophoneRequired && !CameraRequired && !PersistentNotificationsEnabled;
        }
    }
}
