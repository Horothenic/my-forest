using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using System.Linq;

namespace MyForest
{
    [Serializable]
    public class GridData
    {
        private List<TileData> _tiles = new List<TileData>();

        public IReadOnlyList<TileData> Tiles => _tiles;
        
        [JsonIgnore]
        public Dictionary<Coordinates, TileData> TilesMap { get; private set; } = new Dictionary<Coordinates, TileData>();
        [JsonIgnore]
        public int TilesCount => TilesMap.Values.Count;
        [JsonIgnore]
        public bool IsEmpty => TilesCount == default(int);

        public GridData() { }

        [JsonConstructor]
        public GridData(List<TileData> tiles)
        {
            _tiles = tiles;

            foreach (var tile in Tiles)
            {
                TilesMap.Add(tile.Coordinates, tile);
            }
        }

        public void AddTile(TileData newTile)
        {
            _tiles.Add(newTile);
            TilesMap.Add(newTile.Coordinates, newTile);
        }
    }

    [Serializable]
    public class TileData
    {
        public BiomeType BiomeType { get; private set; }
        public bool Surrounded { get; private set; }
        public Coordinates Coordinates { get; private set; }

        [JsonConstructor]
        public TileData(BiomeType biomeType, Coordinates coordinates, bool surrounded)
        {
            BiomeType = biomeType;
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
        public bool Surrounded { get; private set; }
        
        [JsonConstructor]
        public Coordinates(int q, int r, bool surrounded)
        {
            Q = q;
            R = r;
            Surrounded = surrounded;
        }
    }

    public enum BiomeType
    {
        Forest,
        Desert,
        Mountain
    }
}
