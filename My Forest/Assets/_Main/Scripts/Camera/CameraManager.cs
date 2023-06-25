using System;
using System.Collections.Generic;
using Zenject;
using UniRx;

namespace MyForest
{
    public partial class CameraManager
    {
        #region FIELDS
        
        [Inject] private IGridDataSource _gridDataSource = null;

        private readonly Subject<Unit> _rotateLeftSubject = new ();
        private readonly Subject<Unit> _rotateRightSubject = new ();
        private readonly DataSubject<float> _rotationAnglesSubject = new (Constants.Camera.ROTATION_STEP_ANGLES);
        private readonly Subject<Unit> _introFinishedSubject = new ();
        private readonly Subject<Unit> _enableInputSubject = new ();
        private readonly Subject<Unit> _blockInputSubject = new ();
        private readonly Subject<Unit> _setDefaultZoomSubject = new ();
        private readonly Subject<IReadOnlyList<HexagonTile>> _updateDragLimitsSubject = new ();

        #endregion
    }

    public partial class CameraManager : DataManager<CameraData>
    {
        protected override string Key => Constants.Camera.CAMERA_DATA_KEY;
    }

    public partial class CameraManager : IInitializable
    {
        void IInitializable.Initialize()
        {
            Load();
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

        IObservable<IReadOnlyList<HexagonTile>> ICameraGesturesControlSource.UpdateDragLimitsObservable => _updateDragLimitsSubject.AsObservable();

        void ICameraGesturesControlSource.UpdateDragLimits(IReadOnlyList<HexagonTile> tiles)
        {
            _updateDragLimitsSubject.OnNext(tiles);
        }
    }
}
