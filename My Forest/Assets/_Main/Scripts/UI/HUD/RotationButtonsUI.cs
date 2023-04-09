using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace MyForest.UI
{
    public class RotationButtonsUI : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraRotationSource _cameraRotationSource = null;

        [Header("COMPONENTS")]
        [SerializeField] private Button _rotateLeftButton = null;
        [SerializeField] private Button _rotateRightButton = null;

        #endregion

        #region UNITY

        private void Start()
        {
            _rotateLeftButton.onClick.AddListener(_cameraRotationSource.RotateLeft);
            _rotateRightButton.onClick.AddListener(_cameraRotationSource.RotateRight);
        }

        #endregion
    }
}
