using System;
using UnityEngine;
using UnityEditor;

using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace MyIsland.Testing.Editor
{
    public class ContentRefresherButtons : EditorWindow
    {
        #region METHODS

        [InitializeOnLoadMethod]
        [Obsolete("Obsolete")]
        private static void Initialize()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }
        
        [Obsolete("Obsolete")]
        private static void OnSceneGUI(SceneView sceneView)
        {
            if (UnitySceneManager.GetActiveScene().name != "ContentVisualizer") return;
            
            Handles.BeginGUI();

            var customRect = new Rect(2, Screen.height - 78, Screen.width - 6, 30);
            GUILayout.BeginArea(customRect);
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Refresh All Elements", GUILayout.Height(30)))
            {
                FindObjectOfType<AllContentVisualizer>()?.RefreshAllElements();
            }
            
            if (GUILayout.Button("Refresh Trees", GUILayout.Height(30)))
            {
                FindObjectOfType<TreesContentVisualizer>()?.RefreshConfigurations();
            }
            
            if (GUILayout.Button("Refresh Decorations", GUILayout.Height(30)))
            {
                FindObjectOfType<DecorationsContentVisualizer>()?.RefreshConfigurations();
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            
            Handles.EndGUI();
        }

        #endregion
    }
}
