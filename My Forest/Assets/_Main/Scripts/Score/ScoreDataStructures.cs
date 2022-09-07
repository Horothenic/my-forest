using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class ScoreData
    {
        public uint PreviousScore { get; private set; }
        public uint CurrentScore { get; private set; }

        public ScoreData() { }

        [JsonConstructor]
        public ScoreData(uint score)
        {
            PreviousScore = score;
            CurrentScore = score;
        }

        public void IncreaseScore(uint increment)
        {
            PreviousScore = CurrentScore;
            CurrentScore += increment;
        }
    }
}
