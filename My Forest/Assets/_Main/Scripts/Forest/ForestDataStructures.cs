using UnityEngine;
using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class ForestData
    {
        private List<ForestElementData> _forestElements = new List<ForestElementData>();

        public IReadOnlyList<ForestElementData> ForestElements => _forestElements;

        [JsonIgnore]
        public int ForestElementsCount => _forestElements.Count;
        [JsonIgnore]
        public bool IsEmpty => _forestElements.Count == default;

        public ForestData() { }

        [JsonConstructor]
        public ForestData(List<ForestElementData> forestElements)
        {
            _forestElements = forestElements;
        }

        public void AddForestElement(ForestElementData newForestElement)
        {
            _forestElements.Add(newForestElement);
        }
    }

    [Serializable]
    public class ForestElementData
    {
        public int ID { get; private set; }
        public Coordinates Coordinates { get; private set; }
        
        [JsonIgnore]
        public bool IsEmpty => TreeData == null && DecorationData == null;
        
        public TreeData TreeData { get; private set; }
        public DecorationData DecorationData { get; private set; }
        
        public ForestElementData(int id, Coordinates coordinates)
        {
            ID = id;
            Coordinates = coordinates;
        }
        
        [JsonConstructor]
        public ForestElementData(int id, Coordinates coordinates, TreeData treeData, DecorationData decorationData) : this(id, coordinates)
        {
            TreeData = treeData;
            DecorationData = decorationData;
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

    public enum Biome
    {
        Forest,
        Desert,
        Mountain,
        Swamp,
        Lake,
        Tundra
    }

    public enum Rarity
    {
        Common,
        Rare,
        Exquisite,
        Endangered
    }
}
