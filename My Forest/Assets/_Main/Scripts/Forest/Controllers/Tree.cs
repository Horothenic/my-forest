using UnityEngine;

using Zenject;
using UniRx;

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

        #endregion

        #region METHODS

        public void Initialize(TreeData treeData)
        {
            _treeData = treeData;
            _growthDataSource.GrowthChangedObservable.Subscribe(OnGrowthChanged).AddTo(this);
        }

        private void OnGrowthChanged(GrowthData growthData)
        {
            var age = _growthDataSource.GrowthData.CurrentGrowth - _treeData.CreationGrowth;
            var currentLevel = _treeData.Configuration.GetConfigurationLevelByAge(age);

            if (currentLevel == null) return;

            if (currentLevel == _currentLevel)
            {
                OnTreeSizeChanged(age);
                return;
            }

            OnTreeLevelChanged(currentLevel);
        }

        private void OnTreeLevelChanged(TreeConfiguration.TreeConfigurationLevel newLevel)
        {
            _currentLevel = newLevel;
            _objectPoolSource.Return(_currentTree);
            _currentTree = _objectPoolSource.Borrow(_currentLevel.Prefab);
            _currentTree.SetLocal(Vector3.zero, transform);

            _currentTreeBaseSize = _currentLevel.Prefab.transform.localScale;
            _currentTree.transform.localScale = _currentTreeBaseSize;
        }

        private void OnTreeSizeChanged(int age)
        {
            var steps = age - _currentLevel.GrowthNeeded;

            if (steps == default) return;

            if (_currentLevel.HasMaxSteps)
            {
                steps = Mathf.Clamp(steps, default, _currentLevel.MaxSizeSteps);
            }

            _currentTree.transform.localScale = _currentTreeBaseSize + Vector3.one * steps * _currentLevel.ExtraSizeStep;
        }

        #endregion
    }
}
