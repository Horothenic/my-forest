using UnityEngine;

namespace MyForest
{
    public interface IGridServiceSource
    {
        HexagonTile CreateTile(Transform parent, TileData tileData);
        TileData GetRandomTileDataForBiome(Biome biome);
    }

    public interface IGridPositioningSource
    {
        Vector3 GetWorldPosition(Coordinates coordinates);
    }

    public interface IGridConfigurationsSource
    {
        HexagonTile HexagonPrefab { get; }
        float HexagonRadius { get; }
        Color GetBiomeColor(Biome biome);
    }
}
