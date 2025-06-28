using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MyIsland
{
    public class DebugMenuController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IGrowthDebugSource _growthDebugSource;
        
        [Header("COMPONENTS")]
        [SerializeField] private GameObject _debugMenuContainer;

        [Header("DEBUG BUTTONS")]
        [SerializeField] private Button _increaseGrowth1Button;
        [SerializeField] private Button _increaseGrowth10Button;
        [SerializeField] private Button _increaseGrowth100Button;
        [SerializeField] private Button _resetGrowthButton;

        #endregion

        #region METHODS

        private void Start()
        {
            LoadButtons();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                ToggleMenu();
            }
        }

        private void ToggleMenu()
        {
            _debugMenuContainer.SetActive(!_debugMenuContainer.activeSelf);
        }

        private void LoadButtons()
        {
            _increaseGrowth1Button.onClick.AddListener(() => _growthDebugSource.IncreaseGrowth(1));
            _increaseGrowth10Button.onClick.AddListener(() => _growthDebugSource.IncreaseGrowth(10));
            _increaseGrowth100Button.onClick.AddListener(() => _growthDebugSource.IncreaseGrowth(100));
            _resetGrowthButton.onClick.AddListener(_growthDebugSource.ResetGrowth);
        }

        #endregion
    }
}
