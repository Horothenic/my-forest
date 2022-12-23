using System;

namespace MyForest
{
    public interface IForestDataSource
    {
        IObservable<ForestData> CreatedForestObservable { get; }
        IObservable<uint> IncreaseForestSizeLevelObservable { get; }
        IObservable<ForestElementData> GetForestElementDataObservable(ForestElementData elementData);
        bool TryIncreaseForestElementLevel(ForestElementData elementData);
        bool TryIncreaseForestSize();
        uint CurrentForestSize { get; }
        bool IsForestMaxSize { get; }
    }

    public interface IForestAddDataSource
    {
        void AddForestElement(ForestElementData newForestElement);
    }

    public interface IForestElementConfigurationsSource
    {
        ForestElementConfiguration GetElementConfiguration(string elementName);
    }

    public interface IForestSizeConfigurationsSource
    {
        float GetDiameterByLevel(uint level);
        float IncreaseSizeTransitionTime { get; }
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
        void IncreaseForestSize();
    }
}
