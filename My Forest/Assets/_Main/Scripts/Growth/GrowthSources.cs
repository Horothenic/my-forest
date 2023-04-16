using System;
using System.Collections.Generic;

namespace MyForest
{
    public interface IGrowthDataSource
    {
        GrowthData GrowthData { get; }
        IObservable<GrowthData> GrowthChangedObservable { get; }
        IObservable<bool> ClaimDailyGrowthAvailable { get; }
        IObservable<bool> ClaimDailyExtraGrowthAvailable { get; }
        double ExtraDailyGrowthSecondsLeft { get; }
    }

    public interface IGrowthEventSource
    {
        void ClaimDailyGrowth();
        void ClaimExtraDailyGrowth();
    }

    public interface IGrowthConfigurationsSource
    {
        int DailyGrowth { get; }
        int ExtraDailyGrowthSecondsInterval { get; }
    }

    public interface IGrowthTrackSource
    {
        IReadOnlyList<GrowthTrackRecurringEvent> AllRecurrentEvents { get; }
        IReadOnlyList<GrowthTrackEvent> AllPinPointEvents { get; }
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
