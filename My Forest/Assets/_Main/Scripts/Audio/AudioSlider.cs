using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace MyForest
{
    public class AudioSlider : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IAudioChangeVolumeSource _audioChangevolumeSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private AudioType _type = default;
        [SerializeField] private Slider _slider = null;

        #endregion

        #region UNITY

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            _slider.value = _audioChangevolumeSource.GetVolumePercentage(_type);
            _slider.onValueChanged.AddListener(ChangeVolume);
        }

        private void ChangeVolume(float newValue)
        {
            _audioChangevolumeSource.SetVolumePercentage(_type, newValue);
        }

        #endregion
    }
}
