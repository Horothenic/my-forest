using System;
using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class GrowthManager
    {
        #region FIELDS
        
        private const string GROWTH_DAILY_TIMER_KEY = "GrowthDailyTimer";

        [Inject] private IGrowthConfigurationsSource _configurations = null;
        [Inject] private ITimersSource _timersSource = null;
        
        private readonly DataSubject<bool> _growthDailyClaimAvailableSubject = new DataSubject<bool>();

        #endregion

        #region METHODS

        private void IncreaseGrowth(int increment)
        {
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
            _timersSource.AddTimer(GROWTH_DAILY_TIMER_KEY, data.NextClaimDateTime, TimeSpan.FromDays(1));
            DailyGrowthTimer.TimerCompletedObservable.Subscribe(OnDailyGrowthTimerCompleted).AddTo(_disposables);
        }
        
        private void OnDailyGrowthTimerCompleted()
        {
            _growthDailyClaimAvailableSubject.OnNext(true);
        }
    }

    public partial class GrowthManager : IGrowthDataSource
    {
        GrowthData IGrowthDataSource.GrowthData => Data;
        IObservable<GrowthData> IGrowthDataSource.GrowthChangedObservable => PreLoadObservable;
        IObservable<bool> IGrowthDataSource.ClaimDailyGrowthAvailable => _growthDailyClaimAvailableSubject.AsObservable();
        public ITimer DailyGrowthTimer => _timersSource.GetTimer(GROWTH_DAILY_TIMER_KEY);
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
