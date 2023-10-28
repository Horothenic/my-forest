using System;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace MyForest
{
    public partial class CameraManager
    {
        #region FIELDS
        
        private readonly Subject<Unit> _rotateLeftSubject = new Subject<Unit>();
        private readonly Subject<Unit> _rotateRightSubject = new Subject<Unit>();
        private readonly DataSubject<float> _rotationAnglesSubject = new DataSubject<float>(Constants.Camera.ROTATION_STEP_ANGLES);
        private readonly Subject<Unit> _introStartedSubject = new Subject<Unit>();
        private readonly Subject<Unit> _introEndedSubject = new Subject<Unit>();
        private readonly Subject<Unit> _enableInputSubject = new Subject<Unit>();
        private readonly Subject<Unit> _blockInputSubject = new Subject<Unit>();
        private readonly Subject<(float zoom, bool withTransition)> _setZoomSubject = new Subject<(float zoom, bool withTransition)>();
        private readonly Subject<IReadOnlyList<Vector3>> _updateDragLimitsSubject = new Subject<IReadOnlyList<Vector3>>();
        private readonly Subject<Vector3> _newCenterPositionSubject = new Subject<Vector3>();

        #endregion
    }

    public partial class CameraManager : DataManager<CameraData>
    {
        protected override string Key => Constants.Camera.CAMERA_DATA_KEY;
    }

    public partial class CameraManager : ICameraRotationSource
    {
        float ICameraRotationSource.CurrentRotationAngles => _rotationAnglesSubject.Value;
        IObservable<Unit> ICameraRotationSource.RotatedLeftObservable => _rotateLeftSubject.AsObservable();
        IObservable<Unit> ICameraRotationSource.RotatedRightObservable => _rotateRightSubject.AsObservable();

        void ICameraRotationSource.ChangeCameraAngle(float anglesChange)
        {
            _rotationAnglesSubject.OnNext(_rotationAnglesSubject.Value + anglesChange);
        }

        void ICameraRotationSource.RotateLeft()
        {
            _rotateLeftSubject.OnNext();
        }

        void ICameraRotationSource.RotateRight()
        {
            _rotateRightSubject.OnNext();
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

        IObservable<(float zoom, bool withTransition)> ICameraGesturesControlSource.ZoomObservable => _setZoomSubject.AsObservable();

        void ICameraGesturesControlSource.SetZoom(float newZoom, bool withTransition)
        {
            Data.SetCurrentZoom(newZoom);
            Save();
            
            _setZoomSubject.OnNext((newZoom, withTransition));
        }

        float ICameraGesturesControlSource.GetCurrentZoom => Data.CurrentZoom;

        IObservable<IReadOnlyList<Vector3>> ICameraGesturesControlSource.UpdateDragLimitsObservable => _updateDragLimitsSubject.AsObservable();

        void ICameraGesturesControlSource.UpdateDragLimits(IReadOnlyList<Vector3> newPositions)
        {
            _updateDragLimitsSubject.OnNext(newPositions);
        }

        void ICameraGesturesControlSource.UpdateDragLimits(Vector3 newPosition)
        {
            _updateDragLimitsSubject.OnNext(new[] {newPosition});
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
