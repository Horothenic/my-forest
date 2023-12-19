using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
            [SerializeField] private Vector2 _temperatureRange;
            [SerializeField] private Vector2 _humidityRange;

            public string Name => _name;
            public Biome Biome => _biome;
            public Color Color => _color;
            
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
        
        [Header("HEIGHT CONFIGURATIONS")]
        [SerializeField] private float _minHeight = 1;
        [SerializeField] private float _maxHeight = 20;
        
        [Header("LAKE CONFIGURATIONS")]
        [SerializeField] private float _lakeHeight = 8;
        [SerializeField] private Color _lakeColor = Color.white;
        
        [Header("TUNDRA CONFIGURATIONS")]
        [SerializeField] private float _tundraHeight = 8;
        [SerializeField] private Color _tundraColor = Color.white;
        
        [Header("BASE BIOMES")]
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
        
        float ITerrainConfigurationsSource.LakeHeight => _lakeHeight;
        Color ITerrainConfigurationsSource.LakeColor => _lakeColor;
        float ITerrainConfigurationsSource.TundraHeight => _tundraHeight;
        Color ITerrainConfigurationsSource.TundraColor => _tundraColor;
        
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
    }
}
