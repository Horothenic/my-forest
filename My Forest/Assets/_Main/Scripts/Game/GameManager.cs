using System;

using UniRx;

namespace MyForest
{
    public partial class GameManager
    {
        #region FIELDS

        private readonly Subject<Unit> _resetGameSubject = new Subject<Unit>();

        #endregion
    }

    public partial class GameManager : Debug.IGameDebugSource
    {
        IObservable<Unit> Debug.IGameDebugSource.OnResetGameObservable => _resetGameSubject.AsObservable();

        void Debug.IGameDebugSource.ResetGame()
        {
            _resetGameSubject.OnNext();
        }
    }
}
