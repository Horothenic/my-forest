using UnityEngine;

namespace Zenject
{
    public static class ZenjectExtensions
    {
        public static T Instantiate<T>(this DiContainer container, T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Object
        {
            return container.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
        }
    }
}
