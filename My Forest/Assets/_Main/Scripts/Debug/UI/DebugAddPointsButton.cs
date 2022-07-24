using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace MyForest.Debug
{
    [RequireComponent(typeof(Button))]
    public class DebugAddPointsButton : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ScoreManager _scoreManager = null;

        [Header("COMPONENTS")]

        [Header("CONFIGURATIONS")]
        [SerializeField] private uint _pointsToIncrement = 1;

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
            _scoreManager.IncreaseScore(_pointsToIncrement);
        }

        #endregion
    }
}
