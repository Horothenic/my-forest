using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;
using Localization;
using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyIsland
{
    public class TellMeYourNameUIController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IIslandSource _islandSource;
        [Inject] private ISceneSource _sceneSource;

        [Header("COMPONENTS")]
        [SerializeField] private CanvasGroup _balloonContainer;
        [SerializeField] private TimedText _balloonText;
        [SerializeField] private CanvasGroup _continueButtonContainer;
        [SerializeField] private Button _continueButton;
        [SerializeField] private CanvasGroup _textInputContainer;
        [SerializeField] private TMP_InputField _textInput;

        #endregion

        #region METHODS

        private void Start()
        {
            TriggerTellMeYourNameSequence().Forget();
        }

        private async UniTaskVoid TriggerTellMeYourNameSequence()
        {
            InitializeComponents();
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            await WaitForBalloonMessage("Hello...".Localize());
            await WaitForBalloonMessage("You may be confused...".Localize());
            await WaitForBalloonMessage("But before I can explain everything...".Localize());
            await WaitForBalloonMessage("How may I call you?".Localize());
            
            var creatorName = await WaitForTextInput();
            _islandSource.SetCreatorName(creatorName);
            
            await WaitForBalloonMessage("Nice to meet you <b>{0}</b>!".Localize(_islandSource.CreatorName));
            await WaitForBalloonMessage("Seems like you just ascended to some kind of godhood...".Localize());
            await WaitForBalloonMessage("The sad part is that you will only manage a tiny island...".Localize());
            await WaitForBalloonMessage("Your duty is to make this island a paradise...".Localize());
            await WaitForBalloonMessage("Lots of work to do, lets see your island.".Localize());
            
            _sceneSource.LoadScene("Main");
        }

        private void InitializeComponents()
        {
            _balloonContainer.gameObject.SetActive(false);
            _continueButtonContainer.gameObject.SetActive(false);
            _textInputContainer.gameObject.SetActive(false);

            _balloonContainer.alpha = 0;
            _continueButtonContainer.alpha = 0;
            _textInputContainer.alpha = 0;
        }

        private async UniTask WaitForBalloonMessage(string text)
        {
            _balloonText.EmptyText();
            _balloonContainer.gameObject.SetActive(true);
            _balloonContainer.DOFade(1, 0.4f);
            await _balloonText.TriggerTimedText(text, 0.05f);
            await WaitForContinueButton();
            await _balloonContainer.DOFade(0, 0.25f).AsyncWaitForCompletion();
            _balloonContainer.gameObject.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
        }

        private async UniTask WaitForContinueButton()
        {
            _continueButtonContainer.gameObject.SetActive(true);
            _continueButtonContainer.interactable = false;
            await _continueButtonContainer.DOFade(1, 0.3f).AsyncWaitForCompletion();
            _continueButtonContainer.interactable = true;
            await _continueButton.OnClickAsync();
            _continueButtonContainer.interactable = false;
            await _continueButtonContainer.DOFade(0, 0.25f).AsyncWaitForCompletion();
            _continueButtonContainer.gameObject.SetActive(false);
        }

        private async UniTask<string> WaitForTextInput()
        {
            _textInputContainer.gameObject.SetActive(true);
            _textInputContainer.interactable = false;
            await _textInputContainer.DOFade(1, 0.4f).AsyncWaitForCompletion();
            _textInputContainer.interactable = true;
            _textInput.onValueChanged.AddListener(RefreshContinueButtonInteraction);
            
            await _continueButton.OnClickAsync();
            
            _textInput.onValueChanged.RemoveListener(RefreshContinueButtonInteraction);
            
            await _textInputContainer.DOFade(0, 0.25f).AsyncWaitForCompletion();

            return _textInput.text;

            void RefreshContinueButtonInteraction(string text)
            {
                if (string.IsNullOrEmpty(text))
                {
                    _continueButtonContainer.DOKill();
                    _continueButtonContainer
                        .DOFade(0, 0.25f)
                        .OnComplete(() => _continueButtonContainer.gameObject.SetActive(false));
                    _continueButtonContainer.interactable = false;
                }
                else
                {
                    _continueButtonContainer.DOKill();
                    _continueButtonContainer.gameObject.SetActive(true);
                    _continueButtonContainer.DOFade(1, 0.4f);
                    _continueButtonContainer.interactable = true;
                }
            }
        }

        #endregion
    }
}
