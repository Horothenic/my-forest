using System;

using UniRx;

namespace MyForest.Debug
{
    public interface IGameDebugSource
    {
        IObservable<Unit> OnResetControllersObservable { get; }
        IObservable<Unit> OnResetManagersObservable { get; }
        void ResetGame();
    }
}
