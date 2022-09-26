using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

using Zenject;
using UniRx;

namespace MyForest
{
    public abstract class AdButtonBase : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        #region FIELDS

        [Inject] private IAdsDataSource _adsDataSource = null;

        [Header("ADS CONFIGURATIONS")]
        [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
        [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";

        [Header("ADS COMPONENTS")]
        [SerializeField] protected Button _showAdButton = null;

        protected CompositeDisposable _disposables = new CompositeDisposable();

        private string _adUnitId = null;
        protected bool _adLoaded = false;

        #endregion

        #region UNITY

        protected virtual void Awake()
        {
#if UNITY_IOS
            _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

            _showAdButton.interactable = false;
        }

        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void OnDestroy()
        {
            _disposables.Dispose();
        }

        #endregion

        #region METHOD

        private void Initialize()
        {
            OnInitialize();
            _showAdButton.onClick.AddListener(ShowAd);
            _adsDataSource.InitializedObservable.Subscribe(OnInitialized).AddTo(_disposables);
        }

        private void OnInitialized(bool initialized)
        {
            if (!initialized) return;

            Advertisement.Load(_adUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            if (!adUnitId.Equals(_adUnitId)) return;

            _adLoaded = true;
            OnAdLoaded();
        }

        public void ShowAd()
        {
            Advertisement.Show(_adUnitId, this);
            _adLoaded = false;
            OnAdShown();
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (!adUnitId.Equals(_adUnitId)) return;

            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    Advertisement.Load(_adUnitId, this);
                    OnAdCompleted();
                    break;
                case UnityAdsShowCompletionState.SKIPPED:
                    OnAdSkipped();
                    break;
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            _adLoaded = false;
            OnAdLoadFailed(error, message);
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            OnAdShowFailed(error, message);
        }

        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }

        protected void EnableButton()
        {
            _showAdButton.interactable = true;
        }

        protected void DisableButton()
        {
            _showAdButton.interactable = false;
        }

        #endregion

        #region ABSTRACT & VIRTUAL

        protected abstract void OnAdCompleted();

        protected virtual void OnAdLoaded()
        {
            EnableButton();
        }

        protected virtual void OnInitialize()
        {
            DisableButton();
        }

        protected virtual void OnAdShown()
        {
            DisableButton();
        }

        protected virtual void OnAdSkipped() { }

        protected virtual void OnAdLoadFailed(UnityAdsLoadError error, string message) { }
        protected virtual void OnAdShowFailed(UnityAdsShowError error, string message) { }

        #endregion
    }
}
