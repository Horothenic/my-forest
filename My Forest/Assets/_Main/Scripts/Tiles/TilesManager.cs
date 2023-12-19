using UnityEngine;

using Zenject;

namespace MyForest
{
    public partial class TileManager
    {
        [Inject] private ITileConfigurationsSource _tileConfigurationsSource = null;
        [Inject] private ITilePositioningSource _tilePositioningSource = null;
        [Inject] private IObjectPoolSource _objectPoolSource = null;
        
        private static readonly float SQUARE_ROOT_OF_TREE = Mathf.Sqrt(3.0f);
        private const float THREE_OVER_TWO = 3.0f / 2.0f;
    }

    public partial class TileManager : ITileServiceSource
    {
        Tile ITileServiceSource.CreateTile(Transform parent, Coordinates coordinates)
        {
            var tile = _objectPoolSource.Borrow(_tileConfigurationsSource.TilePrefab);
            tile.gameObject.Set(_tilePositioningSource.GetWorldPosition(coordinates), parent);

            tile.Initialize(coordinates);
            return tile;
        }
    }

    public partial class TileManager : ITilePositioningSource
    {
        Vector3 ITilePositioningSource.GetWorldPosition(Coordinates coordinates)
        {
            var q = coordinates.Q;
            var r = coordinates.R;

            var positionX = _tileConfigurationsSource.TileRadius * SQUARE_ROOT_OF_TREE * (q + r / 2f);
            var positionZ = _tileConfigurationsSource.TileRadius * THREE_OVER_TWO * r;

            return new Vector3(positionX, 0, positionZ);
        }
    }
}
