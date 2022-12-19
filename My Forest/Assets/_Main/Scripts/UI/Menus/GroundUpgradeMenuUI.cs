using UnityEngine;
using UnityEngine.UI;

using Zenject;
using UniRx;

namespace MyForest
{
    public class GroundUpgradeMenuUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IForestDataSource _forestDataSource = null;
        [Inject] private IGrowthDataSource _growthDataSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private Button _openButton = null;
        [SerializeField] private Button _levelUpButton = null;
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
        }

        private void Show()
        {
            CheckState();
            _bottomMenuOpener.Appear();
        }

        private void CheckState()
        {
            if (_forestDataSource.IsGroundMaxLevel)
            {
                OnGroundMaxLevel();
            }
            else if (!_growthDataSource.HaveEnoughGrowthForGroundLevelUp(_forestDataSource.CurrentGroundLevel))
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
