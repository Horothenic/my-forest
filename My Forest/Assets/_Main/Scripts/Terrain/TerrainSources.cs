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

    public interface IHeightConfigurationsSource
    {
        PerlinNoiseConfiguration HeightNoiseConfiguration { get; }
        Spline HeightSpline { get; }
    }
    
    public interface IBiomeConfigurationsSource
    {
        PerlinNoiseConfiguration TemperatureNoiseConfiguration { get; }
        PerlinNoiseConfiguration HumidityNoiseConfiguration { get; }
        
        float LakeHeight { get; }
        Color LakeColor { get; }
        float TundraHeight { get; }
        Color TundraColor { get; }
        
        Biome GetBiomeForValues(float temperature, float humidity);
        Color GetColorForBiome(Biome biome);
    }
}
