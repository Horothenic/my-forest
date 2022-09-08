using UnityEngine;

using Newtonsoft.Json;

namespace MyForest
{
    public partial class SaveManager
    {
        #region METHODS

        private T Load<T>(string key)
        {
            var json = PlayerPrefs.GetString(key, string.Empty);
            return string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json);
        }

        private T LoadJSONFromResources<T>(string path)
        {
            var json = Resources.Load<TextAsset>(path)?.text;
            return string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json);
        }

        private void Save(string key, object data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            PlayerPrefs.SetString(key, json);
        }

        #endregion
    }

    public partial class SaveManager : ISaveSource
    {
        T ISaveSource.Load<T>(string key) => Load<T>(key);
        T ISaveSource.LoadJSONFromResources<T>(string path) => LoadJSONFromResources<T>(path);
        void ISaveSource.Save(string key, object data) => Save(key, data);
    }
}
