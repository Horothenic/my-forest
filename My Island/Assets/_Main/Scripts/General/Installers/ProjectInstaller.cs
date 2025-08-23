using Localization;
using Reflex.Core;
using UnityEngine;

namespace MyIsland
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [Header("INSTANCES")]
        [SerializeField] private ObjectPoolManager _objectPoolManager = null;
        [SerializeField] private TimersManager _timersManager = null;
        [SerializeField] private SceneManager _sceneManager = null;

        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(_objectPoolManager, typeof(IObjectPoolSource));
            builder.AddSingleton(_timersManager, typeof(ITimersSource));
            builder.AddSingleton(_sceneManager, typeof(ISceneSource));
            
            builder.AddSingleton(typeof(LocalizationManager), typeof(ILocalizationSource));
            builder.AddSingleton(typeof(SaveManager), typeof(ISaveSource));
            builder.AddSingleton(typeof(IslandManager), typeof(IIslandSource));
            builder.AddSingleton(typeof(GrowthManager), typeof(IGrowthSource), typeof(IGrowthDebugSource));
            builder.AddSingleton(typeof(ForestManager), typeof(IForestSource));
        }
    }
}
