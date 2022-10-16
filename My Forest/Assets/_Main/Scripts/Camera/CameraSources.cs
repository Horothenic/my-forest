using System;

namespace MyForest
{
    public interface ICameraRotationDataSource
    {
        IObservable<float> RotationAnglesObservable { get; }
        float CurrentRotationAngles { get; }
    }

    public interface ICameraRotationEventsSource
    {
        void OnCameraAnglesChanged(float anglesChange);
    }
}
