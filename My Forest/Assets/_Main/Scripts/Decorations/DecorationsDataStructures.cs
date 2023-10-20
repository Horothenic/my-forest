using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class DecorationData
    {
        public int ID { get; private set; }

        [JsonConstructor]
        public DecorationData(int id)
        {
            ID = id;
        }
    }
}
