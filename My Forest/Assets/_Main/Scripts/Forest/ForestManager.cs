using System;
using System.Collections.Generic;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class ForestManager
    {
        #region FIELDS

        [Inject] private ITreeCollectionSource _elementConfigurations = null;

        #endregion
    }

    public partial class ForestManager : DataManager<ForestData>
    {
        protected override string Key => Constants.Forest.FOREST_DATA_KEY;

        protected override void OnLoadReady(ref ForestData data)
        {
            if (data.IsEmpty)
            {
                data = _saveSource.LoadJSONFromResources<ForestData>(Constants.Forest.DEFAULT_FOREST_DATA_FILE);
            }

            foreach (var element in data.Trees)
            {
                element.Hydrate(_elementConfigurations);
            }
        }
    }

    public partial class ForestManager : IInitializable
    {
        void IInitializable.Initialize()
        {
            Load();
        }
    }

    public partial class ForestManager : IForestDataSource
    {
        IObservable<ForestData> IForestDataSource.ForestObservable => DataObservable;
    }
}
