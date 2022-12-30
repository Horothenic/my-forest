using Zenject;

namespace MyForest.Debug
{
    public class DebugIncreaseForestSizeButton : DebugButton
    {
        #region FIELDS

        [Inject] private IForestDebugSource _debugSource = null;

        #endregion

        #region METHODS

        protected override void OnClickHandler()
        {
            _debugSource.IncreaseForestSize();
        }

        #endregion
    }
}
