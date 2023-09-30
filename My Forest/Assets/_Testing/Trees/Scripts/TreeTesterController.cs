using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

using TMPro;

namespace MyForest.Testing
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
    public class TreeTesterController : MonoBehaviour
    {
        #region FIELDS

        private const string TREE_CONFIGURATIONS_FILTER = "t:TreeConfiguration";
        private const string PREFAB_FILTER = "t:Prefab";
        
        [Header("COMPONENTS")]
        [SerializeField] private Transform _configurationsOrigin;
        [SerializeField] private Transform _allElementsOrigin;

        [Header("ELEMENTS CONFIGURATIONS")]
        [SerializeField] private string _contentFolderPath = "Assets/_Main/Assets/Prefabs/Forest/Content";
        [SerializeField] private float _elementsSeparation = 2f;
        [SerializeField] private int _maxRows = 8;
        
        [Header("TREE CONFIGURATIONS")]
        [SerializeField] private GameObject _subTitlePrefab;
        [SerializeField] private float _treeSeparation = 2f;
        [SerializeField] private float _treeLineSeparation = 5f;
        [SerializeField] private float _biomeSeparation = 8f;
        
        private readonly Dictionary<BiomeType, List<TreeConfiguration>> _treeConfigurationsMap = new Dictionary<BiomeType, List<TreeConfiguration>>();

        #endregion

        #region UNITY

        private void Awake()
        {
            RefreshAllElements();
            RefreshTreeConfigurations();
        }

        #endregion

        #region ALL ELEMENTS

        public void RefreshAllElements()
        {
            CleanAllElements();

            var currentPosition = _allElementsOrigin.position;
            var currentRow = 0;
            var allElements = FindPrefabsInContentFolder();
            
            foreach (var element in allElements)
            {
                var newElement = PrefabUtility.InstantiatePrefab(element, _allElementsOrigin) as GameObject;
                newElement.transform.position = currentPosition;

                if (++currentRow >= _maxRows)
                {
                    currentRow = 0;
                    currentPosition.z = _allElementsOrigin.position.z;
                    currentPosition.x += _elementsSeparation;
                }
                else
                {
                    currentPosition.z += _elementsSeparation;
                }
            }
        }

        private void CleanAllElements()
        {
            while (_allElementsOrigin.childCount > 0)
            {
                DestroyImmediate(_allElementsOrigin.GetChild(0).gameObject);
            }
        }
        
        private List<GameObject> FindPrefabsInContentFolder()
        {
            var prefabs = new List<GameObject>();

            // Ensure the folder path is valid.
            if (AssetDatabase.IsValidFolder(_contentFolderPath))
            {
                var prefabGUIDs = AssetDatabase.FindAssets(PREFAB_FILTER, new[] { _contentFolderPath });

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
                UnityEngine.Debug.LogError("[TREES TESTER] Invalid content folder path: " + _contentFolderPath);
            }

            return prefabs;
        }
        
        #endregion

        #region TREE CONFIGURATIONS

        public void RefreshTreeConfigurations()
        {
            CleanConfigurations();
            
            _treeConfigurationsMap.Clear();
            var treeConfigurations = FindScriptableObjects();

            foreach (var treeConfiguration in treeConfigurations)
            {
                if (_treeConfigurationsMap.ContainsKey(treeConfiguration.Biome))
                {
                    _treeConfigurationsMap[treeConfiguration.Biome].Add(treeConfiguration);
                }
                else
                {
                    _treeConfigurationsMap.Add(treeConfiguration.Biome, new List<TreeConfiguration> {treeConfiguration});
                }
            }

            LoadAllTreeConfigurations();
        }

        private void CleanConfigurations()
        {
            while (_configurationsOrigin.childCount > 0)
            {
                DestroyImmediate(_configurationsOrigin.GetChild(0).gameObject);
            }
        }

        private List<TreeConfiguration> FindScriptableObjects()
        {
            var treeConfigurations = new List<TreeConfiguration>();
            var guids = AssetDatabase.FindAssets(TREE_CONFIGURATIONS_FILTER);

            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var so = AssetDatabase.LoadAssetAtPath<TreeConfiguration>(assetPath);

                if (so != null)
                {
                    treeConfigurations.Add(so);
                }
            }

            return treeConfigurations;
        }

        private void LoadAllTreeConfigurations()
        {
            var currentPosition = _configurationsOrigin.position;
            foreach (var (biome, treeConfigurations) in _treeConfigurationsMap)
            {
                var biomeOrigin = new GameObject(biome.ToString())
                {
                    transform =
                    {
                        position = currentPosition,
                        parent = _configurationsOrigin
                    }
                };
                
                var subTitle = PrefabUtility.InstantiatePrefab(_subTitlePrefab, biomeOrigin.transform) as GameObject;
                subTitle.transform.position = currentPosition;
                subTitle.GetComponentInChildren<TextMeshPro>().text = biome.ToString();

                foreach (var treeConfiguration in treeConfigurations)
                {
                    var treeOrigin = new GameObject(treeConfiguration.ID)
                    {
                        transform =
                        {
                            position = currentPosition,
                            parent = biomeOrigin.transform
                        }
                    };

                    var treeTest = new TreeTest(treeOrigin.transform, currentPosition, treeConfiguration);
                    RefreshTreeTest(treeTest);
                    
                    currentPosition.x += _treeLineSeparation;
                }
                
                currentPosition.x = _configurationsOrigin.position.x;
                currentPosition.z += _biomeSeparation;
            }
        }

        private void RefreshTreeTest(TreeTest treeTest)
        {
            for (var i = 0; i <= treeTest.TreeConfiguration.MaxLevel; i++)
            {
                var treePrefab = treeTest.TreeConfiguration.GetConfigurationLevel(i).Prefab;
                
                var tree = PrefabUtility.InstantiatePrefab(treePrefab, treeTest.Origin) as GameObject;
                tree.transform.position = treeTest.Position + Vector3.forward * i * _treeSeparation;
            }
        }

        #endregion
    }
#endif
}
