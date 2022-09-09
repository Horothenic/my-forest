using System;

namespace MyForest
{
    public interface IGrowthDataSource
    {
        IObservable<GrowthData> GrowthChangedObservable { get; }
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
