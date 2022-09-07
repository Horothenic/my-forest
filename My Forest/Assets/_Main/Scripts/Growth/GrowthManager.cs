using System;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class GrowthManager
    {
        #region FIELDS

        private const string GROWTH_DATA_KEY = "growth_data";

        [Inject] private ISaveSource _saveSource = null;

        private DataSubject<GrowthData> _growthDataSubject = new DataSubject<GrowthData>(new GrowthData());

        #endregion

        #region METHODS

        private void Initialize()
        {
            Load();
        }

        private void Load()
        {
            _growthDataSubject.OnNext(_saveSource.Load<GrowthData>(GROWTH_DATA_KEY));
        }

        private void Save()
        {
            _saveSource.Save(GROWTH_DATA_KEY, _growthDataSubject.Value);
        }

        private void IncreaseGrowth(uint increment)
        {
            _growthDataSubject.Value.IncreaseGrowth(increment);
            _growthDataSubject.OnNext();
            Save();
        }

        private void ResetGrowth()
        {
            _growthDataSubject.OnNext(new GrowthData());
            Save();
        }

        #endregion
    }

    public partial class GrowthManager : IInitializable
    {
        void IInitializable.Initialize() => Initialize();
    }

    public partial class GrowthManager : IGrowthDataSource
    {
        IObservable<GrowthData> IGrowthDataSource.GrowthChangedObservable => _growthDataSubject.AsObservable(true);
    }

    public partial class GrowthManager : Debug.IGrowthDebugSource
    {
        void Debug.IGrowthDebugSource.IncreaseGrowth(uint increment) => IncreaseGrowth(increment);
        void Debug.IGrowthDebugSource.ResetGrowth() => ResetGrowth();
    }
}
