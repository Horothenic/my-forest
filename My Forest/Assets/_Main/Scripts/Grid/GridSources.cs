using System;
using UnityEngine;

namespace MyForest
{
    public interface IGridDataSource
    {
        IObservable<GridData> GridObservable { get; }
        void AddTile(TileData newTile);
        bool GetRandomTilePositionForBiome(BiomeType biomeType, out (int, int) foundValue);
    }

    public interface IGridPositioningSource
    {
        void SetRadius(float radius);
        Vector3 GetWorldPosition(int q, int r);
    }

    public interface IGridConfigurationsSource
    {
        Color GetBiomeColor(BiomeType biomeType);
    }
}
