using UnityEngine;
using Zenject;

namespace MyForest
{
    public class MainInstaller : MonoInstaller
    {
        [Header("OTHERS")]
        [SerializeField] private ObjectPoolManager _objectPoolManager = null;
        [SerializeField] private TimersManager _timersManager = null;

        public override void InstallBindings()
        {
            SetBindings();   
        }

        private void SetBindings()
        {
            Container.BindInterfacesTo<ObjectPoolManager>().FromInstance(_objectPoolManager).AsSingle();
            Container.BindInterfacesTo<TimersManager>().FromInstance(_timersManager).AsSingle();
            Container.BindInterfacesTo<SaveManager>().AsSingle();
        }
    }
}
