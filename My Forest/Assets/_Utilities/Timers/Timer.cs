using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace MyForest
{
    public partial class Timer
    {
        private DateTime _targetTime;
        private readonly TimeSpan _updateInterval;
        private readonly Subject<TimeSpan> _timeLeftSubject;
        private readonly Subject<Unit> _timerCompletedSubject;
        private CancellationTokenSource _cancellationTokenSource;

        public Timer(DateTime targetTime, TimeSpan updateInterval)
        {
            _targetTime = targetTime;
            _updateInterval = updateInterval;
            _timeLeftSubject = new Subject<TimeSpan>();
            _timerCompletedSubject = new Subject<Unit>();
        }
        
        public void Start()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            RoutineAsync(_cancellationTokenSource.Token).Forget();
        }
        
        private async UniTaskVoid RoutineAsync(CancellationToken cancellationToken)
        {
            if (_targetTime <= DateTime.UtcNow)
            {
                _timerCompletedSubject.OnNext();
                return;
            }
            
            var timeLeft = _targetTime - DateTime.UtcNow;
            _timeLeftSubject.OnNext(timeLeft);
            
            var reminder = (_targetTime - DateTime.UtcNow).TotalSeconds % _updateInterval.TotalSeconds;
            await UniTask.Delay(TimeSpan.FromSeconds(reminder), cancellationToken: cancellationToken);
            
            if (cancellationToken.IsCancellationRequested) return;
            
            while (!cancellationToken.IsCancellationRequested)
            {
                timeLeft = _targetTime - DateTime.UtcNow;
                _timeLeftSubject.OnNext(timeLeft);
                
                if (timeLeft <= TimeSpan.Zero)
                {
                    _timerCompletedSubject.OnNext();
                    return;
                }
                
                await UniTask.Delay(_updateInterval, ignoreTimeScale: true, cancellationToken: cancellationToken);
            }
        }
        
        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }
    }

    public partial class Timer : ITimer
    {
        IObservable<TimeSpan> ITimer.TimeLeftObservable => _timeLeftSubject.AsObservable();

        IObservable<Unit> ITimer.TimerCompletedObservable => _timerCompletedSubject.AsObservable();
        
        void ITimer.RestartWithNewTargetTime(DateTime targetTime)
        {
            _targetTime = targetTime;
            Start();
        }
    }
}
