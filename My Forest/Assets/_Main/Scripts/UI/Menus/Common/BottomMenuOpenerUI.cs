using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using DG.Tweening;
using Zenject;

namespace MyForest.UI
{
    public class BottomMenuOpenerUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraGesturesControlSource _cameraGesturesControlSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private GameObject _mainContainer = null;
        [SerializeField] private RectTransform _container = null;
        [SerializeField] private Button _backgroundButton = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private float _transitionTime = default;

        [Header("EVENTS")]
        [SerializeField] private UnityEvent _onAppear = default;
        [SerializeField] private UnityEvent _onDisappear = default;

        private Sequence _currentSequence = null;

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
            _backgroundButton.onClick.AddListener(Disappear);
        }

        public void Appear()
        {
            OnAppear();

            _currentSequence.Kill();
            _currentSequence = DOTween.Sequence();

            _currentSequence.AppendCallback(() => _mainContainer.SetActive(true));
            _currentSequence.Append(_container.DOAnchorPosY(default, _transitionTime));
        }

        public void Disappear()
        {
            OnDisappear();

            _currentSequence.Kill();
            _currentSequence = DOTween.Sequence();

            _currentSequence.Append(_container.DOAnchorPosY(-_container.rect.height, _transitionTime));
            _currentSequence.AppendCallback(() => _mainContainer.SetActive(false));
        }

        private void OnAppear()
        {
            _cameraGesturesControlSource.BlockInput();
            _onAppear?.Invoke();
        }

        private void OnDisappear()
        {
            _cameraGesturesControlSource.EnableInput();
            _onDisappear?.Invoke();
        }

        #endregion
    }
}
