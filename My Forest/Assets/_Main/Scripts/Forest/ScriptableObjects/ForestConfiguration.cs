using UnityEngine;
using System;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(ForestConfiguration), menuName = MENU_NAME)]
    public partial class ForestConfiguration : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Forest/" + nameof(ForestConfiguration);

        [Header("PROBABILITIES")]
        [SerializeField][Range(0, 1)] private float _endangeredThreshold = 0.05f;
        [SerializeField][Range(0, 1)] private float _exquisiteThreshold = 0.10f;
        [SerializeField][Range(0, 1)] private float _rareThreshold = 0.30f;
        [SerializeField][Range(0, 1)] private float _commonThreshold = 0.55f;

        #endregion
    }

    public partial class ForestConfiguration : IForestConfigurationSource
    {
        TreeRarity IForestConfigurationSource.GetRandomTreeRarity()
        {
            var randomValue = UnityEngine.Random.value;

            if (randomValue >= _commonThreshold)
            {
                return TreeRarity.Common;
            }
            else if (randomValue >= _rareThreshold)
            {
                return TreeRarity.Rare;
            }
            else if (randomValue >= _exquisiteThreshold)
            {
                return TreeRarity.Exquisite;
            }

            return TreeRarity.Endangered;
        }
    }
}
