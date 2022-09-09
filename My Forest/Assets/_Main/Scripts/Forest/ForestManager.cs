using System;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class ForestManager
    {
        #region FIELDS

        private const string FOREST_DATA_KEY = "forest_data";
        private const string DEFAULT_FOREST_DATA_FILE = "DefaultForest";

        [Inject] private ISaveSource _saveSource = null;

        private DataSubject<ForestData> _forestDataSubject = new DataSubject<ForestData>();
        private Subject<Unit> _createForestSubject = new Subject<Unit>();

        #endregion

        #region METHODS

        private void Initialize()
        {
            Load();
        }

        private void Load()
        {
            var storedForestData = _saveSource.Load<ForestData>(FOREST_DATA_KEY);

            if (storedForestData == null)
            {
                storedForestData = _saveSource.LoadJSONFromResources<ForestData>(DEFAULT_FOREST_DATA_FILE);
            }

            _forestDataSubject.OnNext(storedForestData);
        }

        private void Save()
        {
            _saveSource.Save(FOREST_DATA_KEY, _forestDataSubject.Value);
        }

        private void SetNewForest(ForestData newForest)
        {
            _forestDataSubject.OnNext(newForest);
            Save();
        }

        private void RechargeForest()
        {
            _forestDataSubject.OnNext();
        }

        #endregion
    }

    public partial class ForestManager : IInitializable
    {
        void IInitializable.Initialize() => Initialize();
    }

    public partial class ForestManager : IForestDataSource
    {
        IObservable<ForestData> IForestDataSource.ForestDataObservable => _forestDataSubject.AsObservable(true);

        void IForestDataSource.SetNewForest(ForestData newForest) => SetNewForest(newForest);
    }
}
