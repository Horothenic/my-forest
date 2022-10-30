using UnityEngine;
using UnityEngine.EventSystems;

using Zenject;
using UniRx;

namespace MyForest
{
    public class ForestElement : MonoBehaviour, IPointerClickHandler
    {
        #region FIELDS

        [Inject] private IObjectPoolSource _objectPoolSource = null;
        [Inject] private IForestDataSource _forestDataSource = null;
        [Inject] private IForestElementMenuSource _forestElementMenuSource = null;

        private ForestElementData _forestElementData = null;
        private GameObject _currentElement = null;
        private CompositeDisposable _disposables = new CompositeDisposable();

        #endregion

        #region UNITY

        public void OnPointerClick(PointerEventData eventData)
        {
            RequestForestElementMenu();
        }

        #endregion

        #region METHODS

        public void Initialize(ForestElementData forestElementData)
        {
            _forestElementData = forestElementData;
            _forestDataSource.GetForestElementDataObservable(forestElementData).Subscribe(OnForestElementDataUpdated).AddTo(_disposables);
            SpawnCurrentLevelElement();
        }

        private void SpawnCurrentLevelElement()
        {
            var currentLevelPrefab = _forestElementData.Configuration.GetLevelPrefab(_forestElementData.Level);
            _currentElement = _objectPoolSource.Borrow(currentLevelPrefab);
            _currentElement.SetLocal(Vector3.zero, transform);
        }

        private void OnForestElementDataUpdated(ForestElementData forestElementData)
        {
            _forestElementData = forestElementData;
            _objectPoolSource.Return(_currentElement);
            SpawnCurrentLevelElement();
        }

        private void RequestForestElementMenu()
        {
            _forestElementMenuSource.ResquestForestElementMenu(new ForestElementMenuRequest(gameObject, _forestElementData));
        }

        #endregion
    }
}
