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
            var biome = _terrainGenerationSource.GetBiomeAtCoordinates(coordinates);
            var height = CalculateHeight(_terrainGenerationSource.GetHeightAtCoordinates(coordinates), _terrainConfigurationsSource.GetHeightFactorForBiome(biome));
            
            _meshRenderer.material.SetColor(ShaderColorProperty, _terrainConfigurationsSource.GetColorForBiome(biome));
            
            _model.localScale = new Vector3
            (
                _tileConfigurationsSource.TileRadius, 
                _tileConfigurationsSource.TileRadius, 
                height * _tileConfigurationsSource.TileBaseHeight
            );
        }

        private float CalculateHeight(float terrainHeight, float biomeFactor)
        {
            return Mathf.Clamp(terrainHeight * biomeFactor, _terrainConfigurationsSource.MinHeight, float.MaxValue);
        }

        #endregion
    }
}
