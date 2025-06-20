using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyIsland.Testing
{
    [ExecuteInEditMode]
    public class DecorationsContentVisualizer : ContentVisualizer<DecorationConfiguration>
    {
        protected override string GetContentDataID(DecorationConfiguration configuration)
        {
            return configuration.ID;
        }
        
        protected override Dictionary<Biome, List<DecorationConfiguration>> GetConfigurationsMap(List<DecorationConfiguration> configurations)
        {
            var dataMap = new Dictionary<Biome, List<DecorationConfiguration>>();
            foreach (var configuration in configurations)
            {
                if (dataMap.TryGetValue(configuration.Biome, out var value))
                {
                    value.Add(configuration);
                }
                else
                {
                    dataMap.Add(configuration.Biome, new List<DecorationConfiguration> {configuration});
                }
            }

            return dataMap;
        }
        
        protected override void RefreshTreeTest(ContentTestData<DecorationConfiguration> contentTestData)
        {
            for (var i = 0; i <= contentTestData.Data.AmountOfVariations; i++)
            {
                var decorationPrefab = contentTestData.Data.GetVariation(i);
                
                var decoration = (GameObject)PrefabUtility.InstantiatePrefab(decorationPrefab, contentTestData.Origin);
                decoration.transform.position = contentTestData.Position + Vector3.forward * i * Separation.y;
            }
        }
    }
}
