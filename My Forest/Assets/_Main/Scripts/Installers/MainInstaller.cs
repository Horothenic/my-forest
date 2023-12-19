using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace MyForest
{
    public class MainInstaller : MonoInstaller
    {
        [Header("CONFIGURATIONS")]
        [SerializeField] private GrowthConfigurations _growthConfigurations = null;
        [SerializeField] private AudioConfigurations _audioConfigurations = null;
        [FormerlySerializedAs("_tileConfigurations")]
        [FormerlySerializedAs("_gridConfigurations")]
        [SerializeField] private TilesConfigurations _tilesConfigurations = null;
        [SerializeField] private TerrainConfigurations _terrainConfigurations = null;
        
        [Header("COLLECTIONS")]
        [SerializeField] private TreeConfigurationCollection _treeConfigurationCollection = null;
        [SerializeField] private DecorationConfigurationCollection _decorationConfigurationCollection = null;

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
            _terrainConfigurations.Initialize();
        }

        private void SetBindings()
        {
            Container.BindInterfacesTo<ObjectPoolManager>().FromInstance(_objectPoolManager).AsSingle();
            Container.BindInterfacesTo<TimersManager>().FromInstance(_timersManager).AsSingle();

            Container.BindInterfacesTo<GrowthConfigurations>().FromScriptableObject(_growthConfigurations).AsSingle();
            Container.BindInterfacesTo<AudioConfigurations>().FromScriptableObject(_audioConfigurations).AsSingle();
            Container.BindInterfacesTo<TilesConfigurations>().FromScriptableObject(_tilesConfigurations).AsSingle();
            Container.BindInterfacesTo<TerrainConfigurations>().FromScriptableObject(_terrainConfigurations).AsSingle();
            Container.BindInterfacesTo<TreeConfigurationCollection>().FromScriptableObject(_treeConfigurationCollection).AsSingle().WhenInjectedInto<TreesManager>();
            Container.BindInterfacesTo<DecorationConfigurationCollection>().FromScriptableObject(_decorationConfigurationCollection).AsSingle().WhenInjectedInto<DecorationsManager>();
            
            Container.BindInterfacesTo<GameManager>().AsSingle();
            Container.BindInterfacesTo<ForestManager>().AsSingle();
            Container.BindInterfacesTo<TreesManager>().AsSingle();
            Container.BindInterfacesTo<DecorationsManager>().AsSingle();
            Container.BindInterfacesTo<GrowthManager>().AsSingle();
            Container.BindInterfacesTo<SaveManager>().AsSingle();
            Container.BindInterfacesTo<CameraManager>().AsSingle();
            Container.BindInterfacesTo<AudioManager>().AsSingle();
            Container.BindInterfacesTo<VisualizerManager>().AsSingle();
            Container.BindInterfacesTo<TileManager>().AsSingle();
            Container.BindInterfacesTo<TerrainManager>().AsSingle();
        }
    }
}
