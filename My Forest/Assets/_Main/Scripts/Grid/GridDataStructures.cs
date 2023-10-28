using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using System.Linq;

namespace MyForest
{
    [Serializable]
    public class TileData
    {
        public Biome Biome { get; private set; }
        public bool Surrounded { get; private set; }
        public Coordinates Coordinates { get; private set; }

        [JsonConstructor]
        public TileData(Biome biome, Coordinates coordinates, bool surrounded)
        {
            Biome = biome;
            Surrounded = surrounded;
            Coordinates = coordinates;
        }

        public void SetAsSurrounded()
        {
            Surrounded = true;
        }
    }
    
    [Serializable]
    public struct Coordinates
    {
        public int Q { get; private set; }
        public int R { get; private set; }
        
        [JsonConstructor]
        public Coordinates(int q, int r)
        {
            Q = q;
            R = r;
        }
    }
}
