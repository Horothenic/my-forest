using System;
using System.Collections.Generic;

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

        #region METHODS

        private void OnForestElementDataChanged(ForestElementData newForestElementData)
        {
            Save();
            _forestElementChangedSubject.OnNext(newForestElementData);
        }
        
        #endregion
    }

    public partial class ForestManager : DataManager<ForestData>
    {
        protected override string Key => Constants.Forest.FOREST_DATA_KEY;

        protected override void OnPreLoad(ref ForestData data)
        {
            _terrainInitializationSource.SetSeed("MyForest");

            /*if (data.IsEmpty)
            {
                data = _saveSource.LoadJSONFromResources<ForestData>(Constants.Forest.DEFAULT_FOREST_DATA_FILE);
            }*/

            var testElements = new List<ForestElementData>();

            for (var i = -50; i < 50; i++)
            {
                for (var j = -50; j < 50; j++)
                {
                    testElements.Add(new ForestElementData
                    (
                        testElements.Count,
                        (i, j)
                    ));
                } 
            }
                
            data = new ForestData(testElements);
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
            OnForestElementDataChanged(newForestElementData);
        }
    }
}
