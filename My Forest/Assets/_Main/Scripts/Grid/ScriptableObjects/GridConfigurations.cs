using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(GridConfigurations), menuName = MENU_NAME)]
    public partial class GridConfigurations : ScriptableObject
    {
        [Serializable]
        public class BiomeConfiguration
        {
            private const int MIN_HEIGHT = 1;
            
            [SerializeField] private string _name = default;
            [SerializeField] private Biome _biome;
            [SerializeField] private Color _color = Color.white;
            [SerializeField][Range(0, 1)] private float _lowerHeightThreshold;
            [SerializeField][Range(0, 1)] private float _higherHeightThreshold;

            public string Name => _name;
            public Biome Biome => _biome;
            public Color Color => _color;

            public int GetRandomHeightForTile(int originHeight)
            {
                var randomValue = UnityEngine.Random.value;

                if (randomValue <= _lowerHeightThreshold)
                {
                    return Mathf.Max(originHeight - 1, MIN_HEIGHT);
                }
    
                if (randomValue <= _higherHeightThreshold)
                {
                    return Mathf.Max(originHeight + 1, MIN_HEIGHT);
                }

                return Mathf.Max(originHeight, MIN_HEIGHT);
            }
        }
        
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Grid/" + nameof(GridConfigurations);

        [FormerlySerializedAs("_hexagonPrefab")]
        [Header("HEXAGONS")]
        [SerializeField] private HexagonTile _tilePrefab = null;
        [FormerlySerializedAs("_hexagonRadius")]
        [SerializeField] private float _tileRadius = 1.5f;
        [FormerlySerializedAs("_hexagonBaseHeight")]
        [SerializeField] private float _tileBaseHeight = 1f;
        [FormerlySerializedAs("_hexagonRealHeight")]
        [SerializeField] private float _tileRealHeight = 0.3f;

        [Header("COLLECTION")]
        [SerializeField] private BiomeConfiguration[] _biomeConfigurations = null;
        
        private readonly Dictionary<Biome, BiomeConfiguration> _biomeConfigurationsMap = new Dictionary<Biome, BiomeConfiguration>();

        #endregion

        #region METHODS
        
        public void Initialize()
        {
            foreach (var biomeConfiguration in _biomeConfigurations)
            {
                _biomeConfigurationsMap.Add(biomeConfiguration.Biome, biomeConfiguration);
            }
        }

        #endregion
    }

    public partial class GridConfigurations : IGridConfigurationsSource
    {
        HexagonTile IGridConfigurationsSource.TilePrefab => _tilePrefab;
        float IGridConfigurationsSource.TileRadius => _tileRadius;
        float IGridConfigurationsSource.TileBaseHeight => _tileBaseHeight;
        float IGridConfigurationsSource.TileRealHeight => _tileBaseHeight * _tileRealHeight;

        Color IGridConfigurationsSource.GetBiomeColor(Biome biome)
        {
            _biomeConfigurationsMap.TryGetValue(biome, out var biomeConfiguration);
            return biomeConfiguration?.Color ?? Color.white;
        }

        int IGridConfigurationsSource.GetRandomHeight(Biome biome, int originHeight)
        {
            _biomeConfigurationsMap.TryGetValue(biome, out var biomeConfiguration);
            return biomeConfiguration?.GetRandomHeightForTile(originHeight) ?? 1;
        }
    }
}
