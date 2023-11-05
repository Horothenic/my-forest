using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using Zenject;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(TreeConfigurationCollection), menuName = MENU_NAME)]
    public partial class TreeConfigurationCollection : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Trees/" + nameof(TreeConfigurationCollection);
        
        [Header("TREES")]
        [SerializeField] private Tree _treePrefab = null;
        
        [Header("PROBABILITIES")]
        [SerializeField][Range(0, 1)] private float _endangeredThreshold = 0.05f;
        [SerializeField][Range(0, 1)] private float _exquisiteThreshold = 0.15f;
        [SerializeField][Range(0, 1)] private float _rareThreshold = 0.35f;

        [Header("COLLECTION")]
        [SerializeField] private TreeConfiguration[] _treeConfigurations = null;
        
        private readonly Dictionary<string, TreeConfiguration> _treeConfigurationsMap = new Dictionary<string, TreeConfiguration>();

        #endregion

        #region METHODS
        
        public void Initialize()
        {
            foreach (var treeConfigurations in _treeConfigurations)
            {
                _treeConfigurationsMap.Add(treeConfigurations.ID, treeConfigurations);
            }
        }

        private TreeRarity GetRandomTreeRarity()
        {
            var randomValue = Random.value;

            if (randomValue <= _endangeredThreshold)
            {
                return TreeRarity.Endangered;
            }
            
            if (randomValue <= _exquisiteThreshold)
            {
                return TreeRarity.Exquisite;
            }
            
            if (randomValue <= _rareThreshold)
            {
                return TreeRarity.Rare;
            }

            return TreeRarity.Common;
        }

        #endregion
    }

    public partial class TreeConfigurationCollection : ITreeConfigurationCollectionSource
    {
        Tree ITreeConfigurationCollectionSource.TreePrefab => _treePrefab;
        
        TreeConfiguration ITreeConfigurationCollectionSource.GetConfiguration(string treeID)
        {
            _treeConfigurationsMap.TryGetValue(treeID, out var treeConfiguration);
            return treeConfiguration;
        }

        TreeConfiguration ITreeConfigurationCollectionSource.GetRandomConfigurationForBiome(Biome biome)
        {
            var treeFromBiome = _treeConfigurations.Where(tc => tc.Biome == biome).ToList();
            var randomRarity = GetRandomTreeRarity();
            
            List<TreeConfiguration> configurationsWithRarity = null;
            do
            {
                configurationsWithRarity = treeFromBiome.Where(tc => tc.Rarity == randomRarity).ToList();
                randomRarity--;
            }
            while (configurationsWithRarity.Count == 0);

            return configurationsWithRarity[Random.Range(0, configurationsWithRarity.Count - 1)];
        }
    }
}
