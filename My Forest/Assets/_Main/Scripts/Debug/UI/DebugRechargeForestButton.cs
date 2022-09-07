using Zenject;

namespace MyForest.Debug
{
    public class DebugRechargeForestButton : DebugButton
    {
        #region FIELDS

        [Inject] private IForestDebugSource _debugSource = null;

        #endregion

        #region METHODS

        protected override void OnClickHandler()
        {
            _debugSource.RechargeForest();
        }

        #endregion
    }
}
