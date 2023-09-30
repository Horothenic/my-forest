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
        [SerializeField] private ObjectPoolManager _objectPoolManager = null;
        [SerializeField] private TimersManager _timersManager = null;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameManager>().AsSingle();
            Container.BindInterfacesTo<ForestManager>().AsSingle();
            Container.BindInterfacesTo<GrowthManager>().AsSingle();
            Container.BindInterfacesTo<SaveManager>().AsSingle();
            Container.BindInterfacesTo<CameraManager>().AsSingle();
            Container.BindInterfacesTo<AudioManager>().AsSingle();
            Container.BindInterfacesTo<VisualizerManager>().AsSingle();
            Container.BindInterfacesTo<GridManager>().AsSingle();
            Container.BindInterfacesTo<AdsManager>().AsSingle();

            Container.BindInterfacesTo<ForestConfiguration>().FromScriptableObject(_forestConfigurations).AsSingle();
            Container.BindInterfacesTo<TreeConfigurationCollection>().FromScriptableObject(_treeConfigurationCollection).AsSingle();
            Container.BindInterfacesTo<GrowthConfigurations>().FromScriptableObject(_growthConfigurations).AsSingle();
            Container.BindInterfacesTo<AudioConfigurations>().FromScriptableObject(_audioConfigurations).AsSingle();
            Container.BindInterfacesTo<GridConfigurations>().FromScriptableObject(_gridConfigurations).AsSingle();

            Container.BindInterfacesTo<GrowthTrack>().FromScriptableObject(_growthTrack).AsSingle();

            Container.BindInterfacesTo<ObjectPoolManager>().FromInstance(_objectPoolManager).AsSingle();
            Container.BindInterfacesTo<TimersManager>().FromInstance(_timersManager).AsSingle();
        }
    }
}
