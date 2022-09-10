using System;

namespace MyForest
{
    public interface IForestDataSource
    {
        IObservable<ForestData> ForestDataObservable { get; }
        bool TryIncreaseGrowthLevel(ForestElementData elementData);
    }

    public interface IForestElementConfigurationsSource
    {
        ForestElementConfiguration GetElementConfiguration(string elementName);
    }
}
