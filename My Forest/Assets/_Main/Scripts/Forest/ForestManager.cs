using System;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using Zenject;

namespace MyForest
{
    public partial class ForestManager
    {
        #region FIELDS
        
        [Inject] private IGrowthDataSource _growthDataSource = null;
        [Inject] private IGridServiceSource _gridServiceSource = null;
        [Inject] private ITreesServiceSource _treesServiceSource = null;
        [Inject] private IDecorationsServiceSource _decorationsServiceSource = null;

        private readonly Subject<ForestElementData> _newForestElementAddedSubject = new Subject<ForestElementData>();

        #endregion

        #region METHODS
        
        private void OnGrowthEventOccurred(IReadOnlyList<(IGrowthTrackEvent growthTrackEvent, int growth)> growthTrackEvents)
        {
            foreach (var growthTrackEvent in growthTrackEvents)
            {
                switch (growthTrackEvent.growthTrackEvent.EventType)
                {
                    case GrowthTrackEventType.NewTile:
                        AddNewRandomTile();
                        break;
                    case GrowthTrackEventType.NewTree:
                        AddNewRandomTree(growthTrackEvent.growth);
                        break;
                    case GrowthTrackEventType.NewDecoration:
                        AddNewRandomDecoration();
                        break;
                }
            }
        }

        private ForestElementData CreateNewForestElementData()
        {
            return new ForestElementData
            (
                Data.ForestElementsCount,
                _gridServiceSource.GetRandomTileDataForBiome(EnumExtensions.Random<Biome>())
            );
        }

        private void OnNewForestElementData(ForestElementData newForestElementData)
        {
            Data.AddForestElement(newForestElementData);
            Save();

            _newForestElementAddedSubject.OnNext(newForestElementData);
        }

        private void AddNewRandomTile()
        {
            var newForestElementData = CreateNewForestElementData();
            OnNewForestElementData(newForestElementData);
        }

        private void AddNewRandomTree(int growth)
        {
            var newForestElementData = CreateNewForestElementData();
            newForestElementData.SetTreeData(_treesServiceSource.GetRandomTreeDataForBiome(newForestElementData.Biome, growth));
            
            OnNewForestElementData(newForestElementData);
        }

        private void AddNewRandomDecoration()
        {
            var newForestElementData = CreateNewForestElementData();
            newForestElementData.SetDecorationData(_decorationsServiceSource.GetRandomDecorationDataForBiome(newForestElementData.Biome));
            
            OnNewForestElementData(newForestElementData);
        }
        
        #endregion
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
        }

        protected override void OnPostLoad(ForestData data)
        {
            _growthDataSource.GrowthEventsOccurredObservable.Subscribe(OnGrowthEventOccurred).AddTo(_disposables);
        }
    }

    public partial class ForestManager : IForestDataSource
    {
        ForestData IForestDataSource.ForestData => Data;
        IObservable<ForestData> IForestDataSource.ForestPreLoadObservable => PreLoadObservable;
        IObservable<ForestData> IForestDataSource.ForestPostLoadObservable => PostLoadObservable;
        IObservable<ForestElementData> IForestDataSource.NewForestElementAddedObservable => _newForestElementAddedSubject.AsObservable();
    }
}
