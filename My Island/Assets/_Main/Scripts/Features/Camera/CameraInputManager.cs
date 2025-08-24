using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace MyIsland
{
    public partial class CameraInputManager : MonoBehaviour
    {
        private const float DETECTION_THRESHOLD = 0.01f;
        
        [Header("CONFIGURATIONS")]
        [SerializeField] private float _panFactor = 1;
        [SerializeField] private float _rotationFactor = 1;

        private Finger _firstFinger;
        private Finger _secondFinger;
        private float _lastAngle;
        private CameraInputActions _controls;

        private void Awake()
        {
            _controls = new CameraInputActions();
        }

        private void OnEnable()
        {
            _controls.Enable();
            EnhancedTouchSupport.Enable();

            EnhancedTouch.onFingerDown += HandleFingerDown;
            EnhancedTouch.onFingerUp += HandleFingerUp;
        }

        private void OnDisable()
        {
            _controls.Disable();
            EnhancedTouchSupport.Disable();

            EnhancedTouch.onFingerDown -= HandleFingerDown;
            EnhancedTouch.onFingerUp -= HandleFingerUp;
        }

        private void Update()
        {
            CheckTouch();
            CheckInputActions();
        }

        private void CheckTouch()
        {
            switch (CurrentGesture)
            {
                case GestureType.Moving:
                    if (_firstFinger is { isActive: true })
                    {
                        var delta = _firstFinger.currentTouch.delta;
                        _onPan.OnNext(delta);
                    }
                    break;

                case GestureType.Rotating:
                    if (_firstFinger != null && _secondFinger != null && _firstFinger.isActive && _secondFinger.isActive)
                    {
                        var p1 = _firstFinger.screenPosition;
                        var p2 = _secondFinger.screenPosition;
                        var angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * Mathf.Rad2Deg;

                        var deltaAngle = Mathf.DeltaAngle(_lastAngle, angle);
                        if (Mathf.Abs(deltaAngle) > DETECTION_THRESHOLD)
                            _onRotate.OnNext(deltaAngle);

                        _lastAngle = angle;
                    }
                    break;
            }
        }

        private void CheckInputActions()
        {
            var panInput = _controls.Camera.Pan.ReadValue<Vector2>();
            if (panInput.sqrMagnitude > DETECTION_THRESHOLD)
            {
                _onPan.OnNext(panInput * _panFactor);
                SetGesture(GestureType.Moving);
            }

            var rotateInput = _controls.Camera.Rotate.ReadValue<Vector2>();
            if (Mathf.Abs(rotateInput.x) > DETECTION_THRESHOLD)
            {
                _onRotate.OnNext(rotateInput.x * _rotationFactor);
                SetGesture(GestureType.Rotating);
            }
            
            if (panInput.sqrMagnitude < DETECTION_THRESHOLD && Mathf.Abs(rotateInput.x) < DETECTION_THRESHOLD && _firstFinger == null && _secondFinger == null)
            {
                SetGesture(GestureType.None);
            }
        }

        private void HandleFingerDown(Finger finger)
        {
            if (_firstFinger == null)
            {
                _firstFinger = finger;
                SetGesture(GestureType.Moving);
            }
            else if (_secondFinger == null)
            {
                _secondFinger = finger;
                _lastAngle = GetCurrentAngle();
                SetGesture(GestureType.Rotating);
            }
        }

        private void HandleFingerUp(Finger finger)
        {
            if (finger == _secondFinger)
            {
                _secondFinger = null;
                SetGesture(_firstFinger != null ? GestureType.Moving : GestureType.None);
            }
            else if (finger == _firstFinger)
            {
                _firstFinger = _secondFinger;
                _secondFinger = null;
                SetGesture(_firstFinger != null ? GestureType.Moving : GestureType.None);
            }
        }

        private float GetCurrentAngle()
        {
            if (_firstFinger == null || _secondFinger == null) return 0f;
            
            var p1 = _firstFinger.screenPosition;
            var p2 = _secondFinger.screenPosition;
            return Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * Mathf.Rad2Deg;
        }

        private void SetGesture(GestureType newGesture)
        {
            if (CurrentGesture == newGesture) return;
            
            CurrentGesture = newGesture;
            _onGestureChanged.OnNext(newGesture);
        }
    }

    public partial class CameraInputManager : ICameraInputSource
    {
        private readonly Subject<GestureType> _onGestureChanged = new Subject<GestureType>();
        private readonly Subject<Vector2> _onPan = new Subject<Vector2>();
        private readonly Subject<float> _onRotate = new Subject<float>();
        
        public GestureType CurrentGesture { get; private set; } = GestureType.None;
        IObservable<GestureType> ICameraInputSource.OnGestureChanged => _onGestureChanged.AsObservable();
        
        IObservable<Vector2> ICameraInputSource.OnPan => _onPan.AsObservable();
        IObservable<float> ICameraInputSource.OnRotate => _onRotate.AsObservable();
    }
}
