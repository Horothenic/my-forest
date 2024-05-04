using System;

namespace MyForest
{
    public interface IGrowthDataSource
    {
        GrowthData GrowthData { get; }
        IObservable<GrowthData> GrowthChangedObservable { get; }
    }

    public interface IGrowthConfigurationsSource
    {
        int DailyGrowth { get; }
    }
}

namespace MyForest.Debug
{
    public interface IGrowthDebugSource
    {
        void IncreaseGrowth(int increment);
    }
}
