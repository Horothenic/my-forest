using DG.Tweening;
using UnityEngine;

using Zenject;

namespace MyForest
{
    public class CameraIntroController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraIntroSource _cameraIntroSource = null;
        [Inject] private ICameraGesturesControlSource _cameraGesturesControlSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private Camera _camera = null;
        
        [Header("UI")]
        [SerializeField] private GameObject _container = null;
        [SerializeField] private CanvasGroup _blocker = null;

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
            if (_cameraIntroSource.HasFirstIntroAlreadyPlayed)
            {
                NormalIntro();
                return;
            }

            FirstIntro();
        }

        private void FirstIntro()
        {
            _camera.orthographicSize = 20;
                
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(0.4f);
            sequence.AppendCallback(_cameraIntroSource.IntroStarted);
            sequence.Insert(0.2f, DOTween.To(() => _camera.orthographicSize, size => _camera.orthographicSize = size, 6, 2.4f).SetEase(Ease.InQuad));
            sequence.Insert(0.2f, _blocker.DOFade(0, 0.45f));
            sequence.AppendCallback(() => _container.TurnOff());
            sequence.AppendInterval(0.3f);
            sequence.AppendCallback(_cameraIntroSource.IntroEnded);
            sequence.AppendCallback(_cameraIntroSource.FirstIntroPlayed);
            sequence.AppendCallback(_cameraGesturesControlSource.EnableInput);
        }

        private void NormalIntro()
        {
            var sequence = DOTween.Sequence();;
            sequence.AppendCallback(_cameraIntroSource.IntroStarted);
            sequence.Append(_blocker.DOFade(0, 0.3f));
            sequence.AppendCallback(() => _container.TurnOff());
            sequence.AppendInterval(0.2f);
            sequence.AppendCallback(_cameraIntroSource.IntroEnded);
            sequence.AppendCallback(_cameraGesturesControlSource.EnableInput);
        }

        #endregion
    }
}
