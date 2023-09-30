using System;

namespace MyForest
{
    public interface IAdsSource
    {
        bool IsInitialized { get; }
        IObservable<bool> IsInitializedObservable { get; }
        void LoadRewardedAd(Action<AdLoadStatus> onLoad);
        void ShowRewardedAd(Action<AdShowStatus> onAdCompleted);
    }
}
