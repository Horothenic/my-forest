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
        [SerializeField] private CinemachineCamera _islandCamera;
        [SerializeField] private CinemachineCamera _plantCamera;

        [Header("COMPONENTS")]
        [SerializeField] private Transform _islandCameraTarget;

        private readonly List<CinemachineCamera> _allCameras = new List<CinemachineCamera>();
        private CinemachineCamera _currentCamera;
        private CinemachineOrbitalFollow _currentCameraOrbitalFollow;

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
            _allCameras.Add(_islandCamera);
            _allCameras.Add(_plantCamera);
            
            SelectCamera(_islandCamera);
        }
        
        private void OnGameMode(GameMode gameMode)
        {
            switch (gameMode)
            {
                case GameMode.Plant:
                    SelectCamera(_plantCamera);
                    break;
                default:
                    SelectCamera(_islandCamera);
                    break;
            }
        }

        private void SelectCamera(CinemachineCamera selectedCamera)
        {
            
            _currentCamera = selectedCamera;
            _currentCameraOrbitalFollow = selectedCamera.GetComponent<CinemachineOrbitalFollow>();

            selectedCamera.Priority = HIGH_PRIORITY;
            
            foreach (var camera in _allCameras)
            {
                if (camera == selectedCamera) continue;

                camera.Priority = LOW_PRIORITY;
            }
        }

        private void OnPan(float deltaPosition)
        {
            var forward = _currentCameraOrbitalFollow.transform.forward;
            var forwardFlat = new Vector3(forward.x,0f, forward.z).normalized;
            
            _islandCameraTarget.position += forwardFlat * deltaPosition;
        }

        private void OnRotate(float deltaPosition)
        {
            _currentCameraOrbitalFollow.HorizontalAxis.Value += deltaPosition;
        }

        #endregion
    }
}
