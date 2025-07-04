using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace MyForest
{
    public class PageController : MonoBehaviour
    {
        #region FIELDS

        private const float ANIMATION_DURATION = 0.3f;
        
        [Header("COMPONENTS")]
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _canvasRectTransform;
        [SerializeField] private RectTransform _mainContainer;

        [Header("CONFIGURATIONS")]
        [SerializeField] private MenuPage _name;
        
        public MenuPage Name => _name;
        public int Index => Name.Index();
        public bool IsBeingAnimated { get; private set; }

        #endregion

        #region METHODS

        private void Start()
        {
            DisappearPage();
        }

        public void Animate(PageAnimation pageAnimation)
        {
            switch (pageAnimation)
            {
                case PageAnimation.AppearInstant:
                    InstantAnimation(true);
                    break;
                case PageAnimation.HideInstant:
                    InstantAnimation(false);
                    break;
                case PageAnimation.FromTheRight:
                    SideAnimation(true, true).Forget();
                    break;
                case PageAnimation.FromTheLeft:
                    SideAnimation(true, false).Forget();
                    break;
                case PageAnimation.ToTheRight:
                    SideAnimation(false, true).Forget();
                    break;
                case PageAnimation.ToTheLeft:
                    SideAnimation(false, false).Forget();
                    break;
            }
        }

        private void InstantAnimation(bool appear)
        {
            if (appear)
            {
                _mainContainer.anchoredPosition = Vector2.zero;
                _canvasGroup.interactable = true;
                AppearPage();
            }
            else
            {
                _canvasGroup.interactable = false;
                DisappearPage();
            }
        }

        private async UniTaskVoid SideAnimation(bool appear, bool isRight)
        {
            IsBeingAnimated = true;
            
            var width = _canvasRectTransform.rect.width;
            var objectivePosition = isRight ? width : -width;

            if (appear)
            {
                _mainContainer.anchoredPosition = new Vector2(objectivePosition, 0);
                AppearPage();

                await _mainContainer.DOAnchorPosX(0f, ANIMATION_DURATION).AsyncWaitForCompletion();

                _canvasGroup.interactable = true;
            }
            else
            {
                await _mainContainer.DOAnchorPosX(objectivePosition, ANIMATION_DURATION).AsyncWaitForCompletion();

                DisappearPage();
                _canvasGroup.interactable = false;
            }

            IsBeingAnimated = false;
        }

        private void AppearPage()
        {
            _mainContainer.gameObject.SetActive(true);
            _canvas.enabled = true;
        }

        private void DisappearPage()
        {
            _canvas.enabled = false;
            _mainContainer.gameObject.SetActive(false);
        }

        #endregion
    }

    public enum PageAnimation
    {
        AppearInstant,
        HideInstant,
        FromTheRight,
        FromTheLeft,
        ToTheRight,
        ToTheLeft,
    }

    public enum MenuPage
    {
        Growth,
        BotanicalCompendium,
        WildlifeAlmanac,
        NaturalAugments,
        Settings,
    }
}
