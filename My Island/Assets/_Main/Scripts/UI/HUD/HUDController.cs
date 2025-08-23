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
        [Inject] private IForestSource _forestSource;
        
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
            _enterPlantModeButton.onClick.AddListener(_forestSource.EnterPlantMode);
        }
        
        private void Start()
        {
            _growthSource.DataObservables.Subscribe(_ => Refresh()).AddTo(this);
            _forestSource.IsPlantModeOpen.Subscribe(OnPlantMode).AddTo(this);
            Refresh();
        }

        private void OnPlantMode(bool isActive)
        {
            if (isActive)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void Refresh()
        {
            _currentGrowthText.text = _growthSource.Data.CurrentGrowth.ToString();
            _allTimeGrowthText.text = _growthSource.Data.AllTimeGrowth.ToString();
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
