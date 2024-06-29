using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using Zenject;
using UniRx;

namespace MyForest
{
    public class CameraGesturesController : MonoBehaviour
    {
        #region FIELDS

        private const int FIRST_TOUCH_INDEX = 0;
        private const int SECOND_TOUCH_INDEX = 1;
        private const float PINCH_GESTURE_THRESHOLD = 10f;

        private const float DESKTOP_SHORT_ZOOM_INCREASE = 1f;
        private const float DESKTOP_ZOOM_INCREASE = 4f;
        private const float DESKTOP_SHORT_ROTATION_INCREASE = 5f;
        private const float DESKTOP_ROTATION_INCREASE = 25f;
        private const float DESKTOP_SHORT_ANGLE_INCREASE = 2f;
        private const float DESKTOP_ANGLE_INCREASE = 5f;
        
        [Inject] private ICameraGesturesDataSource _cameraGesturesDataSource = null;
        [Inject] private ICameraGesturesControlSource _cameraGesturesControlSource = null;
        [Inject] private ICameraIntroSource _cameraIntroSource = null;
        
        [Header("COMPONENTS")]
        [SerializeField] private Transform _cameraMoveContainer;
        [SerializeField] private Transform _cameraZoomContainer;
        [SerializeField] private Transform _cameraRotationContainer;
        [SerializeField] private Transform _cameraAngleContainer;

        [Header("DRAG CONFIGURATIONS")]
        [SerializeField] private Spline _dragStrengthBasedOnZoom;

        [Header("ZOOM CONFIGURATIONS")]
        [SerializeField] private float _minZoom = 2;
        [SerializeField] private float _maxZoom = 8;
        [SerializeField] private float _zoomSensitivity = 1.6f;
        
        [Header("ROTATION CONFIGURATIONS")]
        [SerializeField] private float _rotationSensitivity = 1f;

        private GestureType _currentGesture = GestureType.None;
        private bool _inputEnabled = true;
        
        private Vector2 _dragPreviousPosition;
        private Vector2 _dragNextPosition;
        private Vector2 _currentDragPosition;
        private Vector2 _minDragLimits;
        private Vector2 _maxDragLimits;
        private float? _firstDistanceBetweenTouches;
        
        private float _currentZoom;
        private float _zoomOnStartPinch;
        private Tween _zoomTween;
        
        private float _currentRotation;
        
        private float _currentAngle;

        private float CurrentZoomPercentage => Mathf.InverseLerp(_minZoom, _maxZoom, _currentZoom);

        private enum GestureType
        {
            None,
            Move,
            Rotate,
            Angle
        }

        #endregion

        #region UNITY

        private void Start()
        {
            SetupCamera();
            BlockInput();
            
            _cameraGesturesControlSource.EnableInputObservable.Subscribe(EnableInput).AddTo(this);
            _cameraGesturesControlSource.BlockInputObservable.Subscribe(BlockInput).AddTo(this);
            _cameraGesturesControlSource.UpdateDragLimitsObservable.Subscribe(UpdateDragLimits).AddTo(this);

            _cameraGesturesDataSource.ZoomObservable.Subscribe(RefreshZoom).AddTo(this);
            _cameraGesturesDataSource.PositionObservable.Subscribe(RefreshPosition).AddTo(this);
            _cameraGesturesDataSource.RotationObservable.Subscribe(RefreshRotation).AddTo(this);
            _cameraGesturesDataSource.AngleObservable.Subscribe(RefreshAngle).AddTo(this);
            
            _cameraIntroSource.IntroStartedObservable.Subscribe(SetupCamera).AddTo(this);
        }

        private void Update()
        {
            CheckInput();
        }

        #endregion

        #region METHODS

        private void SetupCamera()
        {
            SetStoredPosition();
            SetStoredRotation();
            SetStoredZoom();
            SetStoredAngle();
        }

        private void CheckInput()
        {
            if (!_inputEnabled) return;
            
#if UNITY_EDITOR
            MoveDesktop();
            ZoomDesktop();
            RotateDesktop();
            AngleDesktop();
#else
            if (Input.touchCount == 0) return;

            _currentGesture = SelectGestureType();

            switch (_currentGesture)
            {
                case GestureType.Drag:
                    DragTouch();
                    break;
                case GestureType.Pinch:
                    PinchTouch();
                    break;
            }
#endif
        }

