using System.Collections.Generic;
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
        
        private readonly Dictionary<Coordinates, HexagonTile> _tilesMap = new Dictionary<Coordinates, HexagonTile>();

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
            _forestDataSource.ForestElementChangedObservable.Subscribe(data => OnForestElementChanged(data)).AddTo(this);

            BuildForest(_forestDataSource?.ForestData);
        }

        private void BuildForest(ForestData forestData)
        {
            if (forestData == null) return;

            DeleteForest();
            for (var i = 0; i < forestData.ForestElementsCount; i++)
            {
                var forestDataElement = forestData.ForestElements[i];
                OnForestElementChanged(forestDataElement, false);
            }
        }

        private void DeleteForest()
        {
            var childCount = _root.childCount;
            for (var i = childCount - 1; i >= 0; i--)
            {
                Destroy(_root.GetChild(i).gameObject);
            }
        }

        private void OnForestElementChanged(ForestElementData forestElementData, bool withEntryAnimation = true)
        {
            if (!_tilesMap.TryGetValue(forestElementData.TileData.Coordinates, out var tile))
            {
                tile = _gridServiceSource.CreateTile(_root, forestElementData.TileData);
                _tilesMap.Add(forestElementData.TileData.Coordinates, tile);
            }

            if (forestElementData.TreeData != null)
            {
                _treesServiceSource.CreateTree(tile.transform, forestElementData.TreeData, forestElementData.TileData.Height, withEntryAnimation);
            }
            else if (forestElementData.DecorationData != null)
            {
                _decorationsServiceSource.CreateDecoration(tile.transform, forestElementData.DecorationData, forestElementData.TileData.Height, withEntryAnimation);
            }
        }

        #endregion
    }
}
