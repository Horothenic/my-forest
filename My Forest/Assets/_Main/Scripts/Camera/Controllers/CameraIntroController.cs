using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public class CameraIntroController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraFirstIntroSource _cameraFirstIntroSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private Animator _animator = null;

        #endregion

        #region UNITY

        private void Start()
        {
            SelectIntro();
        }

        #endregion

        #region METHODS

        private void SelectIntro()
        {
            if (_cameraFirstIntroSource.HasFirstIntroAlreadyPlayed)
            {
                _animator.SetTrigger(Constants.Camera.INTRO_KEY);
                return;
            }

            _animator.SetTrigger(Constants.Camera.FIRST_INTRO_KEY);
            _cameraFirstIntroSource.FirstIntroPlayed();
        }

        public void IntroFinishedPlaying()
        {
            _cameraFirstIntroSource.IntroFinishedPlaying();
        }

        #endregion
    }
}
