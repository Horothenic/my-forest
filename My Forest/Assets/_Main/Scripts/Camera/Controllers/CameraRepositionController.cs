using UnityEngine;
using Zenject;
using UniRx;
using DG.Tweening;

namespace MyForest
{
    public class CameraRepositionController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraRepositionDataSource _cameraRepositionDataSource = null;
        
        [Header("COMPONENTS")]
        [SerializeField] private Transform _cameraContainer = null;

        [Header("COMPONENTS")]
        [SerializeField] private float _transitionTime = 0.6f;

        #endregion

        #region UNITY

        private void Start()
        {
            _cameraRepositionDataSource.NewCenterPositionObservable.Subscribe(OnNewCenterPosition).AddTo(this);
        }

        #endregion

        #region METHODS

        private void OnNewCenterPosition(Vector3 newCenterPosition)
        {
            _cameraContainer.DOMove(newCenterPosition, _transitionTime);
        }

        #endregion
    }
}
