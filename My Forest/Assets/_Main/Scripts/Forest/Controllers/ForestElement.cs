using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public class ForestElement : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IObjectPoolSource _objectPoolSource = null;
        [Inject] private IForestDataSource _forestDataSource = null;
        [Inject] private IGrowthDataSource _growthDataSource = null;

        private TreeData _treeData = null;
        private GameObject _currentPrefab = null;
        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region METHODS

        public void Initialize(TreeData treeData)
        {
            _treeData = treeData;
            _forestDataSource.GetTreeDataObservable(treeData).Subscribe(OnForestElementDataUpdated).AddTo(_disposables);
            SpawnCurrentLevelElement();
        }

        private void SpawnCurrentLevelElement()
        {
            var age = _growthDataSource.GrowthData.CurrentGrowthDays - _treeData.CreationDay;
            var currentLevel = _treeData.Configuration.GetConfigurationLevelByAge(age);
            _currentPrefab = _objectPoolSource.Borrow(currentLevel.Prefab);
            _currentPrefab.SetLocal(Vector3.zero, transform);
        }

        private void OnForestElementDataUpdated(TreeData treeData)
        {
            _treeData = treeData;
            _objectPoolSource.Return(_currentPrefab);
            SpawnCurrentLevelElement();
        }

        #endregion
    }
}
