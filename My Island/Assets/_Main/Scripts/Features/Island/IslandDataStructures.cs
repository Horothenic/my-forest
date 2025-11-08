using Newtonsoft.Json;

namespace MyIsland
{
    public class IslandData
    {
        public string CreatorName { get; private set; }

        [JsonConstructor]
        public IslandData(string creatorName)
        {
            CreatorName = creatorName;
        }

        public IslandData()
        {
            CreatorName = null;
        }

        public void SetCreatorName(string creatorName)
        {
            CreatorName = creatorName;
        }
    }
    
    public enum Biome
    {
        Forest,
        Beach,
        Mountain
    }

    public enum Rarity
    {
        Common,
        Rare,
        Exquisite,
        Endangered
    }
}
