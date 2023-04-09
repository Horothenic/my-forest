using Zenject;

namespace MyForest.Debug
{
    public class DebugResetDailyClaimButton : DebugButton
    {
        #region FIELDS

        [Inject] private IGrowthDebugSource _debugSource = null;

        #endregion

        #region METHODS

        protected override void OnClickHandler()
        {
            _debugSource.ResetDailyClaim();
        }

        #endregion
    }
}
