using System;
using UnityEngine;

using Newtonsoft.Json;
using UnityEngine.Serialization;

namespace MyForest
{
    [Serializable]
    public class GrowthData
    {
        public int CurrentGrowth { get; private set; }
        public DateTime NextClaimDateTime { get; private set; }
        public DateTime NextExtraClaimDateTime { get; private set; }

        public GrowthData() { }

        [JsonConstructor]
        public GrowthData(int currentGrowth, string nextClaimDateTime, string nextExtraClaimDateTime)
        {
            CurrentGrowth = currentGrowth;

            if (!string.IsNullOrEmpty(nextClaimDateTime))
            {
                NextClaimDateTime = DateTime.Parse(nextClaimDateTime).ToUniversalTime();
            }

            if (!string.IsNullOrEmpty(nextExtraClaimDateTime))
            {
                NextExtraClaimDateTime = DateTime.Parse(nextExtraClaimDateTime).ToUniversalTime();
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

        public void SetNextExtraClaimDateTime(int secondsToNextExtraClaimDate)
        {
            NextExtraClaimDateTime = DateTime.UtcNow + TimeSpan.FromSeconds(secondsToNextExtraClaimDate);
        }

        public bool IsDailyClaimAvailable()
        {
            return NextClaimDateTime < DateTime.UtcNow;
        }

        public bool IsDailyExtraClaimAvailable()
        {
            return NextExtraClaimDateTime < DateTime.UtcNow;
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
        [SerializeField] private int _growthToStart = default;
        [SerializeField] private int _growthInterval = default;

        public string Name => _name;
        public GrowthTrackEventType EventType => _eventType;
        public int GrowthToStart => _growthToStart;
        public int GrowthInterval => _growthInterval;
    }

    [Serializable]
    public class GrowthTrackEvent : IGrowthTrackEvent
    {
        [SerializeField] private string _name = default;
        [SerializeField] private GrowthTrackEventType _eventType = default;
        [SerializeField] private int _growthThreshold = default;

        public string Name => _name;
        public GrowthTrackEventType EventType => _eventType;
        public int GrowthThreshold => _growthThreshold;
    }

    public enum GrowthTrackEventType
    {
        NewTree,
        NewDecoration
    }
}
