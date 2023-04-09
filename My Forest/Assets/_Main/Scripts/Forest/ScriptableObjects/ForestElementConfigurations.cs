using System.Collections.Generic;
using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(ForestElementConfigurations), menuName = MENU_NAME)]
    public partial class ForestElementConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Forest/" + nameof(ForestElementConfigurations);

        [SerializeField] private ForestElementConfiguration[] _elementConfigurations = null;

        #endregion

        #region METHODS

        private ForestElementConfiguration GetElementConfiguration(string elementName)
        {
            foreach (var elementConfiguration in _elementConfigurations)
            {
                if (elementConfiguration.ElementName == elementName)
                {
                    return elementConfiguration;
                }
            }

            return null;
        }

        #endregion
    }

    public partial class ForestElementConfigurations : IForestElementConfigurationsSource
    {
        ForestElementConfiguration IForestElementConfigurationsSource.GetElementConfiguration(string elementName) => GetElementConfiguration(elementName);
        IReadOnlyList<ForestElementConfiguration> IForestElementConfigurationsSource.GetAllElementConfigurations() => _elementConfigurations;
    }
}
