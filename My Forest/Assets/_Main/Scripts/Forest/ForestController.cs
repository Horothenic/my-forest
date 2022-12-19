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
        [Inject] private IForestAddDataSource _forestAddDataSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private GroundConfigurations _groundConfigurations = null;
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
            _forestDataSource.IncreaseGroundLevelObservable.Subscribe(IncreaseGroundWidth).AddTo(_disposables);
        }

        private void Dispose()
        {
            _disposables.Dispose();
        }

        private void BuildForest(ForestData forestData)
        {
            for (int i = 0; i < forestData.GroundElementsCount; i++)
            {
                var groundElementData = forestData.GroundElements[i];
                SetGroundElement(groundElementData);
            }

            for (int i = 0; i < forestData.ForestElementsCount; i++)
            {
                var forestElementData = forestData.ForestElements[i];
                SetForestElement(forestElementData);
            }
        }

        private void SetGroundElement(GroundElementData groundElementData)
        {
            var prefab = _groundConfigurations.GetGroundPrefab(groundElementData?.GroundName);
            _objectPoolSource.Borrow(prefab).Set(groundElementData.Position, _root);
        }

        private void SetForestElement(ForestElementData forestElementData)
        {
            var newForestElement = _objectPoolSource.Borrow(_forestElementPrefab);
            newForestElement.gameObject.Set(forestElementData.Position, _root);
            newForestElement.Initialize(forestElementData);
        }

        private void IncreaseGroundWidth(uint level)
        {
            void CreateGroundElement(int row, int column)
            {
                var prefabName = _groundConfigurations.GetRandomGroundName();
                var newGroundElement = new GroundElementData(prefabName, new Vector3(row, default, column));

                _forestAddDataSource.AddGroundElement(newGroundElement);

                SetGroundElement(newGroundElement);
            }

            var offset = (int)(level);

            for (int column = -offset; column <= offset; column++)
            {
                CreateGroundElement(-offset, column);
                CreateGroundElement(offset, column);
            }


            for (int row = -offset + 1; row <= offset - 1; row++)
            {
                CreateGroundElement(row, -offset);
                CreateGroundElement(row, offset);
            }
        }

        #endregion
    }
}
