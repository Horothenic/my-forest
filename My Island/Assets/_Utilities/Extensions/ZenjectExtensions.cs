using UnityEngine;

namespace Zenject
{
    public static class ZenjectExtensions
    {
        public static GameObject Instantiate(this DiContainer container, GameObject prefab)
        {
            return container.InstantiatePrefab(prefab);
        }

        public static T Instantiate<T>(this DiContainer container, T prefab) where T : Object
        {
            return container.InstantiatePrefabForComponent<T>(prefab);
        }

        public static T Instantiate<T>(this DiContainer container, T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Object
        {
            return container.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
        }
    }
}
