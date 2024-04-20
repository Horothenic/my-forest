using System;

using UniRx;
using Zenject;

namespace MyForest
{
    public partial class ForestManager
    {
        #region FIELDS
        
        [Inject] private ITerrainInitializationSource _terrainInitializationSource;

        private readonly Subject<ForestElementData> _forestElementChangedSubject = new Subject<ForestElementData>();

        #endregion
    }

    public partial class ForestManager : DataManager<ForestData>
    {
        protected override string Key => Constants.Forest.FOREST_DATA_KEY;

        protected override void Initialize()
        {
            
        }

        protected override void OnPreLoad(ref ForestData data)
        {
            _terrainInitializationSource.SetSeed("MyForest");

            if (data.IsEmpty)
            {
                data = _saveSource.LoadJSONFromResources<ForestData>(Constants.Forest.DEFAULT_FOREST_DATA_FILE);
            }
        }
    }

    public partial class ForestManager : IForestDataSource
    {
        ForestData IForestDataSource.ForestData => Data;
        IObservable<ForestData> IForestDataSource.ForestPreLoadObservable => PreLoadObservable;
        IObservable<ForestData> IForestDataSource.ForestPostLoadObservable => PostLoadObservable;
        IObservable<ForestElementData> IForestDataSource.ForestElementChangedObservable => _forestElementChangedSubject.AsObservable();
    }

    public partial class ForestManager : IForestEventsSource
    {
        void IForestEventsSource.DiscoverTile(Coordinates coordinates)
        {
            var newForestElementData = new ForestElementData
            (
                Data.ForestElementsCount,
                coordinates
            );
            
            Data.AddForestElement(newForestElementData);
            Save();
            _forestElementChangedSubject.OnNext(newForestElementData);
        }
    }
}
