using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class GrowthData
    {
        public uint AllTimeGrowth { get; private set; }
        public uint CurrentGrowth { get; private set; }
        public DateTime LastClaimDateTime { get; private set; }

        public GrowthData() { }

        [JsonConstructor]
        public GrowthData(uint currentGrowth, uint allTimeGrowth, string lastClaimDateTime)
        {
            AllTimeGrowth = allTimeGrowth;
            CurrentGrowth = currentGrowth;

            if (!string.IsNullOrEmpty(lastClaimDateTime))
            {
                LastClaimDateTime = DateTime.Parse(lastClaimDateTime);
            }
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

        public void SetLastClaimDateTime(DateTime dateTime)
        {
            LastClaimDateTime = dateTime;
        }

        public bool IsDailyClaimAvailable()
        {
            var lastClaimDateTime = new DateTime(LastClaimDateTime.Year, LastClaimDateTime.Month, LastClaimDateTime.Day);
            var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            return DateTime.Compare(lastClaimDateTime, today) < 0;
        }
    }
}
