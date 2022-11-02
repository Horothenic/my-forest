using UnityEngine;

using Zenject;
using DG.Tweening;

namespace MyForest
{
    public partial class AudioManager
    {
        #region FIELDS

        [Inject] private IAudioConfigurationsSource _audioConfigurationsSource = null;
        [Inject] private DiContainer _container = null;

        private AudioSource _musicSource = null;
        private AudioSource _soundsSource = null;

        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS

        private void Initialize()
        {
            var audioManagerGameObject = new GameObject(nameof(AudioManager));
            audioManagerGameObject.transform.SetParent(_container.DefaultParent);

            _musicSource = audioManagerGameObject.AddComponent<AudioSource>();
            _soundsSource = audioManagerGameObject.AddComponent<AudioSource>();

            _musicSource.playOnAwake = false;
            _soundsSource.playOnAwake = false;

            SetInitialVolumes();
            SetInitialMusic();
        }

        private void SetInitialVolumes()
        {
            _musicSource.volume = 0;
            SetVolume(_musicSource, _audioConfigurationsSource.MaxMusicVolume);

            _soundsSource.volume = _audioConfigurationsSource.MaxSoundVolume;
        }

        private void SetInitialMusic()
        {
            _musicSource.clip = _audioConfigurationsSource.DefaultMusicClip;
            _musicSource.loop = true;
            _musicSource.Play();
        }

        private void SetVolume(AudioSource audioSource, float newVolume)
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
}
