using System;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class CameraManager
    {
        #region FIELDS

        [Inject] private IGridDataSource _gridDataSource = null;
        [Inject] private IGridPositioningSource _gridPositioningSource = null;

        private readonly Subject<Unit> _rotateLeftSubject = new Subject<Unit>();
        private readonly Subject<Unit> _rotateRightSubject = new Subject<Unit>();
        private readonly DataSubject<float> _rotationAnglesSubject = new DataSubject<float>(Constants.Camera.ROTATION_STEP_ANGLES);
        private readonly Subject<Unit> _introFinishedSubject = new Subject<Unit>();
        private readonly Subject<Unit> _enableInputSubject = new Subject<Unit>();
        private readonly Subject<Unit> _blockInputSubject = new Subject<Unit>();
        private readonly Subject<Unit> _setDefaultZoomSubject = new Subject<Unit>();
        private readonly Subject<IReadOnlyList<Vector3>> _updateDragLimitsSubject = new Subject<IReadOnlyList<Vector3>>();
        private readonly Subject<Vector3> _newCenterPositionSubject = new Subject<Vector3>();

        #endregion
        
        #region METHODS

        private void ReadjustCameraPosition(TileData tileData)
        {
            _newCenterPositionSubject.OnNext(_gridPositioningSource.GetWorldPosition(tileData.Coordinates));
        }
        
        #endregion
    }

    public partial class CameraManager : DataManager<CameraData>
    {
        protected override string Key => Constants.Camera.CAMERA_DATA_KEY;

        protected override void Initialize()
        {
            _gridDataSource.NewTileAddedObservable.Subscribe(ReadjustCameraPosition).AddTo(_disposables);
        }
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

    public partial class CameraManager : ICameraFirstIntroSource
    {
        bool ICameraFirstIntroSource.HasFirstIntroAlreadyPlayed => Data.FirstIntroPlayed;

        IObservable<Unit> ICameraFirstIntroSource.IntroFinishedObservable => _introFinishedSubject.AsObservable();

        void ICameraFirstIntroSource.FirstIntroPlayed()
        {
            Data.SetFirstIntroPlayed();
            Save();
        }

        void ICameraFirstIntroSource.IntroFinishedPlaying()
        {
            _introFinishedSubject.OnNext();
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

        IObservable<Unit> ICameraGesturesControlSource.SetDefaultZoomObservable => _setDefaultZoomSubject.AsObservable();

        void ICameraGesturesControlSource.SetDefaultZoom()
        {
            _setDefaultZoomSubject.OnNext();
        }

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
    }
}
