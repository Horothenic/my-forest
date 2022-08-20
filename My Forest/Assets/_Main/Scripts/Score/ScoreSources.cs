using System;

namespace MyForest
{
    public interface IScoreDataSource
    {
        IObservable<ScoreData> ScoreChangedObservable { get; }
    }
}

namespace MyForest.Debug
{
    public interface IScoreDebugSource
    {
        void IncreaseScore(uint increment);
        void ResetScore();
    }
}
