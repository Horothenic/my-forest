using UnityEngine;

using Zenject;

namespace MyForest
{
    public class Tile : MonoBehaviour
    {
        #region FIELDS
        
        private static readonly int ShaderColorProperty = Shader.PropertyToID("_BaseColor");
        
        [Inject] private ITileConfigurationsSource _tileConfigurationsSource = null;
        [Inject] private IHeightConfigurationsSource _heightConfigurationsSource = null;
        [Inject] private IBiomeConfigurationsSource _biomeConfigurationsSource = null;
        [Inject] private ITerrainGenerationSource _terrainGenerationSource = null;
        
        [Header("COMPONENTS")]
        [SerializeField] private MeshRenderer _meshRenderer = null;
        [SerializeField] private Transform _model = null;

        #endregion

        #region METHODS

        public void Initialize(Coordinates coordinates)
        {
            var height = _terrainGenerationSource.GetHeightAtCoordinates(coordinates) * _tileConfigurationsSource.TileBaseHeight;
            var biome = _terrainGenerationSource.GetBiomeAtCoordinates(coordinates);
            var color = _biomeConfigurationsSource.GetColorForBiome(biome);

            if (height <= _biomeConfigurationsSource.LakeHeight)
            {
                height = _biomeConfigurationsSource.LakeHeight;
                biome = Biome.Lake;
                color = _biomeConfigurationsSource.LakeColor;
            }
            else if (biome == Biome.Mountain && height >= _biomeConfigurationsSource.TundraHeight)
            {
                biome = Biome.Tundra;
                color = _biomeConfigurationsSource.TundraColor;
            }
            
            _meshRenderer.material.SetColor(ShaderColorProperty, color);
            
            _model.localScale = new Vector3
            (
                _tileConfigurationsSource.TileRadius,
                _tileConfigurationsSource.TileRadius,
                height
            );
        }

        #endregion
    }
}
