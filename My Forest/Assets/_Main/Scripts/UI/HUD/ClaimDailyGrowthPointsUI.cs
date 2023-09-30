using UnityEngine;
using UnityEngine.UI;

using UniRx;
using Zenject;

namespace MyForest.UI
{
    public class ClaimDailyGrowthPointsUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IGrowthDataSource _growthDataSource = null;
        [Inject] private IGrowthEventSource _growthEventSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private GameObject _buttonContainer = null;
        [SerializeField] private Button _claimButton = null;

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
            _claimButton.onClick.AddListener(_growthEventSource.ClaimDailyGrowth);
            _growthDataSource.ClaimDailyGrowthAvailable.Subscribe(SetClaimAvailableState).AddTo(this);

            SetClaimAvailableState(_growthDataSource.GrowthData.IsDailyClaimAvailable());
        }

        private void SetClaimAvailableState(bool available)
        {
            _buttonContainer.SetActive(available);
        }

        #endregion
    }
}
