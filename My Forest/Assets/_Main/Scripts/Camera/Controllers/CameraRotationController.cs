using UnityEngine;
using System;

using Zenject;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UniRx;

namespace MyForest
{
    public class CameraRotationController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraRotationSource _cameraEventsSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform _cameraContainer = null;
        [SerializeField] private int _rotationAngles = 90;
        [SerializeField] private float _rotationDuration = 1f;
        [SerializeField] private Ease _rotationEase = Ease.OutCirc;

        private bool _enableRotation = true;

        #endregion

        #region UNITY

        private void Awake()
        {
            _cameraEventsSource.RotatedLeftObservable.Subscribe(RotateLeft).AddTo(this);
            _cameraEventsSource.RotatedRightObservable.Subscribe(RotateRight).AddTo(this);
        }

        #endregion

        #region METHODS

        private void RotateLeft()
        {
            if (!_enableRotation) return;

            Rotate(_rotationAngles);
        }

        private void RotateRight()
        {
            if (!_enableRotation) return;

            Rotate(-_rotationAngles);
        }

        private void Rotate(int angles)
        {
            _cameraEventsSource.ChangeCameraAngle(angles);
            _cameraContainer.DORotate(Vector3.up * angles, _rotationDuration).SetRelative().SetEase(_rotationEase);
            WaitEnableRotation().Forget();
        }

        private async UniTaskVoid WaitEnableRotation()
        {
            _enableRotation = false;
            await UniTask.Delay(TimeSpan.FromSeconds(_rotationDuration));
            _enableRotation = true;
        }

        #endregion
    }
}
