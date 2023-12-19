using UnityEngine;

using Zenject;

namespace MyForest
{
    public class Tile : MonoBehaviour
    {
        #region FIELDS
        
        private static readonly int ShaderColorProperty = Shader.PropertyToID("_BaseColor");
        
        [Inject] private ITileConfigurationsSource _tileConfigurationsSource = null;
        [Inject] private ITerrainConfigurationsSource _terrainConfigurationsSource = null;
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
            var color = _terrainConfigurationsSource.GetColorForBiome(biome);

            if (height <= _terrainConfigurationsSource.LakeHeight)
            {
                height = _terrainConfigurationsSource.LakeHeight;
                biome = Biome.Lake;
                color = _terrainConfigurationsSource.LakeColor;
            }
            else if (biome == Biome.Mountain && height >= _terrainConfigurationsSource.TundraHeight)
            {
                biome = Biome.Tundra;
                color = _terrainConfigurationsSource.TundraColor;
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
