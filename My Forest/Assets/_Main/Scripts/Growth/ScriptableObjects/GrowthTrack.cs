using System.Collections.Generic;
using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(GrowthTrack), menuName = MENU_NAME)]
    public partial class GrowthTrack : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Growth/" + nameof(GrowthTrack);

        [Header("CONFIGURAITIONS")]
        [SerializeField] private List<GrowthTrackRecurringEvent> _growthTrackRecurrentEvents = null;
        [SerializeField] private List<GrowthTrackEvent> _growthTrackPinPointEvents = null;

        #endregion
    }

    public partial class GrowthTrack : IGrowthTrackSource
    {
        IReadOnlyList<GrowthTrackRecurringEvent> IGrowthTrackSource.AllRecurrentEvents => _growthTrackRecurrentEvents;

        IReadOnlyList<GrowthTrackEvent> IGrowthTrackSource.AllPinPointEvents => _growthTrackPinPointEvents;
    }
}
