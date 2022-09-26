using System;
using UnityEngine.Advertisements;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class AdsManager
    {
        #region FIELDS

        private const string ANDROID_GAME_ID = "4945153";
        private const string IOS_GAME_ID = "4945152";
        private const bool TEST_MODE_ENABLED = true;

        private Subject<bool> _initializedSubject = new Subject<bool>();

        #endregion

        #region METHODS

        private void Initialize()
        {
#if UNITY_IOS
            Advertisement.Initialize(IOS_GAME_ID, TEST_MODE_ENABLED, this);
#elif UNITY_ANDROID
            Advertisement.Initialize(ANDROID_GAME_ID, TEST_MODE_ENABLED, this);
#endif
        }

        #endregion
    }

    public partial class AdsManager : IInitializable
    {
        void IInitializable.Initialize() => Initialize();
    }

    public partial class AdsManager : IUnityAdsInitializationListener
    {
        public void OnInitializationComplete()
        {
            UnityEngine.Debug.Log("Unity Ads initialization complete.");
            _initializedSubject.OnNext(true);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            UnityEngine.Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
            _initializedSubject.OnNext(false);
        }
    }

    public partial class AdsManager : IAdsDataSource
    {
        IObservable<bool> IAdsDataSource.InitializedObservable => _initializedSubject.AsObservable();
    }
}
