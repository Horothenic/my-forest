using System.Collections.Generic;

namespace UnityEngine
{
    public static class GameObjectExtensions
    {
        private const string CLONE_SUFFIX = "(Clone)";

        public static RectTransform GetRectTransform(this MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.gameObject.transform as RectTransform;
        }

        public static GameObject Set(this GameObject gameObject, Vector3 position, Transform parent)
        {
            gameObject.transform.SetParent(parent);
            gameObject.transform.position = position;
            return gameObject;
        }

        public static GameObject Set(this GameObject gameObject, Vector3 position, Vector3 eulerAngles, Transform parent)
        {
            gameObject.transform.SetParent(parent);
            gameObject.transform.position = position;
            gameObject.transform.eulerAngles = eulerAngles;
            return gameObject;
        }

        public static GameObject SetLocal(this GameObject gameObject, Vector3 position, Transform parent)
        {
            gameObject.transform.SetParent(parent);
            gameObject.transform.localPosition = position;
            return gameObject;
        }

        public static GameObject SetLocal(this GameObject gameObject, Vector3 position, Vector3 eulerAngles, Transform parent)
        {
            gameObject.transform.SetParent(parent);
            gameObject.transform.localPosition = position;
            gameObject.transform.localEulerAngles = eulerAngles;
            return gameObject;
        }

        public static GameObject TurnOn(this GameObject gameObject)
        {
            gameObject.SetActive(true);
            return gameObject;
        }

        public static GameObject TurnOff(this GameObject gameObject)
        {
            gameObject.SetActive(false);
            return gameObject;
        }

        public static void AddTo(this GameObject gameObject, ICollection<GameObject> collection)
        {
            collection.Add(gameObject);
        }

        public static string PrefabName(this GameObject gameObject)
        {
            return gameObject.name.Replace(CLONE_SUFFIX, string.Empty);
        }
    }
}
