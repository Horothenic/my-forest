using System;
using UnityEngine;

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
        IObservable<ForestElementMenuRequest> ForestElementMenuRequestedObservable { get; }
        void ResquestForestElementMenu(ForestElementMenuRequest request);
    }
}
