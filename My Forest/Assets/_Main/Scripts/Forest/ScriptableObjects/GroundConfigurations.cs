using UnityEngine;
using System.Linq;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(GroundConfigurations), menuName = MENU_NAME)]
    public class GroundConfigurations : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Forest/" + nameof(GroundConfigurations);

        [SerializeField] private GameObject[] _grounds = null;

        public string ElementName => name;

        #endregion

        #region METHODS

        public GameObject GetGroundPrefab(string groundName)
        {
            return _grounds.FirstOrDefault(prefab => prefab.name == groundName);

            #endregion
        }
    }
}
