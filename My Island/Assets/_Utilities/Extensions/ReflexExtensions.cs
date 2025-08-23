using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

namespace Reflex
{
    public static class ReflexExtensions
    {
        public static GameObject Instantiate(this Container container, GameObject prefab)
        {
            var gameObject = Object.Instantiate(prefab);
            GameObjectInjector.InjectSingle(gameObject, container);
            return gameObject;
        }
    }
}
