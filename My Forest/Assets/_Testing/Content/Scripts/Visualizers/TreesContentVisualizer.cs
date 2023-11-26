using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyForest.Testing
{
    [ExecuteInEditMode]
    public class TreesContentVisualizer : ContentVisualizer<TreeConfiguration>
    {
        protected override string GetContentDataID(TreeConfiguration configuration)
        {
            return configuration.ID;
        }
        
        protected override Dictionary<Biome, List<TreeConfiguration>> GetConfigurationsMap(List<TreeConfiguration> configurations)
        {
            var dataMap = new Dictionary<Biome, List<TreeConfiguration>>();
            foreach (var configuration in configurations)
            {
                if (dataMap.TryGetValue(configuration.Biome, out var value))
                {
                    value.Add(configuration);
                }
                else
                {
                    dataMap.Add(configuration.Biome, new List<TreeConfiguration> {configuration});
                }
            }

            return dataMap;
        }
        
        protected override void RefreshTreeTest(ContentTestData<TreeConfiguration> contentTestData)
        {
            for (var i = 0; i <= contentTestData.Data.MaxLevel; i++)
            {
                var treePrefab = contentTestData.Data.GetConfigurationLevel(i).Prefab;
                
                var tree = (GameObject)PrefabUtility.InstantiatePrefab(treePrefab, contentTestData.Origin);
                tree.transform.position = contentTestData.Position + Vector3.forward * i * Separation.y;
            }
        }
    }
}
