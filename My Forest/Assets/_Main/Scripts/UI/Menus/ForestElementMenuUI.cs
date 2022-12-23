using UnityEngine;
using UnityEngine.UI;

using Zenject;
using UniRx;

namespace MyForest
{
    public class ForestElementMenuUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IForestElementMenuSource _forestElementMenuSource = null;
        [Inject] private IForestDataSource _forestDataSource = null;
        [Inject] private IGrowthDataSource _growthDataSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private Button _levelUpButton = null;
        [SerializeField] private BottomMenuOpenerUI _bottomMenuOpener = null;

        private CompositeDisposable _disposables = new CompositeDisposable();
        private ForestElementData _forestElementData = null;

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
            _forestElementMenuSource.ForestElementMenuRequestedObservable.Subscribe(Show).AddTo(_disposables);
            _levelUpButton.onClick.AddListener(LevelUpForestElement);
        }

        private void Show(ForestElementMenuRequest forestElementMenuRequest)
        {
            _forestElementData = forestElementMenuRequest.ForestElementData;
            CheckState();
            _bottomMenuOpener.Appear();
        }

        private void LevelUpForestElement()
        {
            if (_forestElementData.IsMaxLevel) return;

            _forestDataSource.TryIncreaseForestElementLevel(_forestElementData);
            CheckState();
        }

        private void CheckState()
        {
            if (_forestElementData.IsMaxLevel)
            {
                OnElementLoadedMaxLevel();
            }
            else if (!_growthDataSource.HaveEnoughGrowthForElementLevelUp(_forestElementData.Level))
            {
                OnInsufficientGrowth();
            }
            else
            {
                OnSufficientGrowth();
            }
        }

        private void OnElementLoadedMaxLevel()
        {
            _levelUpButton.interactable = false;
        }

        private void OnInsufficientGrowth()
        {
            _levelUpButton.interactable = false;
        }

        private void OnSufficientGrowth()
        {
            _levelUpButton.interactable = true;
        }

        #endregion
    }
}
