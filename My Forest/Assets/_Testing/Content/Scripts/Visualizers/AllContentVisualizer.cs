using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyForest.Testing
{
    [ExecuteInEditMode]
    public class AllContentVisualizer : MonoBehaviour
    {
        #region FIELDS

        private const string PREFAB_FILTER = "t:Prefab";
        
        [Header("COMPONENTS")]
        [SerializeField] private Transform _origin;

        [Header("ELEMENTS CONFIGURATIONS")]
        [SerializeField] private string[] _contentFolderPaths = { "Assets/_Main/Assets/Prefabs/Trees/Content" };
        [SerializeField] private float _offsetWithTitle = 2;
        [SerializeField] private Vector2 _separation = Vector2.one;
        [SerializeField] private int _maxRows = 8;

        #endregion

        #region UNITY

        private void Awake()
        {
            RefreshAllElements();
        }

        #endregion

        #region ALL ELEMENTS

        public void RefreshAllElements()
        {
            CleanAllElements();

            var currentPosition = _origin.position;
            currentPosition.z += _offsetWithTitle;
            var currentRow = 0;
            var allElements = FindPrefabsInContentFolder();
            
            foreach (var element in allElements)
            {
                var newElement = (GameObject)PrefabUtility.InstantiatePrefab(element, _origin);
                newElement.transform.position = currentPosition;

                if (++currentRow >= _maxRows)
                {
                    currentRow = 0;
                    currentPosition.z = _origin.position.z + _offsetWithTitle;
                    currentPosition.x += _separation.x;
                }
                else
                {
                    currentPosition.z += _separation.y;
                }
            }
        }

        private void CleanAllElements()
        {
            while (_origin.childCount > 0)
            {
                DestroyImmediate(_origin.GetChild(0).gameObject);
            }
        }
        
        private List<GameObject> FindPrefabsInContentFolder()
        {
            var prefabs = new List<GameObject>();

            foreach (var contentFolderPath in _contentFolderPaths)
            {
                // Ensure the folder path is valid.
                if (AssetDatabase.IsValidFolder(contentFolderPath))
                {
                    var prefabGUIDs = AssetDatabase.FindAssets(PREFAB_FILTER, new[] { contentFolderPath });

                    foreach (var prefabGUID in prefabGUIDs)
                    {
                        var prefabPath = AssetDatabase.GUIDToAssetPath(prefabGUID);
                        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

                        if (prefab != null)
                        {
                            prefabs.Add(prefab);
                        }
                    }
                }
                else
                {
                    UnityEngine.Debug.LogError("[ALL CONTENT VISUALIZER] Invalid content folder path: " + contentFolderPath);
                }
            }

            return prefabs;
        }
        
        #endregion
    }
}
