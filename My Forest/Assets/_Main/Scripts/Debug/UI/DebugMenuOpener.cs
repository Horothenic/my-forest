using UnityEngine;

namespace MyForest.Debug
{
    public class DebugMenuOpener : DebugButton
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private GameObject _debugContainer = null;

        #endregion

        #region UNITY

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                OnClickHandler();
            }
        }

        #endregion

        #region METHODS

        protected override void OnClickHandler()
        {
            _debugContainer.SetActive(!_debugContainer.activeSelf);
        }

        #endregion
    }
}
