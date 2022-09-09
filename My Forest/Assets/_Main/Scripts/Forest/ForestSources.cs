using System;

namespace MyForest
{
    public interface IForestDataSource
    {
        IObservable<ForestData> ForestDataObservable { get; }
        void SetNewForest(ForestData newForest);
    }
}
