using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;

namespace MyIsland
{
    public class HUDController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IGrowthSource _growthSource;
        [Inject] private IMenuSource _menuSource;
        [Inject] private IGameSource _gameSource;
        
        [Header("COMPONENTS")]
        [SerializeField] private GameObject _container;

        [Header("MAIN MENU")]
        [SerializeField] private Button _openMainMenuButton;
        [SerializeField] private MenuPage _menuPage;

        [Header("GROWTH")]
        [SerializeField] private TextMeshProUGUI _currentGrowthText;
        [SerializeField] private TextMeshProUGUI _allTimeGrowthText;
        
        [Header("FOREST")]
        [SerializeField] private Button _enterPlantModeButton;
        
        #endregion

        #region METHODS

        private void Awake()
        {
            _openMainMenuButton.onClick.AddListener(OpenMenuPage);
            _enterPlantModeButton.onClick.AddListener(EnterPlantMode);
        }
        
        private void Start()
        {
            _growthSource.DataObservables.Subscribe(_ => Refresh()).AddTo(this);
            _gameSource.OnGameMode.Subscribe(OnGameMode).AddTo(this);
            Refresh();
        }

        private void OnGameMode(GameMode gameMode)
        {
            switch (gameMode)
            {
                case GameMode.Island:
                    Show();
                    break;
                default:
                    Hide();
                    break;
            }
        }

        private void Refresh()
        {
            _currentGrowthText.text = _growthSource.Data.CurrentGrowth.ToString();
            _allTimeGrowthText.text = _growthSource.Data.AllTimeGrowth.ToString();
        }

        private void EnterPlantMode()
        {
            _gameSource.SetGameMode(GameMode.Plant);
        }

        private void OpenMenuPage()
        {
            _menuSource.OpenPage(_menuPage);
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
