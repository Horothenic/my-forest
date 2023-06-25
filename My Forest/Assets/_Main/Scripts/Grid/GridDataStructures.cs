using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class GridData
    {
        private List<TileData> _tiles = new();
        private Dictionary<(int, int), TileData> _tilesMap = new();

        public IReadOnlyList<TileData> Tiles => _tiles;

        [JsonIgnore]
        public int TilesCount => _tiles.Count;
        [JsonIgnore]
        public Dictionary<(int, int), TileData> TilesMap => _tilesMap;
        [JsonIgnore]
        public bool IsEmpty => _tiles.Count == default;

        public GridData() { }

        [JsonConstructor]
        public GridData(List<TileData> tiles)
        {
            _tiles = tiles;

            foreach (var tile in _tiles)
            {
                _tilesMap.Add((tile.Q, tile.R), tile);
            }
        }

        public void AddTile(TileData newTile)
        {
            _tiles.Add(newTile);
            _tilesMap.Add((newTile.Q, newTile.R), newTile);
        }
    }

    [Serializable]
    public class TileData
    {
        public BiomeType BiomeType { get; private set; }
        public int Q { get; private set; }
        public int R { get; private set; }
        public bool Surrounded { get; private set; }

        [JsonIgnore]
        public (int, int) Coordinates => (Q, R);

        [JsonConstructor]
        public TileData(BiomeType biomeType, int q, int r, bool surrounded)
        {
            BiomeType = biomeType;
            Q = q;
            R = r;
            Surrounded = surrounded;
        }

        public void SetAsSurrounded()
        {
            Surrounded = true;
        }
    }

    public enum BiomeType
    {
        Forest,
        Desert
    }
}
