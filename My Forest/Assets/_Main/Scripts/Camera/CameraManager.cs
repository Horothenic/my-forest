using System;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class CameraManager
    {
        #region FIELDS

        private DataSubject<float> _rotationAnglesSubject = new DataSubject<float>(Constants.Camera.QUARTER_CIRCLE_ANGLE);
        private Subject<Unit> _introFinishedSubject = new Subject<Unit>();
        private Subject<Unit> _enableInputSubject = new Subject<Unit>();
        private Subject<Unit> _blockInputSubject = new Subject<Unit>();
        private Subject<Unit> _setDefaultZoomSubject = new Subject<Unit>();

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

    public partial class CameraManager : ICameraRotationDataSource
    {
        IObservable<float> ICameraRotationDataSource.RotationAnglesObservable => throw new NotImplementedException();
        float ICameraRotationDataSource.CurrentRotationAngles => _rotationAnglesSubject.Value;
    }

    public partial class CameraManager : ICameraRotationEventsSource
    {
        void ICameraRotationEventsSource.OnCameraAnglesChanged(float anglesChange)
        {
            _rotationAnglesSubject.OnNext(_rotationAnglesSubject.Value + anglesChange);
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
    }
}
