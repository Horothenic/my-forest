using UnityEngine;
using UnityEngine.UI;
using System;

using Zenject;
using UniRx;
using EasyMobile;

namespace MyForest.UI
{
    public class ClaimExtraDailyGrowthPointsUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IGrowthDataSource _growthDataSource = null;
        [Inject] private IGrowthEventSource _growthEventSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private Button _adButton = null;
        [SerializeField] private GameObject _buttonContainer = null;
        [SerializeField] private TimerUI _timerUI = null;

        private CompositeDisposable _disposables = new CompositeDisposable();
        private bool _loaded = false;

        #endregion

        #region UNITY

        [Obsolete]
        private void Start()
        {
            Initialize();
        }

        [Obsolete]
        private void OnDestroy()
        {
            Clean();
        }

        private void OnApplicationPause(bool paused)
        {
            if (paused || !_loaded) return;

            StartTimer();
        }

        #endregion

        #region METHOD

        [Obsolete]
        private void Initialize()
        {
            _buttonContainer.SetActive(false);
            _adButton.onClick.AddListener(ShowAd);

            _growthDataSource.ClaimDailyGrowthAvailable.Subscribe(ClaimAvailableUpdated).AddTo(_disposables);
            _growthDataSource.ClaimDailyExtraGrowthAvailable.Subscribe(ExtraClaimAvailableUpdated).AddTo(_disposables);
            _timerUI.TimerCompleteObservable.Subscribe(OnTimerCompleted).AddTo(_disposables);

            Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;

            _loaded = true;
        }

        [Obsolete]
        private void Clean()
        {
            Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
        }

        private void ClaimAvailableUpdated(bool available)
        {
            _buttonContainer.SetActive(!available);
        }

        private void ExtraClaimAvailableUpdated(bool available)
        {
            if (available)
            {
                EnableButton();
            }
            else
            {
                StartTimer();
                DisableButton();
            }
        }

        private void ShowAd()
        {
            if (Advertising.IsRewardedAdReady())
            {
                Advertising.ShowRewardedAd();
            }
        }

        private void StartTimer()
        {
            _timerUI.StartTimer(_growthDataSource.ExtraDailyGrowthSecondsLeft);
        }

        private void OnTimerCompleted()
        {
            EnableButton();
        }

        private void EnableButton()
        {
            _adButton.interactable = true;
        }

        private void DisableButton()
        {
            _adButton.interactable = false;
        }

        [System.Obsolete]
        private void RewardedAdCompletedHandler(RewardedAdNetwork network, AdLocation location)
        {
            _growthEventSource.ClaimExtraDailyGrowth();
        }

        #endregion
    }
}
