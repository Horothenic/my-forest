using System;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class ScoreManager
    {
        #region FIELDS

        private const string SCORE_DATA_KEY = "score_data";

        [Inject] private ISaveSource _saveSource = null;

        private DataSubject<ScoreData> _scoreDataSubject = new DataSubject<ScoreData>(new ScoreData());

        #endregion

        #region METHODS

        private void Initialize()
        {
            Load();
        }

        private void Load()
        {
            _scoreDataSubject.OnNext(_saveSource.Load<ScoreData>(SCORE_DATA_KEY));
        }

        private void Save()
        {
            _saveSource.Save(SCORE_DATA_KEY, _scoreDataSubject.Value);
        }

        private void IncreaseScore(uint increment)
        {
            _scoreDataSubject.Value.IncreaseScore(increment);
            _scoreDataSubject.OnNext();
            Save();
        }

        private void ResetScore()
        {
            _scoreDataSubject.OnNext(new ScoreData());
            Save();
        }

        #endregion
    }

    public partial class ScoreManager : IInitializable
    {
        void IInitializable.Initialize() => Initialize();
    }

    public partial class ScoreManager : IScoreDataSource
    {
        IObservable<ScoreData> IScoreDataSource.ScoreChangedObservable => _scoreDataSubject.AsObservable(true);
    }

    public partial class ScoreManager : Debug.IScoreDebugSource
    {
        void Debug.IScoreDebugSource.IncreaseScore(uint increment) => IncreaseScore(increment);
        void Debug.IScoreDebugSource.ResetScore() => ResetScore();
    }
}
