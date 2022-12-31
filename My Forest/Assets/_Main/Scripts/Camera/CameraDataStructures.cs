using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class CameraData
    {
        public bool FirstIntroPlayed { get; private set; }

        public CameraData() { }

        [JsonConstructor]
        public CameraData(bool firstIntroPlayed)
        {
            FirstIntroPlayed = firstIntroPlayed;
        }

        public void SetFirstIntroPlayed()
        {
            FirstIntroPlayed = true;
        }
    }
}
