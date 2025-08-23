using System;
using UniRx;

namespace MyIsland
{
    public interface IForestSource
    {
        IObservable<bool> IsPlantModeOpen { get; }
        ForestData Data { get; }

        void EnterPlantMode();
        void ExitPlantMode();
    }
}
