using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MyForest
{
    public partial class TimersManager : MonoBehaviour
    {
        #region FIELDS
        
        private readonly Dictionary<string, Timer> _timers = new Dictionary<string, Timer>();

        #endregion

        #region UNITY

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }

        #endregion

        #region METHODS
        
        private void Resume()
        {
            foreach (var timer in _timers.Values)
            {
                timer.Start();
            }
        }


        private void Pause()
        {
            foreach (var timer in _timers.Values)
            {
                timer.Stop();
            }
        }

        #endregion
    }

    public partial class TimersManager : ITimersSource
    {
        ITimer ITimersSource.AddTimer(string key, DateTime targetTime, TimeSpan updateInterval)
        {
            var newTimer = new Timer(targetTime, updateInterval);
            newTimer.Start();
            
            _timers.Add(key, newTimer);
            return newTimer;
        }

        ITimer ITimersSource.GetTimer(string key)
        {
            return _timers.TryGetValue(key, out var timer) ? timer : null;
        }
    }
}
