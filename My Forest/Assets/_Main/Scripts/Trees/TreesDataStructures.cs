using System;

using Newtonsoft.Json;
using UnityEngine;

namespace MyForest
{
    [Serializable]
    public class TreeData
    {
        public string ID { get; private set; }
        public int CreationGrowth { get; private set; }
        public float SizeVariance { get; private set; }
        public float Rotation { get; private set; }

        [JsonIgnore]
        public TreeConfiguration Configuration { get; private set; }

        [JsonConstructor]
        public TreeData(string id, float rotation, int creationGrowth, float sizeVariance)
        {
            ID = id;
            Rotation = rotation;
            CreationGrowth = creationGrowth;
            SizeVariance = sizeVariance;
        }
        
        public TreeData(string id, float rotation, int creationGrowth, float sizeVariance, TreeConfiguration configuration) : this(id, rotation, creationGrowth, sizeVariance)
        {
            Configuration = configuration;
        }

        public void Hydrate(ITreeConfigurationCollectionSource treeConfigurationCollectionSource)
        {
            Configuration = treeConfigurationCollectionSource.GetConfiguration(ID);
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
