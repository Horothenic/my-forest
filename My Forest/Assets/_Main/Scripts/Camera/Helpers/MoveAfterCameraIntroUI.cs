using UnityEngine;

using Zenject;
using UniRx;
using DG.Tweening;

namespace MyForest
{
    [RequireComponent(typeof(RectTransform))]
    public class MoveAfterCameraIntroUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraIntroSource _cameraIntroSource = null;

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
            _cameraIntroSource.IntroEndedObservable.Subscribe(OnIntroFinished).AddTo(_disposables);
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
