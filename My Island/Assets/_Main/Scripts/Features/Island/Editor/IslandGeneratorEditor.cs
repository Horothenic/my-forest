using UnityEngine;
using UnityEditor;

namespace MyIsland
{
    [CustomEditor(typeof(IslandGenerator))]
    [CanEditMultipleObjects]
    public class IslandGeneratorEditor : Editor
    {
        private SerializedObject serializedObjectTarget;
        private string _testSeed = "My Island";
        
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
            
            GUILayout.Space(10);
            EditorGUILayout.LabelField(new GUIContent("EDITOR-ONLY"), EditorStyles.boldLabel);

            _testSeed = EditorGUILayout.TextField(new GUIContent("Test seed"), _testSeed);
            
            if (serializedObjectTarget.ApplyModifiedProperties())
            {
                foreach (var obj in targets)
                {
                    var generator = (IslandGenerator)obj;
                    generator.Initialize(_testSeed);
                }
            }
            
            if (GUILayout.Button("Generate"))
            {
                foreach (var obj in targets)
                {
                    var generator = (IslandGenerator)obj;

                    generator.Initialize(_testSeed);
                    EditorUtility.SetDirty(generator);
                }
            }
        }
    }
}
