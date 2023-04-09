using System;
using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(GrowthConfigurations), menuName = MENU_NAME)]
    public partial class GrowthConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Growth/" + nameof(GrowthConfigurations);

        [SerializeField] private int _dailyGrowth = default;
        [SerializeField] private int _extraDailyGrowthSecondsInterval = 10;

        #endregion
    }

    public partial class GrowthConfigurations : IGrowthConfigurationsSource
    {
        int IGrowthConfigurationsSource.DailyGrowth => _dailyGrowth;
        int IGrowthConfigurationsSource.ExtraDailyGrowthSecondsInterval => _extraDailyGrowthSecondsInterval;
    }
}
