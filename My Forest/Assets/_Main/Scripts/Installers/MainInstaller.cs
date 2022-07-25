using Zenject;

namespace MyForest
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SaveManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
        }
    }
}
