using UnityEngine;

namespace MyForest
{
    public interface IGridServiceSource
    {
        HexagonTile CreateTile(Transform parent, TileData tileData);
        TileData CreateRandomTileDataForBiome(Biome biome);
    }

    public interface IGridPositioningSource
    {
        Vector3 GetWorldPosition(Coordinates coordinates);
    }

    public interface IGridConfigurationsSource
    {
        HexagonTile TilePrefab { get; }
        float TileRadius { get; }
        float TileBaseHeight { get; }
        float TileRealHeight { get; }
        Color GetBiomeColor(Biome biome);
        int GetRandomHeight(Biome biome, int originHeight);
    }
}
