using UnityEngine;
using UnityEditor;

namespace MyForest
{
    [CustomEditor(typeof(IslandBottomGenerator))]
    [CanEditMultipleObjects]
    public class IslandBottomGeneratorEditor : Editor
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
                    var generator = (IslandBottomGenerator)obj;
                    generator.Initialize();
                }
            }
            
            if (GUILayout.Button("Generate"))
            {
                foreach (var obj in targets)
                {
                    var generator = (IslandBottomGenerator)obj;

                    generator.Initialize();
                    EditorUtility.SetDirty(generator);
                }
            }
        }
    }
}
