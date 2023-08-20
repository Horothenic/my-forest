using UnityEngine;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(ForestConfiguration), menuName = MENU_NAME)]
    public partial class ForestConfiguration : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Forest/" + nameof(ForestConfiguration);

        [Header("PROBABILITIES")]
        [SerializeField][Range(0, 1)] private float _endangeredThreshold = 0.05f;
        [SerializeField][Range(0, 1)] private float _exquisiteThreshold = 0.15f;
        [SerializeField][Range(0, 1)] private float _rareThreshold = 0.45f;

        #endregion
    }

    public partial class ForestConfiguration : IForestConfigurationSource
    {
        TreeRarity IForestConfigurationSource.GetRandomTreeRarity()
        {
            var randomValue = Random.value;

            if (randomValue <= _endangeredThreshold)
            {
                return TreeRarity.Endangered;
            }
            
            if (randomValue <= _exquisiteThreshold)
            {
                return TreeRarity.Exquisite;
            }
            
            if (randomValue <= _rareThreshold)
            {
                return TreeRarity.Rare;
            }

            return TreeRarity.Common;
        }
    }
}
