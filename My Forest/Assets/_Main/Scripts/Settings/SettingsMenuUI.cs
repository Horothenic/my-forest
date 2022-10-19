using UnityEngine;
using UnityEngine.UI;

namespace MyForest
{
    public class SettingsMenuUI : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private Button[] _toggleButtons = null;
        [SerializeField] private GameObject _settingsContainer = null;

        #endregion

        #region UNITY

        private void Awake()
        {
            Initialize();
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            foreach (var button in _toggleButtons)
            {
                button.onClick.AddListener(ToggleContainer);
            }
        }

        private void ToggleContainer()
        {
            _settingsContainer.SetActive(!_settingsContainer.activeSelf);
        }

        #endregion
    }
}
