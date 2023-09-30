using System;

using UniRx;

namespace MyForest
{
    public interface ITimersSource
    {
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
