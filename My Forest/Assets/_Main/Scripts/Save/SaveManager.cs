using UnityEngine;

using Newtonsoft.Json;

namespace MyForest
{
    public class SaveManager
    {
        #region METHODS

        public T Load<T>(string key)
        {
            var json = PlayerPrefs.GetString(key, string.Empty);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Save(string key, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(key, json);
        }

        #endregion
    }
}
