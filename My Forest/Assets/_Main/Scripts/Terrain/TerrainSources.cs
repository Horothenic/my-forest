using UnityEngine;

namespace MyForest
{
    public interface ITerrainInitializationSource
    {
        void SetSeed(string seed);
    }
    
    public interface ITerrainGenerationSource
    {
        Biome GetBiomeAtCoordinates(Coordinates coordinates);
        float GetHeightAtCoordinates(Coordinates coordinates);
    }

    public interface ITerrainConfigurationsSource
    {
        float Resolution { get; }
        float TemperatureScale { get; }
        float HumidityScale { get; }
        float HeightScale { get; }
        float MinHeight { get; }
        float MaxHeight { get; }
        Biome GetBiomeForValues(float temperature, float humidity);
        Color GetColorForBiome(Biome biome);
        float GetHeightFactorForBiome(Biome biome);
    }
}
