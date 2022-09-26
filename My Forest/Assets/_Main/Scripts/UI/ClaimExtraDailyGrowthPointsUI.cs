using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Threading;

using Zenject;
using UniRx;
using Cysharp.Threading.Tasks;

namespace MyForest
{
    public class ClaimExtraDailyGrowthPointsUI : AdButtonBase
    {
        #region FIELDS

        [Inject] private IGrowthDataSource _growthDataSource = null;
        [Inject] private IGrowthEventSource _growthEventSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private GameObject _buttonContainer = null;
        [SerializeField] private TimerUI _timerUI = null;

        private CancellationTokenSource cancellationTokenSource = null;

        #endregion

        #region UNITY

        protected override void Start()
        {
            base.Start();
            Initialize();
        }

        private void OnApplicationPause(bool paused)
        {
            if (paused) return;

            StartTimer();
        }

        #endregion

        #region METHOD

        private void Initialize()
        {
            _buttonContainer.SetActive(false);
            _growthDataSource.ClaimDailyGrowthAvailable.Subscribe(ClaimAvailableUpdated).AddTo(_disposables);
            _growthDataSource.ClaimDailyExtraGrowthAvailable.Subscribe(ExtraClaimAvailableUpdated).AddTo(_disposables);
            _timerUI.TimerCompleteObservable.Subscribe(OnTimerCompleted).AddTo(_disposables);
        }

        private void ClaimAvailableUpdated(bool available)
        {
            _buttonContainer.SetActive(!available);
        }

        private void ExtraClaimAvailableUpdated(bool available)
        {
            cancellationTokenSource = new CancellationTokenSource();
            ExtraClaimAvailableUpdatedAsync(available, cancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid ExtraClaimAvailableUpdatedAsync(bool available, CancellationToken cancellationToken)
        {
            if (available)
            {
                await EnableAfterAdLoadsAsync(cancellationToken);
            }
            else
            {
                StartTimer();
                DisableButton();
            }
        }

        private void StartTimer()
        {
            _timerUI.StartTimer(_growthDataSource.ExtraDailyGrowthSecondsLeft);
        }

        private void OnTimerCompleted()
        {
            EnableAfterAdLoadsAsync(cancellationTokenSource.Token).Forget();
        }

        private async UniTask EnableAfterAdLoadsAsync(CancellationToken cancellationToken)
        {
            await UniTask.WaitUntil(() => _adLoaded, cancellationToken: cancellationToken);

            if (cancellationToken.IsCancellationRequested) return;

            EnableButton();
        }

        protected override void OnAdLoaded()
        {
            // Override to avoid setting interactable as true.
        }

        protected override void OnAdCompleted()
        {
            _growthEventSource.ClaimExtraDailyGrowth();
        }

        protected override void OnAdLoadFailed(UnityAdsLoadError error, string message)
        {
            cancellationTokenSource?.Cancel();
        }

        #endregion
    }
}
