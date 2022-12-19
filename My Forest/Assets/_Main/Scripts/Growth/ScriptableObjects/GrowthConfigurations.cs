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
        [SerializeField] private uint[] _groundCosts = null;
        [SerializeField] private uint _dailyGrowth = default;
        [SerializeField] private uint _extraDailyGrowthSecondsInterval = 10;

        #endregion
    }

    public partial class GrowthConfigurations : IGrowthConfigurationsSource
    {
        uint IGrowthConfigurationsSource.DailyGrowth => _dailyGrowth;
        uint IGrowthConfigurationsSource.ExtraDailyGrowthSecondsInterval => _extraDailyGrowthSecondsInterval;

        uint IGrowthConfigurationsSource.ElementMaxLevel => (uint)_levelCosts.Length;

        uint IGrowthConfigurationsSource.GroundMaxLevel => (uint)_groundCosts.Length;

        uint IGrowthConfigurationsSource.GetNextElementLevelCost(uint level)
        {
            try
            {
                return _levelCosts[level + 1];
            }
            catch (IndexOutOfRangeException e)
            {
                UnityEngine.Debug.LogError($"Growth Level {level + 1} does not exist.");
                return default;
            }
        }

        uint IGrowthConfigurationsSource.GetNextGroundLevelCost(uint level)
        {
            try
            {
                return _groundCosts[level + 1];
            }
            catch (IndexOutOfRangeException e)
            {
                UnityEngine.Debug.LogError($"Ground Level {level + 1} does not exist.");
                return default;
            }
        }
    }
}
