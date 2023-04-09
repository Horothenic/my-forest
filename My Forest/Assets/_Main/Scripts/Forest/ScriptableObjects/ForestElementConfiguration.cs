using UnityEngine;

using Lean.Localization;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(ForestElementConfiguration), menuName = MENU_NAME)]
    public class ForestElementConfiguration : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Forest/" + nameof(ForestElementConfiguration);

        [Header("Display Name")]
        [LeanTranslationName]
        [SerializeField]
        private string _displayName = null;
        
        [Header("Description")]
        [LeanTranslationName]
        [SerializeField]
        private string _description = null;
        
        [SerializeField] private GameObject[] _levels = null;

        public string ElementName => name;
        public string DisplayName => _displayName;
        public string Description => _description;
        public int MaxLevel => _levels.Length - 1;

        #endregion

        #region METHODS

        public GameObject GetLevelPrefab(int level)
        {
            return level > (_levels.Length - 1) ? null : _levels[level];
        }

        #endregion
    }
}
