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
        public TileData TileData { get; private set; }
        
        [JsonIgnore]
        public Biome Biome => TileData.Biome;
        
        [JsonIgnore]
        public bool IsEmpty => TreeData == null && DecorationData == null;
        
        public TreeData TreeData { get; private set; }
        public DecorationData DecorationData { get; private set; }
        
        public ForestElementData(int id, TileData tileData)
        {
            ID = id;
            TileData = tileData;
        }
        
        [JsonConstructor]
        public ForestElementData(int id, TileData tileData, TreeData treeData, DecorationData decorationData) : this(id, tileData)
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

        public class ByDistanceFromOrigin : IComparer<ForestElementData>
        {
            public int Compare(ForestElementData x, ForestElementData y)
            {
                if (x == null && y == null) return 0;
                if (x == null) return -1;
                if (y == null) return 1;
                
                var xCoordinates = x.TileData.Coordinates;
                var yCoordinates = y.TileData.Coordinates;
                
                // TODO: [OPTIMIZATION] We can cache these calculations since we use them very often for the Ordered List.
                var xDistance = (Math.Abs(xCoordinates.Q) + Math.Abs(xCoordinates.R) + Math.Abs(xCoordinates.Q + xCoordinates.R)) / 2;
                var yDistance = (Math.Abs(yCoordinates.Q) + Math.Abs(yCoordinates.R) + Math.Abs(yCoordinates.Q + yCoordinates.R)) / 2;

                if (xDistance < yDistance) return -1;
                if (xDistance > yDistance) return 1;

                // If distances are equal, compare by Q or R
                if (xCoordinates.Q < yCoordinates.Q) return -1;
                if (xCoordinates.Q > yCoordinates.Q) return 1;
            
                // If Q values are equal, compare by R
                if (xCoordinates.R < yCoordinates.R) return -1;
                if (xCoordinates.R > yCoordinates.R) return 1;
            
                return 0; // Both coordinates are the same
            }
        }
    }

    public enum Biome
    {
        Forest,
        Desert,
        Mountain
    }
}
