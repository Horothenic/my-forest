namespace MyForest
{
    public interface ISaveSource
    {
        T LoadJson<T>(string key, T defaultValue = null) where T : class;
        void SaveJson(string key, object data);
        void DeleteJson(string key);
        void DeleteAllJson();
        
        T LoadJsonFromResources<T>(string path);
        
        T LoadFile<T>(string key, T defaultValue = null) where T : class;
        void SaveFile(string key, object data);
        void DeleteFile(string key);
    }
}
