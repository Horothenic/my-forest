using UnityEngine;
using System;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(ForestSizeConfigurations), menuName = MENU_NAME)]
    public partial class ForestSizeConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Forest/" + nameof(ForestSizeConfigurations);

        [SerializeField] private float[] _diametersByLevel = null;

        #endregion
    }

    public partial class ForestSizeConfigurations : IForestSizeConfigurationsSource
    {
        float IForestSizeConfigurationsSource.GetDiameterByLevel(uint level)
        {
            try
            {
                return _diametersByLevel[level];
            }
            catch (IndexOutOfRangeException)
            {
                UnityEngine.Debug.LogError($"Size Level {level + 1} does not exist.");
                return default;
            }
        }
    }
}
