using System;

namespace MyForest
{
    public interface IGrowthDataSource
    {
        GrowthData GrowthData { get; }
        IObservable<GrowthData> GrowthChangedObservable { get; }
        IObservable<bool> ClaimDailyGrowthAvailable { get; }
        IObservable<bool> ClaimDailyExtraGrowthAvailable { get; }
        double ExtraDailyGrowthSecondsLeft { get; }
        bool HaveEnoughGrowthForLevelUp(uint level);
    }

    public interface IGrowthEventSource
    {
        bool TrySpendGrowthForLevel(uint level);
        bool TrySpendGrowthForGround(uint width);
        void ClaimDailyGrowth();
        void ClaimExtraDailyGrowth();
    }

    public interface IGrowthConfigurationsSource
    {
        uint GetNextLevelCost(uint level);
        uint GetNextGroundCost(uint level);
        uint DailyGrowth { get; }
        uint ExtraDailyGrowthSecondsInterval { get; }
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
