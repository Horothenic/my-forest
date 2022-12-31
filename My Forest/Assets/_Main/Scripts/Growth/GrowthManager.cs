using System;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class GrowthManager
    {
        #region FIELDS

        [Inject] private IGrowthConfigurationsSource _configurations = null;

        private DataSubject<bool> _growthDailyClaimAvailableSubject = new DataSubject<bool>();
        private DataSubject<bool> _growthDailyExtraClaimAvailableSubject = new DataSubject<bool>();

        #endregion

        #region METHODS

        private void IncreaseGrowth(uint increment)
        {
            Data.IncreaseGrowth(increment);
            EmitData();
            Save();
        }

        private bool DecreaseGrowth(uint decrement)
        {
            var decreased = Data.DecreaseGrowth(decrement);

            if (decreased)
            {
                EmitData();
                Save();
            }

            return decreased;
        }

        #endregion
    }

    public partial class GrowthManager : DataManager<GrowthData>
    {
        protected override string Key => Constants.Growth.GROWTH_DATA_KEY;

        protected override void OnLoadReady(ref GrowthData data)
        {
            var isDailyClaimAvailable = data.IsDailyClaimAvailable();
            var isDailyExtraClaimAvailable = data.IsDailyExtraClaimAvailable();

            _growthDailyClaimAvailableSubject.OnNext(isDailyClaimAvailable);
            _growthDailyExtraClaimAvailableSubject.OnNext(isDailyExtraClaimAvailable);
        }
    }

    public partial class GrowthManager : IInitializable
    {
        void IInitializable.Initialize()
        {
            Load();
        }
    }

    public partial class GrowthManager : IGrowthDataSource
    {
        GrowthData IGrowthDataSource.GrowthData => Data;
        IObservable<GrowthData> IGrowthDataSource.GrowthChangedObservable => DataObservable;
        IObservable<bool> IGrowthDataSource.ClaimDailyGrowthAvailable => _growthDailyClaimAvailableSubject.AsObservable(true);
        IObservable<bool> IGrowthDataSource.ClaimDailyExtraGrowthAvailable => _growthDailyExtraClaimAvailableSubject.AsObservable(true);
        double IGrowthDataSource.ExtraDailyGrowthSecondsLeft => Data.NextExtraDailyGrowthSecondsLeft;

        bool IGrowthDataSource.HaveEnoughGrowthForElementLevelUp(uint level)
        {
            return _configurations.GetNextForestElementLevelCost(level) <= Data.CurrentGrowth;
        }

        bool IGrowthDataSource.HaveEnoughGrowthForGroundLevelUp(uint level)
        {
            return _configurations.GetNextForestSizeLevelCost(level) <= Data.CurrentGrowth;
        }
    }

    public partial class GrowthManager : IGrowthEventSource
    {
        void IGrowthEventSource.ClaimDailyGrowth()
        {
            Data.SetLastClaimDateTime(DateTime.Now);
            _growthDailyClaimAvailableSubject.OnNext(false);

            IncreaseGrowth(_configurations.DailyGrowth);
        }

        void IGrowthEventSource.ClaimExtraDailyGrowth()
        {
            Data.SetNextExtraClaimDateTime(_configurations.ExtraDailyGrowthSecondsInterval);
            _growthDailyExtraClaimAvailableSubject.OnNext(false);

            IncreaseGrowth(_configurations.DailyGrowth);
        }

        bool IGrowthEventSource.TrySpendGrowthForForestElementLevel(uint level)
        {
            var amountForNextLevel = _configurations.GetNextForestElementLevelCost(level);
            return DecreaseGrowth(amountForNextLevel);
        }

        bool IGrowthEventSource.TrySpendGrowthForForestSizeLevel(uint level)
        {
            var amountForNextLevel = _configurations.GetNextForestSizeLevelCost(level);
            return DecreaseGrowth(amountForNextLevel);
        }
    }

    public partial class GrowthManager : Debug.IGrowthDebugSource
    {
        void Debug.IGrowthDebugSource.IncreaseGrowth(uint increment) => IncreaseGrowth(increment);
        void Debug.IGrowthDebugSource.DecreaseGrowth(uint decrement) => DecreaseGrowth(decrement);

        void Debug.IGrowthDebugSource.ResetGrowth()
        {
            EmitData(new GrowthData());
            _growthDailyClaimAvailableSubject.OnNext(true);
            Save();
        }

        void Debug.IGrowthDebugSource.ResetDailyClaim()
        {
            _growthDailyClaimAvailableSubject.OnNext(true);
        }
    }
}
