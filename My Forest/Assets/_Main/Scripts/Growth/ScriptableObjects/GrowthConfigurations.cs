using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(GrowthConfigurations), menuName = MENU_NAME)]
    public partial class GrowthConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Growth/" + nameof(GrowthConfigurations);

        [SerializeField] private uint[] _levelCosts = null;
        [SerializeField] private uint[] _groundCosts = null;
        [SerializeField] private uint _dailyGrowth = default;
        [SerializeField] private uint _extraDailyGrowthSecondsInterval = 10;

        #endregion
    }

    public partial class GrowthConfigurations : IGrowthConfigurationsSource
    {
        uint IGrowthConfigurationsSource.DailyGrowth => _dailyGrowth;
        uint IGrowthConfigurationsSource.ExtraDailyGrowthSecondsInterval => _extraDailyGrowthSecondsInterval;

        uint IGrowthConfigurationsSource.GetNextLevelCost(uint level)
        {
            return _levelCosts[level + 1];
        }

        uint IGrowthConfigurationsSource.GetNextGroundCost(uint width)
        {
            return _groundCosts[width + 1];
        }
    }
}
