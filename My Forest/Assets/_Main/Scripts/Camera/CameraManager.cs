using System;

using UniRx;

namespace MyForest
{
    public partial class CameraManager
    {
        #region FIELDS

        private const float QUARTER_CIRCLE_ANGLE = 45f;

        private DataSubject<float> _rotationAnglesSubject = new DataSubject<float>(QUARTER_CIRCLE_ANGLE);

        #endregion

        #region METHODS

        private void OnCameraAnglesChanged(float anglesChange)
        {
            _rotationAnglesSubject.OnNext(_rotationAnglesSubject.Value + anglesChange);
        }

        #endregion
    }

    public partial class CameraManager : ICameraRotationDataSource
    {
        IObservable<float> ICameraRotationDataSource.RotationAnglesObservable => throw new NotImplementedException();
        float ICameraRotationDataSource.CurrentRotationAngles => _rotationAnglesSubject.Value;
    }

    public partial class CameraManager : ICameraRotationEventsSource
    {
        void ICameraRotationEventsSource.OnCameraAnglesChanged(float anglesChange) => OnCameraAnglesChanged(anglesChange);
    }
}
