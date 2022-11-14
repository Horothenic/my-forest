using UnityEngine;

using Zenject;
using DG.Tweening;

namespace MyForest
{
    public partial class AudioManager
    {
        #region FIELDS

        private const string AUDIO_DATA_KEY = "audio_data";

        [Inject] private ISaveSource _saveSource = null;
        [Inject] private IAudioConfigurationsSource _audioConfigurationsSource = null;
        [Inject] private DiContainer _container = null;

        private AudioData _audioData = null;
        private AudioSource _musicSource = null;
        private AudioSource _soundsSource = null;

        #endregion

        #region METHODS

        private void Initialize()
        {
            Load();

            var audioManagerGameObject = new GameObject(nameof(AudioManager));
            audioManagerGameObject.transform.SetParent(_container.DefaultParent);

            _musicSource = audioManagerGameObject.AddComponent<AudioSource>();
            _soundsSource = audioManagerGameObject.AddComponent<AudioSource>();

            _musicSource.playOnAwake = false;
            _soundsSource.playOnAwake = false;

            SetInitialVolumes();
            SetInitialMusic();
        }

        private void Load()
        {
            var defaultValue = new AudioData(_audioConfigurationsSource.MaxMusicVolume, _audioConfigurationsSource.MaxSoundVolume);
            _audioData = _saveSource.Load<AudioData>(AUDIO_DATA_KEY, defaultValue);
        }

        private void Save()
        {
            _saveSource.Save(AUDIO_DATA_KEY, _audioData);
        }

        private void SetInitialVolumes()
        {
            _musicSource.volume = 0;
            SetVolumeSmooth(_musicSource, _audioData.MusicVolume);

            _soundsSource.volume = _audioData.SoundVolume;
        }

        private void SetInitialMusic()
        {
            _musicSource.clip = _audioConfigurationsSource.DefaultMusicClip;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        private void SetVolumeSmooth(AudioSource audioSource, float newVolume)
        {
            DOTween.To(() => audioSource.volume, x => audioSource.volume = x, newVolume, _audioConfigurationsSource.ChangeVolumeTweenDuration);
        }

        #endregion
    }

    public partial class AudioManager : IInitializable
    {
        void IInitializable.Initialize() => Initialize();
    }

    public partial class AudioManager : IAudioPlaySoundSource
    {
        void IAudioPlaySoundSource.PlaySound(UnityEngine.AudioClip sound)
        {
            _soundsSource.PlayOneShot(sound);
        }
    }

    public partial class AudioManager : IAudioChangeVolumeSource
    {
        float IAudioChangeVolumeSource.GetVolumePercentage(AudioType type)
        {
            float currentVolume = default;
            float maxVolume = default;

            switch (type)
            {
                case AudioType.Music:
                    currentVolume = _audioData.MusicVolume;
                    maxVolume = _audioConfigurationsSource.MaxMusicVolume;
                    break;
                case AudioType.Sound:
                    currentVolume = _audioData.SoundVolume;
                    maxVolume = _audioConfigurationsSource.MaxSoundVolume;
                    break;
            }

            return Mathf.InverseLerp(default, maxVolume, currentVolume);
        }

        void IAudioChangeVolumeSource.SetVolumePercentage(AudioType type, float t)
        {
            AudioSource audioSource = null;
            float maxVolume = default;

            switch (type)
            {
                case AudioType.Music:
                    audioSource = _musicSource;
                    maxVolume = _audioConfigurationsSource.MaxMusicVolume;
                    break;
                case AudioType.Sound:
                    audioSource = _soundsSource;
                    maxVolume = _audioConfigurationsSource.MaxSoundVolume;
                    break;
            }

            audioSource.volume = Mathf.Lerp(default, maxVolume, t);
            _audioData.SetVolume(type, audioSource.volume);

            Save();
        }
    }
}
