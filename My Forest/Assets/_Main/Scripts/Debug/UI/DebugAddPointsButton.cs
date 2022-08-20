using UnityEngine;

using Zenject;

namespace MyForest.Debug
{
    public class DebugAddPointsButton : DebugButton
    {
        #region FIELDS

        [Inject] private IScoreDebugSource _debugSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private uint _pointsToIncrement = 1;

        #endregion

        #region METHODS

        protected override void OnClickHandler()
        {
            _debugSource.IncreaseScore(_pointsToIncrement);
        }

        #endregion
    }
}
