using UnityEngine;
using Zenject;

namespace MyForest
{
    public partial class TerrainManager
    {
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
        Biome ITerrainGenerationSource.GetBiomeAtCoordinates(Coordinates coordinates)
        {
            var temperatureNoiseConfiguration = _heightConfigurationsSource.HeightNoiseConfiguration;
            var humidityNoiseConfiguration = _heightConfigurationsSource.HeightNoiseConfiguration;
            
            var temperature = GetValueAtCoordinates(coordinates,
                temperatureNoiseConfiguration.Scale,
                temperatureNoiseConfiguration.Octaves,
                temperatureNoiseConfiguration.Persistance,
                temperatureNoiseConfiguration.Lacunarity);
            
            var humidity = GetValueAtCoordinates(coordinates,
                humidityNoiseConfiguration.Scale,
                humidityNoiseConfiguration.Octaves,
                humidityNoiseConfiguration.Persistance,
                humidityNoiseConfiguration.Lacunarity);
            
            return _biomeConfigurationsSource.GetBiomeForValues(temperature, humidity);
        }
        
        float ITerrainGenerationSource.GetHeightAtCoordinates(Coordinates coordinates)
        {
            var heightNoiseConfiguration = _heightConfigurationsSource.HeightNoiseConfiguration;
            
            var heightAtCoordinates = GetValueAtCoordinates(coordinates,
                heightNoiseConfiguration.Scale,
                heightNoiseConfiguration.Octaves,
                heightNoiseConfiguration.Persistance,
                heightNoiseConfiguration.Lacunarity);

            return _heightConfigurationsSource.HeightSpline.Evaluate(heightAtCoordinates);
        }
        
        private float GetValueAtCoordinates(Coordinates coordinates, float scale, int octaves, float persistance, float lacunarity)
        {
            var total = 0f;
            var frequency = 1f;
            var amplitude = 1f;
            var maxValue = 0f;

            for (var i = 0; i < octaves; i++)
            {
                var x = ((coordinates.Q / scale) * frequency) + _offsetX;
                var y = ((coordinates.R / scale) * frequency) + _offsetX;
                
                total += Mathf.PerlinNoise(x, y) * amplitude;

                maxValue += amplitude;
                amplitude *= persistance;
                frequency *= lacunarity;
            }
            
            return total / maxValue;
        }
    }
}
