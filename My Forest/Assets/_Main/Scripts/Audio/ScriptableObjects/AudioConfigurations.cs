using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(AudioConfigurations), menuName = MENU_NAME)]
    public partial class AudioConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Audio/" + nameof(AudioConfigurations);

        [SerializeField] private AudioClip _defaultMusicClip = null;
        [SerializeField] private float _changeVolumeTweenDuration = default;
        [SerializeField] private float _maxMusicVolume = default;
        [SerializeField] private float _maxSoundVolume = default;

        #endregion
    }

    public partial class AudioConfigurations : IAudioConfigurationsSource
    {
        AudioClip IAudioConfigurationsSource.DefaultMusicClip => _defaultMusicClip;
        float IAudioConfigurationsSource.ChangeVolumeTweenDuration => _changeVolumeTweenDuration;
        float IAudioConfigurationsSource.MaxMusicVolume => _maxMusicVolume;
        float IAudioConfigurationsSource.MaxSoundVolume => _maxSoundVolume;

    }
}
