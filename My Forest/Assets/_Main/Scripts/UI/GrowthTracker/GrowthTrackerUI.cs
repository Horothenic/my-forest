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

        [Inject] private IGrowthDataSource _growthDataSource = null;
        [Inject] private IGrowthTrackSource _growthTrackSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _lowLimitStepText = null;
        [SerializeField] private TextMeshProUGUI _highLimitStepText = null;
        [SerializeField] private RectTransform _currentStepGizmo = null;
        [SerializeField] private RectTransform _stepsContainer = null;
        [SerializeField] private RectTransform _stepsStartingPoint = null;
        [SerializeField] private RectTransform _stepPrefab = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private int _stepsToShow = 10;

        private readonly List<RectTransform> _steps = new List<RectTransform>();

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
            for (var i = 0; i < _stepsToShow; i++)
            {
                var newStep = Instantiate(_stepPrefab, _stepsStartingPoint.position, Quaternion.identity, _stepsContainer);
                newStep.anchoredPosition += Vector2.right * positionStep * i;
                _steps.Add(newStep);
            }
        }

        private void Refresh(GrowthData growthData)
        {
            var currentGrowth = growthData.CurrentGrowthDays;

            var lowLimit = currentGrowth / _stepsToShow;
            var highLimit = lowLimit + _stepsToShow;
            var currentStep = currentGrowth % _stepsToShow;

            _lowLimitStepText.text = lowLimit.ToString();
            _highLimitStepText.text = highLimit.ToString();

            _currentStepGizmo.position = _steps[currentStep].position;
        }

        #endregion
    }
}
