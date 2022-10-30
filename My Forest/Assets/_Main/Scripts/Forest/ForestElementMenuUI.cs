using UnityEngine;
using UnityEngine.UI;

using Zenject;
using DG.Tweening;
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
        [SerializeField] private GameObject _mainContainer = null;
        [SerializeField] private RectTransform _container = null;
        [SerializeField] private Button _backgroundButton = null;
        [SerializeField] private Button _levelUpButton = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private float _appearTime = default;

        private CompositeDisposable _disposables = new CompositeDisposable();
        private ForestElementData _forestElementData = null;
        private Sequence _appearSequence = null;
        private Sequence _disappearSequence = null;

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
            _forestElementMenuSource.ForestElementMenuRequestedObservable.Subscribe(Appear).AddTo(_disposables);
            _levelUpButton.onClick.AddListener(LevelUpForestElement);
            _backgroundButton.onClick.AddListener(Disappear);
        }

        private void Appear(ForestElementMenuRequest forestElementMenuRequest)
        {
            _forestElementData = forestElementMenuRequest.ForestElementData;

            CheckState();

            _disappearSequence.Kill();
            _appearSequence = DOTween.Sequence();

            _appearSequence.AppendCallback(() => _mainContainer.SetActive(true));
            _appearSequence.Append(_container.DOAnchorPosY(default, _appearTime));
        }

        private void Disappear()
        {
            _appearSequence.Kill();
            _disappearSequence = DOTween.Sequence();

            _disappearSequence.Append(_container.DOAnchorPosY(-_container.rect.height, _appearTime));
            _disappearSequence.AppendCallback(() => _mainContainer.SetActive(false));
        }

        private void LevelUpForestElement()
        {
            if (_forestElementData.IsMaxLevel) return;

            _forestDataSource.TryIncreaseGrowthLevel(_forestElementData);
            CheckState();
        }

        private void CheckState()
        {
            if (_forestElementData.IsMaxLevel)
            {
                OnElementLoadedMaxLevel();
            }
            else if (!_growthDataSource.HaveEnoughGrowthForLevelUp(_forestElementData.Level))
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
