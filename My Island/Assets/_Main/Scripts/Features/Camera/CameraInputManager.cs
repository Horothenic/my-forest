using System;
using UniRx;
using UnityEngine;

namespace MyIsland
{
    public partial class CameraInputManager : MonoBehaviour
    {
        private const float DETECTION_THRESHOLD = 0.01f;
        
        [Header("CONFIGURATIONS")]
        [SerializeField] private float _panFactor = 1;
        [SerializeField] private float _rotationFactor = 1;
        
        private CameraInputActions _controls;

        private void Awake()
        {
            _controls = new CameraInputActions();
            _controls.Camera.Enable();
        }
        
        private void OnDestroy()
        {
            _controls.Camera.Disable();
        }

        private void Update()
        {
            CheckInputActions();
        }

        private void CheckInputActions()
        {
            var panInput = _controls.Camera.Pan.ReadValue<float>();
            if (Mathf.Abs(panInput) > DETECTION_THRESHOLD)
            {
                _onPan.OnNext(panInput * (_panFactor * Time.deltaTime));
            }

            var rotateInput = _controls.Camera.Rotate.ReadValue<float>();
            if (Mathf.Abs(rotateInput) > DETECTION_THRESHOLD)
            {
                _onRotate.OnNext(rotateInput * _rotationFactor * Time.deltaTime);
            }
        }
    }

    public partial class CameraInputManager : ICameraInputSource
    {
        private readonly Subject<float> _onPan = new Subject<float>();
        private readonly Subject<float> _onRotate = new Subject<float>();
        
        IObservable<float> ICameraInputSource.OnPan => _onPan.AsObservable();
        IObservable<float> ICameraInputSource.OnRotate => _onRotate.AsObservable();
    }
}
