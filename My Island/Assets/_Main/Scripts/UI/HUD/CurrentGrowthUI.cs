using TMPro;
using UnityEngine;
using Zenject;
using UniRx;

namespace MyIsland
{
    public class CurrentGrowthUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IGrowthSource _growthSource;

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _currentGrowthText;
        [SerializeField] private TextMeshProUGUI _allTimeGrowthText;

        #endregion

        #region METHODS

        private void Start()
        {
            _growthSource.DataObservables.Subscribe(Refresh).AddTo(this);
            Refresh(_growthSource.Data);
        }

        private void Refresh(GrowthData data)
        {
            _currentGrowthText.text = data.CurrentGrowth.ToString();
            _allTimeGrowthText.text = data.AllTimeGrowth.ToString();
        }

        #endregion
    }
}
