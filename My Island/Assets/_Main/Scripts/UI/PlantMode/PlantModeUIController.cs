using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace MyIsland
{
    public class PlantModeUIController : MonoBehaviour
    {
        #region FIELDS
        
        [Inject] private IGameSource _gameSource;
        [Inject] private IForestSource _forestSource;
        [Inject] private ICameraTargetSource _cameraTargetSource;
        
        [Header("COMPONENTS")]
        [SerializeField] private GameObject _container;
        [SerializeField] private Button _plantButton;
        [SerializeField] private Button _exitPlantModeButton;
        
        #endregion
        
        #region METHODS
        
        private void Awake()
        {
            _plantButton.onClick.AddListener(PlantTree);
            _exitPlantModeButton.onClick.AddListener(ExitPlantMode);
            _gameSource.OnGameMode.Subscribe(OnGameMode).AddTo(this);
        }

        private void Start()
        {
            Hide();
        }
        
        private void OnGameMode(GameMode gameMode)
        {
            switch (gameMode)
            {
                case GameMode.Island:
                    Hide();
                    break;
                default:
                    Show();
                    break;
            }
        }

        private void PlantTree()
        {
            _forestSource.PlantTree(_cameraTargetSource.IslandTargetRay);
        }

        private void ExitPlantMode()
        {
            _gameSource.SetGameMode(GameMode.Island);
        }
        
        private void Show()
        {
            _container.SetActive(true);
        }

        private void Hide()
        {
            _container.SetActive(false);
        }
        
        #endregion
    }
}
