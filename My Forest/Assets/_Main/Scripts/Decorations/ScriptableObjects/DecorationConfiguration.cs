using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(DecorationConfiguration), menuName = MENU_NAME)]
    public class DecorationConfiguration : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Decorations/" + nameof(DecorationConfiguration);

        [SerializeField] private string _id = default;
        [SerializeField] private Biome _biome = default;
        [SerializeField] private GameObject _prefab = null;

        public string ID => _id;
        public Biome Biome => _biome;
        public GameObject Prefab => _prefab;

        #endregion
    }
}
