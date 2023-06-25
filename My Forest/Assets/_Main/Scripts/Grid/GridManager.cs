using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

using Zenject;

namespace MyForest
{
    public partial class GridManager
    {
        private float _hexRadius = 0;
        private float _squareRootOfThree = 0;
        private float _threeOverTwo = 0;
    }

    public partial class GridManager : DataManager<GridData>
    {
        protected override string Key => Constants.Grid.GRID_DATA_KEY;
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

        void IGridDataSource.AddTile(TileData newTile)
        {
            Data.AddTile(newTile);
            CheckIfNewTileSurroundedAnotherTile(newTile.Coordinates);
            Save();
            EmitData();
        }

        private void CheckIfNewTileSurroundedAnotherTile((int, int) coordinates)
        {
            var tileCoordinatesToCheck = GetTileSurroundingCoordinates(coordinates);

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

        bool IGridDataSource.GetRandomTilePositionForBiome(BiomeType biomeType, out (int, int) foundValue)
        {
            var possibleOriginTiles = Data.Tiles.Where(t => !t.Surrounded && t.BiomeType == biomeType);

            if (possibleOriginTiles.Count() == default)
            {
                possibleOriginTiles = Data.TilesMap.Values;
            }

            possibleOriginTiles.Shuffle();

            foreach (var possibleOriginTile in possibleOriginTiles)
            {
                var surroundingCoordinates = GetTileSurroundingCoordinates((possibleOriginTile.Q, possibleOriginTile.R));

                foreach (var surroundingCoordinate in surroundingCoordinates)
                {
                    if (!Data.TilesMap.ContainsKey((surroundingCoordinate)))
                    {
                        foundValue = surroundingCoordinate;
                        return true;
                    }
                }
            }

            foundValue = default;
            return false;
        }

        private IReadOnlyList<(int, int)> GetTileSurroundingCoordinates((int, int) coordinates)
        {
            var q = coordinates.Item1;
            var r = coordinates.Item2;
            var possibleTiles = new List<(int, int)>();

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

        Vector3 IGridPositioningSource.GetWorldPosition(int q, int r)
        {
            var positionX = _hexRadius * _squareRootOfThree * (q + r / 2f);
            var positionZ = _hexRadius * _threeOverTwo * r;

            return new Vector3(positionX, 0, positionZ);
        }
    }
}
