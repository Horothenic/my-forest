using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace MyForest
{
    [Serializable]
    public struct PerlinNoiseConfiguration
    {
        [SerializeField] private float _smoothness;
        [SerializeField] private int _octaves;
        [SerializeField][Range(0f, 1f)] private float _persistance;
        [SerializeField][Range(1f, 10f)] private float _lacunarity;
        
        public float Smoothness => _smoothness;
        public int Octaves => _octaves;
        public float Persistance => _persistance;
        public float Lacunarity => _lacunarity;
    }
}
