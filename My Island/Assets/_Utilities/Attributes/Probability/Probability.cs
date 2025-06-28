using System;

namespace UnityEngine
{
    [Serializable]
    public class Probability
    {
        #region FIELDS

        [SerializeField] private float[] _probabilities;
        
        private float[] _cumulativeProbabilities;

        #endregion
        
        #region METHODS

        private void CalculateCumulativeProbabilities()
        {
            _cumulativeProbabilities = new float[_probabilities.Length];
            _cumulativeProbabilities[0] = _probabilities[0];

            for (var i = 1; i < _probabilities.Length; i++)
            {
                _cumulativeProbabilities[i] = _cumulativeProbabilities[i - 1] + _probabilities[i];
            }
        }
        
        public TEnum Calculate<TEnum>() where TEnum : Enum
        {
            if (_cumulativeProbabilities == null || _cumulativeProbabilities.Length == 0)
            {
                CalculateCumulativeProbabilities();
            }

            if (_cumulativeProbabilities == null) return default;

            var randomValue = Random.value;

            for (var i = 0; i < _cumulativeProbabilities.Length; i++)
            {
                if (randomValue <= _cumulativeProbabilities[i])
                {
                    return (TEnum)Enum.ToObject(typeof(TEnum), i);
                }
            }

            return default;
        }

        #endregion
    }
}
