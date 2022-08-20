using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public class ForestController : MonoBehaviour
    {
        #region FIELDS

        private const int CELL_SIZE = 1;

        [Inject] private IForestDataSource _forestDataSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private ForestObjectPool _pool = null;
        [SerializeField] private Transform _root = null;

        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region UNITY

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region METHODS

        private async void Initialize()
        {
            await _pool.HydratePoolMap();
            _forestDataSource.ForestDataObservable.Subscribe(BuildForest).AddTo(_disposables);
        }

        private void BuildForest(ForestData forestData)
        {
            ResetForest();

            for (int i = 0; i < forestData.Length; i++)
            {
                var prefabName = forestData.ElementPrefabs[i];
                var gameObject = _pool.Borrow(prefabName);

                if (gameObject == null) return;

                gameObject.Set(forestData.ElementPositions[i], forestData.ElementRotations[i], _root);
            }
        }

        private void ResetForest()
        {
            foreach (Transform t in _root)
            {
                _pool.Return(t.gameObject);
            }
        }

        #endregion
    }
}
