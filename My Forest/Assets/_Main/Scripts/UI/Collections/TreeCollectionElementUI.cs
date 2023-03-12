using UnityEngine;

using Lean.Localization;

namespace MyForest
{
    public partial class TreeCollectionElementUI : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private LeanLocalizedTextMeshProUGUI _displayName = null;
        [SerializeField] private LeanLocalizedTextMeshProUGUI _description = null;
        
        private ForestElementConfiguration _currentElementConfiguration = null;

        #endregion
    }
    
    public partial class TreeCollectionElementUI : ICollectionElementUI
    {
        Transform ICollectionElementUI.Transform => transform;
        
        void ICollectionElementUI.Load(object newElementConfiguration)
        {
            if (newElementConfiguration is not ForestElementConfiguration configuration) return;
            if (_currentElementConfiguration == configuration) return;
            
            _currentElementConfiguration = configuration;
            
            _displayName.TranslationName = _currentElementConfiguration.DisplayName;
            _description.TranslationName = _currentElementConfiguration.Description;
        }
    }
}
