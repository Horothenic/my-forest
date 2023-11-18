using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class DecorationData
    {
        public string ID { get; private set; }
        public int Variation { get; private set; }
        public float Rotation { get; private set; }

        [JsonIgnore]
        public DecorationConfiguration Configuration { get; private set; }

        [JsonConstructor]
        public DecorationData(string id, int variation, float rotation)
        {
            ID = id;
            Variation = variation;
            Rotation = rotation;
        }
        
        public DecorationData(string id, int variation, float rotation, DecorationConfiguration configuration) : this(id, variation, rotation)
        {
            Configuration = configuration;
        }

        public void Hydrate(IDecorationsConfigurationCollectionSource decorationsConfigurationCollectionSource)
        {
            Configuration = decorationsConfigurationCollectionSource.GetConfiguration(ID);
        }
    }
}
