using Zenject;
using EasyMobile;

namespace MyForest
{
    public partial class ServicesManager
    {
        #region METHODS

        private void Initialize()
        {
            InitializeEasyMobile();
        }

        private void InitializeEasyMobile()
        {
            RuntimeManager.Init();
        }

        #endregion
    }

    public partial class ServicesManager : IInitializable
    {
        void IInitializable.Initialize() => Initialize();
    }
}
