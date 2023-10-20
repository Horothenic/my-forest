using UnityEngine;
using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class ForestData
    {
        private List<TreeData> _trees = new List<TreeData>();

        public IReadOnlyList<TreeData> Trees => _trees;

        [JsonIgnore]
        public int TreeCount => _trees.Count;
        [JsonIgnore]
        public bool IsEmpty => _trees.Count == default;

        public ForestData() { }

        [JsonConstructor]
        public ForestData(List<TreeData> trees)
        {
            _trees = trees;
        }

        public void AddForestElement(TreeData newForestElement)
        {
            _trees.Add(newForestElement);
        }
    }

    [Serializable]
    public class ForestElementData
    {
        public int ID { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public float Rotation { get; private set; }
        
        public TreeData TreeData { get; private set; }
        public DecorationData DecorationData { get; private set; }
        
        [JsonConstructor]
        public ForestElementData(int id, Coordinates coordinates, float rotation)
        {
            ID = id;
            Coordinates = coordinates;
            Rotation = rotation;
        }
        
        public void SetTreeData(TreeData treeData)
        {
            TreeData = treeData;
        }
        
        public void SetDecorationData(DecorationData decorationData)
        {
            DecorationData = decorationData;
        }
    }
}
