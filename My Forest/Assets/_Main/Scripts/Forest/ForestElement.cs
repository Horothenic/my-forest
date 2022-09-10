using UnityEngine;

using Zenject;

namespace MyForest
{
    public class ForestElement : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IObjectPoolSource _objectPoolSource = null;
        [Inject] private IForestDataSource _forestDataSource = null;

        private ForestElementData _forestElementData = null;
        private GameObject _currentElement = null;

        #endregion

        #region UNITY

        private void OnMouseDown()
        {
            TryIncreaseGrowthLevel();
        }

        #endregion

        #region METHODS

        public void Initialize(ForestElementData forestElementData)
        {
            _forestElementData = forestElementData;
            SpawnCurrentLevelElement();
        }

        private void SpawnCurrentLevelElement()
        {
            var currentLevelPrefab = _forestElementData.Configuration.GetLevelPrefab(_forestElementData.Level);
            _currentElement = _objectPoolSource.Borrow(currentLevelPrefab);
            _currentElement.SetLocal(Vector3.zero, transform);
        }

        private void TryIncreaseGrowthLevel()
        {
            if (_forestElementData.IsMaxLevel) return;

            if (!_forestDataSource.TryIncreaseGrowthLevel(_forestElementData)) return;

            _objectPoolSource.Return(_currentElement);
            SpawnCurrentLevelElement();
        }

        #endregion
    }
}
