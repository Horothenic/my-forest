using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace MyIsland
{
    public class PlantModeUIController : MonoBehaviour
    {
        #region FIELDS
        
        [Inject] private IForestSource _forestSource;
        
        [Header("COMPONENTS")]
        [SerializeField] private GameObject _container;
        [SerializeField] private Button _exitPlantModeButton;
        
        #endregion
        
        #region METHODS
        
        private void Awake()
        {
            _exitPlantModeButton.onClick.AddListener(_forestSource.ExitPlantMode);
            _forestSource.IsPlantModeOpen.Subscribe(OnPlantMode).AddTo(this);
        }

        private void Start()
        {
            Hide();
        }

        private void OnPlantMode(bool isActive)
        {
            if (isActive)
            {
                Show();
            }
            else
            {
                Hide();
            }
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
