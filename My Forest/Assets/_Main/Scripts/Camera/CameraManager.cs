using System;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace MyForest
{
    public partial class CameraManager
    {
        #region FIELDS
        
        private readonly Subject<Unit> _introStartedSubject = new Subject<Unit>();
        private readonly Subject<Unit> _introEndedSubject = new Subject<Unit>();
        
        private readonly Subject<Unit> _enableInputSubject = new Subject<Unit>();
        private readonly Subject<Unit> _blockInputSubject = new Subject<Unit>();
        private readonly Subject<Unit> _inputEndedSubject = new Subject<Unit>();
        
        private readonly Subject<Vector3> _positionSubject = new Subject<Vector3>();
        private readonly Subject<float> _rotationSubject = new Subject<float>();
        private readonly Subject<float> _zoomSubject = new Subject<float>();
        private readonly Subject<float> _angleSubject = new Subject<float>();
        
        private readonly Subject<IReadOnlyList<Vector3>> _updateDragLimitsSubject = new Subject<IReadOnlyList<Vector3>>();
        private readonly Subject<Vector3> _newCenterPositionSubject = new Subject<Vector3>();

        #endregion
    }

    public partial class CameraManager : DataManager<CameraData>
    {
        protected override string Key => Constants.Camera.CAMERA_DATA_KEY;
    }

    public partial class CameraManager : ICameraGesturesDataSource
    {
        float ICameraGesturesDataSource.CurrentRotation => Data.CurrentRotation;
        IObservable<float> ICameraGesturesDataSource.RotationObservable => _rotationSubject.AsObservable();
        void ICameraGesturesDataSource.SetRotation(float rotation)
        {
            Data.SetRotation(rotation);
            _rotationSubject.OnNext(rotation);
        }
        
        float ICameraGesturesDataSource.CurrentZoom => Data.CurrentZoom;
        IObservable<float> ICameraGesturesDataSource.ZoomObservable => _zoomSubject.AsObservable();
        void ICameraGesturesDataSource.SetZoom(float zoom)
        {
            Data.SetZoom(zoom);
            _zoomSubject.OnNext(zoom);
        }
        
        Vector3 ICameraGesturesDataSource.CurrentPosition => Data.CurrentPosition;
        IObservable<Vector3> ICameraGesturesDataSource.PositionObservable => _positionSubject.AsObservable();
        void ICameraGesturesDataSource.SetPosition(Vector3 position)
        {
            Data.SetPosition(position);
            _positionSubject.OnNext(position);
        }
        
        float ICameraGesturesDataSource.CurrentAngle => Data.CurrentAngle;
        IObservable<float> ICameraGesturesDataSource.AngleObservable => _angleSubject.AsObservable();
        void ICameraGesturesDataSource.SetAngle(float angle)
        {
            Data.SetAngle(angle);
            _angleSubject.OnNext(angle);
        }
    }

    public partial class CameraManager : ICameraIntroSource
    {
        bool ICameraIntroSource.HasFirstIntroAlreadyPlayed => Data.FirstIntroPlayed;
        
        void ICameraIntroSource.FirstIntroPlayed()
        {
            Data.SetFirstIntroPlayed();
            Save();
        }

        IObservable<Unit> ICameraIntroSource.IntroStartedObservable => _introStartedSubject.AsObservable();

        void ICameraIntroSource.IntroStarted()
        {
            _introStartedSubject.OnNext();
        }

        IObservable<Unit> ICameraIntroSource.IntroEndedObservable => _introEndedSubject.AsObservable();

        void ICameraIntroSource.IntroEnded()
        {
            _introEndedSubject.OnNext();
        }
    }

    public partial class CameraManager : ICameraGesturesControlSource
    {
        IObservable<Unit> ICameraGesturesControlSource.EnableInputObservable => _enableInputSubject.AsObservable();
        void ICameraGesturesControlSource.EnableInput()
        {
            _enableInputSubject.OnNext();
        }

        IObservable<Unit> ICameraGesturesControlSource.BlockInputObservable => _blockInputSubject.AsObservable();
        void ICameraGesturesControlSource.BlockInput()
        {
            _blockInputSubject.OnNext();
        }
        
        IObservable<Unit> ICameraGesturesControlSource.InputEndedObservable => _inputEndedSubject.AsObservable();
        void ICameraGesturesControlSource.InputEnded()
        {
            Save();
            _inputEndedSubject.OnNext();
        }
        
        IObservable<IReadOnlyList<Vector3>> ICameraGesturesControlSource.UpdateDragLimitsObservable => _updateDragLimitsSubject.AsObservable();

        void ICameraGesturesControlSource.UpdateDragLimits(IReadOnlyList<Vector3> newPositions)
        {
            _updateDragLimitsSubject.OnNext(newPositions);
        }

        void ICameraGesturesControlSource.UpdateDragLimits(Vector3 newPosition)
        {
            _updateDragLimitsSubject.OnNext(new[] { newPosition });
        }
    }

    public partial class CameraManager : ICameraRepositionDataSource
    {
        IObservable<Vector3> ICameraRepositionDataSource.NewCenterPositionObservable => _newCenterPositionSubject.AsObservable();
        
        void ICameraRepositionDataSource.RepositionCamera(Vector3 newPosition)
        {
            _newCenterPositionSubject.OnNext(newPosition);
        }
    }
}
