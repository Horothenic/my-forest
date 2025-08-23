using Localization;
using MyIsland;
using Reflex.Attributes;
using UnityEngine;

namespace MyForest
{
    public class GrowthPage : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IGrowthSource _growthSource;

        [Header("COMPONENTS")]
        [SerializeField] private TitleText _titleText;
        [SerializeField] private TitleValueText _currentGrowthText;
        [SerializeField] private TitleValueText _allTimeGrowthText;

        #endregion

        #region METHODS

        private void Start()
        {
            Load();
        }

        private void OnEnable()
        {
            RefreshPage();
        }

        private void Load()
        {
            _titleText.SetTitle("Growth".Localize());
            _currentGrowthText.SetTitle("Current Growth".Localize());
            _allTimeGrowthText.SetTitle("All Time Growth".Localize());
        }

        private void RefreshPage()
        {
            _currentGrowthText.SetValue(_growthSource.Data.CurrentGrowth.ToString());
            _allTimeGrowthText.SetValue(_growthSource.Data.AllTimeGrowth.ToString());
        }

        #endregion
    }
}
