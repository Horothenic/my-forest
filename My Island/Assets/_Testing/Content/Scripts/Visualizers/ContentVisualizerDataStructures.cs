using UnityEngine;

namespace MyIsland.Testing
{
    public class ContentTestData<T>
    {
        public Transform Origin { get; }
        public Vector3 Position { get; }
        public T Data { get; }
        
        public ContentTestData(Transform origin, Vector3 position, T data)
        {
            Origin = origin;
            Position = position;
            Data = data;
        }
    }
}
