using Unity.Cinemachine;

namespace MyIsland
{
    public enum CameraIndex
    {
        Island,
        Plant
    }

    public class CameraData
    {
        public CinemachineCamera Camera { get; }
        public CinemachineOrbitalFollow OrbitalFollow { get; }

        public CameraData(CinemachineCamera camera)
        {
            Camera = camera;
            OrbitalFollow = camera.GetComponent<CinemachineOrbitalFollow>();
        }
    }
}
