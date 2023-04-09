using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace MyForest
{
    [RequireComponent(typeof(Button))]
    public class PlaySoundOnButtonClick : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IAudioPlaySoundSource _audioPlaySoundSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private AudioClip _sound = null;

        #endregion

        #region UNITY

        private void Awake()
        {
            Initialize();
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            GetComponent<Button>().onClick.AddListener(() => _audioPlaySoundSource.PlaySound(_sound));
        }

        #endregion
    }
}
