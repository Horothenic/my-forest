namespace UnityEngine
{
    public static class GameObjectExtensions
    {
        private const string CLONE_SUFFIX = "(Clone)";

        public static void Set(this GameObject gameObject, Vector3 position, Transform parent)
        {
            gameObject.transform.SetParent(parent);
            gameObject.transform.position = position;
        }

        public static void Set(this GameObject gameObject, Vector3 position, Vector3 eulerAngles, Transform parent)
        {
            gameObject.transform.SetParent(parent);
            gameObject.transform.position = position;
            gameObject.transform.eulerAngles = eulerAngles;
        }

        public static void SetLocal(this GameObject gameObject, Vector3 position, Transform parent)
        {
            gameObject.transform.SetParent(parent);
            gameObject.transform.localPosition = position;
        }

        public static void SetLocal(this GameObject gameObject, Vector3 position, Vector3 eulerAngles, Transform parent)
        {
            gameObject.transform.SetParent(parent);
            gameObject.transform.localPosition = position;
            gameObject.transform.localEulerAngles = eulerAngles;
        }

        public static string PrefabName(this GameObject gameObject)
        {
            return gameObject.name.Replace(CLONE_SUFFIX, string.Empty);
        }
    }
}
