using UnityEngine;
using UnityEngine.UI;

namespace MyForest
{
    public class FullScreenMenuOpenerUI : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private Button[] _toggleButtons = null;
        [SerializeField] private GameObject _container = null;

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
            _container.SetActive(!_container.activeSelf);
        }

        #endregion
    }
}