        private void EnableInput()
        {
            _inputEnabled = true;
        }

        private void BlockInput()
        {
            _inputEnabled = false;
        }

        #endregion

        #region TOUCH

#if !UNITY_EDITOR
        private GestureType SelectGestureType()
        {
            if (Input.touchCount == 1)
            {
                if (_currentGesture == GestureType.Rotate)
                {
                    _firstDistanceBetweenTouches = null;
                    _dragPreviousPosition = Input.GetTouch(FIRST_TOUCH_INDEX).position;
                }

                return GestureType.Move;
            }

            if (Input.touchCount == 2)
            {
                if (_currentGesture == GestureType.Rotate)
                {
                    return GestureType.Rotate;
                }

                if (_firstDistanceBetweenTouches == null)
                {
                    _firstDistanceBetweenTouches = Vector2.Distance(Input.GetTouch(FIRST_TOUCH_INDEX).position, Input.GetTouch(SECOND_TOUCH_INDEX).position);
                }

                var distanceBetweenTouches = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

                if (Mathf.Abs(distanceBetweenTouches - (float)_firstDistanceBetweenTouches) > PINCH_GESTURE_THRESHOLD)
                {
                    _zoomOnStartPinch = _currentZoom;
                    return GestureType.Rotate;
                }

                return GestureType.Move;
            }

            _firstDistanceBetweenTouches = null;
            return GestureType.None;
        }
        
        private void DragTouch()
        {
            var firstTouch = Input.GetTouch(FIRST_TOUCH_INDEX);

            switch (firstTouch.phase)
            {
                case TouchPhase.Began:
                    _dragPreviousPosition = firstTouch.position;
                    break;
                case TouchPhase.Moved:
                    _dragNextPosition = firstTouch.position;
                    SetContainerDragPosition();
                    break;
            }
        }

        private void PinchTouch()
        {
            if (_firstDistanceBetweenTouches == null) return;

            var currentDistance = Vector2.Distance(Input.GetTouch(FIRST_TOUCH_INDEX).position, Input.GetTouch(SECOND_TOUCH_INDEX).position);
            var zoomFactor = Mathf.Pow(((float)_firstDistanceBetweenTouches / currentDistance), _zoomSensitivity);

            ChangeZoom(_zoomOnStartPinch * zoomFactor);
        }
#endif
        #endregion

        #region MOUSE

#if UNITY_EDITOR
        private void MoveDesktop()
        {
            if (Input.GetMouseButtonDown(FIRST_TOUCH_INDEX))
            {
                _dragPreviousPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(FIRST_TOUCH_INDEX))
            {
                _dragNextPosition = Input.mousePosition;
                SetContainerDragPosition();
            }
            
            if (Input.GetMouseButtonUp(FIRST_TOUCH_INDEX))
            {
                _cameraGesturesControlSource.InputEnded();
            }
        }

