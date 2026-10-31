using UnityEngine;
using Nexo.Progression;

namespace Nexo.Audio
{
    /// <summary>Audio procedural minimo para feedback local, sem assets externos.</summary>
    public sealed class NexoAudioDirector : MonoBehaviour
    {
        private AudioSource source;
        private AudioSettingsData settings = new AudioSettingsData();
        private bool paused;

        public void Configure(AudioSettingsData value)
        {
            settings = value ?? new AudioSettingsData();
            EnsureSource();
            if (source != null) source.volume = settings.muted ? 0f : settings.master * settings.effects;
        }

        public void PlayDiscovery()
        {
            PlayTone(660f, 0.12f, false, 740f);
            if (!paused) Invoke(nameof(PlayDiscoveryTail), 0.13f);
        }

        public void PlayClick() { PlayTone(420f, 0.055f, true, 360f); }

        public void PlayFeedback(bool success)
        {
            PlayTone(success ? 520f : 180f, success ? 0.16f : 0.1f, !success, success ? 680f : 140f);
        }

        public void PlayPortalUnlock()
        {
            PlayTone(740f, 0.1f, false, 900f);
            if (!paused) Invoke(nameof(PlayPortalUnlockTail), 0.1f);
        }

        public void SetPaused(bool value)
        {
            paused = value;
            AudioListener.pause = value;
            if (value && source != null) source.Stop();
        }

        private void OnApplicationPause(bool pauseStatus) { SetPaused(pauseStatus); }
        private void OnApplicationFocus(bool hasFocus) { SetPaused(!hasFocus); }
        private void PlayDiscoveryTail() { PlayTone(880f, 0.14f, false, 980f); }
        private void PlayPortalUnlockTail() { PlayTone(1040f, 0.16f, false, 1240f); }

        private void EnsureSource()
        {
            if (source != null) return;
            source = gameObject.AddComponent<AudioSource>();
            if (source != null)
            {
                source.playOnAwake = false;
                source.loop = false;
                source.spatialBlend = 0f;
            }
        }

        private void PlayTone(float frequency, float duration, bool squareWave, float endFrequency)
        {
            if (paused || settings == null || settings.muted || settings.master <= 0f || settings.effects <= 0f) return;
            EnsureSource();
            if (source == null) return;
            const int rate = 22050;
            int count = Mathf.Max(1, Mathf.CeilToInt(rate * duration));
            AudioClip clip = AudioClip.Create("nexo-procedural-tone", count, 1, rate, false);
            if (clip == null) return;
            float[] samples = new float[count];
            for (int i = 0; i < count; i++)
            {
                float normalized = (float)i / count;
                float currentFrequency = Mathf.Lerp(frequency, endFrequency, normalized);
                float phase = 2f * Mathf.PI * currentFrequency * i / rate;
                float wave = squareWave ? (Mathf.Sin(phase) >= 0f ? 1f : -1f) : Mathf.Sin(phase);
                float envelope = Mathf.Min(normalized * 18f, 1f) * (1f - normalized);
                samples[i] = wave * envelope * 0.12f;
            }
            clip.SetData(samples, 0);
            source.volume = settings.master * settings.effects;
            source.PlayOneShot(clip);
            Destroy(clip, duration + 0.25f);
        }
    }
}
