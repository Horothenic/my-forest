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
        private DataSubject<bool> _growthDailyClaimAvailableSubject = new DataSubject<bool>();
        private DataSubject<bool> _growthDailyExtraClaimAvailableSubject = new DataSubject<bool>();

        #endregion

        #region METHODS

        private void Initialize()
        {
            Load();
        }

        private void Load()
        {
            _growthDataSubject.OnNext(_saveSource.Load<GrowthData>(GROWTH_DATA_KEY) ?? new GrowthData());

            var isDailyClaimAvailable = _growthDataSubject.Value.IsDailyClaimAvailable();
            var isDailyExtraClaimAvailable = _growthDataSubject.Value.IsDailyExtraClaimAvailable();

            _growthDailyClaimAvailableSubject.OnNext(isDailyClaimAvailable);
            _growthDailyExtraClaimAvailableSubject.OnNext(isDailyExtraClaimAvailable);
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

        private void ClaimDailyGrowth()
        {
            _growthDataSubject.Value.SetLastClaimDateTime(DateTime.Now);
            _growthDailyClaimAvailableSubject.OnNext(false);

            IncreaseGrowth(_configurations.DailyGrowth);
        }

        private void ClaimExtraDailyGrowth()
        {
            _growthDataSubject.Value.SetNextExtraClaimDateTime(_configurations.ExtraDailyGrowthSecondsInterval);
            _growthDailyExtraClaimAvailableSubject.OnNext(false);

            IncreaseGrowth(_configurations.DailyGrowth);
        }

        #endregion
    }

    public partial class GrowthManager : IInitializable
    {
        void IInitializable.Initialize() => Initialize();
    }

    public partial class GrowthManager : IGrowthDataSource
    {
        GrowthData IGrowthDataSource.GrowthData => _growthDataSubject.Value;
        IObservable<GrowthData> IGrowthDataSource.GrowthChangedObservable => _growthDataSubject.AsObservable();
        IObservable<bool> IGrowthDataSource.ClaimDailyGrowthAvailable => _growthDailyClaimAvailableSubject.AsObservable(true);
        IObservable<bool> IGrowthDataSource.ClaimDailyExtraGrowthAvailable => _growthDailyExtraClaimAvailableSubject.AsObservable(true);
        double IGrowthDataSource.ExtraDailyGrowthSecondsLeft => _growthDataSubject.Value.ExtraDailyGrowthSecondsLeft;

        bool IGrowthDataSource.HaveEnoughGrowthForLevelUp(uint level)
        {
            return _configurations.GetNextLevelCost(level) <= _growthDataSubject.Value.CurrentGrowth;
        }
    }

    public partial class GrowthManager : IGrowthEventSource
    {
        void IGrowthEventSource.ClaimDailyGrowth() => ClaimDailyGrowth();
        void IGrowthEventSource.ClaimExtraDailyGrowth() => ClaimExtraDailyGrowth();

        bool IGrowthEventSource.TrySpendGrowth(uint level)
        {
            var amountForNextLevel = _configurations.GetNextLevelCost(level);
            return DecreaseGrowth(amountForNextLevel);
        }
    }

    public partial class GrowthManager : Debug.IGrowthDebugSource
    {
        void Debug.IGrowthDebugSource.IncreaseGrowth(uint increment) => IncreaseGrowth(increment);
        void Debug.IGrowthDebugSource.DecreaseGrowth(uint decrement) => DecreaseGrowth(decrement);

        void Debug.IGrowthDebugSource.ResetGrowth()
        {
            _growthDataSubject.OnNext(new GrowthData());
            _growthDailyClaimAvailableSubject.OnNext(true);
            Save();
        }

        void Debug.IGrowthDebugSource.ResetDailyClaim()
        {
            _growthDailyClaimAvailableSubject.OnNext(true);
        }
    }
}
