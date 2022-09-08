using UnityEngine;

namespace MyForest
{
    public interface ISaveSource
    {
        T Load<T>(string key);
        T LoadJSONFromResources<T>(string path);
        void Save(string key, object data);
    }
}
