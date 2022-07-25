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

        private DataSubject<ScoreData> _scoreDataSubject = new DataSubject<ScoreData>(new ScoreData());

        #endregion

        #region METHODS

        private void Initialize()
        {
            Load();
        }

        private void Load()
        {
            _scoreDataSubject.OnNext(_saveManager.Load<ScoreData>(SCORE_DATA_KEY));
        }

        private void Save()
        {
            _saveManager.Save(SCORE_DATA_KEY, _scoreDataSubject.Value);
        }

        public void IncreaseScore(uint increment)
        {
            _scoreDataSubject.Value.IncreaseScore(increment);
            _scoreDataSubject.OnNext();
            Save();
        }

        public void ResetScore()
        {
            _scoreDataSubject.OnNext(new ScoreData());
            Save();
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

    public partial class ScoreManager : IScorePointsUIDataSource
    {
        IObservable<ScoreData> IScorePointsUIDataSource.ScoreChangedObservable => _scoreDataSubject.AsObservable(true);
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
