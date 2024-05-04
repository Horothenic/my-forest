using UnityEngine;

using TMPro;
using UniRx;
using Zenject;

namespace MyForest
{
    public class GrowthCounterUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IGrowthDataSource _growthDataSource;

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _text;

        #endregion

        #region METHODS

        private void Start()
        {
            _growthDataSource.GrowthChangedObservable.Subscribe(Refresh).AddTo(this);
            Refresh(_growthDataSource.GrowthData);
        }

        private void Refresh(GrowthData growthData)
        {
            _text.text = growthData.CurrentGrowth.ToString();
        }

        #endregion
    }
}
