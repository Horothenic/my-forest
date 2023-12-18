using UnityEngine;

using Zenject;

namespace MyForest
{
    public class MainInstaller : MonoInstaller
    {
        [Header("CONFIGURATIONS")]
        [SerializeField] private GrowthConfigurations _growthConfigurations = null;
        [SerializeField] private AudioConfigurations _audioConfigurations = null;
        [SerializeField] private GridConfigurations _gridConfigurations = null;
        
        [Header("COLLECTIONS")]
        [SerializeField] private TreeConfigurationCollection _treeConfigurationCollection = null;
        [SerializeField] private DecorationConfigurationCollection _decorationConfigurationCollection = null;

        [Header("TRACKS")]
        [SerializeField] private GrowthTrack _growthTrack = null;

        [Header("OTHERS")]
        [SerializeField] private ObjectPoolManager _objectPoolManager = null;
        [SerializeField] private TimersManager _timersManager = null;

        public override void InstallBindings()
        {
            InitializeConfigurations();
            SetBindings();   
        }

        private void InitializeConfigurations()
        {
            _treeConfigurationCollection.Initialize();
            _decorationConfigurationCollection.Initialize();
            _gridConfigurations.Initialize();
        }

        private void SetBindings()
        {
            Container.BindInterfacesTo<ObjectPoolManager>().FromInstance(_objectPoolManager).AsSingle();
            Container.BindInterfacesTo<TimersManager>().FromInstance(_timersManager).AsSingle();
            
            Container.BindInterfacesTo<GrowthConfigurations>().FromScriptableObject(_growthConfigurations).AsSingle();
            Container.BindInterfacesTo<AudioConfigurations>().FromScriptableObject(_audioConfigurations).AsSingle();
            Container.BindInterfacesTo<GridConfigurations>().FromScriptableObject(_gridConfigurations).AsSingle();
            Container.BindInterfacesTo<TreeConfigurationCollection>().FromScriptableObject(_treeConfigurationCollection).AsSingle().WhenInjectedInto<TreesManager>();
            Container.BindInterfacesTo<DecorationConfigurationCollection>().FromScriptableObject(_decorationConfigurationCollection).AsSingle().WhenInjectedInto<DecorationsManager>();
            
            Container.BindInterfacesTo<GrowthTrack>().FromScriptableObject(_growthTrack).AsSingle();
            
            Container.BindInterfacesTo<GameManager>().AsSingle();
            Container.BindInterfacesTo<ForestManager>().AsSingle();
            Container.BindInterfacesTo<TreesManager>().AsSingle();
            Container.BindInterfacesTo<DecorationsManager>().AsSingle();
            Container.BindInterfacesTo<GrowthManager>().AsSingle();
            Container.BindInterfacesTo<SaveManager>().AsSingle();
            Container.BindInterfacesTo<CameraManager>().AsSingle();
            Container.BindInterfacesTo<AudioManager>().AsSingle();
            Container.BindInterfacesTo<VisualizerManager>().AsSingle();
            Container.BindInterfacesTo<GridManager>().AsSingle();
        }
    }
}
