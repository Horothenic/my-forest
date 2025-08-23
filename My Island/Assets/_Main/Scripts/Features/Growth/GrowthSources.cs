using System;

namespace MyIsland
{
    public interface IGrowthSource
    {
        IObservable<GrowthData> DataObservables { get; }
        GrowthData Data { get; }

        bool SpendGrowth(int amount);
        void ReclaimGrowth();
    }
    
    public interface IGrowthDebugSource
    {
        void IncreaseGrowth(int amount);
        void ResetGrowth();
    }
}
