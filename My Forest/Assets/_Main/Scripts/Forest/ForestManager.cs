using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace MyForest
{
    public partial class ForestManager
    {
        #region FIELDS

        private const int MAX_TREE_ROTATION = 359;

        [Inject] private ITreeConfigurationCollectionSource _treeConfigurationCollectionSource = null;
        [Inject] private IGrowthDataSource _growthDataSource = null;
        [Inject] private IGridEventSource _gridEventSource = null;
        [Inject] private IGridPositioningSource _gridPositioningSource = null;

        private Subject<TreeData> _newTreeAddedSubject = new Subject<TreeData>();

        #endregion

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
            var newTile = _gridEventSource.CreateRandomTileForBiome(randomTreeConfiguration.Biome);

            var newTreeData = new TreeData
            (
                Data.TreeCount,
                randomTreeConfiguration.ID,
                _growthDataSource.GrowthData.CurrentGrowth,
                _gridPositioningSource.GetWorldPosition(newTile.Coordinates),
                Vector3.up * UnityEngine.Random.Range(default, MAX_TREE_ROTATION)
            );

            newTreeData.Hydrate(_treeConfigurationCollectionSource);
            Data.AddForestElement(newTreeData);
            Save();

            _newTreeAddedSubject.OnNext(newTreeData);
        }
    }

    public partial class ForestManager : DataManager<ForestData>
    {
        protected override string Key => Constants.Forest.FOREST_DATA_KEY;

        protected override void OnPreLoad(ref ForestData data)
        {
            if (data.IsEmpty)
            {
                data = _saveSource.LoadJSONFromResources<ForestData>(Constants.Forest.DEFAULT_FOREST_DATA_FILE);
            }

            foreach (var element in data.Trees)
            {
                element.Hydrate(_treeConfigurationCollectionSource);
            }
        }
    }

    public partial class ForestManager : IInitializable
    {
        void IInitializable.Initialize()
        {
            Load();

            _growthDataSource.GrowthEventsOcurredObservable.Subscribe(OnGrowthEventOcurred);
        }
    }

    public partial class ForestManager : IForestDataSource
    {
        ForestData IForestDataSource.ForestData => Data;
        IObservable<ForestData> IForestDataSource.ForestObservable => DataObservable;
        IObservable<TreeData> IForestDataSource.NewTreeAddedObservable => _newTreeAddedSubject.AsObservable();
    }
}
