using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(TerrainConfigurations), menuName = MENU_NAME)]
    public partial class TerrainConfigurations : ScriptableObject
    {
        [Serializable]
        public class BiomeConfiguration
        {
            [SerializeField] private string _name;
            [SerializeField] private Biome _biome;
            [SerializeField] private Color _color = Color.white;
            [SerializeField] private float _heightFactor = 1;
            [SerializeField] private Vector2 _temperatureRange;
            [SerializeField] private Vector2 _humidityRange;

            public string Name => _name;
            public Biome Biome => _biome;
            public Color Color => _color;
            public float HeightFactor => _heightFactor;
            
            public bool IsInMapRange(float temperature, float humidity)
            {
                return temperature >= _temperatureRange.x && temperature <= _temperatureRange.y && humidity >= _humidityRange.x && humidity <= _humidityRange.y;
            }
        }
        
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Terrain/" + nameof(TerrainConfigurations);

        [Header("NOISE CONFIGURATIONS")]
        [SerializeField] private float _resolution = 256;
        [SerializeField] private float _temperatureScale = 10;
        [SerializeField] private float _humidityScale = 20;
        [SerializeField] private float _heightScale = 4;
        
        [Header("CONFIGURATIONS")]
        [SerializeField] private float _minHeight = 1;
        [SerializeField] private float _maxHeight = 20;
        
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

    public partial class TerrainConfigurations : ITerrainConfigurationsSource
    {
        float ITerrainConfigurationsSource.Resolution => _resolution;
        float ITerrainConfigurationsSource.TemperatureScale => _temperatureScale;
        float ITerrainConfigurationsSource.HumidityScale => _humidityScale;
        float ITerrainConfigurationsSource.HeightScale => _heightScale;
        float ITerrainConfigurationsSource.MinHeight => _minHeight;
        float ITerrainConfigurationsSource.MaxHeight => _maxHeight;
        
        Biome ITerrainConfigurationsSource.GetBiomeForValues(float temperature, float humidity)
        {
            foreach (var biomeConfiguration in _biomeConfigurationsMap.Values)
            {
                if (biomeConfiguration.IsInMapRange(temperature, humidity))
                {
                    return biomeConfiguration.Biome;
                }
            }

            return default(Biome);
        }
        
        Color ITerrainConfigurationsSource.GetColorForBiome(Biome biome)
        {
            _biomeConfigurationsMap.TryGetValue(biome, out var biomeConfiguration);
            return biomeConfiguration?.Color ?? Color.white;
        }
        float ITerrainConfigurationsSource.GetHeightFactorForBiome(Biome biome)
        {
            _biomeConfigurationsMap.TryGetValue(biome, out var biomeConfiguration);
            return biomeConfiguration?.HeightFactor ?? 1f;
        }
    }
}
