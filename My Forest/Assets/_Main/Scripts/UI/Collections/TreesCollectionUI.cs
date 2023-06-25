using UnityEngine;

using Zenject;

namespace MyForest.UI
{
    public class TreesCollectionUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ITreeConfigurationCollectionSource _forestElementConfigurationsSource = null;

        [Header("COMPONENTS")]
        [SerializeField]
        private CollectionContainerUI _collectionContainer = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private GameObject _treeCollectionElementUIPrefab = null;

        #endregion

        #region UNITY

        private void Start()
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
