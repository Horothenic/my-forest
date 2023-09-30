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

        [Inject] private IObjectPoolSource _objectPoolSource = null;
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
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            var positionStep = _stepsContainer.rect.width / (float)_stepsToShow;
            for (var i = 0; i < _stepsToShow + 1; i++)
            {
                var newStep = _objectPoolSource.Borrow(_stepPrefab);
                newStep.gameObject.Set(_stepsStartingPoint.position, _stepsContainer).SetScale(Vector3.one);

                newStep.GetRectTransform().anchoredPosition += Vector2.right * positionStep * i;
                _steps.Add(newStep);
            }

            _growthDataSource.GrowthChangedObservable.Subscribe(Refresh).AddTo(this);
            
            Refresh(_growthDataSource.GrowthData);
        }

        private void Refresh(GrowthData growthData)
        {
            var currentGrowth = growthData.CurrentGrowth;

            if (currentGrowth == 0 && _lastLowLimit > currentGrowth)
            {
                _lastLowLimit = -1;
            }

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
