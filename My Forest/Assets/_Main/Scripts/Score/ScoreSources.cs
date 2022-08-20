using System;

namespace MyForest
{
    public interface IScoreDataSource
    {
        IObservable<ScoreData> ScoreChangedObservable { get; }
    }

    public interface IScoreDebugSource
    {
        void IncreaseScore(uint increment);
        void ResetScore();
    }
}
