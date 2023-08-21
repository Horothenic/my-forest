using System;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace MyForest
{
    public interface ICameraRotationSource
    {
        IObservable<Unit> RotatedLeftObservable { get; }
        IObservable<Unit> RotatedRightObservable { get; }
        float CurrentRotationAngles { get; }
        void RotateLeft();
        void RotateRight();
        void ChangeCameraAngle(float anglesChange);
    }

    public interface ICameraFirstIntroSource
    {
        bool HasFirstIntroAlreadyPlayed { get; }
        void FirstIntroPlayed();
        IObservable<Unit> IntroFinishedObservable { get; }
        void IntroFinishedPlaying();
    }

    public interface ICameraGesturesControlSource
    {
        IObservable<Unit> EnableInputObservable { get; }
        void EnableInput();
        IObservable<Unit> BlockInputObservable { get; }
        void BlockInput();
        IObservable<Unit> SetDefaultZoomObservable { get; }
        void SetDefaultZoom();
        IObservable<IReadOnlyList<Vector3>> UpdateDragLimitsObservable { get; }
        void UpdateDragLimits(IReadOnlyList<Vector3> newPositions);
        void UpdateDragLimits(Vector3 newPosition);
    }

    public interface ICameraRepositionDataSource
    {
        IObservable<Vector3> NewCenterPositionObservable { get; }
    }
}
