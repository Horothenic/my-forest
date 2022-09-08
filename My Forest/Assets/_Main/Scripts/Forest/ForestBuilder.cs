using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public class ForestBuilder : MonoBehaviour
    {
        #region FIELDS

        private const int CELL_SIZE = 1;

        [Inject] private IForestDataSource _forestDataSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private ForestObjectPool _objectPool = null;
        [SerializeField] private ForestElementConfigurations _elementConfigurations = null;
        [SerializeField] private Transform _root = null;

        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region UNITY

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion

        #region METHODS

        private async void Initialize()
        {
            await _objectPool.HydratePoolMap();
            _forestDataSource.ForestDataObservable.Subscribe(BuildForest).AddTo(_disposables);
        }

        private void Dispose()
        {
            _disposables.Dispose();
        }

        private void BuildForest(ForestData forestData)
        {
            ResetForest();

            for (int i = 0; i < forestData.GroundSize; i++)
            {
                var groundElementData = forestData.GroundElements[i];
                SetGroundElement(groundElementData);
            }

            for (int i = 0; i < forestData.ForestSize; i++)
            {
                var forestElementData = forestData.ForestElements[i];
                SetForestElement(forestElementData);
            }
        }

        private void SetGroundElement(GroundElementData groundElementData)
        {
            var newElement = _objectPool.Borrow(groundElementData?.GroundName);

            if (newElement == null) return;

            newElement.Set(groundElementData.Position, _root);
        }

        private void SetForestElement(ForestElementData forestElementData)
        {
            var elementConfiguration = _elementConfigurations.GetElementConfiguration(forestElementData?.ElementName);
            var prefab = elementConfiguration.GetLevelPrefab(forestElementData.Level);

            var newElement = _objectPool.Borrow(prefab);

            if (newElement == null) return;

            newElement.Set(forestElementData.Position, _root);
        }

        private void ResetForest()
        {
            foreach (Transform t in _root)
            {
                _objectPool.Return(t.gameObject);
            }
        }

        #endregion
    }
}
