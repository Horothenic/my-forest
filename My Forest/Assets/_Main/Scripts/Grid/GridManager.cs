using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class GridManager
    {
        [Inject] private IForestDataSource _forestDataSource = null;
        [Inject] private IGridConfigurationsSource _gridConfigurationsSource = null;
        [Inject] private IGridPositioningSource _gridPositioningSource = null;
        [Inject] private IObjectPoolSource _objectPoolSource = null;
        
        private float _squareRootOfThree = 0;
        private float _threeOverTwo = 0;
        
        private readonly Dictionary<Coordinates, TileData> _tilesMap = new Dictionary<Coordinates, TileData>();
    }

    public partial class GridManager : IInitializable
    {
        void IInitializable.Initialize()
        {
            _squareRootOfThree = Mathf.Sqrt(3.0f);
            _threeOverTwo = 3.0f / 2.0f;

            LoadTileMap(_forestDataSource.ForestData);
            _forestDataSource.ForestPostLoadObservable.Subscribe(LoadTileMap);
        }

        private void LoadTileMap(ForestData forestData)
        {
            if (forestData == null) return;

            _tilesMap.Clear();
            foreach (var forestElementData in forestData.ForestElements)
            {
                AddTile(forestElementData.TileData);
            }
        }
        
        private void AddTile(TileData newTile)
        {
            _tilesMap.Add(newTile.Coordinates, newTile);
        }
    }

    public partial class GridManager : IGridServiceSource
    {
        HexagonTile IGridServiceSource.CreateTile(Transform parent, TileData tileData)
        {
            var tile = _objectPoolSource.Borrow(_gridConfigurationsSource.TilePrefab);
            tile.gameObject.Set(_gridPositioningSource.GetWorldPosition(tileData.Coordinates), parent);

            tile.Initialize(tileData);
            return tile;
        }
    }

    public partial class GridManager : IGridPositioningSource
    {
        Vector3 IGridPositioningSource.GetWorldPosition(Coordinates coordinates)
        {
            var q = coordinates.Q;
            var r = coordinates.R;

            var positionX = _gridConfigurationsSource.TileRadius * _squareRootOfThree * (q + r / 2f);
            var positionZ = _gridConfigurationsSource.TileRadius * _threeOverTwo * r;

            return new Vector3(positionX, 0, positionZ);
        }
    }
}
