using UnityEngine;
using Zenject;

namespace MyForest
{
    public partial class TerrainManager
    {
        [Inject] private ITerrainConfigurationsSource _terrainConfigurationsSource;
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
            var temperature = GetValueAtCoordinates(coordinates, _terrainConfigurationsSource.TemperatureScale);
            var humidity = GetValueAtCoordinates(coordinates, _terrainConfigurationsSource.HumidityScale);
            
            return _terrainConfigurationsSource.GetBiomeForValues(temperature, humidity);
        }
        
        float ITerrainGenerationSource.GetHeightAtCoordinates(Coordinates coordinates)
        {
            return Mathf.Lerp(1f, _terrainConfigurationsSource.MaxHeight, GetValueAtCoordinates(coordinates, _terrainConfigurationsSource.HeightScale));
        }
        
        private float GetValueAtCoordinates(Coordinates coordinates, float scale)
        {
            var x = coordinates.Q / _terrainConfigurationsSource.Resolution * scale + _offsetX;
            var y = coordinates.R / _terrainConfigurationsSource.Resolution * scale + _offsetY;
            return Mathf.PerlinNoise(x, y);
        }
    }
}
