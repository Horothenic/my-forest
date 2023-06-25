using UnityEngine;
using System.Collections.Generic;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class ForestController : MonoBehaviour
    {
        #region FIELDS

        private const int MAX_ROTATION = 359;

        [Inject] private IObjectPoolSource _objectPoolSource = null;
        [Inject] private IForestDataSource _forestDataSource = null;
        [Inject] private IForestEventSource _forestEventSource = null;
        [Inject] private ITreeConfigurationCollectionSource _treeConfigurationCollectionSource = null;
        [Inject] private IGrowthDataSource _growthDataSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform _root = null;
        [SerializeField] private Tree _treePrefab = null;

        private readonly List<Tree> _trees = new List<Tree>();

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
            _forestDataSource.ForestObservable.Subscribe(BuildForest).AddTo(this);
            _growthDataSource.GrowthEventsOcurredObservable.Subscribe(OnGrowthEventOcurred).AddTo(this);
        }

        private void BuildForest(ForestData forestData)
        {
            foreach (var tree in _trees)
            {
                _objectPoolSource.Return(tree.gameObject);
            }
            _trees.Clear();

            for (int i = 0; i < forestData.TreeCount; i++)
            {
                var treeData = forestData.Trees[i];
                _trees.Add(CreateTree(treeData));
            }
        }

        private Tree CreateTree(TreeData treeData)
        {
            var newForestElement = _objectPoolSource.Borrow(_treePrefab);
            newForestElement.gameObject.Set(treeData.Position, _root);
            newForestElement.Initialize(treeData);
            return newForestElement;
        }

        private void OnGrowthEventOcurred(IReadOnlyList<IGrowthTrackEvent> growthTrackEvents)
        {
            foreach (var growthTrackEvent in growthTrackEvents)
            {
                if (growthTrackEvent.EventType != GrowthTrackEventType.NewTree) continue;

                AddNewRandomTree();
            }
        }

        private void AddNewRandomTree()
        {
            var randomTreeConfiguration = _treeConfigurationCollectionSource.GetRandomConfiguration();

            var newTreeData = new TreeData
            (
                _forestDataSource.ForestData.TreeCount,
                randomTreeConfiguration.ID,
                _growthDataSource.GrowthData.CurrentGrowth,
                Vector3.zero, // TODO: Get valid position based on hexagons
                Vector3.up * Random.Range(default, MAX_ROTATION)
            );

            _forestEventSource.AddNewTree(newTreeData);
        }

        #endregion
    }
}
