using System;

using UniRx;

namespace MyForest
{
    public interface IForestDataSource
    {
        IObservable<ForestData> ForestDataObservable { get; }
        void SetNewForest(ForestData newForest);
    }
}

namespace MyForest.Debug
{
    public interface IForestDebugSource
    {
        void RechargeForest();
    }
}
