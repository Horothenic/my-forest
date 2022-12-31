using UnityEngine;

using Zenject;
using DG.Tweening;
using UniRx;

namespace MyForest
{
    public class CameraRepositionController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IForestElementMenuSource _forestElementMenuSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private Transform _cameraContainer = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Vector3 _offset = default;
        [SerializeField] private float repositionTime = default;

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
            _forestElementMenuSource.ForestElementMenuRequestedObservable.Subscribe(Appear).AddTo(_disposables);
        }

        private void Appear(ForestElementMenuRequest forestElementMenuRequest)
        {
            var sequence = DOTween.Sequence();

            var requesterPosition = forestElementMenuRequest.Requester.transform.position;
            var targetPosition = new Vector3(requesterPosition.x + _offset.x, _cameraContainer.position.y, requesterPosition.z + _offset.z);

            sequence.Append(_cameraContainer.DOMove(targetPosition, repositionTime));
        }

        #endregion
    }
}
