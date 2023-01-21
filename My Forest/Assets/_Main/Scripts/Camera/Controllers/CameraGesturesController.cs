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
        private const float PINCH_GESTURE_THRESHOLD = 10f;
        private const string MOUSE_SCROLL_WHEEL_KEY = "Mouse ScrollWheel";

        [Inject] private IForestDataSource _forestDataSource = null;
        [Inject] private IForestElementMenuSource _forestElementMenuSource = null;
        [Inject] private ICameraRotationDataSource _cameraRotationSource = null;
        [Inject] private IForestSizeConfigurationsSource _forestSizeConfigurationsSource = null;

        [Header("DRAG CONFIGURATIONS")]
        [SerializeField] private Transform _cameraContainer = null;
        [SerializeField] private Camera _camera = null;
        [SerializeField] private float _dragStrength = 1;
        
        [Header("PINCH CONFIGURATIONS")]
        [SerializeField] private float _minZoom = 2;
        [SerializeField] private float _maxZoom = 8;
        [SerializeField] private float _defaultZoom = 3;
        [SerializeField] private float _zoomMouseSensitivity = 1;
        [SerializeField] private float _zoomTransitionTime = 0.5f;

        private Vector2 _dragPreviousPosition = default;
        private Vector2 _dragNextPosition = default;
        private Vector2 _dragLimits = default;
        private float? _firstDistanceBetweenTouches = null;
        private float _currentZoom = default;
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
                
            _forestDataSource.CreatedForestObservable.Subscribe(forest => UpdateDragLimits(forest.SizeLevel)).AddTo(this);
            _forestDataSource.IncreaseForestSizeLevelObservable.Subscribe(UpdateDragLimits).AddTo(this);
            _forestElementMenuSource.ForestElementMenuRequestedObservable.Subscribe(_ => SetDefaultZoomForElementMenu()).AddTo(this);
            _forestElementMenuSource.ForestElementMenuClosedObservable.Subscribe(EnableInput).AddTo(this);
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
                _firstDistanceBetweenTouches = null;
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
                    _firstDistanceBetweenTouches = Vector2.Distance(Input.touches[0].rawPosition, Input.touches[1].rawPosition);
                }
                
                var distanceBetweenTouches = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                
                if (Mathf.Abs(distanceBetweenTouches - (float)_firstDistanceBetweenTouches) > PINCH_GESTURE_THRESHOLD)
                {
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
            // TODO: compare first distance with new distance and n/x will give you the factor needed to zoom down or up.
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

            _currentZoom = Mathf.Clamp(_currentZoom - (_zoomMouseSensitivity * mouseWheelDirection), _minZoom, _maxZoom);
            _camera.orthographicSize = _currentZoom;
        }
        
        #endregion
        
        #region DRAG
        
        private void UpdateDragLimits(uint forestSizeLevel)
        {
            var limit = _forestSizeConfigurationsSource.GetDiameterByLevel(forestSizeLevel) / 2f;
            _dragLimits = new Vector2(-limit, limit);
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
            if (newPosition.x < _dragLimits.x)
            {
                newPosition.x = _dragLimits.x;
            }

            if (newPosition.z < _dragLimits.x)
            {
                newPosition.z = _dragLimits.x;
            }

            if (newPosition.x > _dragLimits.y)
            {
                newPosition.x = _dragLimits.y;
            }

            if (newPosition.z > _dragLimits.y)
            {
                newPosition.z = _dragLimits.y;
            }

            return newPosition;
        }
        
        #endregion
        
        #region ZOOM

        private void SetZoomWithTransition(float newZoom)
        {
            _zoomTween?.Kill();
            _zoomTween = DOTween.To(()=> _camera.orthographicSize, x => _camera.orthographicSize = x, newZoom, _zoomTransitionTime);
        }
        
        private void SetDefaultZoomForElementMenu()
        {
            SetZoomWithTransition(_defaultZoom);
            BlockInput();
        }

        #endregion
    }
}
