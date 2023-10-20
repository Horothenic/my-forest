using System;
using UnityEngine;

namespace MyForest
{
    public interface IGridDataSource
    {
        GridData GridData { get; }
        IObservable<GridData> GridObservable { get; }
        IObservable<TileData> NewTileAddedObservable { get; }
    }

    public interface IGridEventSource
    {
        TileData CreateRandomTileForBiome(BiomeType biomeType);
    }

    public interface IGridPositioningSource
    {
        Vector3 GetWorldPosition(Coordinates coordinates);
    }

    public interface IGridConfigurationsSource
    {
        float HexagonRadius { get; }
        Color GetBiomeColor(BiomeType biomeType);
    }
}
