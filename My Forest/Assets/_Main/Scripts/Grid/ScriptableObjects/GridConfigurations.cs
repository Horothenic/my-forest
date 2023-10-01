using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(GridConfigurations), menuName = MENU_NAME)]
    public partial class GridConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Grid/" + nameof(GridConfigurations);

        [Header("HEXAGONS")]
        [SerializeField] private float _hexagonRadius = 1f;

        [Header("COLORS")]
        [SerializeField] private Color _forestColor = Color.white;
        [SerializeField] private Color _desertColor = Color.white;
        [SerializeField] private Color _mountainColor = Color.white;

        #endregion
    }

    public partial class GridConfigurations : IGridConfigurationsSource
    {
        float IGridConfigurationsSource.HexagonRadius => _hexagonRadius;

        Color IGridConfigurationsSource.GetBiomeColor(BiomeType biomeType)
        {
            switch (biomeType)
            {
                case BiomeType.Forest:
                    return _forestColor;
                case BiomeType.Desert:
                    return _desertColor;
                case BiomeType.Mountain:
                    return _mountainColor;
                default:
                    return Color.white;
            }
        }
    }
}
