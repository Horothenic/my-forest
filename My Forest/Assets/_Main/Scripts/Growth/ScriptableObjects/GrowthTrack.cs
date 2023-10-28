using UnityEngine;
using System.Collections.Generic;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(GrowthTrack), menuName = MENU_NAME)]
    public partial class GrowthTrack : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Growth/" + nameof(GrowthTrack);

        [SerializeField] private List<GrowthTrackRecurringEvent> _growthTrackRecurrentEvents = null;
        [SerializeField] private List<GrowthTrackEvent> _growthTrackPinPointEvents = null;

        #endregion
    }

    public partial class GrowthTrack : IGrowthTrackSource
    {
        public IReadOnlyList<(IGrowthTrackEvent growthTackEvent, int growth)> GetEventsForGrowth(int currentGrowth)
        {
            return GetEventsForGrowth(currentGrowth, currentGrowth);
        }
        
        public IReadOnlyList<(IGrowthTrackEvent growthTackEvent, int growth)> GetEventsForGrowth(int previousGrowth, int currentGrowth)
        {
            var list = new List<(IGrowthTrackEvent growthTackEvent, int growth)>();

            for (var growth = previousGrowth; growth <= currentGrowth; growth++)
            {
                foreach (var growthTrackEvent in _growthTrackPinPointEvents)
                {
                    if (growthTrackEvent.GrowthThreshold == growth)
                    {
                        list.Add((growthTrackEvent, growth));
                    }
                }

                foreach (var growthTrackEvent in _growthTrackRecurrentEvents)
                {
                    if (growth >= growthTrackEvent.GrowthToStart && growth % growthTrackEvent.GrowthInterval == default)
                    {
                        list.Add((growthTrackEvent, growth));
                    }
                }
            }

            return list;
        }
    }
}