        private void ZoomDesktop()
        {
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
                {
                    ChangeZoom(_currentZoom + DESKTOP_SHORT_ZOOM_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
                else
                {
                    ChangeZoom(_currentZoom + DESKTOP_ZOOM_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
                {
                    ChangeZoom(_currentZoom - DESKTOP_SHORT_ZOOM_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
                else
                {
                    ChangeZoom(_currentZoom - DESKTOP_ZOOM_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
            }
        }
        
        private void RotateDesktop()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
                {
                    ChangeRotation(_currentRotation + DESKTOP_SHORT_ROTATION_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
                else
                {
                    ChangeRotation(_currentRotation + DESKTOP_ROTATION_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
                {
                    ChangeRotation(_currentRotation - DESKTOP_SHORT_ROTATION_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
                else
                {
                    ChangeRotation(_currentRotation - DESKTOP_ROTATION_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
            }
        }

        private void AngleDesktop()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
                {
                    ChangeAngle(_currentAngle + DESKTOP_SHORT_ANGLE_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
                else
                {
                    ChangeAngle(_currentAngle + DESKTOP_ANGLE_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
                {
                    ChangeAngle(_currentAngle - DESKTOP_SHORT_ANGLE_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
                else
                {
                    ChangeAngle(_currentAngle - DESKTOP_ANGLE_INCREASE);
                    _cameraGesturesControlSource.InputEnded();
                }
            }
        }
#endif

        #endregion

        #region DRAG
        
        private void SetStoredPosition()
        {
            if (!_cameraIntroSource.HasFirstIntroAlreadyPlayed) return;
            
            _cameraMoveContainer.position = _cameraGesturesDataSource.CurrentPosition;
        }
        
        private void UpdateDragLimits(IReadOnlyList<Vector3> hexagonTiles)
        {
            foreach (var position in hexagonTiles)
            {
                if (position.x < _minDragLimits.x)
                    _minDragLimits.x = position.x;

                if (position.z < _minDragLimits.y)
                    _minDragLimits.y = position.z;

                if (position.x > _maxDragLimits.x)
                    _maxDragLimits.x = position.x;

                if (position.z > _maxDragLimits.y)
                    _maxDragLimits.y = position.z;
            }
        }

        private void SetContainerDragPosition()
        {
            var dragStrength = _dragStrengthBasedOnZoom.Evaluate(CurrentZoomPercentage);
            Vector3 deltaPosition = (_dragPreviousPosition - _dragNextPosition) * dragStrength;

            deltaPosition.z = deltaPosition.y;
            deltaPosition.y = default;

            deltaPosition = Quaternion.AngleAxis(_cameraGesturesDataSource.CurrentRotation, Vector3.up) * deltaPosition;

            var newPosition = ClampDragPositionInBounds(_cameraMoveContainer.position + deltaPosition);
            _cameraGesturesDataSource.SetPosition(newPosition);

            _dragPreviousPosition = _dragNextPosition;
        }

        private void RefreshPosition(Vector3 newPosition)
        {
            _cameraMoveContainer.position = newPosition;
        }

        private Vector3 ClampDragPositionInBounds(Vector3 newPosition)
        {
            // TODO: Re-add functionality when we detect new tiles being created.
            return newPosition;
            var x = Mathf.Clamp(newPosition.x, _minDragLimits.x, _maxDragLimits.x);
            var z = Mathf.Clamp(newPosition.z, _minDragLimits.y, _maxDragLimits.y);

            return new Vector3(x, newPosition.y, z);
        }

        #endregion

        #region ZOOM
        
        private void SetStoredZoom()
        {
            if (!_cameraIntroSource.HasFirstIntroAlreadyPlayed) return;
            
            _currentZoom = _cameraGesturesDataSource.CurrentZoom < 0 ? 3 : _cameraGesturesDataSource.CurrentZoom;
            _cameraGesturesDataSource.SetZoom(_currentZoom);
        }

        private void ChangeZoom(float newZoom)
        {
            _currentZoom = Mathf.Clamp(newZoom, _minZoom, _maxZoom);
            _cameraGesturesDataSource.SetZoom(_currentZoom);
        }

        private void RefreshZoom(float zoom)
        {
            _cameraZoomContainer.localPosition = _cameraZoomContainer.localPosition.SetY(zoom);
        }

        #endregion

        #region ROTATION
        
        private void SetStoredRotation()
        {
            if (!_cameraIntroSource.HasFirstIntroAlreadyPlayed) return;
            
            _currentRotation = _cameraGesturesDataSource.CurrentRotation;
            _cameraGesturesDataSource.SetRotation(_currentRotation);
        }

        private void ChangeRotation(float rotation)
        {
            _currentRotation = rotation % Constants.Camera.MAX_ROTATION;
            _cameraGesturesDataSource.SetRotation(_currentRotation);
        }

        private void RefreshRotation(float rotation)
        {
            _cameraRotationContainer.localRotation = Quaternion.Euler(Vector3.up * rotation);
        }
        
        #endregion

        #region ANGLE
        
        private void SetStoredAngle()
        {
            if (!_cameraIntroSource.HasFirstIntroAlreadyPlayed) return;
            
            _currentAngle = _cameraGesturesDataSource.CurrentAngle;
            _cameraGesturesDataSource.SetAngle(_currentAngle);
        }

        private void ChangeAngle(float angle)
        {
            _currentAngle = angle % Constants.Camera.MAX_ROTATION;
            _cameraGesturesDataSource.SetAngle(_currentAngle);
        }

        private void RefreshAngle(float angle)
        {
            _cameraAngleContainer.localRotation = Quaternion.Euler(Vector3.right * angle);
        }
        
        #endregion
    }
}
