namespace MyForest
{
    public interface ITerrainInitializationSource
    {
        void SetSeed(string seed);
    }
    
    public interface ITerrainGenerationSource
    {
        private const float DEFAULT_SCALE = 10f;

        float GetValueAtCoordinates(Coordinates coordinates, float scale = DEFAULT_SCALE);
    }
}
