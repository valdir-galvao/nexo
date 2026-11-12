using UnityEngine;
using Nexo.Progression;

namespace Nexo.Audio
{
    /// <summary>Audio procedural minimo para feedback local, sem assets externos.</summary>
    public sealed class NexoAudioDirector : MonoBehaviour
    {
        private AudioSource source;
        private AudioSource musicSource;
        private AudioClip ambientClip;
        private AudioSettingsData settings = new AudioSettingsData();
        private bool paused;

        public void Configure(AudioSettingsData value)
        {
            settings = value ?? new AudioSettingsData();
            EnsureSource();
            EnsureMusicSource();
            if (source != null) source.volume = settings.muted ? 0f : settings.master * settings.effects;
            PlayAmbient();
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
            if (value && musicSource != null) musicSource.Pause();
            if (!value && musicSource != null && !musicSource.isPlaying) musicSource.UnPause();
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

        private void EnsureMusicSource()
        {
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                if (musicSource != null)
                {
                    musicSource.playOnAwake = false;
                    musicSource.loop = true;
                    musicSource.spatialBlend = 0f;
                }
            }
            if (ambientClip != null || musicSource == null) return;
            const int rate = 22050;
            const int seconds = 8;
            int count = rate * seconds;
            ambientClip = AudioClip.Create("nexo-procedural-ambient", count, 2, rate, false);
            if (ambientClip == null) return;
            float[] samples = new float[count * 2];
            for (int i = 0; i < count; i++)
            {
                float t = (float)i / rate;
                float pad = Mathf.Sin(2f * Mathf.PI * 110f * t) * 0.035f;
                pad += Mathf.Sin(2f * Mathf.PI * 164.81f * t) * 0.022f;
                pad += Mathf.Sin(2f * Mathf.PI * 220f * t) * 0.014f;
                float pulse = 0.65f + 0.35f * Mathf.Sin(2f * Mathf.PI * 0.25f * t);
                float value = pad * pulse;
                samples[i * 2] = value;
                samples[i * 2 + 1] = value;
            }
            ambientClip.SetData(samples, 0);
            musicSource.clip = ambientClip;
        }

        private void PlayAmbient()
        {
            EnsureMusicSource();
            if (musicSource == null || ambientClip == null || paused || settings.muted || settings.music <= 0f) return;
            musicSource.volume = settings.master * settings.music;
            if (!musicSource.isPlaying) musicSource.Play();
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
