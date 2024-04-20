using UnityEngine;

namespace MyForest
{
    public interface ITileServiceSource
    {
        Tile CreateTile(Transform parent, Coordinates coordinates);
    }

    public interface ITilePositioningSource
    {
        Vector3 GetWorldPosition(Coordinates coordinates);
    }

    public interface ITileConfigurationsSource
    {
        Tile TilePrefab { get; }
        float TileRadius { get; }
        float TileBaseHeight { get; }
        float TileRealHeight { get; }
    }
}
