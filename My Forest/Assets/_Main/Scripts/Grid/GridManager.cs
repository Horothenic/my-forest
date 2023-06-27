using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class GridManager
    {
        private float _hexRadius = 0;
        private float _squareRootOfThree = 0;
        private float _threeOverTwo = 0;

        private Subject<TileData> _newTileAddedSubject = new Subject<TileData>();
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
    }

    public partial class GridManager : IInitializable
    {
        void IInitializable.Initialize()
        {
            Load();

            _squareRootOfThree = Mathf.Sqrt(3.0f);
            _threeOverTwo = 3.0f / 2.0f;
        }
    }

    public partial class GridManager : IGridDataSource
    {
        IObservable<GridData> IGridDataSource.GridObservable => DataObservable;
        IObservable<TileData> IGridDataSource.NewTileAddedObservable => _newTileAddedSubject.AsObservable();
    }

    public partial class GridManager : IGridEventSource
    {
        TileData IGridEventSource.CreateRandomTileForBiome(BiomeType biomeType)
        {
            var possibleOriginTiles = Data.Tiles.Where(t => !t.Surrounded && t.BiomeType == biomeType);

            if (possibleOriginTiles.Count() == default)
            {
                possibleOriginTiles = Data.TilesMap.Values;
            }

            possibleOriginTiles.Shuffle();

            (int, int) coordinates = default;

            foreach (var possibleOriginTile in possibleOriginTiles)
            {
                var surroundingCoordinates = GetTileSurroundingCoordinates(possibleOriginTile.Coordinates);

                foreach (var surroundingCoordinate in surroundingCoordinates)
                {
                    if (!Data.TilesMap.ContainsKey((surroundingCoordinate)))
                    {
                        coordinates = surroundingCoordinate;
                    }
                }
            }

            var newTileData = new TileData
            (
                biomeType,
                coordinates,
                false
            );

            CheckIfNewTileSurroundedAnotherTile(newTileData.Coordinates);
            Data.AddTile(newTileData);
            Save();

            _newTileAddedSubject.OnNext(newTileData);

            return newTileData;
        }

        private void CheckIfNewTileSurroundedAnotherTile((int, int) coordinates)
        {
            var tileCoordinatesToCheck = GetTileSurroundingCoordinates(coordinates, true);

            foreach (var tileCoordinate in tileCoordinatesToCheck)
            {
                var surroundingCoordinates = GetTileSurroundingCoordinates(tileCoordinate);

                var surrounded = true;
                foreach (var surroundingCoordinate in surroundingCoordinates)
                {
                    if (!Data.TilesMap.ContainsKey((surroundingCoordinate)))
                    {
                        surrounded = false;
                        break;
                    }
                }

                if (surrounded)
                {
                    Data.TilesMap[tileCoordinate].SetAsSurrounded();
                }
            }
        }

        private IReadOnlyList<(int, int)> GetTileSurroundingCoordinates((int, int) coordinates, bool includeSelf = false)
        {
            var q = coordinates.Item1;
            var r = coordinates.Item2;
            var possibleTiles = new List<(int, int)>();

            if (includeSelf)
            {
                possibleTiles.Add((q, r));
            }

            possibleTiles.Add((q, r + 1));
            possibleTiles.Add((q + 1, r));
            possibleTiles.Add((q + 1, r - 1));
            possibleTiles.Add((q, r - 1));
            possibleTiles.Add((q - 1, r));
            possibleTiles.Add((q - 1, r + 1));

            return possibleTiles.Shuffle();
        }
    }

    public partial class GridManager : IGridPositioningSource
    {
        void IGridPositioningSource.SetRadius(float radius)
        {
            _hexRadius = radius;
        }

        Vector3 IGridPositioningSource.GetWorldPosition((int, int) coordinates)
        {
            var q = coordinates.Item1;
            var r = coordinates.Item2;

            var positionX = _hexRadius * _squareRootOfThree * (q + r / 2f);
            var positionZ = _hexRadius * _threeOverTwo * r;

            return new Vector3(positionX, 0, positionZ);
        }
    }
}
