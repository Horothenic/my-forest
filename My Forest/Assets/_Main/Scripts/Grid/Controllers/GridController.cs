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
        [Inject] private IObjectPoolSource _objectPoolSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private Transform _gridParent = null;
        [SerializeField] private HexagonTile _tilePrefab = null;

        private readonly Dictionary<TileCoordinates, HexagonTile> _tiles = new Dictionary<TileCoordinates, HexagonTile>();

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
            _gridDataSource.GridObservable.Subscribe(LoadGrid).AddTo(this);
            _gridDataSource.NewTileAddedObservable.Subscribe(CreateNewTile).AddTo(this);

            LoadGrid(_gridDataSource.GridData);
        }

        private void ResetGrid()
        {
            foreach (var tile in _tiles.Values)
            {
                _objectPoolSource.Return(tile.gameObject);
            }

            _tiles.Clear();
        }

        private void LoadGrid(GridData gridData)
        {
            ResetGrid();

            foreach (var tileData in gridData.Tiles)
            {
                CreateTile(tileData);
            }

            var tilesPositions = _tiles.Values.Select(tile => tile.transform.position).ToList();
            _cameraGesturesControlSource.UpdateDragLimits(tilesPositions);
        }

        private HexagonTile CreateTile(TileData tileData)
        {
            var tile = _objectPoolSource.Borrow<HexagonTile>(_tilePrefab);
            tile.gameObject.Set(_gridPositioningSource.GetWorldPosition(tileData.Coordinates), _gridParent);

            _tiles.Add(tileData.Coordinates, tile);
            tile.Initialize(tileData);
            return tile;
        }

        private void CreateNewTile(TileData tileData)
        {
            var newTile = CreateTile(tileData);
            _cameraGesturesControlSource.UpdateDragLimits(newTile.transform.position);
        }

        #endregion
    }
}
