#if UNITY_IOS || UNITY_ANDROID
using System;

namespace UnityEngine.Advertisements
{
    public interface IAdsSource
    {
        bool IsInitialized { get; }
        IObservable<bool> IsInitializedObservable { get; }
        void LoadRewardedAd(Action<AdLoadStatus> onLoad);
        void ShowRewardedAd(Action<AdShowStatus> onAdCompleted);
    }
}
#endif
