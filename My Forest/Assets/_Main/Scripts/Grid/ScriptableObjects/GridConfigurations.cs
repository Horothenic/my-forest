using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(GridConfigurations), menuName = MENU_NAME)]
    public partial class GridConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Grid/" + nameof(GridConfigurations);

        [Header("HEXAGONS")]
        [SerializeField] private HexagonTile _hexagonPrefab = null;
        [SerializeField] private float _hexagonRadius = 1f;

        [Header("COLORS")]
        [SerializeField] private Color _forestColor = Color.white;
        [SerializeField] private Color _desertColor = Color.white;
        [SerializeField] private Color _mountainColor = Color.white;

        #endregion
    }

    public partial class GridConfigurations : IGridConfigurationsSource
    {
        HexagonTile IGridConfigurationsSource.HexagonPrefab => _hexagonPrefab;
        float IGridConfigurationsSource.HexagonRadius => _hexagonRadius;

        Color IGridConfigurationsSource.GetBiomeColor(Biome biome)
        {
            switch (biome)
            {
                case Biome.Forest:
                    return _forestColor;
                case Biome.Desert:
                    return _desertColor;
                case Biome.Mountain:
                    return _mountainColor;
                default:
                    return Color.white;
            }
        }
    }
}
