using Zenject;

namespace MyForest.Debug
{
    public class DebugResetGrowthPointsButton : DebugButton
    {
        #region FIELDS

        [Inject] private IGrowthDebugSource _debugSource = null;

        #endregion

        #region METHODS

        protected override void OnClickHandler()
        {
            _debugSource.ResetGrowth();
        }

        #endregion
    }
}
