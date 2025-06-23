using System.Linq;
using UnityEngine;

namespace MyForest
{
    public partial class MainMenuController : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private Canvas _mainMenuCanvas;
        
        [Header("PAGES")]
        [SerializeField] private PageController[] _pages;

        private PageController _currentPage = null;

        public bool IsMenuOpen => _mainMenuCanvas.enabled;

        #endregion

        #region METHODS

        private void Start()
        {
            CloseMenu();
        }

        private void ShowPage(PageController page)
        {
            if (!IsMenuOpen)
            {
                if (_currentPage != null)
                {
                    _currentPage.Animate(PageAnimation.HideInstant);
                }
                
                _currentPage = page;
                _currentPage.Animate(PageAnimation.AppearInstant);
                OpenMenu();
                return;
            }
            
            if (_currentPage.IsBeingAnimated || page.IsBeingAnimated) return;
            
            if (page.Index > _currentPage.Index)
            {
                _currentPage.Animate(PageAnimation.ToTheLeft);
                page.Animate(PageAnimation.FromTheRight);
                _currentPage = page;
            }
            else
            {
                _currentPage.Animate(PageAnimation.ToTheRight);
                page.Animate(PageAnimation.FromTheLeft);
                _currentPage = page;
            }
        }

        private void OpenMenu()
        {
            _mainMenuCanvas.enabled = true;
        }

        private void CloseMenu()
        {
            _mainMenuCanvas.enabled = false;

            if (_currentPage != null)
            {
                _currentPage.Animate(PageAnimation.HideInstant);
                _currentPage = null;
            }
        }

        #endregion
    }

    public partial class MainMenuController : IMenuSource
    {
        void IMenuSource.OpenPage(MenuPage menuPage)
        {
            var page = _pages.FirstOrDefault(p => p.Name == menuPage);

            if (page == null)
            {
                Debug.LogError("[Main Menu] Page not found.");
                return;
            }

            if (_currentPage != null && _currentPage.Name == page.Name) return;

            ShowPage(page);
        }

        void IMenuSource.CloseMenu()
        {
            CloseMenu();
        }
    }
}
