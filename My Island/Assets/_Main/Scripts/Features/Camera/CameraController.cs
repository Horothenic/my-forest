using Reflex.Attributes;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.Cinemachine;

namespace MyIsland
{
    public class CameraController : MonoBehaviour
    {
        #region FIELDS

        private const int HIGH_PRIORITY = 10;
        private const int LOW_PRIORITY = 5;

        [Inject] private IGameSource _gameSource;
        [Inject] private ICameraInputSource _cameraInputSource;
        
        [Header("CAMERAS")]
        [SerializeField] private CinemachineCamera[] _cameras;
        
        [Header("COMPONENTS")]
        [SerializeField] private float _islandClampRadius = 100;

        [Header("COMPONENTS")]
        [SerializeField] private Transform _islandTarget;
        [SerializeField] private GameObject _islandTargetHighlight;

        private readonly List<CameraData> _allCameras = new List<CameraData>();
        private CameraData _currentCamera;

        #endregion

        #region METHODS

        private void Awake()
        {
            SetCameras();
            
            _gameSource.OnGameMode.Subscribe(OnGameMode).AddTo(this);
            _cameraInputSource.OnPan.Subscribe(OnPan).AddTo(this);
            _cameraInputSource.OnRotate.Subscribe(OnRotate).AddTo(this);
        }

        private void SetCameras()
        {
            foreach (var camera in _cameras)
            {
                _allCameras.Add(new CameraData(camera));
            }
            
            SelectCamera(CameraIndex.Island);
        }
        
        private void OnGameMode(GameMode gameMode)
        {
            switch (gameMode)
            {
                case GameMode.Plant:
                    SetIslandTargetHighlightVisibility(true);
                    SelectCamera(CameraIndex.Plant);
                    break;
                default:
                    SetIslandTargetHighlightVisibility(false);
                    SelectCamera(CameraIndex.Island);
                    break;
            }
        }

        private void SelectCamera(CameraIndex cameraIndex)
        {
            var index = (int)cameraIndex;
            var newCamera = _allCameras[index];
            
            if (_currentCamera == newCamera) return;
            
            if (_currentCamera != null)
            {
                newCamera.OrbitalFollow.HorizontalAxis.Value = _currentCamera.OrbitalFollow.HorizontalAxis.Value;
            }
            
            foreach (var cam in _allCameras)
            {
                cam.Camera.Priority = (cam == newCamera) ? HIGH_PRIORITY : LOW_PRIORITY;
            }

            _currentCamera = newCamera;
        }

        private void OnPan(float deltaPosition)
        {
            var forward = _currentCamera.OrbitalFollow.transform.forward;
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
            _currentCamera.OrbitalFollow.HorizontalAxis.Value += deltaPosition;
        }

        private void SetIslandTargetHighlightVisibility(bool visible)
        {
            _islandTargetHighlight.SetActive(visible);
        }

        #endregion
    }
}
