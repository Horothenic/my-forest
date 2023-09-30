using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace MyForest.Testing.Editor
{
    public class TreeTestRefresher : EditorWindow
    {
        #region METHODS

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }
        
        private static void OnSceneGUI(SceneView sceneView)
        {
            if (SceneManager.GetActiveScene().name != "Trees") return;
            
            Handles.BeginGUI();

            var customRect = new Rect(2, Screen.height - 78, Screen.width - 6, 30);
            GUILayout.BeginArea(customRect);
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Refresh All Elements", GUILayout.Height(30)))
            {
                FindObjectOfType<TreeTesterController>()?.RefreshAllElements();
            }
            
            if (GUILayout.Button("Refresh Tree Configurations", GUILayout.Height(30)))
            {
                FindObjectOfType<TreeTesterController>()?.RefreshTreeConfigurations();
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            
            Handles.EndGUI();
        }

        #endregion
    }
}
