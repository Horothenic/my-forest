using UnityEngine;

using Zenject;

namespace MyForest
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private ForestElementConfigurations _forestElementConfigurations = null;
        [SerializeField] private GrowthConfigurations _growthConfigurations = null;
        [SerializeField] private AudioConfigurations _audioConfigurations = null;
        [SerializeField] private ObjectPool _objectPool = null;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ServicesManager>().AsSingle();
            Container.BindInterfacesTo<ForestManager>().AsSingle();
            Container.BindInterfacesTo<GrowthManager>().AsSingle();
            Container.BindInterfacesTo<SaveManager>().AsSingle();
            Container.BindInterfacesTo<CameraManager>().AsSingle();
            Container.BindInterfacesTo<AudioManager>().AsSingle();

            Container.BindInterfacesTo<ForestElementConfigurations>().FromInstance(_forestElementConfigurations).AsSingle();
            Container.BindInterfacesTo<GrowthConfigurations>().FromInstance(_growthConfigurations).AsSingle();
            Container.BindInterfacesTo<AudioConfigurations>().FromInstance(_audioConfigurations).AsSingle();

            Container.BindInterfacesTo<ObjectPool>().FromInstance(_objectPool).AsSingle();
        }
    }
}
