using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(TilesConfigurations), menuName = MENU_NAME)]
    public partial class TilesConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Tiles/" + nameof(TilesConfigurations);
        
        [Header("HEXAGONS")]
        [SerializeField] private Tile _tilePrefab = null;
        [SerializeField] private float _tileRadius = 1.5f;
        [SerializeField] private float _tileBaseHeight = 1f;
        [SerializeField] private float _tileRealHeight = 0.3f;
        
        #endregion
    }

    public partial class TilesConfigurations : ITileConfigurationsSource
    {
        Tile ITileConfigurationsSource.TilePrefab => _tilePrefab;
        float ITileConfigurationsSource.TileRadius => _tileRadius;
        float ITileConfigurationsSource.TileBaseHeight => _tileBaseHeight;
        float ITileConfigurationsSource.TileRealHeight => _tileBaseHeight * _tileRealHeight;
    }
}
