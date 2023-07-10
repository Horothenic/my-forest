using UnityEngine;
using System;

using Lean.Localization;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(TreeConfiguration), menuName = MENU_NAME)]
    public class TreeConfiguration : ScriptableObject
    {
        [Serializable]
        public class TreeConfigurationLevel
        {
            [SerializeField] private string _name = default;
            [SerializeField] private int _growthNeeded = default;
            [SerializeField] private GameObject _prefab = null;
            [SerializeField] private float _extraSizeStep = .01f;
            [SerializeField] private int _maxSizeSteps = -1;

            public string ID => _name;
            public int GrowthNeeded => _growthNeeded;
            public GameObject Prefab => _prefab;
            public float ExtraSizeStep => _extraSizeStep;
            public int MaxSizeSteps => _maxSizeSteps;
            public bool HasMaxSteps => _maxSizeSteps > 0;
        }

        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Forest/" + nameof(TreeConfiguration);

        [SerializeField] private TreeRarity _rarity = default;
        [SerializeField] private BiomeType _biome = default;

        [Header("Display Name")]
        [LeanTranslationName]
        [SerializeField]
        private string _displayName = null;

        [Header("Description")]
        [LeanTranslationName]
        [SerializeField]
        private string _description = null;

        [SerializeField] private TreeConfigurationLevel[] _levels = null;

        public string ID => name;
        public string DisplayName => _displayName;
        public string Description => _description;
        public TreeRarity Rarity => _rarity;
        public BiomeType Biome => _biome;
        public int MaxLevel => _levels.Length - 1;

        #endregion

        #region METHODS

        public TreeConfigurationLevel GetConfigurationLevelByAge(int treeAge)
        {
            TreeConfigurationLevel bestLevel = null;

            foreach (var level in _levels)
            {
                if (level.GrowthNeeded > treeAge)
                {
                    return bestLevel;
                }

                bestLevel = level;
            }

            return bestLevel;
        }

        public TreeConfigurationLevel GetConfigurationLevel(int level)
        {
            return level > MaxLevel ? null : _levels[level];
        }

        #endregion
    }
}
