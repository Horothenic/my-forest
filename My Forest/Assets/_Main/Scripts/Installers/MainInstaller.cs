using UnityEngine;

using Zenject;

namespace MyForest
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private GrowthConfigurations _growthConfigurations = null;
        [SerializeField] private ForestElementConfigurations _forestElementConfigurations = null;
        [SerializeField] private ObjectPool _objectPool = null;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AdsManager>().AsSingle();
            Container.BindInterfacesTo<ForestManager>().AsSingle();
            Container.BindInterfacesTo<GrowthManager>().AsSingle();
            Container.BindInterfacesTo<SaveManager>().AsSingle();
            Container.BindInterfacesTo<CameraManager>().AsSingle();

            Container.BindInterfacesTo<ForestElementConfigurations>().FromInstance(_forestElementConfigurations).AsSingle();
            Container.BindInterfacesTo<GrowthConfigurations>().FromInstance(_growthConfigurations).AsSingle();

            Container.BindInterfacesTo<ObjectPool>().FromInstance(_objectPool).AsSingle();
        }
    }
}
