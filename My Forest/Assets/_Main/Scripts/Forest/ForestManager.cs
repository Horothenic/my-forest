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

        private readonly Subject<ForestElementData> _forestElementChangedSubject = new Subject<ForestElementData>();

        private readonly OrderedList<ForestElementData> _emptyElementsByDistanceFromOrigin = new OrderedList<ForestElementData>(new ForestElementData.ByDistanceFromOrigin());

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
                _gridServiceSource.CreateRandomTileDataForBiome(EnumExtensions.Random<Biome>())
            );
        }

        private ForestElementData GetEmptyForestElementData()
        {
            if (_emptyElementsByDistanceFromOrigin.Count == 0)
            {
                return CreateNewForestElementData();
            }
            
            var minLimit = Mathf.FloorToInt(_emptyElementsByDistanceFromOrigin.Count * Constants.Forest.GET_EMPTY_TILE_OUTER_THRESHOLD);
            var maxLimit = _emptyElementsByDistanceFromOrigin.Count;
            
            return _emptyElementsByDistanceFromOrigin.PopAt(UnityEngine.Random.Range(minLimit, maxLimit));
        }

        private void OnForestElementDataChanged(ForestElementData newForestElementData)
        {
            Save();
            _forestElementChangedSubject.OnNext(newForestElementData);
        }

        private void AddNewRandomTile()
        {
            var newForestElementData = CreateNewForestElementData();
                
            _emptyElementsByDistanceFromOrigin.Add(newForestElementData);
            Data.AddForestElement(newForestElementData);
            
            OnForestElementDataChanged(newForestElementData);
        }

        private void AddNewRandomTree(int growth)
        {
            var forestElementData = GetEmptyForestElementData();
            forestElementData.SetTreeData(_treesServiceSource.GetRandomTreeDataForBiome(forestElementData.Biome, growth));
            
            OnForestElementDataChanged(forestElementData);
        }

        private void AddNewRandomDecoration()
        {
            var forestElementData = GetEmptyForestElementData();
            forestElementData.SetDecorationData(_decorationsServiceSource.GetRandomDecorationDataForBiome(forestElementData.Biome));
            
            OnForestElementDataChanged(forestElementData);
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

            foreach (var forestElementData in data.ForestElements)
            {
                if (forestElementData.IsEmpty) continue;
                
                _emptyElementsByDistanceFromOrigin.Add(forestElementData);
            }
        }
    }

    public partial class ForestManager : IForestDataSource
    {
        ForestData IForestDataSource.ForestData => Data;
        IObservable<ForestData> IForestDataSource.ForestPreLoadObservable => PreLoadObservable;
        IObservable<ForestData> IForestDataSource.ForestPostLoadObservable => PostLoadObservable;
        IObservable<ForestElementData> IForestDataSource.ForestElementChangedObservable => _forestElementChangedSubject.AsObservable();
    }
}
