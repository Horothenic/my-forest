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
        
        [Header("TREES")]
        [SerializeField] private Decoration _decorationPrefab = null;
        
        [Header("PROBABILITIES")]
        [SerializeField][Range(0, 1)] private float _endangeredThreshold = 0.05f;
        [SerializeField][Range(0, 1)] private float _exquisiteThreshold = 0.15f;
        [SerializeField][Range(0, 1)] private float _rareThreshold = 0.35f;
        
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

        private Rarity GetRandomDecorationRarity()
        {
            var randomValue = Random.value;

            if (randomValue <= _endangeredThreshold)
            {
                return Rarity.Endangered;
            }
            
            if (randomValue <= _exquisiteThreshold)
            {
                return Rarity.Exquisite;
            }
            
            if (randomValue <= _rareThreshold)
            {
                return Rarity.Rare;
            }

            return Rarity.Common;
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
            return decorationsForBiome[Random.Range(0, decorationsForBiome.Count - 1)];
        }
    }
}
