using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyForest
{
    public interface IGridDataSource
    {
        IObservable<GridData> GridObservable { get; }
        void AddTiles(IReadOnlyList<TileData> newTiles);
    }
    
    public interface IGridConfigurationsSource
    {
        Color GetBiomeColor(BiomeType biomeType);
    }
}
