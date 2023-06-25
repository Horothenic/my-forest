using UnityEngine;
using System.Collections.Generic;

using TMPro;
using Zenject;
using UniRx;

namespace MyForest
{
    public class GrowthTrackerUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private DiContainer _container = null;
        [Inject] private IGrowthDataSource _growthDataSource = null;
        [Inject] private IGrowthTrackSource _growthTrackSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _lowLimitStepText = null;
        [SerializeField] private TextMeshProUGUI _highLimitStepText = null;
        [SerializeField] private RectTransform _currentStepGizmo = null;
        [SerializeField] private RectTransform _stepsContainer = null;
        [SerializeField] private RectTransform _stepsStartingPoint = null;
        [SerializeField] private GrowthTrackerStepUI _stepPrefab = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private int _stepsToShow = 10;

        private int _lastLowLimit = -1;

        private readonly List<GrowthTrackerStepUI> _steps = new List<GrowthTrackerStepUI>();

        #endregion

        #region UNITY

        private void Start()
        {
            Initialize();
            _growthDataSource.GrowthChangedObservable.Subscribe(Refresh).AddTo(this);
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            var positionStep = _stepsContainer.rect.width / (float)_stepsToShow;
            for (var i = 0; i < _stepsToShow + 1; i++)
            {
                var newStep = _container.Instantiate(_stepPrefab, _stepsStartingPoint.position, Quaternion.identity, _stepsContainer);
                newStep.GetRectTransform().anchoredPosition += Vector2.right * positionStep * i;
                _steps.Add(newStep);
            }
        }

        private void Refresh(GrowthData growthData)
        {
            var currentGrowth = growthData.CurrentGrowth;
            var lowLimit = (currentGrowth / _stepsToShow) * _stepsToShow;
            var currentStep = currentGrowth % _stepsToShow;

            _currentStepGizmo.position = _steps[currentStep].transform.position;

            if (_lastLowLimit >= lowLimit) return;

            _lastLowLimit = lowLimit;
            var highLimit = lowLimit + _stepsToShow;

            _lowLimitStepText.text = lowLimit.ToString();
            _highLimitStepText.text = highLimit.ToString();

            for (var i = 0; i < _steps.Count; i++)
            {
                _steps[i].Initialize(_growthTrackSource.GetEventsForGrowth(lowLimit + i));
            }
        }

        #endregion
    }
}
