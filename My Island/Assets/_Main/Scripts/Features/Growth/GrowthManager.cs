using System;

namespace MyIsland
{
    public partial class GrowthManager : DataManager<GrowthData>
    {
        protected override string Key => "Growth";
        protected override SaveStyle SaveStyle => SaveStyle.Json;
    }

    public partial class GrowthManager : IGrowthSource
    {
        IObservable<GrowthData> IGrowthSource.DataObservables => DataObservable;
        GrowthData IGrowthSource.Data => Data;
        
        bool IGrowthSource.SpendGrowth(int amount)
        {
            if (Data.CurrentGrowth < amount)
            {
                return false;
            }
            
            Data.DecreaseGrowth(amount);
            SaveAndEmit();
            return true;
        }

        void IGrowthSource.ReclaimGrowth()
        {
            Data.IncreaseGrowth(1);
            SaveAndEmit();
        }
    }
    
    public partial class GrowthManager : IGrowthDebugSource
    {
        public void IncreaseGrowth(int amount)
        {
            Data.IncreaseGrowth(amount);
            SaveAndEmit();
        }

        public void ResetGrowth()
        {
            Data.ResetAll();
            SaveAndEmit();
        }
    }
}