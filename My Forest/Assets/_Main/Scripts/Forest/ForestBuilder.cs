using UnityEngine;
using System.Collections.Generic;

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
        [SerializeField] private ForestObjectPool _pool = null;
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
            await _pool.HydratePoolMap();
            _forestDataSource.ForestDataObservable.Subscribe(BuildForest).AddTo(_disposables);
        }

        private void Dispose()
        {
            _disposables.Dispose();
        }

        private void BuildForest(ForestData forestData)
        {
            ResetForest();

            for (int i = 0; i < forestData.ForestSize; i++)
            {
                var cell = forestData.ForestCells[i];

                SetForestElement(cell.Ground);
                SetForestElement(cell.Tree);

                foreach (var element in cell.Decorations)
                {
                    SetForestElement(element);
                }
            }
        }

        private void SetForestElement(ForestElementData forestElementData)
        {
            var newElement = _pool.Borrow(forestElementData?.PrefabName);

            if (newElement == null) return;

            newElement.Set(forestElementData.Position, _root);
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
