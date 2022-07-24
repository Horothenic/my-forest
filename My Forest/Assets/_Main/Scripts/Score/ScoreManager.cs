using System;

using Newtonsoft.Json;
using Zenject;
using UniRx;

namespace MyForest
{
    public partial class ScoreManager
    {
        #region FIELDS

        private const string SCORE_DATA_KEY = "score_data";

        [Inject] private SaveManager _saveManager = null;

        private Subject<ScoreData> _scoreDataSubject = new Subject<ScoreData>();

        public IObservable<ScoreData> ScoreChangedObservable => _scoreDataSubject.AsObservable();
        public ScoreData CurrentScoreData { get; private set; }

        #endregion

        #region METHODS

        private void Initialize()
        {
            Load();
        }

        private void Load()
        {
            CurrentScoreData = _saveManager.Load<ScoreData>(SCORE_DATA_KEY);
            CurrentScoreData ??= new ScoreData();
        }

        private void Save()
        {
            _saveManager.Save(SCORE_DATA_KEY, CurrentScoreData);
        }

        private void NotifyChange()
        {
            _scoreDataSubject.OnNext(CurrentScoreData);
        }

        public void IncreaseScore(uint increment)
        {
            CurrentScoreData.IncreaseScore(increment);
            Save();
            NotifyChange();
        }

        public void ResetScore()
        {
            CurrentScoreData = new ScoreData();
            Save();
            NotifyChange();
        }

        #endregion
    }

    public partial class ScoreManager : IInitializable
    {
        void IInitializable.Initialize()
        {
            Initialize();
        }
    }

    [Serializable]
    public class ScoreData
    {
        public uint Score { get; private set; }

        public ScoreData() { }

        [JsonConstructor]
        public ScoreData(uint score)
        {
            Score = score;
        }

        public void IncreaseScore(uint increment)
        {
            Score += increment;
        }
    }
}
