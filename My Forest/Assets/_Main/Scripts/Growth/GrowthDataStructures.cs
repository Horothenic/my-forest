using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class GrowthData
    {
        public uint PreviousGrowth { get; private set; }
        public uint CurrentGrowth { get; private set; }

        public GrowthData() { }

        [JsonConstructor]
        public GrowthData(uint growth)
        {
            PreviousGrowth = growth;
            CurrentGrowth = growth;
        }

        public void IncreaseGrowth(uint increment)
        {
            PreviousGrowth = CurrentGrowth;
            CurrentGrowth += increment;
        }
    }
}
