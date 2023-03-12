using System;
using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(GrowthConfigurations), menuName = MENU_NAME)]
    public partial class GrowthConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Growth/" + nameof(GrowthConfigurations);

        [SerializeField] private uint[] _levelCosts = null;
        [SerializeField] private uint[] _forestSizeCosts = null;
        [SerializeField] private uint _dailyGrowth = default;
        [SerializeField] private uint _extraDailyGrowthSecondsInterval = 10;

        #endregion
    }

    public partial class GrowthConfigurations : IGrowthConfigurationsSource
    {
        uint IGrowthConfigurationsSource.DailyGrowth => _dailyGrowth;
        uint IGrowthConfigurationsSource.ExtraDailyGrowthSecondsInterval => _extraDailyGrowthSecondsInterval;
        uint IGrowthConfigurationsSource.ForestElementMaxLevel => (uint)_levelCosts.Length - 1;
        uint IGrowthConfigurationsSource.ForestSizeMaxLevel => (uint)_forestSizeCosts.Length - 1;

        uint IGrowthConfigurationsSource.GetNextForestElementLevelCost(int level)
        {
            try
            {
                return _levelCosts[level + 1];
            }
            catch (IndexOutOfRangeException)
            {
                UnityEngine.Debug.LogError($"Forest Element Level {level + 1} does not exist.");
                return default;
            }
        }

        uint IGrowthConfigurationsSource.GetNextForestSizeLevelCost(uint level)
        {
            try
            {
                return _forestSizeCosts[level + 1];
            }
            catch (IndexOutOfRangeException)
            {
                UnityEngine.Debug.LogError($"Forest Size Level {level + 1} does not exist.");
                return default;
            }
        }
    }
}
