using UnityEngine;

using Zenject;

namespace MyForest.Debug
{
    public class DebugIncreaseGrowthPointsButton : DebugButton
    {
        #region FIELDS

        [Inject] private IGrowthDebugSource _debugSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private int _pointsToIncrement = 1;

        #endregion

        #region METHODS

        protected override void OnClickHandler()
        {
            _debugSource.IncreaseGrowth(_pointsToIncrement);
        }

        #endregion
    }
}
