using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MyForest
{
    public class OpenPageButton : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IMenuSource _menuSource;

        [Header("COMPONENTS")]
        [SerializeField] private Button _button;

        [Header("CONFIGURATIONS")]
        [SerializeField] private MenuPage _menuPage;

        #endregion

        #region METHODS

        private void Awake()
        {
            _button.onClick.AddListener(OpenMenuPage);
        }

        private void OpenMenuPage()
        {
            _menuSource.OpenPage(_menuPage);
        }

        #endregion
    }
}
