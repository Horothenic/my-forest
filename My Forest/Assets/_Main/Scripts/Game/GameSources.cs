using System;

using UniRx;

namespace MyForest.Debug
{
    public interface IGameDebugSource
    {
        IObservable<Unit> OnResetGameObservable { get; }
        void ResetGame();
    }
}
