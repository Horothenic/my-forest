using UnityEngine;

namespace MyForest.Testing
{
    public class TreeTest
    {
        public Transform Origin { get; }
        public Vector3 Position { get; }
        public TreeConfiguration TreeConfiguration { get; }
        
        public TreeTest(Transform origin, Vector3 position, TreeConfiguration treeConfiguration)
        {
            Origin = origin;
            Position = position;
            TreeConfiguration = treeConfiguration;
        }
    }
}
