using System;

namespace UnityEngine
{
    public interface IObjectPoolSource
    {
        GameObject Borrow(GameObject prefab);
        T Borrow<T>(GameObject prefab) where T : class;
        T Borrow<T>(T prefab) where T : MonoBehaviour;
        void Return(GameObject gameObject);
    }
}
