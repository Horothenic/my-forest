using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public class GridController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraGesturesControlSource _cameraGesturesControlSource = null;
        [Inject] private IGridDataSource _gridDataSource = null;
        [Inject] private IGridPositioningSource _gridPositioningSource = null;
        [Inject] private DiContainer _container = null;

        [Header("COMPONENTS")]
        [SerializeField] private Transform _gridParent = null;
        [SerializeField] private HexagonTile _tilePrefab = null;

        private readonly Dictionary<(int, int), HexagonTile> _tiles = new();

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
            CalculateParameters();
            _gridDataSource.GridObservable.Subscribe(LoadGrid).AddTo(this);
        }

        private void CalculateParameters()
        {
            _gridPositioningSource.SetRadius(_tilePrefab.Radius);
        }

        private void LoadGrid(GridData gridData)
        {
            if (gridData.IsEmpty)
            {
                _gridDataSource.AddTile(CreateNewTile(BiomeType.Forest, 0, 0));
                return;
            }

            foreach (var tileData in gridData.Tiles)
            {
                LoadTile(tileData);
            }

            _cameraGesturesControlSource.UpdateDragLimits(_tiles.Values.ToList());
        }

        private void LoadTile(TileData tileData)
        {
            var q = tileData.Q;
            var r = tileData.R;

            if (!_tiles.TryGetValue((q, r), out var tile))
            {
                tile = _container.Instantiate(_tilePrefab, _gridPositioningSource.GetWorldPosition(q, r), Quaternion.identity, _gridParent);
                _tiles.Add((q, r), tile);
            }

            tile.Initialize(tileData);
        }

        private TileData CreateNewTile(BiomeType biomeType, int q, int r)
        {
            if (_tiles.ContainsKey((q, r))) return null;

            var tileData = new TileData(biomeType, q, r);
            return tileData;
        }

        #endregion
    }
}
