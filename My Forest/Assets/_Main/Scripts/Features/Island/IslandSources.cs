namespace MyForest
{
    public interface IIslandSource
    {
        string CreatorName { get; }
        bool HasCreatorName { get; }
        
        void SetCreatorName(string creatorName);
    }
}
