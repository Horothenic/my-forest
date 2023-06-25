using System;

using UniRx;
using Zenject;

namespace MyForest
{
    public partial class ForestManager
    {
        #region FIELDS

        [Inject] private ITreeConfigurationCollectionSource _treeConfigurationCollectionSource = null;

        private Subject<TreeData> _growthDailyClaimAvailableSubject = new Subject<TreeData>();

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
                element.Hydrate(_treeConfigurationCollectionSource);
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
        ForestData IForestDataSource.ForestData => Data;
        IObservable<ForestData> IForestDataSource.ForestObservable => DataObservable;
    }

    public partial class ForestManager : IForestEventSource
    {
        void IForestEventSource.AddNewTree(TreeData treeData)
        {
            Data.AddForestElement(treeData);
            Save();
        }
    }
}
