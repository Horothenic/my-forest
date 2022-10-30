using System;

namespace MyForest
{
    public interface IForestDataSource
    {
        IObservable<ForestData> ForestDataObservable { get; }
        IObservable<ForestElementData> GetForestElementDataObservable(ForestElementData elementData);
        bool TryIncreaseGrowthLevel(ForestElementData elementData);
    }

    public interface IForestElementConfigurationsSource
    {
        ForestElementConfiguration GetElementConfiguration(string elementName);
    }

    public interface IForestElementMenuSource
    {
        IObservable<ForestElementData> ForestElementMenuRequestedObservable { get; }
        void ResquestForestElementMenu(ForestElementData forestElementData);
    }
}
