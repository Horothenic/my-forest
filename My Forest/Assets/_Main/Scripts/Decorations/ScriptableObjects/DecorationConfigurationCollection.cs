using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(DecorationConfigurationCollection), menuName = MENU_NAME)]
    public partial class DecorationConfigurationCollection : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Decorations/" + nameof(DecorationConfigurationCollection);
        
        [Header("DECORATIONS")]
        [SerializeField] private Decoration _decorationPrefab = null;
        [Probability("Decorations Rarity Probability", typeof(Rarity))] public Probability _decorationRarityProbability;
        
        [Header("COLLECTION")]
        [SerializeField] private DecorationConfiguration[] _decorationsConfigurations = null;

        private readonly Dictionary<string, DecorationConfiguration> _decorationConfigurationsMap = new Dictionary<string, DecorationConfiguration>();

        #endregion

        #region NETHODS

        public void Initialize()
        {
            foreach (var decorationConfiguration in _decorationsConfigurations)
            {
                _decorationConfigurationsMap.Add(decorationConfiguration.ID, decorationConfiguration);
            }
        }

        #endregion
    }

    public partial class DecorationConfigurationCollection : IDecorationsConfigurationCollectionSource
    {
        Decoration IDecorationsConfigurationCollectionSource.DecorationPrefab => _decorationPrefab;
        
        DecorationConfiguration IDecorationsConfigurationCollectionSource.GetConfiguration(string decorationID)
        {
            _decorationConfigurationsMap.TryGetValue(decorationID, out var decorationConfiguration);
            return decorationConfiguration;
        }

        DecorationConfiguration IDecorationsConfigurationCollectionSource.GetRandomConfigurationForBiome(Biome biome)
        {
            var decorationsForBiome = _decorationsConfigurations.Where(tc => tc.Biome == biome).ToList();
            var randomRarity = _decorationRarityProbability.Calculate<Rarity>();
            
            List<DecorationConfiguration> configurationsWithRarity = null;
            do
            {
                configurationsWithRarity = decorationsForBiome.Where(dc => dc.Rarity == randomRarity).ToList();
                randomRarity--;
            }
            while (configurationsWithRarity.Count == 0 && randomRarity >= 0);

            return configurationsWithRarity[Random.Range(0, configurationsWithRarity.Count - 1)];
        }
    }
}
