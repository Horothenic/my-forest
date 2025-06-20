using UnityEngine;
using System;

namespace MyIsland
{
    [CreateAssetMenu(fileName = nameof(TreeConfiguration), menuName = MENU_NAME)]
    public class TreeConfiguration : ScriptableObject
    {
        [Serializable]
        public class Level
        {
            [SerializeField] private int _growthNeeded = default;
            [SerializeField] private GameObject _prefab = null;
            [SerializeField] private float _sizeStep = .01f;
            [SerializeField] private int _maxSizeSteps = -1;

            public int GrowthNeeded => _growthNeeded;
            public GameObject Prefab => _prefab;
            public float SizeStep => _sizeStep;
            public int MaxSizeSteps => _maxSizeSteps;
            public bool HasMaxSteps => _maxSizeSteps > 0;
        }

        #region FIELDS

        private const string MENU_NAME = nameof(MyIsland) + "/Trees/" + nameof(TreeConfiguration);

        [SerializeField] private string _id = default;
        [SerializeField] private Rarity _rarity = default;
        [SerializeField] private Biome _biome = default;

        [SerializeField] private float _minSizeVariance = .8f;
        [SerializeField] private float _maxSizeVariance = 1.2f;
        [SerializeField] private Level[] _levels = null;

        public string ID => _id;
        public Rarity Rarity => _rarity;
        public Biome Biome => _biome;
        public int MaxLevel => _levels.Length - 1;
        public float MinSizeVariance => _minSizeVariance;
        public float MaxSizeVariance => _maxSizeVariance;

        #endregion

        #region METHODS

        public Level GetConfigurationLevelByAge(int treeAge)
        {
            Level bestLevel = null;

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

        public Level GetConfigurationLevel(int level)
        {
            return level > MaxLevel ? null : _levels[level];
        }

        #endregion
    }
}
