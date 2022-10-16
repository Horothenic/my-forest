using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public class CameraGesturesController : MonoBehaviour
    {
        #region FIELDS

        private const int FIRST_TOUCH_INDEX = 0;

        [Inject] private IForestDataSource _forestDataSource = null;
        [Inject] private ICameraRotationDataSource _cameraRotationSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform _cameraContainer = null;
        [SerializeField] private float _dragStrength = 1;
        [SerializeField] private float _dragExtraLimit = 1;

        private Vector2 _dragPreviousPosition = default;
        private Vector2 _dragNextPosition = default;
        private Vector2 _dragLimits = default;
        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region UNITY

        private void Start()
        {
            _forestDataSource.ForestDataObservable.Subscribe(UpdateDragLimits).AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void Update()
        {
#if UNITY_EDITOR
            DragMouse();
#else
            DragTouch();
#endif
        }

        #endregion

        #region METHODS

        private void UpdateDragLimits(ForestData newForest)
        {
            var limit = newForest.GroundWidth.Half() + _dragExtraLimit;
            _dragLimits = new Vector2(-limit, limit);
        }

        private void DragTouch()
        {
            if (Input.touchCount == 0) return;

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
    }
}
