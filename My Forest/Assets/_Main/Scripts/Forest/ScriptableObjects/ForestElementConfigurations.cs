using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(ForestElementConfigurations), menuName = MENU_NAME)]
    public class ForestElementConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Configurations/Elements Container";

        [SerializeField] private ForestElementConfiguration[] _elementConfigurations = null;

        #endregion

        #region METHODS

        public ForestElementConfiguration GetElementConfiguration(string elementName)
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
}
