using UnityEngine;
using UnityEngine.UI;

using Zenject;
using UniRx;
using TMPro;
using Lean.Localization;

namespace MyForest
{
    public class ForestSizeMenuUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IForestDataSource _forestDataSource = null;
        [Inject] private IGrowthDataSource _growthDataSource = null;
        [Inject] private IGrowthConfigurationsSource _growthConfigurationsSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private Button _openButton = null;
        [SerializeField] private Button _levelUpButton = null;
        [SerializeField] private TextMeshProUGUI _costText = null;
        [SerializeField] private BottomMenuOpenerUI _bottomMenuOpener = null;

        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region UNITY

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            _openButton.onClick.AddListener(Show);
            _levelUpButton.onClick.AddListener(LevelUpForestSize);
        }

        private void Show()
        {
            CheckState();
            _bottomMenuOpener.Appear();
        }

        private void LevelUpForestSize()
        {
            if (_forestDataSource.IsForestMaxSize) return;

            _forestDataSource.TryIncreaseForestSize();
            CheckState();
        }

        private void CheckState()
        {
            if (_forestDataSource.IsForestMaxSize)
            {
                OnGroundMaxLevel();
            }
            else if (!_growthDataSource.HaveEnoughGrowthForGroundLevelUp(_forestDataSource.CurrentForestSize))
            {
                OnInsufficientGrowth();
            }
            else
            {
                OnSufficientGrowth();
            }
        }

        private void OnGroundMaxLevel()
        {
            _costText.text = LocalizationExtensions.Localize(Constants.UI.IS_MAX_LEVEL_KEY);
            _levelUpButton.interactable = false;
        }

        private void OnInsufficientGrowth()
        {
            _costText.text = LocalizationExtensions.Localize(Constants.UI.GENERIC_COST_FORMAT_KEY, GetNextForestSizeCost());
            _levelUpButton.interactable = false;
        }

        private void OnSufficientGrowth()
        {
            _costText.text = LocalizationExtensions.Localize(Constants.UI.GENERIC_COST_FORMAT_KEY, GetNextForestSizeCost());
            _levelUpButton.interactable = true;
        }

        private uint GetNextForestSizeCost()
        {
            return _growthConfigurationsSource.GetNextForestSizeLevelCost(_forestDataSource.CurrentForestSize);
        }

        #endregion
    }
}
