using System;

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
        [Inject] private IForestElementConfigurationsSource _elementConfigurations = null;

        private DataSubject<ForestData> _forestDataSubject = new DataSubject<ForestData>();
        private Subject<Unit> _createForestSubject = new Subject<Unit>();

        #endregion

        #region METHODS

        private void Initialize()
        {
            Load();
        }

        private void Load()
        {
            var storedForestData = _saveSource.Load<ForestData>(FOREST_DATA_KEY);

            if (storedForestData == null)
            {
                storedForestData = _saveSource.LoadJSONFromResources<ForestData>(DEFAULT_FOREST_DATA_FILE);
            }

            foreach (var element in storedForestData.ForestElements)
            {
                element.Hydrate(_elementConfigurations);
            }

            _forestDataSubject.OnNext(storedForestData);
        }

        private void Save()
        {
            _saveSource.Save(FOREST_DATA_KEY, _forestDataSubject.Value);
        }

        private bool TryIncreaseGrowthLevel(ForestElementData elementData)
        {
            if (!_growthEventSource.TrySpendGrowth(elementData.Level)) return false;

            elementData.IncreaseLevel();
            Save();
            return true;
        }

        #endregion
    }

    public partial class ForestManager : IInitializable
    {
        void IInitializable.Initialize() => Initialize();
    }

    public partial class ForestManager : IForestDataSource
    {
        IObservable<ForestData> IForestDataSource.ForestDataObservable => _forestDataSubject.AsObservable(true);

        bool IForestDataSource.TryIncreaseGrowthLevel(ForestElementData elementData) => TryIncreaseGrowthLevel(elementData);
    }
}
