using System;

namespace MyForest
{
    public interface IGrowthDataSource
    {
        IObservable<GrowthData> GrowthChangedObservable { get; }
        bool TrySpendGrowth(uint level);
    }

    public interface IGrowthConfigurationsSource
    {
        uint GetNextLevelCost(uint level);
    }
}

namespace MyForest.Debug
{
    public interface IGrowthDebugSource
    {
        void IncreaseGrowth(uint increment);
        void DecreaseGrowth(uint decrement);
        void ResetGrowth();
    }
}
