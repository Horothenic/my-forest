using System;

namespace MyForest
{
    public interface IForestDataSource
    {
        IObservable<ForestData> CreatedForestObservable { get; }
        IObservable<uint> IncreaseGroundObservable { get; }
        IObservable<ForestElementData> GetForestElementDataObservable(ForestElementData elementData);
        bool TryIncreaseGrowthLevel(ForestElementData elementData);
        bool TryIncreaseGroundSize();
    }

    public interface IForestAddDataSource
    {
        void AddGroundElement(GroundElementData newGroundElement);
        void AddForestElement(ForestElementData newForestElement);
    }

    public interface IForestElementConfigurationsSource
    {
        ForestElementConfiguration GetElementConfiguration(string elementName);
    }

    public interface IForestElementMenuSource
    {
        IObservable<ForestElementMenuRequest> ForestElementMenuRequestedObservable { get; }
        void RequestForestElementMenu(ForestElementMenuRequest request);
    }
}


namespace MyForest.Debug
{
    public interface IForestDebugSource
    {
        void IncreaseGroundWidth();
    }
}
