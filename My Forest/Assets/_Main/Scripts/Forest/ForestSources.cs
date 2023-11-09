using System;

namespace MyForest
{
    public interface IForestDataSource
    {
        ForestData ForestData { get; }
        IObservable<ForestData> ForestPreLoadObservable { get; }
        IObservable<ForestData> ForestPostLoadObservable { get; }
        IObservable<ForestElementData> ForestElementChangedObservable { get; }
    }
}
