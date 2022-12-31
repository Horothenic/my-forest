using System;

using UniRx;

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

    public interface ICameraFirstIntroSource
    {
        bool HasFirstIntroAlreadyPlayed { get; }
        void FirstIntroPlayed();
        IObservable<Unit> IntroFinishedObservable { get; }
        void IntroFinishedPlaying();
    }
}
