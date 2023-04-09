using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public class VisualizerController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IObjectPoolSource _objectPoolSource = null;
        [Inject] private IVisualizerLoaderSource _visualizerLoaderSource = null;
        
        [Header("COMPONENTS")]
        [SerializeField] private Transform _visualizerContainer = null;

        private GameObject _currentObject = null;

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
            _visualizerLoaderSource.VisualizerLoadedObservable.Subscribe(LoadObject).AddTo(this);
        }

        private void LoadObject(GameObject objectToLoad)
        {
            if (_currentObject != null)
            {
                _objectPoolSource.Return(_currentObject);
                _currentObject = null;
            }

            _currentObject = _objectPoolSource.Borrow(objectToLoad);
            _currentObject.SetLocal(Vector3.zero, _visualizerContainer);
        }

        #endregion
    }
}
