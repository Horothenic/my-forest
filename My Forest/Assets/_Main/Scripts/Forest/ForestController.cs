using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class ForestController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IObjectPoolSource _objectPoolSource = null;
        [Inject] private IForestDataSource _forestDataSource = null;
        [Inject] private IForestSizeConfigurationsSource _forestSizeConfigurationsSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform _root = null;
        [SerializeField] private ForestElement _forestElementPrefab = null;

        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region UNITY

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            _forestDataSource.CreatedForestObservable.Subscribe(BuildForest).AddTo(_disposables);
            _forestDataSource.IncreaseForestSizeLevelObservable.Subscribe(SetForestSize).AddTo(_disposables);
        }

        private void Dispose()
        {
            _disposables.Dispose();
        }

        private void BuildForest(ForestData forestData)
        {
            SetForestSize(forestData.SizeLevel);

            for (int i = 0; i < forestData.ForestElementsCount; i++)
            {
                var forestElementData = forestData.ForestElements[i];
                SetForestElement(forestElementData);
            }
        }

        private void SetForestSize(uint level)
        {
            var size = _forestSizeConfigurationsSource.GetDiameterByLevel(level);
            // TODO
        }

        private void SetForestElement(ForestElementData forestElementData)
        {
            var newForestElement = _objectPoolSource.Borrow(_forestElementPrefab);
            newForestElement.gameObject.Set(forestElementData.Position, _root);
            newForestElement.Initialize(forestElementData);
        }

        #endregion
    }
}
