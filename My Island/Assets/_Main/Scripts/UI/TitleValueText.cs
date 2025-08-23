using TMPro;
using UnityEngine;

namespace MyIsland
{
    public class TitleValueText : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _valueText;

        #endregion

        #region METHODS

        public void SetTitle(string title)
        {
            _titleText.text = title;
        }

        public void SetValue(string value)
        {
            _valueText.text = value;
        }

        #endregion
    }
}
