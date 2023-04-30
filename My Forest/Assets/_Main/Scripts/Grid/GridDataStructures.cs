using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class GridData
    {
        private List<TileData> _tiles = new List<TileData>();

        public IReadOnlyList<TileData> Tiles => _tiles;

        [JsonIgnore]
        public int TilesCount => _tiles.Count;
        [JsonIgnore]
        public bool IsEmpty => _tiles.Count == default;

        public GridData() { }

        [JsonConstructor]
        public GridData(List<TileData> tiles)
        {
            _tiles = tiles;
        }

        public void AddTile(TileData newTile)
        {
            _tiles.Add(newTile);
        }
    }
    
    [Serializable]
    public class TileData
    {
        public BiomeType BiomeType { get; private set; }
        public int Q { get; private set; }
        public int R { get; private set; }

        [JsonConstructor]
        public TileData(BiomeType biomeType, int q, int r)
        {
            BiomeType = biomeType;
            Q = q;
            R = r;
        }
    }

    public enum BiomeType
    {
        Forest,
        Desert
    }
}
