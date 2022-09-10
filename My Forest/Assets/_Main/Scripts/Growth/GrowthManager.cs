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
        [Inject] private IGrowthConfigurationsSource _configurations = null;

        private DataSubject<GrowthData> _growthDataSubject = new DataSubject<GrowthData>(new GrowthData());

        #endregion

        #region METHODS

        private void Initialize()
        {
            Load();
        }

        private void Load()
        {
            _growthDataSubject.OnNext(_saveSource.Load<GrowthData>(GROWTH_DATA_KEY) ?? new GrowthData());
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

        private bool DecreaseGrowth(uint decrement)
        {
            var decreased = _growthDataSubject.Value.DecreaseGrowth(decrement);

            if (decreased)
            {
                _growthDataSubject.OnNext();
                Save();
            }

            return decreased;
        }

        private void ResetGrowth()
        {
            _growthDataSubject.OnNext(new GrowthData());
            Save();
        }

        private bool TrySpendGrowth(uint level)
        {
            var amountForNextLevel = _configurations.GetNextLevelCost(level);
            return DecreaseGrowth(amountForNextLevel);
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

        bool IGrowthDataSource.TrySpendGrowth(uint level) => TrySpendGrowth(level);
    }

    public partial class GrowthManager : Debug.IGrowthDebugSource
    {
        void Debug.IGrowthDebugSource.IncreaseGrowth(uint increment) => IncreaseGrowth(increment);
        void Debug.IGrowthDebugSource.DecreaseGrowth(uint decrement) => DecreaseGrowth(decrement);
        void Debug.IGrowthDebugSource.ResetGrowth() => ResetGrowth();
    }
}
