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
        [SerializeField][Range(0, 1)] private float _endangeredProbability = 0.05f;
        [SerializeField][Range(0, 1)] private float _exquisiteProbability = 0.10f;
        [SerializeField][Range(0, 1)] private float _rareProbability = 0.30f;
        [SerializeField][Range(0, 1)] private float _commonProbability = 0.55f;

        #endregion
    }

    public partial class ForestConfiguration : IForestConfigurationSource
    {
        TreeRarity IForestConfigurationSource.GetRandomTreeRarity()
        {
            var randomValue = UnityEngine.Random.value;

            if (randomValue >= _commonProbability)
            {
                return TreeRarity.Common;
            }
            else if (randomValue >= _rareProbability)
            {
                return TreeRarity.Rare;
            }
            else if (randomValue >= _commonProbability)
            {
                return TreeRarity.Exquisite;
            }

            return TreeRarity.Endangered;
        }
    }
}
