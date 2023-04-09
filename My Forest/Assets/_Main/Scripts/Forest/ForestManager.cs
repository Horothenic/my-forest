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

        private Dictionary<int, Subject<TreeData>> _treeDataSubjectMap = new Dictionary<int, Subject<TreeData>>();

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

        IObservable<TreeData> IForestDataSource.GetTreeDataObservable(TreeData elementData)
        {
            return GetForestElementDataSubject(elementData.Id).AsObservable();
        }

        private Subject<TreeData> GetForestElementDataSubject(int id)
        {
            if (!_treeDataSubjectMap.ContainsKey(id))
            {
                _treeDataSubjectMap.Add(id, new Subject<TreeData>());
            }

            return _treeDataSubjectMap[id];
        }
    }
}
