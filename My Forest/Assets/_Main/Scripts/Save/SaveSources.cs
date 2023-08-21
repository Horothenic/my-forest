namespace MyForest
{
    public interface ISaveSource
    {
        T Load<T>(string key, T defaultValue = null) where T : class;
        T LoadJSONFromResources<T>(string path);
        void Save(string key, object data);
        void Delete(string key);
        void DeleteAll();
    }
}
