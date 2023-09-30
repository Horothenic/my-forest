using System;
using System.Collections.Generic;
using UnityEngine.Advertisements;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class AdsManager
    {
        #region FIELDS

        private const bool TEST_MODE_ENABLED = true;
        
#if UNITY_IOS
        private const string GAME_ID = "4945152";
        private const string REWARDED_AD_PLACEMENT_ID = "Rewarded_iOS";
#elif UNITY_ANDROID
        private const string GAME_ID = "4945153";
        private const string REWARDED_AD_PLACEMENT_ID = "Rewarded_Android";
#endif

        private readonly DataSubject<bool> _initializedSubject = new DataSubject<bool>();

        private readonly Dictionary<string, Action<AdLoadStatus>> _onAdLoadedMap = new Dictionary<string, Action<AdLoadStatus>>();
        private readonly Dictionary<string, Action<AdShowStatus>> _onAdShowMap = new Dictionary<string, Action<AdShowStatus>>();

        #endregion
    }

    public partial class AdsManager : IInitializable
    {
        void IInitializable.Initialize()
        {
            Advertisement.Initialize(GAME_ID, TEST_MODE_ENABLED, this);
        }
    }

    public partial class AdsManager : IUnityAdsInitializationListener
    {
        void IUnityAdsInitializationListener.OnInitializationComplete()
        {
            UnityEngine.Debug.Log("Unity Ads initialization complete.");
            _initializedSubject.OnNext(true);
        }

        void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            UnityEngine.Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
            _initializedSubject.OnNext(false);
        }
    }

    public partial class AdsManager : IUnityAdsLoadListener
    {
        void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
        {
            if (_onAdLoadedMap.TryGetValue(placementId, out var onAdLoaded))
            {
                onAdLoaded?.Invoke(AdLoadStatus.Loaded);
                _onAdLoadedMap.Remove(placementId);
                return;
            }
            
            UnityEngine.Debug.LogWarning("[ADS] Loaded ad without a listener.");
        }

        void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            if (_onAdLoadedMap.TryGetValue(placementId, out var onAdLoaded))
            {
                onAdLoaded?.Invoke(AdLoadStatus.Failed);
                _onAdLoadedMap.Remove(placementId);
                return;
            }
            
            UnityEngine.Debug.LogWarning("[ADS] Failed Loaded ad without a listener.");
        }
    }

    public partial class AdsManager : IUnityAdsShowListener
    {
        void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            if (_onAdShowMap.TryGetValue(placementId, out var onAdShow))
            {
                onAdShow?.Invoke(AdShowStatus.Failed);
                _onAdShowMap.Remove(placementId);
                return;
            }
            
            UnityEngine.Debug.LogWarning("[ADS] Failed Show ad without a listener.");
        }

        void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
        {
            if (_onAdShowMap.TryGetValue(placementId, out var onAdShow))
            {
                onAdShow?.Invoke(AdShowStatus.Started);
                return;
            }
            
            UnityEngine.Debug.LogWarning("[ADS] Show Start ad without a listener.");
        }

        void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId)
        {
            if (_onAdShowMap.TryGetValue(placementId, out var onAdShow))
            {
                onAdShow?.Invoke(AdShowStatus.Clicked);
                return;
            }
            
            UnityEngine.Debug.LogWarning("[ADS] Show Click ad without a listener.");
        }

        void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            var status = showCompletionState switch
            {
                UnityAdsShowCompletionState.COMPLETED => AdShowStatus.Completed,
                UnityAdsShowCompletionState.SKIPPED => AdShowStatus.Skipped,
                _ => AdShowStatus.Unknown
            };
            
            if (_onAdShowMap.TryGetValue(placementId, out var onAdShow))
            {
                onAdShow?.Invoke(status);
                _onAdShowMap.Remove(placementId);
                return;
            }
            
            UnityEngine.Debug.LogWarning("[ADS] Completed ad without a listener.");
        }
    }

    public partial class AdsManager : IAdsSource
    {
        bool IAdsSource.IsInitialized => _initializedSubject.Value;
        IObservable<bool> IAdsSource.IsInitializedObservable => _initializedSubject.AsObservable();

        void IAdsSource.LoadRewardedAd(Action<AdLoadStatus> onAdLoaded)
        {
            if (!_initializedSubject.Value)
            {
                onAdLoaded?.Invoke(AdLoadStatus.NotInitialized);
                return;
            }

            if (_onAdLoadedMap.TryGetValue(REWARDED_AD_PLACEMENT_ID, out var foundOnAdLoaded))
            {
                foundOnAdLoaded?.Invoke(AdLoadStatus.Pending);
                return;
            }
            
            _onAdLoadedMap.Add(REWARDED_AD_PLACEMENT_ID, onAdLoaded);
            Advertisement.Load(REWARDED_AD_PLACEMENT_ID, this);
        }

        void IAdsSource.ShowRewardedAd(Action<AdShowStatus> onAdShow)
        {
            if (!_initializedSubject.Value)
            {
                onAdShow?.Invoke(AdShowStatus.NotInitialized);
                return;
            }

            if (_onAdShowMap.TryGetValue(REWARDED_AD_PLACEMENT_ID, out var foundOnAdCompleted))
            {
                foundOnAdCompleted?.Invoke(AdShowStatus.Pending);
                return;
            }
            
            _onAdShowMap.Add(REWARDED_AD_PLACEMENT_ID, onAdShow);
            Advertisement.Show(REWARDED_AD_PLACEMENT_ID, this);
        }
    }
}
