using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(GrowthConfigurations), menuName = MENU_NAME)]
    public partial class GrowthConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Growth/" + nameof(GrowthConfigurations);

        [SerializeField] private uint[] _levelCosts = null;

        #endregion

        #region METHODS

        private uint GetNextLevelCost(uint level)
        {
            return _levelCosts[level + 1];
        }

        #endregion
    }

    public partial class GrowthConfigurations : IGrowthConfigurationsSource
    {
        uint IGrowthConfigurationsSource.GetNextLevelCost(uint level) => GetNextLevelCost(level);
    }
}
