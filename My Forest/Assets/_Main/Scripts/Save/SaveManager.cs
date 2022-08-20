using UnityEngine;

using Newtonsoft.Json;

namespace MyForest
{
    public partial class SaveManager
    {
        #region METHODS

        private T Load<T>(string key) where T : new()
        {
            var json = PlayerPrefs.GetString(key, string.Empty);
            return string.IsNullOrEmpty(json) ? new T() : JsonConvert.DeserializeObject<T>(json);
        }

        private void Save(string key, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(key, json);
        }

        #endregion
    }

    public partial class SaveManager : ISaveSource
    {
        T ISaveSource.Load<T>(string key) => Load<T>(key);
        void ISaveSource.Save(string key, object data) => Save(key, data);
    }
}
