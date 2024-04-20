using UnityEngine;
using Zenject;

namespace MyForest
{
    public partial class TerrainManager
    {
        [Inject] private ITileConfigurationsSource _tileConfigurationsSource = null;
        [Inject] private IHeightConfigurationsSource _heightConfigurationsSource;
        [Inject] private IBiomeConfigurationsSource _biomeConfigurationsSource;
    }

    public partial class TerrainManager : ITerrainInitializationSource
    {
        private const float OFFSET_MIN = 100000;
        private const float OFFSET_MAX = 500000;
        
        private float _offsetX;
        private float _offsetY;

        void ITerrainInitializationSource.SetSeed(string seed)
        {
            var hash = seed.GetSHA256Hash();
            Random.InitState(hash);
            _offsetX = Random.Range(OFFSET_MIN, OFFSET_MAX);
            _offsetY = Random.Range(OFFSET_MIN, OFFSET_MAX);
        }
    }

    public partial class TerrainManager : ITerrainGenerationSource
    {
        (Biome biome, Color color, float height) ITerrainGenerationSource.GetTerrainValues(Coordinates coordinates)
        {
            var biome = GetBiomeAtCoordinates(coordinates);
            var color = _biomeConfigurationsSource.GetBiomeColor(biome);
            var steepness = _biomeConfigurationsSource.GetBiomeSteepness(biome);
            
            var baseHeight = GetBaseHeightAtCoordinates(coordinates) * _tileConfigurationsSource.TileBaseHeight;
            var height = SteepValue(baseHeight, steepness);
            
            if (baseHeight <= _biomeConfigurationsSource.LakeHeight)
            {
                height = _biomeConfigurationsSource.LakeHeight;
                color = _biomeConfigurationsSource.LakeColor;
            }
            else if (biome == Biome.Mountain && baseHeight >= _biomeConfigurationsSource.TundraHeight)
            {
                biome = Biome.Tundra;
                color = _biomeConfigurationsSource.TundraColor;
            }
            
            return (biome, color, height);
        }
        
        private Biome GetBiomeAtCoordinates(Coordinates coordinates)
        {
            var temperatureNoiseConfiguration = _biomeConfigurationsSource.TemperatureNoiseConfiguration;
            var humidityNoiseConfiguration = _biomeConfigurationsSource.HumidityNoiseConfiguration;
            
            var temperature = GetValueAtCoordinates(coordinates,
                temperatureNoiseConfiguration.Smoothness,
                temperatureNoiseConfiguration.Octaves,
                temperatureNoiseConfiguration.Persistance,
                temperatureNoiseConfiguration.Lacunarity);
            
            var humidity = GetValueAtCoordinates(coordinates,
                humidityNoiseConfiguration.Smoothness,
                humidityNoiseConfiguration.Octaves,
                humidityNoiseConfiguration.Persistance,
                humidityNoiseConfiguration.Lacunarity);
            
            return _biomeConfigurationsSource.GetBiomeForValues(temperature, humidity);
        }
        
        private float GetBaseHeightAtCoordinates(Coordinates coordinates)
        {
            var heightNoiseConfiguration = _heightConfigurationsSource.HeightNoiseConfiguration;
            
            var heightAtCoordinates = GetValueAtCoordinates(coordinates,
                heightNoiseConfiguration.Smoothness,
                heightNoiseConfiguration.Octaves,
                heightNoiseConfiguration.Persistance,
                heightNoiseConfiguration.Lacunarity);

            return _heightConfigurationsSource.HeightSpline.Evaluate(heightAtCoordinates);
        }

        private float GetValueAtCoordinates(Coordinates coordinates, float smoothness, int octaves, float persistance, float lacunarity)
        {
            var total = 0f;
            var frequency = 1f;
            var amplitude = 1f;
            var maxValue = 0f;

            for (var i = 0; i < octaves; i++)
            {
                var x = ((coordinates.Q + _offsetX) / smoothness) * frequency;
                var y = ((coordinates.R + _offsetY) / smoothness) * frequency;
                
                total += Mathf.PerlinNoise(x, y) * amplitude;

                maxValue += amplitude;
                amplitude *= persistance;
                frequency *= lacunarity;
            }

            return total / maxValue;
        }
        
        private float SteepValue(float value, float steepness)
        {
            return Mathf.Clamp(Mathf.Pow(value, steepness), _biomeConfigurationsSource.CoastalMinHeight, float.MaxValue);
        }
    }
}
