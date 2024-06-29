using System;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace MyForest
{
    public interface ICameraIntroSource
    {
        bool HasFirstIntroAlreadyPlayed { get; }
        void FirstIntroPlayed();
        IObservable<Unit> IntroStartedObservable { get; }
        void IntroStarted();
        IObservable<Unit> IntroEndedObservable { get; }
        void IntroEnded();
    }
    
    public interface ICameraGesturesDataSource
    {
        float CurrentRotation { get; }
        IObservable<float> RotationObservable { get; }
        void SetRotation(float rotation);
        float CurrentZoom { get; }
        IObservable<float> ZoomObservable { get; }
        void SetZoom(float zoom);
        Vector3 CurrentPosition { get; }
        IObservable<Vector3> PositionObservable { get; }
        void SetPosition(Vector3 position);
        float CurrentAngle { get; }
        IObservable<float> AngleObservable { get; }
        void SetAngle(float angle);
    }

    public interface ICameraGesturesControlSource
    {
        IObservable<Unit> EnableInputObservable { get; }
        void EnableInput();
        IObservable<Unit> BlockInputObservable { get; }
        void BlockInput();
        IObservable<Unit> InputEndedObservable { get; }
        void InputEnded();
        IObservable<IReadOnlyList<Vector3>> UpdateDragLimitsObservable { get; }
        void UpdateDragLimits(IReadOnlyList<Vector3> newPositions);
        void UpdateDragLimits(Vector3 newPosition);
    }

    public interface ICameraRepositionDataSource
    {
        IObservable<Vector3> NewCenterPositionObservable { get; }
        void RepositionCamera(Vector3 newPosition);
    }
}
