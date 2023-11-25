using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using TMPro;

namespace MyForest.Testing
{
    [ExecuteInEditMode]
    public abstract class ContentVisualizer<T> : MonoBehaviour where T : Object
    {
        #region FIELDS

        private static string DataFilter => $"t:{typeof(T).Name}";
        
        [Header("COMPONENTS")]
        [SerializeField] private Transform _origin;
        
        [Header("CONFIGURATIONS")]
        [SerializeField] private GameObject _subTitlePrefab;
        [SerializeField] private float _offsetWithSubTitle = 2;
        [SerializeField] private Vector2 _separation = Vector2.zero;
        [SerializeField] private float _biomeSeparation = 8f;
        
        private Dictionary<Biome, List<T>> _configurationsMap = new Dictionary<Biome, List<T>>();

        protected Vector2 Separation => _separation;

        #endregion

        #region UNITY

        private void Awake()
        {
            RefreshConfigurations();
        }

        #endregion

        #region METHODS

        public void RefreshConfigurations()
        {
            CleanConfigurations();
            
            _configurationsMap.Clear();
            _configurationsMap = GetConfigurationsMap(FindScriptableObjects());

            LoadAllConfigurations();
        }

        private void CleanConfigurations()
        {
            while (_origin.childCount > 0)
            {
                DestroyImmediate(_origin.GetChild(0).gameObject);
            }
        }

        private List<T> FindScriptableObjects()
        {
            var configurations = new List<T>();
            var guids = AssetDatabase.FindAssets(DataFilter);

            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var so = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (so != null)
                {
                    configurations.Add(so);
                }
            }

            return configurations;
        }

        private void LoadAllConfigurations()
        {
            var currentPosition = _origin.position;
            foreach (var (biome, configurations) in _configurationsMap)
            {
                var biomeOrigin = new GameObject(biome.ToString())
                {
                    transform =
                    {
                        position = currentPosition,
                        parent = _origin
                    }
                };
                
                var subTitle = (GameObject)PrefabUtility.InstantiatePrefab(_subTitlePrefab, biomeOrigin.transform);
                subTitle.transform.position = currentPosition;
                subTitle.GetComponentInChildren<TextMeshPro>().text = biome.ToString();
                currentPosition.z += _offsetWithSubTitle;

                foreach (var configuration in configurations)
                {
                    var treeOrigin = new GameObject(GetContentDataID(configuration))
                    {
                        transform =
                        {
                            position = currentPosition,
                            parent = biomeOrigin.transform
                        }
                    };

                    RefreshTreeTest(new ContentTestData<T>(treeOrigin.transform, currentPosition, configuration));
                    
                    currentPosition.x += _separation.x;
                }
                
                currentPosition.x = _origin.position.x;
                currentPosition.z += _biomeSeparation;
            }
        }

        protected abstract string GetContentDataID(T configuration);

        protected abstract Dictionary<Biome, List<T>> GetConfigurationsMap(List<T> configurations);
        
        protected abstract void RefreshTreeTest(ContentTestData<T> contentTestData);

        #endregion
    }
}
