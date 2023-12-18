using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class TileData
    {
        public Biome Biome { get; private set; }
        public int Height { get; private set; }
        public Coordinates Coordinates { get; private set; }

        [JsonConstructor]
        public TileData(Biome biome, Coordinates coordinates, int height)
        {
            Biome = biome;
            Coordinates = coordinates;
            Height = height;
        }
    }
    
    [Serializable]
    public struct Coordinates
    {
        public int Q { get; private set; }
        public int R { get; private set; }
        
        public static implicit operator Coordinates((int x, int y) tuple) => new Coordinates(tuple.x, tuple.y);
        public static implicit operator (int x, int y)(Coordinates coordinates) => (coordinates.Q, coordinates.R);
        
        [JsonConstructor]
        public Coordinates(int q, int r)
        {
            Q = q;
            R = r;
        }
    }
}
