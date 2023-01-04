using UnityEngine;

using Zenject;
using UniRx;
using DG.Tweening;

namespace MyForest
{
    [RequireComponent(typeof(RectTransform))]
    public class MoveOnCameraIntroUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraFirstIntroSource _cameraFirstIntroSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Vector2 _endPosition = default;
        [SerializeField] private float _transitionTime = 0.3f;
        [SerializeField] private float _delay = default;

        private RectTransform _rectTransform = null;
        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region UNITY

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _cameraFirstIntroSource.IntroFinishedObservable.Subscribe(OnIntroFinished).AddTo(_disposables);
        }

        #endregion

        #region METHODS

        private void OnIntroFinished()
        {
            _rectTransform.DOAnchorPos(_endPosition, _transitionTime).SetDelay(_delay);
        }

        #endregion
    }
}
