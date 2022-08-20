using System;

using UniRx;

namespace MyForest
{
    public interface IForestDataSource
    {
        IObservable<ForestData> ForestDataObservable { get; }
    }

    public interface IForestEventSource
    {
        IObservable<Unit> CreateNewForestObservable { get; }
    }

    public interface IForestDebugSource
    {
        void RechargeForest();
    }
}
