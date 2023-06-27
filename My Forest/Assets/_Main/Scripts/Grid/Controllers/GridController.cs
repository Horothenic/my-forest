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
            _gridDataSource.NewTileAddedObservable.Subscribe(CreateTile).AddTo(this);
        }

        private void CalculateParameters()
        {
            _gridPositioningSource.SetRadius(_tilePrefab.Radius);
        }

        private void LoadGrid(GridData gridData)
        {
            foreach (var tileData in gridData.Tiles)
            {
                CreateTile(tileData);
            }

            _cameraGesturesControlSource.UpdateDragLimits(_tiles.Values.ToList());
        }

        private void CreateTile(TileData tileData)
        {
            var tile = _container.Instantiate(_tilePrefab, _gridPositioningSource.GetWorldPosition(tileData.Coordinates), Quaternion.identity, _gridParent);
            _tiles.Add(tileData.Coordinates, tile);
            tile.Initialize(tileData);
        }

        #endregion
    }
}
