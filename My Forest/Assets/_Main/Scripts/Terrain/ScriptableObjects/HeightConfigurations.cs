using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(HeightConfigurations), menuName = MENU_NAME)]
    public partial class HeightConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Terrain/" + nameof(HeightConfigurations);
        
        [Header("CONFIGURATIONS")]
        [SerializeField] private PerlinNoiseConfiguration _heightNoiseConfiguration;
        [SerializeField] private Spline _heightSpline;

        #endregion
    }

    public partial class HeightConfigurations : IHeightConfigurationsSource
    {
        public PerlinNoiseConfiguration HeightNoiseConfiguration => _heightNoiseConfiguration;
        public Spline HeightSpline => _heightSpline;
    }
}
