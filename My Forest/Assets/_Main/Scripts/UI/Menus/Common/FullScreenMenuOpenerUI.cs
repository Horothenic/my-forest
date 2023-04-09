using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using DG.Tweening;
using Zenject;

namespace MyForest.UI
{
    public class FullScreenMenuOpenerUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraGesturesControlSource _cameraGesturesControlSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private Button[] _toggleButtons = null;
        [SerializeField] private RectTransform _mainContainer = null;
        [SerializeField] private RectTransform _innerContainer = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private float _innerContainerTransitionTime = 0.2f;

        [Header("EVENTS")]
        [SerializeField] private UnityEvent _onAppear = default;
        [SerializeField] private UnityEvent _onDisappear = default;

        private Vector2 _originalInnerContainerPosition = default;

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
            foreach (var button in _toggleButtons)
            {
                button.onClick.AddListener(ToggleContainer);
            }

            var anchoredPosition = _innerContainer.anchoredPosition;
            _originalInnerContainerPosition = anchoredPosition;

            anchoredPosition += Vector2.up * _mainContainer.rect.height;
            _innerContainer.anchoredPosition = anchoredPosition;
        }

        private void ToggleContainer()
        {
            var appear = !_mainContainer.gameObject.activeSelf;

            if (appear)
            {
                _innerContainer.DOAnchorPos(_originalInnerContainerPosition, _innerContainerTransitionTime);
                OnAppear();
            }
            else
            {
                _innerContainer.anchoredPosition += Vector2.up * _mainContainer.rect.height;
                OnDisappear();
            }

            _mainContainer.gameObject.SetActive(appear);
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
