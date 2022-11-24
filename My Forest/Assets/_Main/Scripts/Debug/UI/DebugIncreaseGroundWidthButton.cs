using Zenject;

namespace MyForest.Debug
{
    public class DebugIncreaseGroundWidthButton : DebugButton
    {
        #region FIELDS

        [Inject] private IForestDebugSource _debugSource = null;

        #endregion

        #region METHODS

        protected override void OnClickHandler()
        {
            _debugSource.IncreaseGroundWidth();
        }

        #endregion
    }
}
