using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;

namespace MyForest
{
    public class TimedText : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _textComponent;

        [Header("CONFIGURATIONS")]
        [SerializeField] private float _characterInterval = 0.3f;

        private string _text;

        #endregion

        #region METHODS

        private void Awake()
        {
            HideText();
        }

        private void HideText()
        {
            _text = _textComponent.text;
            _textComponent.text = string.Empty;
        }

        public async UniTask TriggerTimedText()
        {
            foreach (var character in _text)
            {
                _textComponent.text += character;

                await UniTask.Delay(TimeSpan.FromSeconds(_characterInterval));
            }
        }

        #endregion
    }
}
