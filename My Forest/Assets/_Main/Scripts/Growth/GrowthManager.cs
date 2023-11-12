using System;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class GrowthManager
    {
        #region FIELDS
        
        private const string GROWTH_DAILY_TIMER_KEY = "GrowthDailyTimer";
        private const string GROWTH_DAILY_EXTRA_TIMER_KEY = "GrowthDailyExtraTimer";

        [Inject] private IGrowthConfigurationsSource _configurations = null;
        [Inject] private IGrowthTrackSource _growthTrackSource = null;
        [Inject] private ITimersSource _timersSource = null;

        private readonly DataSubject<IReadOnlyList<(IGrowthTrackEvent growthTackEvent, int growth)>> _growthEventsOccuredSubject = new DataSubject<IReadOnlyList<(IGrowthTrackEvent growthTackEvent, int growth)>>();
        private readonly DataSubject<bool> _growthDailyClaimAvailableSubject = new DataSubject<bool>();
        private readonly DataSubject<bool> _growthDailyExtraClaimAvailableSubject = new DataSubject<bool>();

        #endregion

        #region METHODS

        private void IncreaseGrowth(int increment)
        {
            var events = _growthTrackSource.GetEventsForGrowth(Data.CurrentGrowth, Data.CurrentGrowth + increment);
            if (events.Count > 0)
            {
                _growthEventsOccuredSubject.OnNext(events);
            }
            
            Data.IncreaseGrowth(increment);
            EmitData();
            Save();
        }

        #endregion
    }

    public partial class GrowthManager : DataManager<GrowthData>
    {
        protected override string Key => Constants.Growth.GROWTH_DATA_KEY;

        protected override void OnPreLoad(ref GrowthData data)
        {
            _timersSource.RemoveTimer(GROWTH_DAILY_TIMER_KEY);
            _timersSource.RemoveTimer(GROWTH_DAILY_EXTRA_TIMER_KEY);
            
            _timersSource.AddTimer(GROWTH_DAILY_TIMER_KEY, data.NextClaimDateTime, TimeSpan.FromDays(1));
            _timersSource.AddTimer(GROWTH_DAILY_EXTRA_TIMER_KEY, data.NextExtraClaimDateTime, TimeSpan.FromSeconds(1));

            DailyGrowthTimer.TimerCompletedObservable.Subscribe(OnDailyGrowthTimerCompleted).AddTo(_disposables);
            DailyExtraGrowthTimer.TimerCompletedObservable.Subscribe(OnDailyExtraGrowthTimerCompleted).AddTo(_disposables);
        }
        
        private void OnDailyGrowthTimerCompleted()
        {
            _growthDailyClaimAvailableSubject.OnNext(true);
        }
        
        private void OnDailyExtraGrowthTimerCompleted()
        {
            _growthDailyExtraClaimAvailableSubject.OnNext(true);
        }
    }

    public partial class GrowthManager : IGrowthDataSource
    {
        GrowthData IGrowthDataSource.GrowthData => Data;
        IObservable<GrowthData> IGrowthDataSource.GrowthChangedObservable => PreLoadObservable;
        IObservable<IReadOnlyList<(IGrowthTrackEvent growthTackEvent, int growth)>> IGrowthDataSource.GrowthEventsOccurredObservable => _growthEventsOccuredSubject.AsObservable();
        IObservable<bool> IGrowthDataSource.ClaimDailyGrowthAvailable => _growthDailyClaimAvailableSubject.AsObservable();
        IObservable<bool> IGrowthDataSource.ClaimDailyExtraGrowthAvailable => _growthDailyExtraClaimAvailableSubject.AsObservable();
        public ITimer DailyGrowthTimer => _timersSource.GetTimer(GROWTH_DAILY_TIMER_KEY);
        public ITimer DailyExtraGrowthTimer => _timersSource.GetTimer(GROWTH_DAILY_EXTRA_TIMER_KEY);
    }

    public partial class GrowthManager : IGrowthEventSource
    {
        void IGrowthEventSource.ClaimDailyGrowth()
        {
            if (!Data.IsDailyClaimAvailable()) return;

            Data.SetNextClaimDateTime(DateTime.UtcNow + TimeSpan.FromDays(1));
            DailyGrowthTimer.RestartWithNewTargetTime(Data.NextClaimDateTime);
            _growthDailyClaimAvailableSubject.OnNext(false);

            IncreaseGrowth(_configurations.DailyGrowth);
        }

        void IGrowthEventSource.ClaimExtraDailyGrowth()
        {
            if (!Data.IsDailyExtraClaimAvailable()) return;
            
            Data.SetNextExtraClaimDateTime(_configurations.ExtraDailyGrowthSecondsInterval);
            DailyExtraGrowthTimer.RestartWithNewTargetTime(Data.NextExtraClaimDateTime);
            _growthDailyExtraClaimAvailableSubject.OnNext(false);

            IncreaseGrowth(_configurations.DailyGrowth);
        }
    }

    public partial class GrowthManager : Debug.IGrowthDebugSource
    {
        void Debug.IGrowthDebugSource.IncreaseGrowth(int increment) => IncreaseGrowth(increment);

        void Debug.IGrowthDebugSource.ResetDailyClaim()
        {
            DailyGrowthTimer.RestartWithNewTargetTime(DateTime.UtcNow);
            Data.SetNextClaimDateTime(DateTime.UtcNow);
            
            Save();
        }
    }
}
