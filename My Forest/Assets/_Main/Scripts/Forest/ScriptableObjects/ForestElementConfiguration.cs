using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(ForestElementConfiguration), menuName = MENU_NAME)]
    public class ForestElementConfiguration : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Forest/" + nameof(ForestElementConfiguration);

        [SerializeField] private GameObject[] _levels = null;

        public string ElementName => name;
        public int MaxLevel => _levels.Length - 1;

        #endregion

        #region METHODS

        public GameObject GetLevelPrefab(uint level)
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
