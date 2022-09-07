using UnityEngine;

using Zenject;

namespace MyForest.Debug
{
    public class DebugAddGrowthPointsButton : DebugButton
    {
        #region FIELDS

        [Inject] private IGrowthDebugSource _debugSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private uint _pointsToIncrement = 1;

        #endregion

        #region METHODS

        protected override void OnClickHandler()
        {
            _debugSource.IncreaseGrowth(_pointsToIncrement);
        }

        #endregion
    }
}
