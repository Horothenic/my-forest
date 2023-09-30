using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

using Zenject;
using UniRx;
using Cysharp.Threading.Tasks;
using TMPro;

namespace MyForest.UI
{
    public class ClaimExtraDailyGrowthPointsUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IGrowthDataSource _growthDataSource = null;
        [Inject] private IGrowthEventSource _growthEventSource = null;
        [Inject] private IAdsSource _adsSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private GameObject _buttonContainer = null;
        [SerializeField] private TextMeshProUGUI _timerText = null;
        [SerializeField] private Button _adButton = null;
        
        #endregion

        #region UNITY

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region METHOD

        private void Initialize()
        {
            DisableAll();
            
            _adButton.onClick.AddListener(ShowAd);

            _growthDataSource.ClaimDailyGrowthAvailable.Subscribe(IsClaimAvailable).AddTo(this);
            _growthDataSource.DailyExtraGrowthTimer.TimeLeftObservable.Subscribe(OnTimerInterval).AddTo(this);
            _adsSource.IsInitializedObservable.Subscribe(OnAdsInitialized).AddTo(this);

            IsClaimAvailable(_growthDataSource.GrowthData.IsDailyClaimAvailable());
        }
        
        private void OnTimerInterval(TimeSpan timeLeft)
        {
            var secondsLeft = (long)timeLeft.TotalSeconds;
            
            var hours = secondsLeft / 3600;
            var minutes = secondsLeft / 60;
            var seconds = secondsLeft % 60;

            var format = hours == 0 ? Constants.Formats.MINUTE_TIMER_FORMAT : Constants.Formats.HOUR_TIMER_FORMAT;

            _timerText.text = string.Format(format, seconds, minutes, hours);
        }

        private void IsClaimAvailable(bool available)
        {
            _buttonContainer.SetActive(!available);
        }
        
        private void OnAdsInitialized(bool initialized)
        {
            if (!initialized) return;

            _growthDataSource.ClaimDailyExtraGrowthAvailable.Subscribe(ExtraClaimAvailableUpdated).AddTo(this);
            
            ExtraClaimAvailableUpdated(_growthDataSource.GrowthData.IsDailyExtraClaimAvailable());
        }

        private void ExtraClaimAvailableUpdated(bool available)
        {
            if (available)
            {
                LoadAd();
            }
            else
            {
                DisableButton();
            }
        }

        private void LoadAd()
        {
            _adsSource.LoadRewardedAd(OnAdLoaded);
        }
        
        private void OnAdLoaded(AdLoadStatus adLoadStatus)
        {
            switch (adLoadStatus)
            {
                case AdLoadStatus.Loaded:
                    EnableButton();
                    break;
                case AdLoadStatus.Failed:
                case AdLoadStatus.NotInitialized:
                    DisableButton();
                    break;
            }
        }

        private void ShowAd()
        {
            _adsSource.ShowRewardedAd(OnShowAd);
        }

        private void OnShowAd(AdShowStatus adShowStatus)
        {
            switch (adShowStatus)
            {
                case AdShowStatus.Skipped:
                    LoadAd();
                    break;
                case AdShowStatus.Completed:
                    _growthEventSource.ClaimExtraDailyGrowth();
                    break;
                case AdShowStatus.Failed:
                case AdShowStatus.NotInitialized:
                case AdShowStatus.Unknown:
                    LoadAd();
                    break;
            }
        }

        private void EnableButton()
        {
            _timerText.gameObject.TurnOff();
            _adButton.interactable = true;
        }

        private void DisableButton()
        {
            _timerText.gameObject.TurnOn();
            _adButton.interactable = false;
        }

        private void DisableAll()
        {
            _timerText.gameObject.TurnOff();
            _adButton.interactable = false;
        }

        #endregion
    }
}
