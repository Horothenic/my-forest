using UnityEngine;

using Zenject;

namespace MyForest
{
    public partial class TreesManager
    {
        [Inject] private ITreeConfigurationCollectionSource _treeConfigurationCollectionSource = null;
        [Inject] private ITileConfigurationsSource _tileConfigurationsSource = null;
        [Inject] private IObjectPoolSource _objectPoolSource = null;
    }

    public partial class TreesManager : ITreesServiceSource
    {
        Tree ITreesServiceSource.CreateTree(Transform parent, TreeData treeData, float height, bool withEntryAnimation)
        {
            if (treeData.Configuration == null)
            {
                treeData.Hydrate(_treeConfigurationCollectionSource);
            }

            var newTree = _objectPoolSource.Borrow(_treeConfigurationCollectionSource.TreePrefab);
            newTree.gameObject.SetLocal(Vector3.up * height * _tileConfigurationsSource.TileRealHeight, parent);
            newTree.Initialize(treeData, withEntryAnimation);
            return newTree;
        }
        
        TreeData ITreesServiceSource.GetRandomTreeDataForBiome(Biome biome, int growth)
        {
            var randomTreeConfiguration = _treeConfigurationCollectionSource.GetRandomConfigurationForBiome(biome);

            return new TreeData
            (
                randomTreeConfiguration.ID,
                Random.Range(0, Constants.ForestElements.MAX_ROTATION),
                growth,
                Random.Range(randomTreeConfiguration.MinSizeVariance, randomTreeConfiguration.MaxSizeVariance),
                randomTreeConfiguration
            );
        }
    }
}
