using System;

using UniRx;

namespace MyForest
{
    public partial class GameManager
    {
        #region FIELDS

        private Subject<Unit> _resetManagersSubject = new Subject<Unit>();
        private Subject<Unit> _resetControllersSubject = new Subject<Unit>();

        #endregion
    }

    public partial class GameManager : Debug.IGameDebugSource
    {
        IObservable<Unit> Debug.IGameDebugSource.OnResetControllersObservable => _resetControllersSubject.AsObservable();
        IObservable<Unit> Debug.IGameDebugSource.OnResetManagersObservable => _resetManagersSubject.AsObservable();

        void Debug.IGameDebugSource.ResetGame()
        {
            _resetControllersSubject.OnNext();
            _resetManagersSubject.OnNext();
        }
    }
}
