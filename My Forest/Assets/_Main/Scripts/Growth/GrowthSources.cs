using System;

namespace MyForest
{
    public interface IGrowthDataSource
    {
        GrowthData GrowthData { get; }
        IObservable<GrowthData> GrowthChangedObservable { get; }
        IObservable<bool> ClaimDailyGrowthAvailable { get; }
    }

    public interface IGrowthEventSource
    {
        bool TrySpendGrowth(uint level);
        void ClaimDailyGrowth();
    }

    public interface IGrowthConfigurationsSource
    {
        uint GetNextLevelCost(uint level);
        uint DailyGrowth { get; }
    }
}

namespace MyForest.Debug
{
    public interface IGrowthDebugSource
    {
        void IncreaseGrowth(uint increment);
        void DecreaseGrowth(uint decrement);
        void ResetGrowth();
        void ResetDailyClaim();
    }
}
