using System.Linq;
using UnityEngine;

namespace MyIsland
{
    [CreateAssetMenu(fileName = nameof(DecorationConfiguration), menuName = MENU_NAME)]
    public class DecorationConfiguration : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyIsland) + "/Decorations/" + nameof(DecorationConfiguration);

        [SerializeField] private string _id;
        [SerializeField] private Biome _biome;
        [SerializeField] private Rarity _rarity;
        [SerializeField] private GameObject[] _variations;

        public string ID => _id;
        public Biome Biome => _biome;
        public Rarity Rarity => _rarity;
        public int AmountOfVariations => _variations.Length;

        public GameObject GetVariation(int variation)
        {
            try
            {
                return _variations[variation];
            }
            catch
            {
                return _variations.FirstOrDefault();
            }
        }

        #endregion
    }
}
