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
        IReadOnlyList<IGrowthTrackEvent> IGrowthTrackSource.GetEventsForGrowth(int growth)
        {
            var list = new List<IGrowthTrackEvent>();

            foreach (var growthTrackEvent in _growthTrackPinPointEvents)
            {
                if (growthTrackEvent.DayForEvent == growth)
                {
                    list.Add(growthTrackEvent);
                }
            }

            foreach (var growthTrackEvent in _growthTrackRecurrentEvents)
            {
                if (growth >= growthTrackEvent.StartDay && growth % growthTrackEvent.DaysInterval == default)
                {
                    list.Add(growthTrackEvent);
                }
            }

            return list;
        }
    }
}
