using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace MyForest.Debug
{
    [RequireComponent(typeof(Button))]
    public class DebugResetPointsButton : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ScoreManager _scoreManager = null;

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
            GetComponent<Button>().onClick.AddListener(OnClickHandler);
        }

        private void OnClickHandler()
        {
            _scoreManager.ResetScore();
        }

        #endregion
    }
}
