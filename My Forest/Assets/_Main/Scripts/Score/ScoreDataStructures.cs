using System;

using Newtonsoft.Json;

namespace MyForest
{
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
