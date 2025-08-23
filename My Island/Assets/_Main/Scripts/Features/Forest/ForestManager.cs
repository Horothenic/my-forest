using System;
using UniRx;

namespace MyIsland
{
    public partial class ForestManager : DataManager<ForestData>
    {
        protected override string Key => "Forest";
        protected override SaveStyle SaveStyle => SaveStyle.Json;
        
        public ForestManager(ISaveSource saveSource) : base(saveSource) { }

        private readonly Subject<bool> _isPlantModeOpenSubject = new Subject<bool>();
    }

    public partial class ForestManager : IForestSource
    {
        ForestData IForestSource.Data => Data;

        IObservable<bool> IForestSource.IsPlantModeOpen => _isPlantModeOpenSubject.AsObservable();
        void IForestSource.EnterPlantMode() => _isPlantModeOpenSubject.OnNext(true);
        void IForestSource.ExitPlantMode() => _isPlantModeOpenSubject.OnNext(false);
    }
}
