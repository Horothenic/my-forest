using UnityEngine;

namespace MyForest
{
    public partial class TerrainManager
    {
        
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
        float ITerrainGenerationSource.GetValueAtCoordinates(Coordinates coordinates, float scale)
        {
            var x = (_offsetX + coordinates.Q) / scale;
            var y = (_offsetY + coordinates.R) / scale;
            return Mathf.PerlinNoise(x, y);
        }
        
    }
}
