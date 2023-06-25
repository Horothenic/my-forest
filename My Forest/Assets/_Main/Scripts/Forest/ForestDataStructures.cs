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
    public class TreeData
    {
        public int Id { get; private set; }
        public string TreeID { get; private set; }
        public int CreationGrowth { get; private set; }
        public SerializedVector3 Position { get; private set; }
        public SerializedVector3 Rotation { get; private set; }

        [JsonIgnore]
        public TreeConfiguration Configuration { get; private set; }

        [JsonConstructor]
        public TreeData(int id, string treeID, int creationGrowth, Vector3 position, Vector3 rotation)
        {
            Id = id;
            TreeID = treeID;
            CreationGrowth = creationGrowth;
            Position = position;
            Rotation = rotation;
        }

        public void Hydrate(ITreeConfigurationCollectionSource elementConfigurationsSource)
        {
            Configuration = elementConfigurationsSource.GetTreeConfiguration(TreeID);
        }
    }

    public enum TreeRarity
    {
        Common,
        Rare,
        Exquisite,
        Endangered
    }
}
