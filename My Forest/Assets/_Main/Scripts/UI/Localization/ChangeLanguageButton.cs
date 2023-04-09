using UnityEngine;
using UnityEngine.UI;

using Lean.Localization;

namespace MyForest.UI
{
    [RequireComponent(typeof(Button))]
    public class ChangeLanguageButton : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private string _language = null;

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
            GetComponent<Button>().onClick.AddListener(ChangeLanguage);
        }

        private void ChangeLanguage()
        {
            LeanLocalization.SetCurrentLanguageAll(_language);
        }

        #endregion
    }
}
