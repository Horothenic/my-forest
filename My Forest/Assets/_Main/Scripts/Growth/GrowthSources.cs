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
        bool HaveEnoughGrowthForElementLevelUp(int level);
        bool HaveEnoughGrowthForGroundLevelUp(uint level);
    }

    public interface IGrowthEventSource
    {
        bool TrySpendGrowthForForestElementLevel(int level);
        bool TrySpendGrowthForForestSizeLevel(uint level);
        void ClaimDailyGrowth();
        void ClaimExtraDailyGrowth();
    }

    public interface IGrowthConfigurationsSource
    {
        uint GetNextForestElementLevelCost(int level);
        uint GetNextForestSizeLevelCost(uint level);
        uint DailyGrowth { get; }
        uint ExtraDailyGrowthSecondsInterval { get; }
        uint ForestElementMaxLevel { get; }
        uint ForestSizeMaxLevel { get; }
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
