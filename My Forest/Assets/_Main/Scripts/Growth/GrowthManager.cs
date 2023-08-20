using System;
using System.Collections.Generic;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class GrowthManager
    {
        #region FIELDS

        [Inject] private IGrowthConfigurationsSource _configurations = null;
        [Inject] private IGrowthTrackSource _growthTrackSource = null;

        private DataSubject<IReadOnlyList<IGrowthTrackEvent>> _growthEventsOcurredSubject = new DataSubject<IReadOnlyList<IGrowthTrackEvent>>();
        private DataSubject<bool> _growthDailyClaimAvailableSubject = new DataSubject<bool>();
        private DataSubject<bool> _growthDailyExtraClaimAvailableSubject = new DataSubject<bool>();

        #endregion

        #region METHODS

        private void IncreaseGrowth(int increment)
        {
            Data.IncreaseGrowth(increment);
            EmitData();
            Save();

            var events = _growthTrackSource.GetEventsForGrowth(Data.CurrentGrowth);
            if (events.Count > 0)
            {
                _growthEventsOcurredSubject.OnNext(events);
            }
        }

        #endregion
    }

    public partial class GrowthManager : DataManager<GrowthData>
    {
        protected override string Key => Constants.Growth.GROWTH_DATA_KEY;

        protected override void OnPreLoad(ref GrowthData data)
        {
            var isDailyClaimAvailable = data.IsDailyClaimAvailable();
            var isDailyExtraClaimAvailable = data.IsDailyExtraClaimAvailable();

            _growthDailyClaimAvailableSubject.OnNext(isDailyClaimAvailable);
            _growthDailyExtraClaimAvailableSubject.OnNext(isDailyExtraClaimAvailable);
        }
    }

    public partial class GrowthManager : IGrowthDataSource
    {
        GrowthData IGrowthDataSource.GrowthData => Data;
        IObservable<GrowthData> IGrowthDataSource.GrowthChangedObservable => DataObservable;
        IObservable<IReadOnlyList<IGrowthTrackEvent>> IGrowthDataSource.GrowthEventsOccurredObservable => _growthEventsOcurredSubject.AsObservable();
        IObservable<bool> IGrowthDataSource.ClaimDailyGrowthAvailable => _growthDailyClaimAvailableSubject.AsObservable();
        IObservable<bool> IGrowthDataSource.ClaimDailyExtraGrowthAvailable => _growthDailyExtraClaimAvailableSubject.AsObservable();
        double IGrowthDataSource.ExtraDailyGrowthSecondsLeft => Data.NextExtraDailyGrowthSecondsLeft;
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
    }

    public partial class GrowthManager : Debug.IGrowthDebugSource
    {
        void Debug.IGrowthDebugSource.IncreaseGrowth(int increment) => IncreaseGrowth(increment);

        void Debug.IGrowthDebugSource.ResetDailyClaim()
        {
            _growthDailyClaimAvailableSubject.OnNext(true);
        }
    }
}
