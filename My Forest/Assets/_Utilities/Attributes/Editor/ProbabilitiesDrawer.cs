using System;
using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(ProbabilitiesAttribute))]
    public class ProbabilitiesDrawer : PropertyDrawer
    {
        private static readonly Color BackgroundColor = ColorExtensions.HexToColor("282828");
        private static readonly Color LinesColor = ColorExtensions.HexToColor("5E5E5E");
        private static readonly Color HandlesColor = ColorExtensions.HexToColor("999999");
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!(attribute is ProbabilitiesAttribute probabilitiesAttribute && probabilitiesAttribute.Type.IsEnum)) return;

            var type = probabilitiesAttribute.Type;

            EditorGUI.LabelField(position, $"{type.Name}", EditorStyles.boldLabel);

            position.x += EditorGUIUtility.labelWidth;
            position.width -= EditorGUIUtility.labelWidth;
            
            DrawBoxWithSections(position, Enum.GetValues(type).Length);
        }

        private void DrawBoxWithSections(Rect position, int numSections)
        {
            var sectionWidth = position.width / numSections;

            // Draw the box outline
            Handles.DrawSolidRectangleWithOutline(new[]
            {
                new Vector3(position.x, position.y),
                new Vector3(position.x + position.width, position.y),
                new Vector3(position.x + position.width, position.y + EditorGUIUtility.singleLineHeight),
                new Vector3(position.x, position.y + EditorGUIUtility.singleLineHeight),
            }, BackgroundColor, BackgroundColor);

            // Draw vertical lines to separate sections
            Handles.color = LinesColor;
            for (var i = 1; i < numSections; i++)
            {
                var x = position.x + i * sectionWidth;
                Handles.DrawLine(new Vector3(x, position.y), new Vector3(x, position.y + EditorGUIUtility.singleLineHeight));
            }
        }
    }
}
