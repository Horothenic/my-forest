using DelaunatorSharp;
using DelaunatorSharp.Unity.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MyIsland
{
    public class IslandGenerator : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IIslandSource _islandSource;

        private const string BASE_SEED_PHRASE = "My Forest ";
        
        [Header("POINTS")]
        [SerializeField] private float _radius = 100;
        [SerializeField] private float _minimumDistance = 2;
        [SerializeField] private int _maxAttempts = 30;
        [SerializeField] private float _falloffThreshold = 100;
        [SerializeField] private float _extraHeight = 1;
        [SerializeField] private int _borderPoints = 500;
        [SerializeField] private AnimationCurve _borderCurve;
        [SerializeField] private float _borderHeight = 1;
        
        [Header("PERLIN NOISE")]
        [SerializeField] [Range(1, 15)] private int _octaves = 6;
        [SerializeField] [Range(0f, 1f)] private float _persistence = 0.274f;
        [SerializeField] [Range(1f, 10f)] private float _lacunarity = 2.85f;
        [SerializeField] [Range(1f, 3000f)] private float _heightScale = 200;
        [SerializeField] [Range(-10f, 20f)] private float _bottomReductionFactor = 9;
        [SerializeField] [Range(0.1f, 5f)] private float _topIncreaseFactor = 1f;
        [SerializeField] [Range(5f, 300f)] private float _scale = 50;
        [SerializeField] [Range(0.001f, 1.00f)] private float _dampening = 0.144f;
        
        [Header("PERSONALIZATION")]
        [SerializeField] private Gradient _heightColorGradient;
        [SerializeField] private Gradient _borderColorGradient;

        private MeshFilter _meshFilter;
        private MeshCollider _meshCollider;
        
        private Dictionary<IPoint, float> _heightMap;
        private float _seedOffset = 0;
        private float _minNoiseHeight;
        private float _maxNoiseHeight;
        
        #endregion
        
        #region METHODS

        private void Start()
        {
            Initialize(_islandSource.CreatorName);
        }

        public void Initialize(string seed)
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();
            
            _minNoiseHeight = float.PositiveInfinity;
            _maxNoiseHeight = float.NegativeInfinity;
            
            _seedOffset = RandomExtensions.GenerateFloatFromPhrase(BASE_SEED_PHRASE + seed);
            
            var poissonSample = PoissonDiskSampler.Circle.GeneratePoints(_radius, _minimumDistance, _maxAttempts, _borderPoints);
            var poissonSamplePoints = poissonSample.ToPoints();
            CalculateElevations(poissonSamplePoints);
            
            var delaunayTriangulation = new Delaunator(poissonSamplePoints);
            var mesh = CreateMesh(delaunayTriangulation);
            
            _meshFilter.mesh = mesh;
            _meshCollider.sharedMesh = mesh;
        }

        private void CalculateElevations(IPoint[] points)
        {   
            _heightMap = new Dictionary<IPoint, float>();
            
            foreach (var point in points)
            {
                var amplitude = 1f;
                var frequency = 1f;
                var noiseHeight = 0f;

                for (var o = 0; o < _octaves; o++)
                {
                    var xValue = (float)point.X / _scale * frequency;
                    var yValue = (float)point.Y / _scale * frequency;

                    var perlinValue = Mathf.PerlinNoise(xValue + _seedOffset, yValue + _seedOffset) * 2 - 1;
                    perlinValue *= _dampening;

                    noiseHeight += perlinValue * amplitude;

                    amplitude *= _persistence;
                    frequency *= _lacunarity;
                }

                if (noiseHeight > _maxNoiseHeight)
                {
                    _maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < _minNoiseHeight)
                {
                    _minNoiseHeight = noiseHeight;
                }

                var pointDistance = GetDistance(point);
                var falloffProximityFactor = GetFalloffProximityFactor(pointDistance);
                
                noiseHeight = (noiseHeight < 0f) ? noiseHeight * _heightScale / _bottomReductionFactor : noiseHeight * _heightScale * _topIncreaseFactor;
                noiseHeight += _extraHeight;
                noiseHeight *= falloffProximityFactor;

                if (falloffProximityFactor < 1)
                {
                    var borderProximity = GetBorderProximity(pointDistance);
                    noiseHeight += _borderCurve.Evaluate(borderProximity) * _borderHeight;
                }
                
                _heightMap.Add(point, noiseHeight);
            }
        }
        
        private float GetFalloffProximityFactor(float distance)
        {
            if (distance <= _falloffThreshold) return 1f;
            
            return 1f - Mathf.Clamp01((distance - _falloffThreshold) / (_radius - _falloffThreshold));
        }
        
        private float GetBorderProximity(float distance)
        {
            return Mathf.InverseLerp(_falloffThreshold, _radius, distance);
        }
        
        private float GetDistance(IPoint point)
        {
            return Mathf.Sqrt((float)(point.X.Squared() + point.Y.Squared()));
        }
        
        private Color GetTriangleColor(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            var height = (v0.y + v1.y + v2.y) / 3f;
            height -= _extraHeight;
            height = (height < 0f) ? height / _heightScale * _bottomReductionFactor : height / _heightScale * _topIncreaseFactor;
            
            var gradientVal = Mathf.InverseLerp(_minNoiseHeight, _maxNoiseHeight, height);
            
            var center = (v0 + v1 + v2) / 3f;
            var pointDistance = GetDistance(new Point(center.x, center.z));
            var falloffProximityFactor = GetFalloffProximityFactor(pointDistance);

            return falloffProximityFactor < 1 ? _borderColorGradient.Evaluate(gradientVal) : _heightColorGradient.Evaluate(gradientVal);
        }
        
        private Vector3 GetPointPosition(IPoint point)
        {
            return new Vector3((float)point.X, _heightMap[point], (float)point.Y);
        }

        private Mesh CreateMesh(Delaunator delaunayTriangulation)
        {
            var vertices = new List<Vector3>();
            var colors = new List<Color>();
            var normals = new List<Vector3>();
            var uvs = new List<Vector2>();
            var triangles = new List<int>();
            
            // First we recreate the triangles cause the originals will not allow us to color them properly.
            foreach (var triangle in delaunayTriangulation.GetTriangles())
            {
                var points = triangle.Points.ToArray();
                
                var v0 = GetPointPosition(points[0]);
                var v1 = GetPointPosition(points[1]);
                var v2 = GetPointPosition(points[2]);
            
                triangles.Add(vertices.Count);
                triangles.Add(vertices.Count + 1);
                triangles.Add(vertices.Count + 2);
            
                vertices.Add(v0);
                vertices.Add(v1);
                vertices.Add(v2);
                
                var normal = Vector3.Cross(v1 - v0, v2 - v0);
                Repeater.Repeat(3, () => normals.Add(normal));
            }
            
            // Then with we use the elevated vertices for the color calculations.
            for (var i = 0; i < vertices.Count; i += 3)
            {
                var v0 = vertices[i];
                var v1 = vertices[i + 1];
                var v2 = vertices[i + 2];
                
                var triangleColor = GetTriangleColor(v0, v1, v2);
                
                Repeater.Repeat(3, () => uvs.Add(Vector2.zero));
                Repeater.Repeat(3, () => colors.Add(triangleColor));
            }
            
            _heightMap.Clear();
            _heightMap = null;
            
            var mesh = new Mesh
            {
                vertices = vertices.ToArray(),
                triangles = triangles.ToArray(),
                colors = colors.ToArray(),
                normals = normals.ToArray(),
                uv = uvs.ToArray()
            };

            return mesh;
        }

        #endregion
    }
}
