using UnityEngine;

using Cysharp.Threading.Tasks;
using Zenject;

namespace MyForest
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexagonTile : MonoBehaviour
    {
        #region FIELDS
        
        private const int SIDES = 6;
        
        private static readonly int ShaderColorProperty = Shader.PropertyToID("_BaseColor");
        
        [Inject] private IGridConfigurationsSource _gridConfigurationsSource = null;
        
        [Header("COMPONENTS")]
        [SerializeField] private MeshRenderer _meshRenderer = null;
        [SerializeField] private MeshFilter _meshFilter = null;
        
        [Header("CONFIGURATIONS")]
        [SerializeField] private float _radius = 1f;
        
        [Header("DATA")]
        [SerializeField] [ReadOnly] private Vector2 _coordinates = default;

        private Mesh _mesh = null;
        private TileData _tileData = null;

        public float Radius => _radius;
        
        #endregion

        #region UNITY

        private void Awake()
        {
            GenerateMesh().Forget();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            GenerateMesh().Forget();
        }

        private void OnDrawGizmos()
        {
            if (_mesh == null)
            {
                GenerateMesh().Forget();
            }

            Gizmos.DrawMesh(_mesh, transform.position, transform.rotation, Vector3.one);
        }
#endif

        #endregion

        #region METHODS

        public void Initialize(TileData tileData)
        {
            _tileData = tileData;
            _meshRenderer.material.SetColor(ShaderColorProperty, _gridConfigurationsSource.GetBiomeColor(tileData.BiomeType));
        }

        private async UniTaskVoid GenerateMesh()
        {
            await UniTask.NextFrame();
            if (_meshFilter == null) return;
            
            _mesh = new Mesh();
            _meshFilter.mesh = _mesh;
            _mesh.name = "Hexagon";

            var vertices = new Vector3[SIDES + 1];
            var normals = new Vector3[SIDES + 1];
            var triangles = new int[SIDES * 3];

            vertices[0] = Vector3.zero;
            normals[0] = Vector3.up;

            var angleStep = 360f / SIDES;

            for (var i = 1; i <= SIDES; i++)
            {
                var angle = i * angleStep;
                var x = _radius * Mathf.Cos(angle * Mathf.Deg2Rad);
                var z = _radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                vertices[i] = new Vector3(z, 0, x);
                normals[i] = Vector3.up;

                var triangleIndex = (i - 1) * 3;
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = i;
                triangles[triangleIndex + 2] = (i % SIDES) + 1;
            }

            _mesh.vertices = vertices;
            _mesh.normals = normals;
            _mesh.triangles = triangles;
        }

        #endregion
    }
}
