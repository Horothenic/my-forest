using System;
using System.Collections.Generic;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class ForestManager
    {
        #region FIELDS

        [Inject] private IGrowthEventSource _growthEventSource = null;
        [Inject] private IGrowthConfigurationsSource _growthConfigurationsSource = null;
        [Inject] private IForestElementConfigurationsSource _elementConfigurations = null;

        private Subject<uint> _increaseForestSizeLevelSubject = new Subject<uint>();
        private Subject<ForestElementMenuRequest> _forestElementMenuRequestedSubject = new Subject<ForestElementMenuRequest>();
        private Dictionary<int, Subject<ForestElementData>> _forestElementDataSubjectMap = new Dictionary<int, Subject<ForestElementData>>();

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

            foreach (var element in data.ForestElements)
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
        IObservable<ForestData> IForestDataSource.CreatedForestObservable => DataObservable;

        IObservable<uint> IForestDataSource.IncreaseForestSizeLevelObservable => _increaseForestSizeLevelSubject.AsObservable();

        uint IForestDataSource.CurrentForestSize => Data.SizeLevel;

        bool IForestDataSource.IsForestMaxSize => Data.SizeLevel == _growthConfigurationsSource.ForestSizeMaxLevel;

        IObservable<ForestElementData> IForestDataSource.GetForestElementDataObservable(ForestElementData elementData)
        {
            return GetForestElementDataSubject(elementData.Id).AsObservable();
        }

        private Subject<ForestElementData> GetForestElementDataSubject(int id)
        {
            if (!_forestElementDataSubjectMap.ContainsKey(id))
            {
                _forestElementDataSubjectMap.Add(id, new Subject<ForestElementData>());
            }

            return _forestElementDataSubjectMap[id];
        }

        bool IForestDataSource.TryIncreaseForestElementLevel(ForestElementData elementData)
        {
            if (!_growthEventSource.TrySpendGrowthForForestElementLevel(elementData.Level)) return false;

            elementData.IncreaseLevel();
            GetForestElementDataSubject(elementData.Id).OnNext(elementData);
            Save();
            return true;
        }

        bool IForestDataSource.TryIncreaseForestSize()
        {
            if (!_growthEventSource.TrySpendGrowthForForestSizeLevel(Data.SizeLevel)) return false;

            Data.IncreaseSizeLevel();
            _increaseForestSizeLevelSubject.OnNext(Data.SizeLevel);
            Save();
            return true;
        }
    }

    public partial class ForestManager : IForestAddDataSource
    {
        void IForestAddDataSource.AddForestElement(ForestElementData newForestElement)
        {
            Data.AddForestElement(newForestElement);
        }
    }

    public partial class ForestManager : IForestElementMenuSource
    {
        IObservable<ForestElementMenuRequest> IForestElementMenuSource.ForestElementMenuRequestedObservable => _forestElementMenuRequestedSubject.AsObservable();

        void IForestElementMenuSource.RequestForestElementMenu(ForestElementMenuRequest request)
        {
            _forestElementMenuRequestedSubject.OnNext(request);
        }
    }

    public partial class ForestManager : Debug.IForestDebugSource
    {
        void Debug.IForestDebugSource.IncreaseForestSize()
        {
            Data.IncreaseSizeLevel();
            _increaseForestSizeLevelSubject.OnNext(Data.SizeLevel);
            Save();
        }
    }
}
