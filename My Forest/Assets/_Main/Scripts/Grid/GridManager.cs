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
            var tile = _objectPoolSource.Borrow(_gridConfigurationsSource.HexagonPrefab);
            tile.gameObject.Set(_gridPositioningSource.GetWorldPosition(tileData.Coordinates), parent);

            tile.Initialize(tileData);
            return tile;
        }
        
        TileData IGridServiceSource.GetRandomTileDataForBiome(Biome biome)
        {
            var possibleOriginTiles = _tilesMap.Values.Where(t => !t.Surrounded && t.Biome == biome).ToList();

            if (possibleOriginTiles.Count == default(int))
            {
                possibleOriginTiles = _tilesMap.Values.ToList();
            }
                
            var originTile = possibleOriginTiles.GetRandom();
            var originSurroundingCoordinates = GetTileSurroundingCoordinates(originTile.Coordinates, shuffle: true);
            
            Coordinates newCoordinates = default;
            foreach (var surroundingCoordinate in originSurroundingCoordinates)
            {
                if (_tilesMap.ContainsKey(surroundingCoordinate)) continue;
                
                newCoordinates = surroundingCoordinate;
                break;
            }

            var newTileData = new TileData
            (
                biome,
                newCoordinates,
                false
            );

            AddTile(newTileData);
            CheckIfNewTileSurroundedAnotherTile(newTileData);

            return newTileData;
        }

        private void CheckIfNewTileSurroundedAnotherTile(TileData newTile)
        {
            var possibleSurroundedTilesCoordinates = GetTileSurroundingCoordinates(newTile.Coordinates, includeSelf: true);

            foreach (var possibleSurroundedTileCoordinate in possibleSurroundedTilesCoordinates)
            {
                if (!_tilesMap.TryGetValue(possibleSurroundedTileCoordinate, out var possibleSurroundedTile))
                {
                    continue;
                }
                
                var surroundingTilesCoordinates = GetTileSurroundingCoordinates(possibleSurroundedTileCoordinate);
                var surrounded = true;
                
                foreach (var surroundingTileCoordinate in surroundingTilesCoordinates)
                {
                    if (_tilesMap.ContainsKey(surroundingTileCoordinate)) continue;
                    
                    surrounded = false;
                    break;
                }

                if (surrounded)
                {
                    possibleSurroundedTile.SetAsSurrounded();
                }
            }
        }

        private IReadOnlyList<Coordinates> GetTileSurroundingCoordinates(Coordinates coordinates, bool shuffle = false, bool includeSelf = false)
        {
            var q = coordinates.Q;
            var r = coordinates.R;
            var possibleTiles = new List<Coordinates>();

            if (includeSelf)
            {
                possibleTiles.Add(new Coordinates(q, r));
            }

            possibleTiles.Add(new Coordinates(q, r + 1));
            possibleTiles.Add(new Coordinates(q + 1, r));
            possibleTiles.Add(new Coordinates(q + 1, r - 1));
            possibleTiles.Add(new Coordinates(q, r - 1));
            possibleTiles.Add(new Coordinates(q - 1, r));
            possibleTiles.Add(new Coordinates(q - 1, r + 1));

            return shuffle ? possibleTiles.Shuffle() : possibleTiles;
        }
    }

    public partial class GridManager : IGridPositioningSource
    {
        Vector3 IGridPositioningSource.GetWorldPosition(Coordinates coordinates)
        {
            var q = coordinates.Q;
            var r = coordinates.R;

            var positionX = _gridConfigurationsSource.HexagonRadius * _squareRootOfThree * (q + r / 2f);
            var positionZ = _gridConfigurationsSource.HexagonRadius * _threeOverTwo * r;

            return new Vector3(positionX, 0, positionZ);
        }
    }
}
