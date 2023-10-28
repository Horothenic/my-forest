using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class CameraData
    {
        public bool FirstIntroPlayed { get; private set; }
        public float CurrentZoom { get; private set; } = -1;

        public CameraData() { }

        [JsonConstructor]
        public CameraData(bool firstIntroPlayed, float currentZoom)
        {
            FirstIntroPlayed = firstIntroPlayed;
            CurrentZoom = currentZoom;
        }

        public void SetFirstIntroPlayed()
        {
            FirstIntroPlayed = true;
        }

        public void SetCurrentZoom(float currentZoom)
        {
            CurrentZoom = currentZoom;
        }
    }
}
