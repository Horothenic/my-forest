using UnityEngine;
using UnityEngine.UI;

using Lean.Localization;
using Zenject;

namespace MyForest.UI
{
    public partial class TreeCollectionElementUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IVisualizerLoaderSource _visualizerLoaderSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private LeanLocalizedTextMeshProUGUI _displayName = null;
        [SerializeField] private LeanLocalizedTextMeshProUGUI _description = null;
        [SerializeField] private Button _previousButton = null;
        [SerializeField] private Button _nextButton = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private string _levelToken = null;

        private TreeConfiguration _currentElementConfiguration = null;
        private int _currentLevel = default;

        private void Awake()
        {
            _previousButton.onClick.AddListener(SeePreviousLevel);
            _nextButton.onClick.AddListener(SeeNextLevel);
        }

        private void SeePreviousLevel()
        {
            _currentLevel = Mathf.Max(0, _currentLevel - 1);
            RefreshLevel();
            RefreshVisualizer();
        }

        private void SeeNextLevel()
        {
            _currentLevel = Mathf.Min(_currentElementConfiguration.MaxLevel, _currentLevel + 1);
            RefreshLevel();
            RefreshVisualizer();
        }

        private void RefreshLevel()
        {
            _previousButton.gameObject.SetActive(_currentLevel > 0);
            _nextButton.gameObject.SetActive(_currentLevel < _currentElementConfiguration.MaxLevel);

            LeanLocalization.SetToken(_levelToken, (_currentLevel + 1).ToString());
        }

        private void RefreshVisualizer()
        {
            _visualizerLoaderSource.LoadVisualizer(_currentElementConfiguration.GetConfigurationLevel(_currentLevel).Prefab);
        }

        #endregion
    }

    public partial class TreeCollectionElementUI : ICollectionElementUI
    {
        Transform ICollectionElementUI.Transform => transform;

        void ICollectionElementUI.Load(object newElementConfiguration)
        {
            if (newElementConfiguration is not TreeConfiguration configuration) return;

            _currentElementConfiguration = configuration;

            _displayName.TranslationName = _currentElementConfiguration.DisplayName;
            _description.TranslationName = _currentElementConfiguration.Description;

            _currentLevel = default;
            RefreshLevel();
            RefreshVisualizer();
        }
    }
}
