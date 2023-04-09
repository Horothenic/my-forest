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

        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region UNITY

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            _claimButton.onClick.AddListener(_growthEventSource.ClaimDailyGrowth);
            _growthDataSource.ClaimDailyGrowthAvailable.Subscribe(SetClaimAvailableState).AddTo(_disposables);
        }

        private void SetClaimAvailableState(bool isAvailable)
        {
            _buttonContainer.SetActive(isAvailable);
        }

        #endregion
    }
}
