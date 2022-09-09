using UnityEngine;

using Zenject;

namespace MyForest.Debug
{
    public class DebugDecreaseGrowthPointsButton : DebugButton
    {
        #region FIELDS

        [Inject] private IGrowthDebugSource _debugSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private uint _pointsToDecrement = 1;

        #endregion

        #region METHODS

        protected override void OnClickHandler()
        {
            _debugSource.DecreaseGrowth(_pointsToDecrement);
        }

        #endregion
    }
}
