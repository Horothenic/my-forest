using System.Collections.Generic;
using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(TreeCollection), menuName = MENU_NAME)]
    public partial class TreeCollection : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Forest/" + nameof(TreeCollection);

        [SerializeField] private TreeConfiguration[] _treeConfigurations = null;

        #endregion

        #region METHODS

        private TreeConfiguration GetElementConfiguration(string treeID)
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

        #endregion
    }

    public partial class TreeCollection : ITreeCollectionSource
    {
        TreeConfiguration ITreeCollectionSource.GetTreeConfiguration(string treeID) => GetElementConfiguration(treeID);
        IReadOnlyList<TreeConfiguration> ITreeCollectionSource.GetAllElementConfigurations() => _treeConfigurations;
    }
}
