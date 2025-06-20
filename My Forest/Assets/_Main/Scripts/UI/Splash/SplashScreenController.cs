using Cysharp.Threading.Tasks;
using System;
using Localization;
using UnityEngine;
using Zenject;

namespace MyIsland
{
    public class SplashScreenController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IIslandSource _islandSource;
        [Inject] private ISceneSource _sceneSource;

        [Header("COMPONENTS")]
        [SerializeField] private TimedText _madeByText;

        #endregion

        #region METHODS

        private void Start()
        {
            TriggerSplashSequence().Forget();
        }

        private async UniTaskVoid TriggerSplashSequence()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            await _madeByText.TriggerTimedText("made by Horothenic".Localize(), 0.1f);
            
            await UniTask.Delay(TimeSpan.FromSeconds(0.8f));

            if (_islandSource.HasCreatorName)
            {
                _sceneSource.LoadScene("Main");
            }
            else
            {
                _sceneSource.LoadScene("TellMeYourName");
            }
        }

        #endregion
    }
}
