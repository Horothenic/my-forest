using System;
using UnityEngine;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class GrowthData
    {
        public int CurrentGrowth { get; private set; }
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
        public GrowthData(int currentGrowth, string lastClaimDateTime, string nextExtraClaimDateTime)
        {
            CurrentGrowth = currentGrowth;

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
            CurrentGrowth += increment;
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

    public interface IGrowthTrackEvent
    {
        string Name { get; }
        public GrowthTrackEventType EventType { get; }
    }

    [Serializable]
    public class GrowthTrackRecurringEvent : IGrowthTrackEvent
    {
        [SerializeField] private string _name = default;
        [SerializeField] private GrowthTrackEventType _eventType = default;
        [SerializeField] private int _startDay = default;
        [SerializeField] private int _daysInterval = default;

        public string Name => _name;
        public GrowthTrackEventType EventType => _eventType;
        public int StartDay => _startDay;
        public int DaysInterval => _daysInterval;
    }

    [Serializable]
    public class GrowthTrackEvent : IGrowthTrackEvent
    {
        [SerializeField] private string _name = default;
        [SerializeField] private GrowthTrackEventType _eventType = default;
        [SerializeField] private int _dayForEvent = default;

        public string Name => _name;
        public GrowthTrackEventType EventType => _eventType;
        public int DayForEvent => _dayForEvent;
    }

    public enum GrowthTrackEventType
    {
        NewTree
    }
}
