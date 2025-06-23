using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;

namespace MyIsland
{
    public class TimedText : MonoBehaviour
    {
        #region FIELDS

        private const float DEFAULT_CHARACTER_INTERVAL = 0.08f;
        
        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _textComponent;

        #endregion

        #region METHODS

        private void Awake()
        {
            EmptyText();
        }

        public void EmptyText()
        {
            _textComponent.text = string.Empty;
        }
        
        public async UniTask TriggerTimedText(string text, float characterInterval = DEFAULT_CHARACTER_INTERVAL)
        {
            EmptyText();

            var visibleText = "";
            var insideTag = false;

            foreach (var c in text)
            {
                if (c == '<')
                {
                    insideTag = true;
                }

                if (insideTag)
                {
                    visibleText += c;

                    if (c == '>')
                    {
                        insideTag = false;
                    }

                    continue;
                }

                visibleText += c;
                _textComponent.text = visibleText;

                await UniTask.Delay(TimeSpan.FromSeconds(characterInterval));
            }
        }


        #endregion
    }
}
