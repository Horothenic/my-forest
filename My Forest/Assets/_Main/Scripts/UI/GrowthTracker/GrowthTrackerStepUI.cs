using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using Zenject;
using TMPro;

namespace MyForest
{
    public class GrowthTrackerStepUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IGrowthConfigurationsSource _growthConfigurations = null;

        [Header("COMPONENTS")]
        [SerializeField] private Transform _iconContainer;

        [Header("CONFIGURATIONS")]
        [SerializeField] private TextMeshProUGUI _iconPrefab;

        private readonly List<TextMeshProUGUI> _icons = new List<TextMeshProUGUI>();

        #endregion

        #region METHODS

        public void Initialize(IReadOnlyList<(IGrowthTrackEvent growthTackEvent, int growth)> growthTrackEvents)
        {
            if (growthTrackEvents == null || growthTrackEvents.Count == 0)
            {
                ResetStep();
            }

            EnsureCapacity(growthTrackEvents.Count);

            for (var i = 0; i < _icons.Count; i++)
            {
                var icon = _icons[i];

                if (i < growthTrackEvents.Count)
                {
                    icon.text = _growthConfigurations.GetIcon(growthTrackEvents[i].growthTackEvent.EventType);
                    icon.gameObject.SetActive(true);
                }
                else
                {
                    icon.gameObject.SetActive(false);
                }
            }
        }

        private void ResetStep()
        {
            foreach (var icon in _icons)
            {
                icon.gameObject.SetActive(false);
            }
        }

        private void EnsureCapacity(int amount)
        {
            while (amount > _icons.Count)
            {
                var icon = Instantiate(_iconPrefab, _iconContainer);
                _icons.Add(icon);
            }
        }

        #endregion
    }
}
