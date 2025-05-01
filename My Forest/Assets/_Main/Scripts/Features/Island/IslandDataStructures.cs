namespace MyForest
{
    public class IslandData
    {
        public string CreatorName { get; private set; }

        public void SetCreatorName(string creatorName)
        {
            CreatorName = creatorName;
        }
    }
    
    public enum Biome
    {
        Forest,
        Beach,
        Mountain,
        Lake,
        Tundra
    }

    public enum Rarity
    {
        Common,
        Rare,
        Exquisite,
        Endangered
    }
}
