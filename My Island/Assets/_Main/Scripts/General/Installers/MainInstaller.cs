using MyForest;
using UnityEngine;
using Zenject;

namespace MyIsland
{
    public class MainInstaller : MonoInstaller
    {
        [Header("INSTANCES")]
        [SerializeField] private MainMenuController _mainMenuController = null;
        
        public override void InstallBindings()
        {
            SetBindings();   
        }

        private void SetBindings()
        {
            Container.BindInterfacesTo<MainMenuController>().FromInstance(_mainMenuController).AsSingle();
        }
    }
}
