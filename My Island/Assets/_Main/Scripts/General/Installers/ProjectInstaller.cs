using Localization;
using Reflex.Core;
using UnityEngine;

namespace MyIsland
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [Header("INSTANCES")]
        [SerializeField] private CameraInputManager _cameraInputManager = null;
        [SerializeField] private ObjectPoolManager _objectPoolManager = null;
        [SerializeField] private TimersManager _timersManager = null;
        [SerializeField] private SceneManager _sceneManager = null;

        private GameObject _persistentParent;
        
        public void InstallBindings(ContainerBuilder builder)
        {
            CreatePersistentParent();
                
            builder.AddSingleton(CreateManager(_cameraInputManager), typeof(ICameraInputSource));
            builder.AddSingleton(CreateManager(_objectPoolManager), typeof(IObjectPoolSource));
            builder.AddSingleton(CreateManager(_timersManager), typeof(ITimersSource));
            builder.AddSingleton(CreateManager(_sceneManager), typeof(ISceneSource));
            
            builder.AddSingleton(typeof(LocalizationManager), typeof(ILocalizationSource));
            builder.AddSingleton(typeof(SaveManager), typeof(ISaveSource));
            builder.AddSingleton(typeof(IslandManager), typeof(IIslandSource));
            builder.AddSingleton(typeof(ForestManager), typeof(IForestSource));
        }

        private void CreatePersistentParent()
        {
            _persistentParent = new GameObject("PersistentManagers");
            DontDestroyOnLoad(_persistentParent);
        }

        private T CreateManager<T>(T managerPrefab) where T : Object
        {
            return Instantiate(managerPrefab, _persistentParent.transform);
        }
    }
}
