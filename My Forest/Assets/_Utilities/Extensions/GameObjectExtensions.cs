namespace UnityEngine
{
    public static class GameObjectExtensions
    {
        public static void Set(this GameObject gameObject, Vector3 position, Vector3 eulerAngles, Transform parent)
        {
            gameObject.transform.position = position;
            gameObject.transform.eulerAngles = eulerAngles;
            gameObject.transform.SetParent(parent);
        }
    }
}
