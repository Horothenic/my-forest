using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(BiomeConfigurations), menuName = MENU_NAME)]
    public partial class BiomeConfigurations : ScriptableObject
    {
        [Serializable]
        public class BiomeConfiguration
        {
            [SerializeField] private string _name;
            [SerializeField] private Biome _biome;
            [SerializeField] private Color _color = Color.white;
            [SerializeField] private Vector2 _temperatureRange;
            [SerializeField] private Vector2 _humidityRange;
            [Range(0f, 5f)][SerializeField] private float _steepness;

            public string Name => _name;
            public Biome Biome => _biome;
            public Color Color => _color;
            public float Steepness => _steepness;
            
            public bool IsInMapRange(float temperature, float humidity)
            {
                return temperature >= _temperatureRange.x && temperature <= _temperatureRange.y && humidity >= _humidityRange.x && humidity <= _humidityRange.y;
            }
        }
        
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Terrain/" + nameof(BiomeConfigurations);

        [Header("NOISE CONFIGURATIONS")]
        [SerializeField] private PerlinNoiseConfiguration _temperatureNoiseConfiguration;
        [SerializeField] private PerlinNoiseConfiguration _humidityNoiseConfiguration;
        
        [Header("BASE BIOMES")]
        [SerializeField] private BiomeConfiguration[] _biomeConfigurations = null;
        
        [Header("FIXED BIOMES")]
        [SerializeField] private float _lakeHeight = 8;
        [SerializeField] private float _coastalMinHeight = 9;
        [SerializeField] private Color _lakeColor = Color.white;
        [SerializeField] private float _tundraHeight = 8;
        [SerializeField] private float _tundraSteepness = 2f;
        [SerializeField] private Color _tundraColor = Color.white;
        
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

    public partial class BiomeConfigurations : IBiomeConfigurationsSource
    {
        public PerlinNoiseConfiguration TemperatureNoiseConfiguration => _temperatureNoiseConfiguration;
        public PerlinNoiseConfiguration HumidityNoiseConfiguration => _humidityNoiseConfiguration;
        
        float IBiomeConfigurationsSource.LakeHeight => _lakeHeight;
        Color IBiomeConfigurationsSource.LakeColor => _lakeColor;
        float IBiomeConfigurationsSource.CoastalMinHeight => _coastalMinHeight;
        float IBiomeConfigurationsSource.TundraHeight => _tundraHeight;
        Color IBiomeConfigurationsSource.TundraColor => _tundraColor;
        float IBiomeConfigurationsSource.TundraSteepness => _tundraSteepness;
        
        Biome IBiomeConfigurationsSource.GetBiomeForValues(float temperature, float humidity)
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
        
        Color IBiomeConfigurationsSource.GetBiomeColor(Biome biome)
        {
            _biomeConfigurationsMap.TryGetValue(biome, out var biomeConfiguration);
            return biomeConfiguration?.Color ?? Color.white;
        }
        
        float IBiomeConfigurationsSource.GetBiomeSteepness(Biome biome)
        {
            switch (biome)
            {
                case Biome.Lake:
                    return 1f;
                case Biome.Tundra:
                    return _tundraSteepness;
                default:
                    _biomeConfigurationsMap.TryGetValue(biome, out var biomeConfiguration);
                    return biomeConfiguration?.Steepness ?? 1f;
            }
        }
    }
}
