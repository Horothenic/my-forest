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
        [Inject] private ITileServiceSource _tileServiceSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform _root = null;
        
        private readonly Dictionary<Coordinates, Tile> _tilesMap = new Dictionary<Coordinates, Tile>();

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
            if (!_tilesMap.TryGetValue(forestElementData.Coordinates, out var tile))
            {
                tile = _tileServiceSource.CreateTile(_root, forestElementData.Coordinates);
                _tilesMap.Add(forestElementData.Coordinates, tile);
            }
        }

        #endregion
    }
}
