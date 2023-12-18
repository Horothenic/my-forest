using UnityEngine;

using Zenject;

namespace MyForest
{
    public class HexagonTile : MonoBehaviour
    {
        #region FIELDS
        
        private static readonly int ShaderColorProperty = Shader.PropertyToID("_BaseColor");
        
        [Inject] private IGridConfigurationsSource _gridConfigurationsSource = null;
        [Inject] private ITerrainGenerationSource _terrainGenerationSource = null;
        
        [Header("COMPONENTS")]
        [SerializeField] private MeshRenderer _meshRenderer = null;
        [SerializeField] private Transform _model = null;
        
        #endregion

        #region METHODS

        public void Initialize(TileData tileData)
        {
            //_meshRenderer.material.SetColor(ShaderColorProperty, _gridConfigurationsSource.GetBiomeColor(tileData.Biome));
            var value = _terrainGenerationSource.GetValueAtCoordinates(tileData.Coordinates);
            _meshRenderer.material.SetColor(ShaderColorProperty, new Color(value, value, value));
            
            _model.localScale = new Vector3
            (
                _gridConfigurationsSource.TileRadius, 
                _gridConfigurationsSource.TileRadius, 
                tileData.Height * _gridConfigurationsSource.TileBaseHeight
            );
        }

        #endregion
    }
}
