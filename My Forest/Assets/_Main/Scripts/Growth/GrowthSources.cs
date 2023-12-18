using System;
using UnityEngine;

namespace MyForest
{
    public interface IGrowthDataSource
    {
        GrowthData GrowthData { get; }
        IObservable<GrowthData> GrowthChangedObservable { get; }
        IObservable<bool> ClaimDailyGrowthAvailable { get; }
        ITimer DailyGrowthTimer { get; }
    }

    public interface IGrowthEventSource
    {
        void ClaimDailyGrowth();
    }

    public interface IGrowthConfigurationsSource
    {
        int DailyGrowth { get; }
        int ExtraDailyGrowthSecondsInterval { get; }
    }
}

namespace MyForest.Debug
{
    public interface IGrowthDebugSource
    {
        void IncreaseGrowth(int increment);
        void ResetDailyClaim();
    }
}
