using System;

namespace MyForest
{
    public interface IForestDataSource
    {
        ForestData ForestData { get; }
        IObservable<ForestData> ForestLoadObservable { get; }
        IObservable<ForestElementData> ForestElementChangedObservable { get; }
    }
    
    public interface IForestEventsSource
    {
        void DiscoverTile(Coordinates coordinates);
    }
}
