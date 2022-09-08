using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(ForestElementConfiguration), menuName = MENU_NAME)]
    public class ForestElementConfiguration : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Configurations/Element";

        [SerializeField] private GameObject[] _levels = null;

        public string ElementName => name;

        #endregion

        #region METHODS

        public GameObject GetLevelPrefab(int level)
        {
            if (_levels.Length - 1 < level)
            {
                return null;
            }

            return _levels[level];
        }

        #endregion
    }
}
