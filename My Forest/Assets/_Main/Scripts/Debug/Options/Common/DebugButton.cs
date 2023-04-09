using UnityEngine;
using UnityEngine.UI;

namespace MyForest.Debug
{
    [RequireComponent(typeof(Button))]
    public abstract class DebugButton : MonoBehaviour
    {
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

        protected abstract void OnClickHandler();

        #endregion
    }
}
