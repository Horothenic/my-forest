using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(GrowthConfigurations), menuName = MENU_NAME)]
    public partial class GrowthConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Growth/" + nameof(GrowthConfigurations);

        [Header("INCREASE")]
        [SerializeField] private int _dailyGrowth = default;

        #endregion
    }

    public partial class GrowthConfigurations : IGrowthConfigurationsSource
    {
        int IGrowthConfigurationsSource.DailyGrowth => _dailyGrowth;
    }
}
