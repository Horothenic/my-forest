using TMPro;
using UnityEngine;

namespace MyIsland
{
    public class TitleText : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _titleText;

        #endregion

        #region METHODS

        public void SetTitle(string title)
        {
            _titleText.text = title;
        }

        #endregion
    }
}
