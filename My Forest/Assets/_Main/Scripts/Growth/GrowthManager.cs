using System;

using Zenject;

namespace MyForest
{
    public partial class GrowthManager
    {
        #region FIELDS

        [Inject] private IGrowthConfigurationsSource _configurations = null;

        #endregion

        #region METHODS

        private void IncreaseGrowth(int increment)
        {
            Data.IncreaseGrowth(increment);
            EmitData();
            Save();
        }
        
        private void TryClaimDailyGrowth()
        {
            if (!Data.IsDailyClaimAvailable()) return;

            Data.SetNextClaimDateTime(DateTime.UtcNow + TimeSpan.FromDays(1));
            IncreaseGrowth(_configurations.DailyGrowth);
        }

        #endregion
    }

    public partial class GrowthManager : DataManager<GrowthData>
    {
        protected override string Key => Constants.Growth.GROWTH_DATA_KEY;

        protected override void Initialize()
        {
            TryClaimDailyGrowth();
        }
    }

    public partial class GrowthManager : IGrowthDataSource
    {
        GrowthData IGrowthDataSource.GrowthData => Data;
        IObservable<GrowthData> IGrowthDataSource.GrowthChangedObservable => LoadObservable;
    }

    public partial class GrowthManager : Debug.IGrowthDebugSource
    {
        void Debug.IGrowthDebugSource.IncreaseGrowth(int increment) => IncreaseGrowth(increment);
    }
}
