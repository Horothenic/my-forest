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
        private GameObject _currentPrefab = null;

        public TreeData TreeData => _treeData;

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

            if (currentLevel == null || currentLevel == _currentLevel) return;

            _currentLevel = currentLevel;
            _objectPoolSource.Return(_currentPrefab);
            _currentPrefab = _objectPoolSource.Borrow(_currentLevel.Prefab);
            _currentPrefab.SetLocal(Vector3.zero, transform);
        }

        #endregion
    }
}
