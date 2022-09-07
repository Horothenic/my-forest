namespace MyForest
{
    public interface ISaveSource
    {
        T Load<T>(string key) where T : new();
        void Save(string key, object data);
    }
}
