using Reflex.Attributes;
using UnityEngine;

namespace MyIsland
{
    public class CameraController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private ICameraInputSource _cameraInputSource;

        #endregion

        #region METHODS

        private void Awake()
        {
            
        }

        #endregion
    }
}
