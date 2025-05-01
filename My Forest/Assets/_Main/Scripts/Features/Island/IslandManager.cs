namespace MyForest
{
    public partial class IslandManager : DataManager<IslandData>
    {
        protected override string Key => "Island";
        protected override SaveStyle SaveStyle => SaveStyle.Json;
    }
    
    public partial class IslandManager : IIslandSource
    {
        string IIslandSource.CreatorName => Data.CreatorName;
        bool IIslandSource.HasCreatorName => !string.IsNullOrEmpty(Data.CreatorName);

        void IIslandSource.SetCreatorName(string creatorName)
        {
            Data.SetCreatorName(creatorName);
            Save();
        }
    }
}
