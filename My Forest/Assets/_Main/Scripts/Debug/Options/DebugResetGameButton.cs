using Zenject;

namespace MyForest.Debug
{
    public class DebugResetGameButton : DebugButton
    {
        #region FIELDS

        [Inject] private IGameDebugSource _debugSource = null;

        #endregion

        #region METHODS

        protected override void OnClickHandler()
        {
            _debugSource.ResetGame();
        }

        #endregion
    }
}
