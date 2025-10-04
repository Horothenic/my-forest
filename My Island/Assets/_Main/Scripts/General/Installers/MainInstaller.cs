using Reflex.Core;
using UnityEngine;

namespace MyIsland
{
    public class MainInstaller : MonoBehaviour, IInstaller
    {
        [Header("INSTANCES")]
        [SerializeField] private MainMenuController _mainMenuController = null;
        [SerializeField] private CameraController _cameraController = null;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(_mainMenuController, typeof(IMenuSource));
            builder.AddSingleton(_cameraController, typeof(ICameraTargetSource));
        }
    }
}
