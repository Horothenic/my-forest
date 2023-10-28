using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public class ForestController : MonoBehaviour
    {
        #region FIELDS
        
        [Inject] private IForestDataSource _forestDataSource = null;
        [Inject] private IGridServiceSource _gridServiceSource = null;
        [Inject] private ITreesServiceSource _treesServiceSource = null;
        [Inject] private IDecorationsServiceSource _decorationsServiceSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform _root = null;

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
            _forestDataSource.ForestPostLoadObservable.Subscribe(BuildForest).AddTo(this);
            _forestDataSource.NewForestElementAddedObservable.Subscribe(CreateNewForestElement).AddTo(this);

            BuildForest(_forestDataSource?.ForestData);
        }

        private void BuildForest(ForestData forestData)
        {
            if (forestData == null) return;
            
            for (var i = 0; i < forestData.ForestElementsCount; i++)
            {
                var forestDataElement = forestData.ForestElements[i];
                CreateForestElement(forestDataElement, false);
            }
        }

        private void CreateNewForestElement(ForestElementData forestElementData)
        {
            CreateForestElement(forestElementData);
        }

        private void CreateForestElement(ForestElementData forestElementData, bool withEntryAnimation = true)
        {
            var tile = _gridServiceSource.CreateTile(_root, forestElementData.TileData);

            if (forestElementData.TreeData != null)
            {
                _treesServiceSource.CreateTree(tile.transform, forestElementData.TreeData, withEntryAnimation);
            }
            else if (forestElementData.DecorationData != null)
            {
                _decorationsServiceSource.CreateDecoration(tile.transform, forestElementData.DecorationData, withEntryAnimation);
            }
        }

        #endregion
    }
}
