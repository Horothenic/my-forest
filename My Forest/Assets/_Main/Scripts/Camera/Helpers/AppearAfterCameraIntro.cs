using UnityEngine;
using System;

using Zenject;
using UniRx;
using Cysharp.Threading.Tasks;

namespace MyForest
{
    public class AppearAfterCameraIntro : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraIntroSource _cameraIntroSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private float _delay = default;

        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region UNITY

        private void Start()
        {
            gameObject.SetActive(false);
            _cameraIntroSource.IntroEndedObservable.Subscribe(() => OnIntroFinished().Forget()).AddTo(_disposables);
        }

        #endregion

        #region METHODS

        private async UniTask OnIntroFinished()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delay));
            gameObject.SetActive(true);
        }

        #endregion
    }
}
