using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class GrowthData
    {
        public int CurrentGrowth { get; private set; }
        public DateTime NextClaimDateTime { get; private set; }

        public GrowthData() { }

        [JsonConstructor]
        public GrowthData(int currentGrowth, string nextClaimDateTime, string nextExtraClaimDateTime)
        {
            CurrentGrowth = currentGrowth;

            if (!string.IsNullOrEmpty(nextClaimDateTime))
            {
                NextClaimDateTime = DateTime.Parse(nextClaimDateTime).ToUniversalTime();
            }
        }

        public void IncreaseGrowth(int increment)
        {
            CurrentGrowth += increment;
        }

        public void SetNextClaimDateTime(DateTime dateTime)
        {
            NextClaimDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public bool IsDailyClaimAvailable()
        {
            return NextClaimDateTime < DateTime.UtcNow;
        }
    }
}
