using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class GrowthData
    {
        public int CurrentGrowthDays { get; private set; }
        public DateTime LastClaimDateTime { get; private set; }
        public DateTime NextExtraClaimDateTime { get; private set; }

        [JsonIgnore]
        public double NextExtraDailyGrowthSecondsLeft
        {
            get
            {
                if (NextExtraClaimDateTime.Ticks == 0)
                {
                    return default;
                }
                else
                {
                    return (NextExtraClaimDateTime - DateTime.Now).TotalSeconds;
                }
            }
        }

        public GrowthData() { }

        [JsonConstructor]
        public GrowthData(int currentGrowthDays, string lastClaimDateTime, string nextExtraClaimDateTime)
        {
            CurrentGrowthDays = currentGrowthDays;

            if (!string.IsNullOrEmpty(lastClaimDateTime))
            {
                LastClaimDateTime = DateTime.Parse(lastClaimDateTime);
            }

            if (!string.IsNullOrEmpty(nextExtraClaimDateTime))
            {
                NextExtraClaimDateTime = DateTime.Parse(nextExtraClaimDateTime);
            }
        }

        public void IncreaseGrowth(int increment)
        {
            CurrentGrowthDays += increment;
        }

        public void SetLastClaimDateTime(DateTime dateTime)
        {
            LastClaimDateTime = dateTime;
        }

        public void SetNextExtraClaimDateTime(int secondsToNextExtraClaimDate)
        {
            NextExtraClaimDateTime = DateTime.Now + TimeSpan.FromSeconds(secondsToNextExtraClaimDate);
        }

        public bool IsDailyClaimAvailable()
        {
            var lastClaimDateTime = new DateTime(LastClaimDateTime.Year, LastClaimDateTime.Month, LastClaimDateTime.Day);
            var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            return DateTime.Compare(lastClaimDateTime, today) < 0;
        }

        public bool IsDailyExtraClaimAvailable()
        {
            return NextExtraDailyGrowthSecondsLeft <= 0;
        }
    }
}
