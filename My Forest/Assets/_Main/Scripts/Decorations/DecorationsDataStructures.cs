using System;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class DecorationData
    {
        public string ID { get; private set; }
        public float Rotation { get; private set; }

        [JsonIgnore]
        public DecorationConfiguration Configuration { get; private set; }

        [JsonConstructor]
        public DecorationData(string id, float rotation)
        {
            ID = id;
            Rotation = rotation;
        }
        
        public DecorationData(string id, float rotation, DecorationConfiguration configuration) : this(id, rotation)
        {
            Configuration = configuration;
        }

        public void Hydrate(IDecorationsConfigurationCollectionSource decorationsConfigurationCollectionSource)
        {
            Configuration = decorationsConfigurationCollectionSource.GetConfiguration(ID);
        }
    }
}
