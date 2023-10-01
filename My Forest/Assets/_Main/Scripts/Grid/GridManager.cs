using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

using Zenject;
using UniRx;
using Random = UnityEngine.Random;

namespace MyForest
{
    public partial class GridManager
    {
        [Inject] private IGridConfigurationsSource _gridConfigurationsSource = null;
        
        private float _squareRootOfThree = 0;
        private float _threeOverTwo = 0;

        private readonly Subject<TileData> _newTileAddedSubject = new Subject<TileData>();
    }

    public partial class GridManager : DataManager<GridData>
    {
        protected override string Key => Constants.Grid.GRID_DATA_KEY;

        protected override void OnPreLoad(ref GridData data)
        {
            if (data.IsEmpty)
            {
                data = _saveSource.LoadJSONFromResources<GridData>(Constants.Grid.DEFAULT_GRID_DATA_FILE);
            }
        }

        protected override void OnPostLoad(GridData data)
        {
            _squareRootOfThree = Mathf.Sqrt(3.0f);
            _threeOverTwo = 3.0f / 2.0f;
        }
    }

    public partial class GridManager : IGridDataSource
    {
        GridData IGridDataSource.GridData => Data;
        IObservable<GridData> IGridDataSource.GridObservable => DataObservable;
        IObservable<TileData> IGridDataSource.NewTileAddedObservable => _newTileAddedSubject.AsObservable();
    }

    public partial class GridManager : IGridEventSource
    {
        TileData IGridEventSource.CreateRandomTileForBiome(BiomeType biomeType)
        {
            var possibleOriginTiles = Data.Tiles.Where(t => !t.Surrounded && t.BiomeType == biomeType).ToList();

            if (possibleOriginTiles.Count == default(int))
            {
                possibleOriginTiles = Data.TilesMap.Values.ToList();
            }
                
            var originTile = possibleOriginTiles.GetRandom();
            var originSurroundingCoordinates = GetTileSurroundingCoordinates(originTile.Coordinates, shuffle: true);
            
            TileCoordinates newTileCoordinates = default;
            foreach (var surroundingCoordinate in originSurroundingCoordinates)
            {
                if (Data.TilesMap.ContainsKey(surroundingCoordinate)) continue;
                
                newTileCoordinates = surroundingCoordinate;
                break;
            }

            var newTileData = new TileData
            (
                biomeType,
                newTileCoordinates,
                false
            );

            Data.AddTile(newTileData);
            CheckIfNewTileSurroundedAnotherTile(newTileData);
            Save();

            _newTileAddedSubject.OnNext(newTileData);

            return newTileData;
        }

        private void CheckIfNewTileSurroundedAnotherTile(TileData newTile)
        {
            var possibleSurroundedTilesCoordinates = GetTileSurroundingCoordinates(newTile.Coordinates, includeSelf: true);

            foreach (var possibleSurroundedTileCoordinate in possibleSurroundedTilesCoordinates)
            {
                if (!Data.TilesMap.TryGetValue(possibleSurroundedTileCoordinate, out var possibleSurroundedTile))
                {
                    continue;
                }
                
                var surroundingTilesCoordinates = GetTileSurroundingCoordinates(possibleSurroundedTileCoordinate);
                var surrounded = true;
                
                foreach (var surroundingTileCoordinate in surroundingTilesCoordinates)
                {
                    if (Data.TilesMap.ContainsKey(surroundingTileCoordinate)) continue;
                    
                    surrounded = false;
                    break;
                }

                if (surrounded)
                {
                    possibleSurroundedTile.SetAsSurrounded();
                }
            }
        }

        private IReadOnlyList<TileCoordinates> GetTileSurroundingCoordinates(TileCoordinates coordinates, bool shuffle = false, bool includeSelf = false)
        {
            var q = coordinates.Q;
            var r = coordinates.R;
            var possibleTiles = new List<TileCoordinates>();

            if (includeSelf)
            {
                possibleTiles.Add(new TileCoordinates(q, r));
            }

            possibleTiles.Add(new TileCoordinates(q, r + 1));
            possibleTiles.Add(new TileCoordinates(q + 1, r));
            possibleTiles.Add(new TileCoordinates(q + 1, r - 1));
            possibleTiles.Add(new TileCoordinates(q, r - 1));
            possibleTiles.Add(new TileCoordinates(q - 1, r));
            possibleTiles.Add(new TileCoordinates(q - 1, r + 1));

            return shuffle ? possibleTiles.Shuffle() : possibleTiles;
        }
    }

    public partial class GridManager : IGridPositioningSource
    {
        Vector3 IGridPositioningSource.GetWorldPosition(TileCoordinates coordinates)
        {
            var q = coordinates.Q;
            var r = coordinates.R;

            var positionX = _gridConfigurationsSource.HexagonRadius * _squareRootOfThree * (q + r / 2f);
            var positionZ = _gridConfigurationsSource.HexagonRadius * _threeOverTwo * r;

            return new Vector3(positionX, 0, positionZ);
        }
    }
}
