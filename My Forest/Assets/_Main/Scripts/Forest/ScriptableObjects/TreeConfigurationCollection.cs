using UnityEngine;
using System.Collections.Generic;
using System.Linq;

using Zenject;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(TreeConfigurationCollection), menuName = MENU_NAME)]
    public partial class TreeConfigurationCollection : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Forest/" + nameof(TreeConfigurationCollection);

        [Inject] private IForestConfigurationSource _forestConfigurationSource = null;

        [SerializeField] private TreeConfiguration[] _treeConfigurations = null;

        #endregion
    }

    public partial class TreeConfigurationCollection : ITreeConfigurationCollectionSource
    {
        IReadOnlyList<TreeConfiguration> ITreeConfigurationCollectionSource.GetAllElementConfigurations() => _treeConfigurations;

        TreeConfiguration ITreeConfigurationCollectionSource.GetTreeConfiguration(string treeID)
        {
            foreach (var elementConfiguration in _treeConfigurations)
            {
                if (elementConfiguration.ID == treeID)
                {
                    return elementConfiguration;
                }
            }

            return null;
        }

        TreeConfiguration ITreeConfigurationCollectionSource.GetRandomConfiguration()
        {
            var randomRarity = _forestConfigurationSource.GetRandomTreeRarity();

            IEnumerable<TreeConfiguration> configurationsWithRarity = null;
            do
            {
                configurationsWithRarity = _treeConfigurations.Where(c => c.Rarity == randomRarity);
                randomRarity--;
            }
            while (configurationsWithRarity == null);

            return configurationsWithRarity.Shuffle().ElementAt(Random.Range(default, configurationsWithRarity.Count() - 1));
        }
    }
}
