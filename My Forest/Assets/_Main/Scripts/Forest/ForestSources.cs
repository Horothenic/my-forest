using System;
using System.Collections.Generic;

using UniRx;

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
        IReadOnlyList<ForestElementConfiguration> GetAllElementConfigurations();
    }

    public interface IForestSizeConfigurationsSource
    {
        float GetDiameterByLevel(uint level);
        float IncreaseSizeTransitionTime { get; }
    }

    public interface IForestElementMenuSource
    {
        IObservable<ForestElementMenuRequest> ForestElementMenuRequestedObservable { get; }
        IObservable<Unit> ForestElementMenuClosedObservable { get; }
        void RequestForestElementMenu(ForestElementMenuRequest request);
        void RaiseCloseForestElementMenu();
    }
}


namespace MyForest.Debug
{
    public interface IForestDebugSource
    {
        void IncreaseForestSize();
    }
}
