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
        public DateTime LastExtraClaimDateTime { get; private set; }

        [JsonIgnore]
        public double ExtraDailyGrowthSecondsLeft => (LastExtraClaimDateTime - DateTime.Now).TotalSeconds;

        public GrowthData() { }

        [JsonConstructor]
        public GrowthData(uint currentGrowth, uint allTimeGrowth, string lastClaimDateTime, string lastExtraClaimDateTime)
        {
            AllTimeGrowth = allTimeGrowth;
            CurrentGrowth = currentGrowth;

            if (!string.IsNullOrEmpty(lastClaimDateTime))
            {
                LastClaimDateTime = DateTime.Parse(lastClaimDateTime);
            }

            if (!string.IsNullOrEmpty(lastExtraClaimDateTime))
            {
                LastExtraClaimDateTime = DateTime.Parse(lastExtraClaimDateTime);
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

        public void SetNextExtraClaimDateTime(ulong secondsToNextExtraClaimDate)
        {
            LastExtraClaimDateTime = DateTime.Now + TimeSpan.FromSeconds(secondsToNextExtraClaimDate);
        }

        public bool IsDailyClaimAvailable()
        {
            var lastClaimDateTime = new DateTime(LastClaimDateTime.Year, LastClaimDateTime.Month, LastClaimDateTime.Day);
            var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            return DateTime.Compare(lastClaimDateTime, today) < 0;
        }

        public bool IsDailyExtraClaimAvailable()
        {
            return ExtraDailyGrowthSecondsLeft <= 0;
        }
    }
}
