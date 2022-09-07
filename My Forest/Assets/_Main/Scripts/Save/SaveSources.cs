namespace MyForest
{
    public interface ISaveSource
    {
        T Load<T>(string key) where T : new();
        T LoadJSONFromResources<T>(string path) where T : new();
        void Save(string key, object data);
    }
}
