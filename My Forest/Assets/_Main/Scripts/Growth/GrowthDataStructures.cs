using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class GrowthData
    {
        public uint AllTimeGrowth { get; private set; }
        public uint CurrentGrowth { get; private set; }

        public GrowthData() { }

        [JsonConstructor]
        public GrowthData(uint currentGrowth, uint allTimeGrowth)
        {
            AllTimeGrowth = allTimeGrowth;
            CurrentGrowth = currentGrowth;
        }

        public void IncreaseGrowth(uint increment)
        {
            CurrentGrowth += increment;
            AllTimeGrowth += increment;
        }

        public bool DecreaseGrowth(uint decrement)
        {
            if (CurrentGrowth < decrement)
            {
                return false;
            }

            CurrentGrowth -= decrement;

            return true;
        }
    }
}
