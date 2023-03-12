using UnityEngine;

using Zenject;

namespace MyForest
{
    public class TreesCollectionUI : MonoBehaviour
    {
        #region FIELDS
        
        [Inject] private IForestElementConfigurationsSource _forestElementConfigurationsSource = null;

        [Header("COMPONENTS")] [SerializeField]
        private CollectionContainerUI _collectionContainer = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private TreeCollectionElementUI _treeCollectionElementUIPrefab = null;

        #endregion

        #region UNITY

        private void Awake()
        {
            Initialize();
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            _collectionContainer.Initialize(_treeCollectionElementUIPrefab, _forestElementConfigurationsSource.GetAllElementConfigurations());
        }

        #endregion
    }
}
