using UnityEngine;
using System.IO;

using Newtonsoft.Json;
using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyForest
{
    public partial class SaveManager
    {

    }

    public partial class SaveManager : ISaveSource
    {
        T ISaveSource.LoadJson<T>(string key, T defaultValue)
        {
            var json = PlayerPrefs.GetString(key, string.Empty);
            return string.IsNullOrEmpty(json) ? defaultValue : JsonConvert.DeserializeObject<T>(json);
        }

        void ISaveSource.SaveJson(string key, object data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            PlayerPrefs.SetString(key, json);
        }

        void ISaveSource.DeleteJson(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        void ISaveSource.DeleteAllJson()
        {
            PlayerPrefs.DeleteAll();
        }

        T ISaveSource.LoadJsonFromResources<T>(string path)
        {
            var json = Resources.Load<TextAsset>(path)?.text;
            return string.IsNullOrEmpty(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
        }
        
        private byte[] ObjectToByteArray<T>(T obj)
        {
            if (obj == null) return null;

            var formatter = new BinaryFormatter();
            using var stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }
        
        T ISaveSource.LoadFile<T>(string key, T defaultValue)
        {
            var filePath = Path.Combine(Application.persistentDataPath, key + ".dat");

            if (!File.Exists(filePath)) return defaultValue;
            
            try
            {
                var formatter = new BinaryFormatter();
                using var fileStream = new FileStream(filePath, FileMode.Open);
                return (T)formatter.Deserialize(fileStream);
            }
            catch (Exception e)
            {
                Debug.LogError($"[SAVE MANAGER] Failed to load file: {e.Message}");
                return defaultValue;
            }
        }

        void ISaveSource.SaveFile(string key, object data)
        {
            var filePath = Path.Combine(Application.persistentDataPath, key + ".dat");

            try
            {
                var formattedData = ObjectToByteArray(data);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                fileStream.Write(formattedData, 0, formattedData.Length);
            }
            catch (Exception e)
            {
                Debug.LogError($"[SAVE MANAGER] Failed to save file: {e.Message}");
            }
        }
        
        void ISaveSource.DeleteFile(string key)
        {
            var filePath = Path.Combine(Application.persistentDataPath, key + ".dat");

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    Debug.Log($"[SAVE MANAGER] File '{filePath}' deleted successfully.");
                }
                catch (Exception e)
                {
                    Debug.LogError($"[SAVE MANAGER] Failed to delete file: {e.Message}");
                }
            }
            else
            {
                Debug.LogWarning($"[SAVE MANAGER] File '{filePath}' does not exist.");
            }
        }
    }
}
