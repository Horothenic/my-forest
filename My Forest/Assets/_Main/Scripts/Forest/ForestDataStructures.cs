using UnityEngine;
using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class ForestData
    {
        public int Length { get; private set; }
        public string[] ElementPrefabs { get; private set; }
        public Vector3[] ElementPositions { get; private set; }
        public Vector3[] ElementRotations { get; private set; }

        public ForestData() { }

        [JsonConstructor]
        public ForestData(string[] elementPrefabs, Vector3[] elementPositions, Vector3[] elementRotations)
        {
            Length = elementPrefabs.Length;
            ElementPrefabs = elementPrefabs;
            ElementPositions = elementPositions;
            ElementRotations = elementRotations;
        }
    }

    public enum ForestElementType
    {
        Bush,
        DeadTree,
        Ground,
        PalmTree,
        PineTree,
        Plant,
        Rock,
        SimpleTree
    }
}
