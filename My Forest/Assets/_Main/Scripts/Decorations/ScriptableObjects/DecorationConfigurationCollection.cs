using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using Zenject;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(DecorationConfigurationCollection), menuName = MENU_NAME)]
    public partial class DecorationConfigurationCollection : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Decorations/" + nameof(DecorationConfigurationCollection);
        
        [Header("TREES")]
        [SerializeField] private Decoration _decorationPrefab = null;
        
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
            return decorationsForBiome[Random.Range(0, decorationsForBiome.Count - 1)];
        }
    }
}
