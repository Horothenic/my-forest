using Newtonsoft.Json;

namespace MyIsland
{
    public class GrowthData
    {
        public int CurrentGrowth { get; private set; }
        public int AllTimeGrowth { get; private set; }

        [JsonConstructor]
        public GrowthData(int currentGrowth, int allTimeGrowth)
        {
            CurrentGrowth = currentGrowth;
            AllTimeGrowth = allTimeGrowth;
        }

        public GrowthData()
        {
            
        }

        public void IncreaseGrowth(int amount)
        {
            CurrentGrowth += amount;
            AllTimeGrowth += amount;
        }

        public void DecreaseGrowth(int amount)
        {
            CurrentGrowth -= amount;
        }

        public void ResetAll()
        {
            CurrentGrowth = 0;
            AllTimeGrowth = 0;
        }
    }
}
