using Reflex.Attributes;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.Cinemachine;
using UnityEngine.Serialization;

namespace MyIsland
{
    public class CameraController : MonoBehaviour
    {
        #region FIELDS

        private const int HIGH_PRIORITY = 10;
        private const int LOW_PRIORITY = 5;

        [Inject] private IGameSource _gameSource;
        [Inject] private ICameraInputSource _cameraInputSource;
        
        [Header("COMPONENTS")]
        [SerializeField] private CinemachineCamera _islandCamera;
        [SerializeField] private CinemachineCamera _plantCamera;

        private readonly List<CinemachineCamera> _allCameras = new List<CinemachineCamera>();

        #endregion

        #region METHODS

        private void Awake()
        {
            _allCameras.Add(_islandCamera);
            _allCameras.Add(_plantCamera);
            
            _gameSource.OnGameMode.Subscribe(OnGameMode).AddTo(this);
            
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
            selectedCamera.Priority = HIGH_PRIORITY;

            foreach (var camera in _allCameras)
            {
                if (camera == selectedCamera) continue;

                camera.Priority = LOW_PRIORITY;
            }
        }

        #endregion
    }
}
