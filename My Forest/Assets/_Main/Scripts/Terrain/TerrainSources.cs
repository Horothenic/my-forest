using UnityEngine;

namespace MyForest
{
    public interface ITerrainInitializationSource
    {
        void SetSeed(string seed);
    }
    
    public interface ITerrainGenerationSource
    {
        (Biome biome, Color color, float height) GetTerrainValues(Coordinates coordinates);
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
        float CoastalMinHeight { get; }
        float TundraHeight { get; }
        Color TundraColor { get; }
        float TundraSteepness { get; }
        
        Biome GetBiomeForValues(float temperature, float humidity);
        Color GetBiomeColor(Biome biome);
        float GetBiomeSteepness(Biome biome);
    }
}
