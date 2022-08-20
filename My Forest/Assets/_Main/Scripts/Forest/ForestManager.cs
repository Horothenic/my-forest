using System;
using UnityEngine;

using Zenject;
using UniRx;
using Newtonsoft.Json;

namespace MyForest
{
    public partial class ForestManager
    {
        #region FIELDS

        private const string FOREST_DATA_KEY = "forest_data";

        [Inject] private ISaveSource _saveSource = null;

        private DataSubject<ForestData> _forestDataSubject = new DataSubject<ForestData>(new ForestData());
        private Subject<Unit> _createForestSubject = new Subject<Unit>();

        #endregion

        #region METHODS

        private void Initialize()
        {
            Load();
        }

        private void Load()
        {
            _forestDataSubject.OnNext(_saveSource.Load<ForestData>(FOREST_DATA_KEY));
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

        private void CreateNewForest()
        {
            _createForestSubject.OnNext();
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
    }

    public partial class ForestManager : IForestEventSource
    {
        IObservable<Unit> IForestEventSource.CreateNewForestObservable => _createForestSubject.AsObservable();
    }

    public partial class ForestManager : Debug.IForestDebugSource
    {
        void Debug.IForestDebugSource.RechargeForest() => RechargeForest();
    }
}
