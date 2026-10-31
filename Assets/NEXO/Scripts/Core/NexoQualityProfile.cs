using UnityEngine;
using UnityEngine.Rendering;

namespace Nexo.Core
{
    public enum NexoQualityTier
    {
        Low,
        Medium,
        High
    }

    /// <summary>Applies a conservative mobile profile from measurable device capabilities.</summary>
    public static class NexoQualityProfile
    {
        private const string OverrideKey = "NEXO_Quality_Override";
        public static NexoQualityTier Current { get; private set; } = NexoQualityTier.Medium;

        public static void ApplyAutomatic()
        {
            var overrideValue = PlayerPrefs.GetInt(OverrideKey, -1);
            if (overrideValue >= 0 && overrideValue <= 2)
                Apply((NexoQualityTier)overrideValue);
            else
                Apply(DetectTier());
        }

        public static void SetManual(NexoQualityTier tier)
        {
            PlayerPrefs.SetInt(OverrideKey, (int)tier);
            PlayerPrefs.Save();
            Apply(tier);
        }

        private static NexoQualityTier DetectTier()
        {
            int memory = SystemInfo.systemMemorySize;
            int graphicsMemory = SystemInfo.graphicsMemorySize;
            int cores = SystemInfo.processorCount;
            if (memory > 0 && memory <= 3072 || graphicsMemory > 0 && graphicsMemory <= 512 || cores <= 4)
                return NexoQualityTier.Low;
            if (memory <= 6144 || graphicsMemory <= 2048 || cores <= 6)
                return NexoQualityTier.Medium;
            return NexoQualityTier.High;
        }

        private static void Apply(NexoQualityTier tier)
        {
            Current = tier;
            QualitySettings.vSyncCount = 0;
            QualitySettings.antiAliasing = tier == NexoQualityTier.High ? 2 : 0;
            QualitySettings.pixelLightCount = tier == NexoQualityTier.Low ? 1 : 2;
            QualitySettings.shadowDistance = tier == NexoQualityTier.Low ? 0f : tier == NexoQualityTier.Medium ? 25f : 50f;
            QualitySettings.shadows = tier == NexoQualityTier.Low ? ShadowQuality.Disable : ShadowQuality.HardOnly;
            QualitySettings.realtimeReflectionProbes = tier == NexoQualityTier.High;
            QualitySettings.softParticles = tier == NexoQualityTier.High;
            QualitySettings.anisotropicFiltering = tier == NexoQualityTier.Low
                ? AnisotropicFiltering.Disable
                : AnisotropicFiltering.Enable;
            Application.targetFrameRate = tier == NexoQualityTier.Low ? 30 : 60;
        }
    }
}
