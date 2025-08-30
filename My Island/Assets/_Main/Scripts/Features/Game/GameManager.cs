using System;
using UniRx;

namespace MyIsland
{
    public partial class GameManager
    {
        
    }
    
    public partial class GameManager : IGameSource
    {
        private readonly DataSubject<GameMode> _onGameModeSubject = new DataSubject<GameMode>(GameMode.Island);

        public IObservable<GameMode> OnGameMode => _onGameModeSubject.AsObservable();
        
        public void SetGameMode(GameMode gameMode)
        {
            _onGameModeSubject.OnNext(gameMode);
        }
    }
}
