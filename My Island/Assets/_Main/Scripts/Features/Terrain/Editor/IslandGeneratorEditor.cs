using UnityEngine;
using UnityEditor;

namespace MyIsland
{
    [CustomEditor(typeof(IslandGenerator))]
    [CanEditMultipleObjects]
    public class IslandGeneratorEditor : Editor
    {
        private SerializedObject serializedObjectTarget;

        public override void OnInspectorGUI()
        {
            serializedObjectTarget = serializedObject;
            serializedObjectTarget.Update();
            
            var property = serializedObjectTarget.GetIterator();
            property.NextVisible(true);

            while (property.NextVisible(false))
            {
                EditorGUILayout.PropertyField(property, true);
            }
            
            if (serializedObjectTarget.ApplyModifiedProperties())
            {
                foreach (var obj in targets)
                {
                    var generator = (IslandGenerator)obj;
                    generator.Initialize();
                }
            }
            
            if (GUILayout.Button("Generate"))
            {
                foreach (var obj in targets)
                {
                    var generator = (IslandGenerator)obj;

                    generator.Initialize();
                    EditorUtility.SetDirty(generator);
                }
            }
        }
    }
}
