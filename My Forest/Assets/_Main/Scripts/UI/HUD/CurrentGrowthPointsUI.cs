using UnityEngine;

using Zenject;
using UniRx;
using TMPro;
using DG.Tweening;

namespace MyForest.UI
{
    public class CurrentGrowthPointsUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IGrowthDataSource _dataSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _text = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform _animationContainer = null;
        [SerializeField] private float _animationStrength = default;
        [SerializeField] private float _animationDuration = default;

        private CompositeDisposable _disposables = new CompositeDisposable();
        private Sequence _tweenSequence = null;

        #endregion

        #region UNITY

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Clean();
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            UpdateText(_dataSource.GrowthData, false);
            _dataSource.GrowthChangedObservable.Subscribe(data => UpdateText(data, true)).AddTo(_disposables);
        }

        private void Clean()
        {
            _disposables.Dispose();
        }

        private void UpdateText(GrowthData growthData, bool animate)
        {
            if (growthData == null) return;

            _text.text = growthData.CurrentGrowthDays.ToString();

            if (animate)
            {
                TriggerAnimation();
            }
        }

        private void TriggerAnimation()
        {
            _tweenSequence?.Kill();

            _tweenSequence = DOTween.Sequence();

            _tweenSequence.Append(_animationContainer.DOScale(Vector3.one * _animationStrength, _animationDuration / 2).SetEase(Ease.OutQuint));
            _tweenSequence.Append(_animationContainer.DOScale(Vector3.one, _animationDuration / 2).SetEase(Ease.InQuart));
        }

        #endregion
    }
}
