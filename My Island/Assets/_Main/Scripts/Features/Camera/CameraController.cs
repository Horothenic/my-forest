using Reflex.Attributes;
using UnityEngine;
using UniRx;
using Unity.Cinemachine;

namespace MyIsland
{
    public partial class CameraController : MonoBehaviour
    {
        #region FIELDS

        private const int HIGH_PRIORITY = 10;
        private const int LOW_PRIORITY = 5;
        
        [Inject] private ICameraInputSource _cameraInputSource;
        
        [Header("CAMERAS")]
        [SerializeField] private CinemachineCamera _camera;
        [SerializeField] private CinemachineOrbitalFollow _cameraOrbitalFollow;
        
        [Header("COMPONENTS")]
        [SerializeField] private float _islandClampRadius = 100;

        [Header("COMPONENTS")]
        [SerializeField] private Transform _islandTarget;

        #endregion

        #region METHODS

        private void Awake()
        {
            _cameraInputSource.OnPan.Subscribe(OnPan).AddTo(this);
            _cameraInputSource.OnRotate.Subscribe(OnRotate).AddTo(this);
        }

        private void OnPan(float deltaPosition)
        {
            var forward = _cameraOrbitalFollow.transform.forward;
            var forwardFlat = new Vector3(forward.x,0f, forward.z).normalized;
            
            _islandTarget.position += forwardFlat * deltaPosition;
            ClampPositionWithinIsland(_islandTarget);
        }
        
        private void ClampPositionWithinIsland(Transform target)
        {
            var center = Vector3.zero;
            var offset = target.position - center;
            offset.y = 0f;

            if (!(offset.sqrMagnitude > _islandClampRadius * _islandClampRadius)) return;
            
            offset = offset.normalized * _islandClampRadius;
            target.position = new Vector3(center.x + offset.x, target.position.y, center.z + offset.z);
        }

        private void OnRotate(float deltaPosition)
        {
            _cameraOrbitalFollow.HorizontalAxis.Value += deltaPosition;
        }

        #endregion
    }
}
