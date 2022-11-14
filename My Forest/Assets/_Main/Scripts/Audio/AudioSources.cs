using UnityEngine;

namespace MyForest
{
    public interface IAudioConfigurationsSource
    {
        AudioClip DefaultMusicClip { get; }
        float ChangeVolumeTweenDuration { get; }
        float MaxMusicVolume { get; }
        float MaxSoundVolume { get; }
    }

    public interface IAudioPlaySoundSource
    {
        void PlaySound(AudioClip sound);
    }

    public interface IAudioChangeVolumeSource
    {
        void SetVolumePercentage(AudioType type, float t);
        float GetVolumePercentage(AudioType type);
    }
}
