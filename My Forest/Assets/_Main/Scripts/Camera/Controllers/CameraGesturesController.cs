using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

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
        private Vector2 _minDragLimits = default;
        private Vector2 _maxDragLimits = default;
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
            _currentZoom = _minZoom;

            _cameraGesturesControlSource.SetDefaultZoomObservable.Subscribe(SetDefaultZoomWithTransition).AddTo(this);
            _cameraGesturesControlSource.EnableInputObservable.Subscribe(EnableInput).AddTo(this);
            _cameraGesturesControlSource.BlockInputObservable.Subscribe(BlockInput).AddTo(this);
            _cameraGesturesControlSource.UpdateDragLimitsObservable.Subscribe(UpdateDragLimits).AddTo(this);
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

            SetZoom(_zoomOnStartPinch * zoomFactor);
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

            if (mouseWheelDirection == default) return;

            SetZoom(_currentZoom - (_zoomMouseSensitivity * mouseWheelDirection));
        }

        #endregion

        #region DRAG
        
        private void UpdateDragLimits(IReadOnlyList<HexagonTile> hexagonTiles)
        {
            var minX = float.MaxValue;
            var minZ = float.MaxValue;
            var maxX = float.MinValue;
            var maxZ = float.MinValue;

            foreach (var tile in hexagonTiles)
            {
                var position = tile.transform.position;

                if (position.x < minX)
                    minX = position.x;

                if (position.z < minZ)
                    minZ = position.z;

                if (position.x > maxX)
                    maxX = position.x;

                if (position.z > maxZ)
                    maxZ = position.z;
            }

            _minDragLimits = new Vector2(minX, minZ);
            _maxDragLimits = new Vector2(maxX, maxZ);
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

        private void SetZoom(float newZoom)
        {
            _currentZoom = Mathf.Clamp(newZoom, _minZoom, _maxZoom);
            _camera.orthographicSize = _currentZoom;
        }

        private void SetZoomWithTransition(float newZoom)
        {
            _zoomTween?.Kill();
            _zoomTween = DOTween.To(() => _camera.orthographicSize, x => _camera.orthographicSize = x, newZoom, _zoomTransitionTime);
        }

        private void SetDefaultZoomWithTransition()
        {
            SetZoomWithTransition(_defaultZoom);
        }

        #endregion
    }
}
