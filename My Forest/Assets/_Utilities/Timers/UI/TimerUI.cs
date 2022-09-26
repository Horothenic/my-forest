using System;
using System.Threading;

using TMPro;
using Cysharp.Threading.Tasks;
using UniRx;

namespace UnityEngine.UI
{
    public class TimerUI : MonoBehaviour
    {
        #region FIELDS

        [Header("COMPONENTS")]
        [SerializeField] private TextMeshProUGUI _text = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private string _hourTimerFormat = "{2:00}:{1:00}:{0:00}";
        [SerializeField] private string minuteTimerFormat = "{1:00}:{0:00}";

        private Subject<Unit> _timerCompleteSubject = new Subject<Unit>();
        private CancellationTokenSource cancellationTokenSource = null;

        public IObservable<Unit> TimerCompleteObservable => _timerCompleteSubject.AsObservable();

        #endregion

        #region UNITY

        private void Start()
        {
            ShowText(false);
        }

        #endregion

        #region METHODS

        public void StartTimer(double seconds)
        {
            StopTimer();
            ShowText(true);

            if (seconds < 0)
            {
                seconds = 0;
            }

            StartTimerRoutineAsync((ulong)seconds, cancellationTokenSource.Token).Forget();
        }

        public void StopTimer()
        {
            ShowText(false);
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
        }

        private async UniTaskVoid StartTimerRoutineAsync(ulong seconds, CancellationToken cancellationToken)
        {
            for (ulong i = seconds; i > 0; i--)
            {
                if (cancellationToken.IsCancellationRequested) return;

                FormatTimeLeft(i);

                await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: true, cancellationToken: cancellationToken);
            }

            CompleteTimer();
        }

        private void CompleteTimer()
        {
            ShowText(false);
            _timerCompleteSubject.OnNext();
        }

        private void FormatTimeLeft(ulong secondsLeft)
        {
            if (_text == null) return;

            var hours = secondsLeft / 3600;
            var minutes = secondsLeft / 60;
            var seconds = secondsLeft % 60;

            var format = hours == 0 ? minuteTimerFormat : _hourTimerFormat;

            _text.text = string.Format(format, seconds, minutes, hours);
        }

        private void ShowText(bool show)
        {
            _text.gameObject.SetActive(show);
        }

        #endregion
    }
}
