using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MyIsland
{
    [CreateAssetMenu(fileName = nameof(TreeConfigurationCollection), menuName = MENU_NAME)]
    public class TreeConfigurationCollection : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyIsland) + "/Trees/" + nameof(TreeConfigurationCollection);
        
        [Header("CONFIGURATIONS")]
        [Probability("Tree Rarity Probability", typeof(Rarity))] public Probability _treeRarityProbability;

        [Header("COLLECTION")]
        [SerializeField] private TreeConfiguration[] _treeConfigurations = null;

        #endregion

        #region METHODS

        public TreeConfiguration GetRandomTreeForBiome(Biome biome)
        {
            var treesFromBiome = _treeConfigurations.Where(tc => tc.Biome == biome).ToList();
            var randomRarity = _treeRarityProbability.Calculate<Rarity>();

            List<TreeConfiguration> configurationsWithRarity = null;
            do
            {
                configurationsWithRarity = treesFromBiome.Where(tc => tc.Rarity == randomRarity).ToList();
                randomRarity--;
            }
            while (configurationsWithRarity.Count == 0 && randomRarity >= 0);

            return configurationsWithRarity[Random.Range(0, configurationsWithRarity.Count - 1)];
        }

        #endregion
    }
}