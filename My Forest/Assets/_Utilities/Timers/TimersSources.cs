using System;

using UniRx;

namespace UnityEngine
{
    public interface ITimersSource
    {
        void RemoveTimer(string key);
        ITimer AddTimer(string key, DateTime targetTime, TimeSpan updateInterval);
        ITimer GetTimer(string key);
    }
    
    public interface ITimer
    {
        void RestartWithNewTargetTime(DateTime targetTime);
        IObservable<Unit> TimerCompletedObservable { get; }
        IObservable<TimeSpan> TimeLeftObservable { get; }
    }
}
