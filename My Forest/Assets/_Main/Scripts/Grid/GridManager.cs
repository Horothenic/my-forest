using System;
using System.Collections.Generic;

using Zenject;

namespace MyForest
{
    public partial class GridManager
    {
        #region FIELDS
        
         
        
        #endregion
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
        }
    }

    public partial class GridManager : IGridDataSource
    {
        IObservable<GridData> IGridDataSource.GridObservable => DataObservable;
        
        void IGridDataSource.AddTiles(IReadOnlyList<TileData> newTiles)
        {
            if (newTiles.Count == 0) return;
            
            foreach (var newTile in newTiles)
            {
                Data.AddTile(newTile);
            }
            
            Save();
            EmitData();
        }
    }
}
