using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class TreeData
    {
        public int ID { get; private set; }
        public int CreationGrowth { get; private set; }
        public float SizeVariance { get; private set; }

        [JsonIgnore]
        public TreeConfiguration Configuration { get; private set; }

        [JsonConstructor]
        public TreeData(int id, int creationGrowth, float sizeVariance)
        {
            ID = id;
            CreationGrowth = creationGrowth;
            SizeVariance = sizeVariance;
        }

        public void Hydrate(ITreeConfigurationCollectionSource treeConfigurationCollectionSource)
        {
            Configuration = treeConfigurationCollectionSource.GetTreeConfiguration(ID);
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
