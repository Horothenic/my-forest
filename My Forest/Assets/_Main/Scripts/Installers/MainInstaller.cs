using UnityEngine;

using Zenject;

namespace MyForest
{
    public class MainInstaller : MonoInstaller
    {
        [Header("CONFIGURATIONS")]
        [SerializeField] private ForestConfiguration _forestConfigurations = null;
        [SerializeField] private TreeConfigurationCollection _treeConfigurationCollection = null;
        [SerializeField] private GrowthConfigurations _growthConfigurations = null;
        [SerializeField] private AudioConfigurations _audioConfigurations = null;
        [SerializeField] private GridConfigurations _gridConfigurations = null;

        [Header("INSTANCES")]
        [SerializeField] private GrowthTrack _growthTrack = null;

        [Header("OTHERS")]
        [SerializeField] private ObjectPool _objectPool = null;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ServicesManager>().AsSingle();
            Container.BindInterfacesTo<ForestManager>().AsSingle();
            Container.BindInterfacesTo<GrowthManager>().AsSingle();
            Container.BindInterfacesTo<SaveManager>().AsSingle();
            Container.BindInterfacesTo<CameraManager>().AsSingle();
            Container.BindInterfacesTo<AudioManager>().AsSingle();
            Container.BindInterfacesTo<VisualizerManager>().AsSingle();
            Container.BindInterfacesTo<GridManager>().AsSingle();

            Container.BindInterfacesTo<ForestConfiguration>().FromInstance(_forestConfigurations).AsSingle();
            Container.BindInterfacesTo<TreeConfigurationCollection>().FromInstance(_treeConfigurationCollection).AsSingle();
            Container.BindInterfacesTo<GrowthConfigurations>().FromInstance(_growthConfigurations).AsSingle();
            Container.BindInterfacesTo<AudioConfigurations>().FromInstance(_audioConfigurations).AsSingle();
            Container.BindInterfacesTo<GridConfigurations>().FromInstance(_gridConfigurations).AsSingle();

            Container.BindInterfacesTo<GrowthTrack>().FromInstance(_growthTrack).AsSingle();

            Container.BindInterfacesTo<ObjectPool>().FromInstance(_objectPool).AsSingle();
        }
    }
}
