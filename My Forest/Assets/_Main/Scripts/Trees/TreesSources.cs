using UnityEngine;

namespace MyForest
{
    public interface ITreesServiceSource
    {
        Tree CreateTree(Transform parent, TreeData treeData, bool withEntryAnimation);
        TreeData GetRandomTreeDataForBiome(Biome biome, int growth);
    }

    public interface ITreeConfigurationCollectionSource
    {
        Tree TreePrefab { get; }
        TreeConfiguration GetConfiguration(string treeID);
        TreeConfiguration GetRandomConfigurationForBiome(Biome biome);
    }
}
