using UnityEngine;

using Zenject;
using UniRx;
using DG.Tweening;

namespace MyForest
{
    public class Tree : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IObjectPoolSource _objectPoolSource = null;
        [Inject] private IGrowthDataSource _growthDataSource = null;

        private TreeData _treeData = null;
        private TreeConfiguration.TreeConfigurationLevel _currentLevel = null;
        private GameObject _currentTree = null;
        private Vector3 _currentTreeBaseSize = default;
        private Tween _scaleTween = null;

        #endregion

        #region METHODS

        public void Initialize(TreeData treeData, bool withEntryAnimation)
        {
            _treeData = treeData;
            
            _growthDataSource.GrowthChangedObservable.Subscribe(OnGrowthChanged).AddTo(this);
            
            SetStartValues(_growthDataSource.GrowthData, withEntryAnimation);
        }

        private void SetStartValues(GrowthData growthData, bool withAnimation)
        {
            var age = growthData.CurrentGrowth - _treeData.CreationGrowth;
            var currentLevel = _treeData.Configuration.GetConfigurationLevelByAge(age);

            if (currentLevel == null) return;

            SetNewTreeLevel(currentLevel);
            OnTreeSizeChanged(age, false);

            if (withAnimation)
            {
                TriggerEntryAnimation(age);
            }
        }

        private void OnGrowthChanged(GrowthData growthData)
        {
            var age = growthData.CurrentGrowth - _treeData.CreationGrowth;
            var currentLevel = _treeData.Configuration.GetConfigurationLevelByAge(age);

            if (currentLevel == null) return;

            if (currentLevel == _currentLevel)
            {
                OnTreeSizeChanged(age, true);
                return;
            }

            SetNewTreeLevel(currentLevel);
            TriggerEntryAnimation(age);
        }

        private void OnTreeSizeChanged(int age, bool withAnimation)
        {
            var newScale = (_currentTreeBaseSize + Vector3.one * GetStepsNeededForAge(age) * _currentLevel.SizeStep) * _treeData.SizeVariance;
            
            if (withAnimation)
            {
                _scaleTween?.Kill();
                _scaleTween = _currentTree.transform.DOScale(newScale, Constants.ForestElements.SCALE_TRANSITION_TIME).SetEase(Ease.OutQuint);
            }
            else
            {
                _currentTree.transform.localScale = newScale;
            }
        }

        private void SetNewTreeLevel(TreeConfiguration.TreeConfigurationLevel newLevel)
        {
            _currentLevel = newLevel;
            _objectPoolSource.Return(_currentTree);
            _currentTree = _objectPoolSource.Borrow(_currentLevel.Prefab);
            _currentTree.SetLocal(Vector3.zero, Vector3.up * _treeData.Rotation, transform);

            _currentTreeBaseSize = _currentLevel.Prefab.transform.localScale;
        }

        private void TriggerEntryAnimation(int age)
        {
            _scaleTween?.Kill();
            
            var newScale = (_currentTreeBaseSize + Vector3.one * GetStepsNeededForAge(age) * _currentLevel.SizeStep) * _treeData.SizeVariance;
            var startScale = _currentTreeBaseSize * _treeData.SizeVariance * Constants.ForestElements.START_SCALE_FACTOR;
            
            _scaleTween = _currentTree.transform.DOScale(newScale, Constants.ForestElements.SCALE_TRANSITION_TIME).From(startScale).SetEase(Ease.OutBounce);
        }

        private int GetStepsNeededForAge(int age)
        {
            var steps = age - _currentLevel.GrowthNeeded;

            if (_currentLevel.HasMaxSteps)
            {
                steps = Mathf.Clamp(steps, 0, _currentLevel.MaxSizeSteps);
            }

            return steps;
        }

        #endregion
    }
}
