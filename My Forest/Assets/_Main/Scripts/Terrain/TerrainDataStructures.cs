using System;
using UnityEngine;

namespace MyForest
{
    [Serializable]
    public struct PerlinNoiseConfiguration
    {
        [SerializeField] private float _scale;
        [SerializeField] private int _octaves;
        [SerializeField][Range(0f, 1f)] private float _persistance;
        [SerializeField][Range(1f, 10f)] private float _lacunarity;
        
        public float Scale => _scale;
        public int Octaves => _octaves;
        public float Persistance => _persistance;
        public float Lacunarity => _lacunarity;
    }

    [Serializable]
    public class Spline
    {
        [SerializeField] private AnimationCurve _map;
        [SerializeField] private Vector2 _evaluationRange;
        
        public float Evaluate(float t)
        {
            return _map.Evaluate(Mathf.Lerp(_evaluationRange.x, _evaluationRange.y, t));
        }
    }
}
