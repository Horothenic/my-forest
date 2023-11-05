using UnityEngine;

using Zenject;

namespace MyForest
{
    public partial class DecorationsManager
    {
        [Inject] private IDecorationsConfigurationCollectionSource _decorationsConfigurationCollectionSource = null;
        [Inject] private IGridConfigurationsSource _gridConfigurationsSource = null;
        [Inject] private IObjectPoolSource _objectPoolSource = null;
    }

    public partial class DecorationsManager : IDecorationsServiceSource
    {
        Decoration IDecorationsServiceSource.CreateDecoration(Transform parent, DecorationData decorationData, int height, bool withEntryAnimation)
        {
            if (decorationData.Configuration == null)
            {
                decorationData.Hydrate(_decorationsConfigurationCollectionSource);
            }

            var newDecoration = _objectPoolSource.Borrow(_decorationsConfigurationCollectionSource.DecorationPrefab);
            newDecoration.gameObject.SetLocal(Vector3.up * height * _gridConfigurationsSource.TileRealHeight, parent);
            newDecoration.Initialize(decorationData, withEntryAnimation);
            return newDecoration;
        }
        
        DecorationData IDecorationsServiceSource.GetRandomDecorationDataForBiome(Biome biome)
        {
            var randomDecorationConfiguration = _decorationsConfigurationCollectionSource.GetRandomConfigurationForBiome(biome);

            return new DecorationData
            (
                randomDecorationConfiguration.ID,
                Random.Range(0, Constants.ForestElements.MAX_ROTATION),
                randomDecorationConfiguration
            );
        }
    }
}
