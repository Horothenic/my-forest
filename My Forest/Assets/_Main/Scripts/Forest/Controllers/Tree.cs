using DG.Tweening;
using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public class Tree : MonoBehaviour
    {
        #region FIELDS
        
        private const float SCALE_TRANSITION_TIME = 0.5f;
        private static readonly Vector3 StartScaleBeforeAppearing = new Vector3(0.2f, 0.2f, 0.2f);

        [Inject] private IObjectPoolSource _objectPoolSource = null;
        [Inject] private IGrowthDataSource _growthDataSource = null;

        private TreeData _treeData = null;
        private TreeConfiguration.TreeConfigurationLevel _currentLevel = null;
        private GameObject _currentTree = null;
        private Vector3 _currentTreeBaseSize = default;
        private Tween _scaleTween = null;

        #endregion

        #region METHODS

        public void Initialize(TreeData treeData, bool withAnimation)
        {
            _treeData = treeData;
            _growthDataSource.GrowthChangedObservable.Subscribe(OnGrowthChanged).AddTo(this);
            
            SetStartValues(_growthDataSource.GrowthData);

            if (withAnimation)
            {
                TriggerNewTreeAnimation();
            }
        }

        private void SetStartValues(GrowthData growthData)
        {
            var age = growthData.CurrentGrowth - _treeData.CreationGrowth;
            var currentLevel = _treeData.Configuration.GetConfigurationLevelByAge(age);

            if (currentLevel == null) return;

            OnTreeLevelChanged(currentLevel);
            OnTreeSizeChanged(age, false);
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

            OnTreeLevelChanged(currentLevel);
            TriggerNewTreeAnimation();
        }

        private void OnTreeSizeChanged(int age, bool withAnimation)
        {
            var steps = age - _currentLevel.GrowthNeeded;

            if (steps == 0) return;

            if (_currentLevel.HasMaxSteps)
            {
                steps = Mathf.Clamp(steps, 0, _currentLevel.MaxSizeSteps);
            }

            var newScale = (_currentTreeBaseSize + Vector3.one * steps * _currentLevel.SizeStep) * _treeData.SizeVariance;
            
            if (withAnimation)
            {
                _scaleTween?.Kill();
                _scaleTween = _currentTree.transform.DOScale(newScale, SCALE_TRANSITION_TIME).SetEase(Ease.OutQuint);
            }
            else
            {
                _currentTree.transform.localScale = newScale;
            }
        }

        private void OnTreeLevelChanged(TreeConfiguration.TreeConfigurationLevel newLevel)
        {
            _currentLevel = newLevel;
            _objectPoolSource.Return(_currentTree);
            _currentTree = _objectPoolSource.Borrow(_currentLevel.Prefab);
            _currentTree.SetLocal(Vector3.zero, transform);

            _currentTreeBaseSize = _currentLevel.Prefab.transform.localScale;
        }

        private void TriggerNewTreeAnimation()
        {
            _scaleTween?.Kill();
            var startScale = _currentTreeBaseSize * _treeData.SizeVariance;
            _scaleTween = _currentTree.transform.DOScale(startScale, SCALE_TRANSITION_TIME).From(StartScaleBeforeAppearing).SetEase(Ease.OutQuint);
        }

        #endregion
    }
}
