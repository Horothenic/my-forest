using Zenject;

namespace MyForest
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SaveManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GrowthManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ForestManager>().AsSingle();
        }
    }
}
