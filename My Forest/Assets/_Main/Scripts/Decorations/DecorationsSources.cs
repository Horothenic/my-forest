using UnityEngine;

namespace MyForest
{
    public interface IDecorationsServiceSource
    {
        Decoration CreateDecoration(Transform parent, DecorationData treeData, int height, bool withEntryAnimation);
        DecorationData GetRandomDecorationDataForBiome(Biome biome);
    }

    public interface IDecorationsConfigurationCollectionSource
    {
        Decoration DecorationPrefab { get; }
        DecorationConfiguration GetConfiguration(string decorationID);
        DecorationConfiguration GetRandomConfigurationForBiome(Biome biome);
    }
}
