using UnityEngine;

using Newtonsoft.Json;

namespace MyForest
{
    public partial class SaveManager
    {

    }

    public partial class SaveManager : ISaveSource
    {
        T ISaveSource.Load<T>(string key, T defaultValue)
        {
            var json = PlayerPrefs.GetString(key, string.Empty);
            return string.IsNullOrEmpty(json) ? defaultValue : JsonConvert.DeserializeObject<T>(json);
        }

        T ISaveSource.LoadJSONFromResources<T>(string path)
        {
            var json = Resources.Load<TextAsset>(path)?.text;
            return string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json);
        }

        void ISaveSource.Save(string key, object data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            PlayerPrefs.SetString(key, json);
        }
    }
}
