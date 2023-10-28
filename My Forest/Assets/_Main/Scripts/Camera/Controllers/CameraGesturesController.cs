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
        private const string MOUSE_SCROLL_WHEEL_KEY = "Mouse ScrollWheel";

        [Inject] private ICameraRotationSource _cameraRotationSource = null;
        [Inject] private ICameraGesturesControlSource _cameraGesturesControlSource = null;
        [Inject] private ICameraIntroSource _cameraIntroSource = null;

        [Header("DRAG CONFIGURATIONS")]
        [SerializeField] private Transform _cameraContainer = null;
        [SerializeField] private Camera _camera = null;
        [SerializeField] private float _dragStrength = 1;

        [Header("PINCH CONFIGURATIONS")]
        [SerializeField] private float _minZoom = 2;
        [SerializeField] private float _maxZoom = 8;
        [SerializeField] private float _defaultZoom = 3;
        [SerializeField] private float _zoomTouchSensitivity = 1;
        [SerializeField] private float _zoomMouseSensitivity = 1;
        [SerializeField] private float _zoomTransitionTime = 0.5f;

        private Vector2 _dragPreviousPosition = default;
        private Vector2 _dragNextPosition = default;
        private Vector2 _minDragLimits;
        private Vector2 _maxDragLimits;
        private float? _firstDistanceBetweenTouches = null;
        private float _currentZoom = default;
        private float _zoomOnStartPinch = default;
        private GestureType _currentGesture = GestureType.None;
        private bool _inputEnabled = true;
        private Tween _zoomTween = null;

        private enum GestureType
        {
            None,
            Drag,
            Pinch
        }

        #endregion

        #region UNITY

        private void Start()
        {
            _cameraGesturesControlSource.ZoomObservable.Subscribe(SetZoomWithTransition).AddTo(this);
            _cameraGesturesControlSource.EnableInputObservable.Subscribe(EnableInput).AddTo(this);
            _cameraGesturesControlSource.BlockInputObservable.Subscribe(BlockInput).AddTo(this);
            _cameraGesturesControlSource.UpdateDragLimitsObservable.Subscribe(UpdateDragLimits).AddTo(this);
            _cameraIntroSource.IntroStartedObservable.Subscribe(SetStoredZoom).AddTo(this);
        }

        private void Update()
        {
            CheckInput();
        }

        #endregion

        #region METHODS

        private void CheckInput()
        {
            if (!_inputEnabled) return;

#if UNITY_EDITOR
            DragMouse();
            PinchMouse();
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

        private GestureType SelectGestureType()
        {
            if (Input.touchCount == 1)
            {
                if (_currentGesture == GestureType.Pinch)
                {
                    _firstDistanceBetweenTouches = null;
                    _dragPreviousPosition = Input.GetTouch(FIRST_TOUCH_INDEX).position;
                }

                return GestureType.Drag;
            }

            if (Input.touchCount == 2)
            {
                if (_currentGesture == GestureType.Pinch)
                {
                    return GestureType.Pinch;
                }

                if (_firstDistanceBetweenTouches == null)
                {
                    _firstDistanceBetweenTouches = Vector2.Distance(Input.GetTouch(FIRST_TOUCH_INDEX).position, Input.GetTouch(SECOND_TOUCH_INDEX).position);
                }

                var distanceBetweenTouches = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

                if (Mathf.Abs(distanceBetweenTouches - (float)_firstDistanceBetweenTouches) > PINCH_GESTURE_THRESHOLD)
                {
                    _zoomOnStartPinch = _currentZoom;
                    return GestureType.Pinch;
                }

                return GestureType.Drag;
            }

            _firstDistanceBetweenTouches = null;
            return GestureType.None;
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
            var zoomFactor = Mathf.Pow(((float)_firstDistanceBetweenTouches / currentDistance), _zoomTouchSensitivity);

            ChangeZoom(_zoomOnStartPinch * zoomFactor);
        }

        #endregion

        #region MOUSE

        private void DragMouse()
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
        }

        private void PinchMouse()
        {
            var mouseWheelDirection = Input.GetAxisRaw(MOUSE_SCROLL_WHEEL_KEY);

            if (mouseWheelDirection == 0f) return;

            ChangeZoom(_currentZoom - (_zoomMouseSensitivity * mouseWheelDirection));
        }

        #endregion

        #region DRAG
        
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
            Vector3 deltaPosition = (_dragPreviousPosition - _dragNextPosition) * _dragStrength;

            deltaPosition.z = deltaPosition.y;
            deltaPosition.y = default;

            deltaPosition = Quaternion.AngleAxis(_cameraRotationSource.CurrentRotationAngles, Vector3.up) * deltaPosition;

            var newPosition = ClampDragPositionInBounds(_cameraContainer.position + deltaPosition);
            _cameraContainer.position = newPosition;

            _dragPreviousPosition = _dragNextPosition;
        }

        private Vector3 ClampDragPositionInBounds(Vector3 newPosition)
        {
            var x = Mathf.Clamp(newPosition.x, _minDragLimits.x, _maxDragLimits.x);
            var z = Mathf.Clamp(newPosition.z, _minDragLimits.y, _maxDragLimits.y);

            return new Vector3(x, newPosition.y, z);
        }

        #endregion

        #region ZOOM
        
        private void SetStoredZoom()
        {
            if (!_cameraIntroSource.HasFirstIntroAlreadyPlayed) return;
            
            _currentZoom = _cameraGesturesControlSource.GetCurrentZoom < 0 ? _defaultZoom : _cameraGesturesControlSource.GetCurrentZoom;
            SetZoomWithTransition((_currentZoom, false));
        }

        private void ChangeZoom(float newZoom)
        {
            _currentZoom = Mathf.Clamp(newZoom, _minZoom, _maxZoom);
            _cameraGesturesControlSource.SetZoom(newZoom, true);
        }

        private void SetZoomWithTransition((float zoom, bool withTransition) value)
        {
            if (value.withTransition)
            {
                _zoomTween?.Kill();
                _zoomTween = DOTween.To(() => _camera.orthographicSize, x => _camera.orthographicSize = x, value.zoom, _zoomTransitionTime);
            }
            else
            {
                _camera.orthographicSize = value.zoom;
            }
        }

        #endregion
    }
}
