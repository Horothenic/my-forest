using System;
using System.Collections.Generic;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class ForestManager
    {
        #region FIELDS

        private const string FOREST_DATA_KEY = "forest_data";
        private const string DEFAULT_FOREST_DATA_FILE = "DefaultForest";

        [Inject] private ISaveSource _saveSource = null;
        [Inject] private IGrowthEventSource _growthEventSource = null;
        [Inject] private IGrowthDataSource _growthDataSource = null;
        [Inject] private IGrowthConfigurationsSource _growthConfigurationsSource = null;
        [Inject] private IForestElementConfigurationsSource _elementConfigurations = null;
        
        private DataSubject<ForestData> _loadedForestSubject = new DataSubject<ForestData>(new ForestData());
        private Subject<uint> _increaseGroundSubject = new Subject<uint>();
        private Subject<ForestElementMenuRequest> _forestElementMenuRequestedSubject = new Subject<ForestElementMenuRequest>();
        private Dictionary<int, Subject<ForestElementData>> _forestElementDataSubjectMap = new Dictionary<int, Subject<ForestElementData>>();

        private ForestData CurrentForestData => _loadedForestSubject.Value;

        #endregion

        #region METHODS

        private void Initialize()
        {
            Load();
        }

        private void Load()
        {
            var _forestData = _saveSource.Load<ForestData>(FOREST_DATA_KEY);

            if (_forestData == null)
            {
                _forestData = _saveSource.LoadJSONFromResources<ForestData>(DEFAULT_FOREST_DATA_FILE);
            }

            foreach (var element in _forestData.ForestElements)
            {
                element.Hydrate(_elementConfigurations);
            }

            _loadedForestSubject.OnNext(_forestData);
        }

        private void Save()
        {
            _saveSource.Save(FOREST_DATA_KEY, CurrentForestData);
        }

        #endregion
    }

    public partial class ForestManager : IInitializable
    {
        void IInitializable.Initialize() => Initialize();
    }

    public partial class ForestManager : IForestDataSource
    {
        IObservable<ForestData> IForestDataSource.CreatedForestObservable => _loadedForestSubject.AsObservable(true);

        IObservable<uint> IForestDataSource.IncreaseGroundObservable => _increaseGroundSubject.AsObservable();

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

        bool IForestDataSource.TryIncreaseGrowthLevel(ForestElementData elementData)
        {
            if (!_growthEventSource.TrySpendGrowthForLevel(elementData.Level)) return false;

            elementData.IncreaseLevel();
            GetForestElementDataSubject(elementData.Id).OnNext(elementData);
            Save();
            return true;
        }

        bool IForestDataSource.TryIncreaseGroundSize()
        {
            if (!_growthEventSource.TrySpendGrowthForGround(CurrentForestData.GroundLevel)) return false;

            _increaseGroundSubject.OnNext(CurrentForestData.GroundWidth + 1);
            Save();
            return true;
        }
        
        uint IForestDataSource.CurrentGroundLevel => CurrentForestData.GroundLevel;

        bool IForestDataSource.IsGroundMaxLevel => CurrentForestData.GroundLevel == _growthConfigurationsSource.GroundMaxLevel;
    }

    public partial class ForestManager : IForestAddDataSource
    {
        void IForestAddDataSource.AddForestElement(ForestElementData newForestElement)
        {
            CurrentForestData.AddForestElement(newForestElement);
        }

        void IForestAddDataSource.AddGroundElement(GroundElementData newGroundElement)
        {
            CurrentForestData.AddGroundElement(newGroundElement);
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
        void Debug.IForestDebugSource.IncreaseGroundWidth()
        {
            _increaseGroundSubject.OnNext(CurrentForestData.GroundWidth + 1);
            Save();
        }
    }
}
