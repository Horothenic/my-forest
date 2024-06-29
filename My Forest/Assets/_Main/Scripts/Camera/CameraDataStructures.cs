using System;
using UnityEngine;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class CameraData
    {
        public bool FirstIntroPlayed { get; private set; }
        public float CurrentZoom { get; private set; } = -1;
        public float CurrentRotation { get; private set; } = 0;
        public float CurrentAngle { get; private set; } = 0;
        public SerializedVector3 CurrentPosition { get; private set; }

        public CameraData() { }

        [JsonConstructor]
        public CameraData(bool firstIntroPlayed, float currentZoom, float currentRotation, float currentAngle, SerializedVector3 currentPosition)
        {
            FirstIntroPlayed = firstIntroPlayed;
            CurrentZoom = currentZoom;
            CurrentRotation = currentRotation;
            CurrentPosition = currentPosition;
            CurrentAngle = currentAngle;
        }

        public void SetFirstIntroPlayed()
        {
            FirstIntroPlayed = true;
        }

        public void SetZoom(float zoom)
        {
            CurrentZoom = zoom;
        }

        public void SetRotation(float rotation)
        {
            CurrentRotation = rotation;
        }

        public void SetPosition(Vector3 position)
        {
            CurrentPosition = position;
        }

        public void SetAngle(float angle)
        {
            CurrentAngle = angle;
        }
    }
}
