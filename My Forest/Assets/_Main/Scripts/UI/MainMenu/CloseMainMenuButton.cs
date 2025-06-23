using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MyForest
{
    public class CloseMainMenuButton : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IMenuSource _menuSource;

        [Header("COMPONENTS")]
        [SerializeField] private Button _button;

        #endregion

        #region METHODS

        private void Awake()
        {
            _button.onClick.AddListener(CloseMainMenu);
        }

        private void CloseMainMenu()
        {
            _menuSource.CloseMenu();
        }

        #endregion
    }
}
