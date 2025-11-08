namespace MyIsland
{
    public partial class ForestManager : DataManager<ForestData>
    {
        protected override string Key => "Forest";
        protected override SaveStyle SaveStyle => SaveStyle.Json;
        
        public ForestManager(ISaveSource saveSource) : base(saveSource) { }
    }

    public partial class ForestManager : IForestSource
    {
        ForestData IForestSource.Data => Data;
    }
}
