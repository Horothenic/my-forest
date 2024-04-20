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
            var terrainValues = _terrainGenerationSource.GetTerrainValues(coordinates);
            
            _meshRenderer.material.SetColor(ShaderColorProperty, terrainValues.color);
            _model.localScale = new Vector3
            (
                _tileConfigurationsSource.TileRadius,
                _tileConfigurationsSource.TileRadius,
                terrainValues.height
            );
        }

        #endregion
    }
}
