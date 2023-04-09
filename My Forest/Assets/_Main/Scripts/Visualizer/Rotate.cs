using UnityEngine;

namespace MyForest
{
    public class Rotate : MonoBehaviour
    {
        #region FIELDS

        [Header("CONFIGURATIONS")]
        [SerializeField] private Vector3 _velocity = default;

        #endregion

        #region UNITY

        private void Update()
        {
            transform.Rotate(_velocity * Time.deltaTime);
        }

        #endregion
    }
}
