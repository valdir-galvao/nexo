using UnityEngine;
using Nexo.Progression;

namespace Nexo.Audio
{
    public sealed class NexoAudioDirector : MonoBehaviour
    {
        private AudioSource source;
        private AudioSettingsData settings = new AudioSettingsData();

        public void Configure(AudioSettingsData value)
        {
            settings = value ?? new AudioSettingsData();
            if (source != null) source.volume = settings.muted ? 0f : settings.master * settings.effects;
        }

        public void PlayDiscovery()
        {
            PlayTone(660f, 0.12f);
            Invoke("PlayDiscoveryTail", 0.13f);
        }

        public void PlayFeedback(bool success)
        {
            PlayTone(success ? 520f : 180f, success ? 0.16f : 0.1f);
        }

        private void PlayDiscoveryTail() { PlayTone(880f, 0.14f); }

        private void PlayTone(float frequency, float duration)
        {
            if (settings.muted || settings.master <= 0f) return;
            if (source == null) source = gameObject.AddComponent<AudioSource>();
            source.volume = settings.master * settings.effects;
            int rate = 22050;
            int count = Mathf.CeilToInt(rate * duration);
            AudioClip clip = AudioClip.Create("nexo-tone", count, 1, rate, false);
            float[] samples = new float[count];
            for (int i = 0; i < count; i++)
            {
                float envelope = 1f - (float)i / count;
                samples[i] = Mathf.Sin(2f * Mathf.PI * frequency * i / rate) * envelope * 0.16f;
            }
            clip.SetData(samples, 0);
            source.PlayOneShot(clip);
        }
    }
}
