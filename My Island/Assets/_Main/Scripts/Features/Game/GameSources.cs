using System;

namespace MyIsland
{
    public interface IGameSource
    {
        IObservable<GameMode> OnGameMode { get; }
        void SetGameMode(GameMode gameMode);
    }
}
