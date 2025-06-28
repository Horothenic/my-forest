using Localization;
using UnityEngine;
using Zenject;

namespace MyIsland
{
    public class ProjectInstaller : MonoInstaller
    {
        [Header("INSTANCES")]
        [SerializeField] private ObjectPoolManager _objectPoolManager = null;
        [SerializeField] private TimersManager _timersManager = null;
        [SerializeField] private SceneManager _sceneManager = null;

        public override void InstallBindings()
        {
            SetBindings();   
        }

        private void SetBindings()
        {
            Container.BindInterfacesTo<ObjectPoolManager>().FromInstance(_objectPoolManager).AsSingle();
            Container.BindInterfacesTo<TimersManager>().FromInstance(_timersManager).AsSingle();
            Container.BindInterfacesTo<SceneManager>().FromInstance(_sceneManager).AsSingle();

            Container.BindInterfacesTo<LocalizationManager>().AsSingle();
            Container.BindInterfacesTo<SaveManager>().AsSingle();
            Container.BindInterfacesTo<IslandManager>().AsSingle();
            Container.BindInterfacesTo<GrowthManager>().AsSingle();
        }
    }
}
