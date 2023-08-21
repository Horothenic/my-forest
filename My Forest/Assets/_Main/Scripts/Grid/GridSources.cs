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
        void SetRadius(float radius);
        Vector3 GetWorldPosition(TileCoordinates coordinates);
    }

    public interface IGridConfigurationsSource
    {
        Color GetBiomeColor(BiomeType biomeType);
    }
}
