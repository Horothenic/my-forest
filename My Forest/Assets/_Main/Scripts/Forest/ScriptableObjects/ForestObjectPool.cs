using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

namespace MyForest
{
    [CreateAssetMenu(fileName = nameof(ForestObjectPool), menuName = MENU_NAME)]
    public class ForestObjectPool : ScriptableObject
    {
        #region FIELDS

        private const string MENU_NAME = nameof(MyForest) + "/Pool/Object Pool";

        [Header("PREFABS")]
        [SerializeField] private GameObject[] _simpleTrees = null;
        [SerializeField] private GameObject[] _palmTrees = null;
        [SerializeField] private GameObject[] _pineTrees = null;
        [SerializeField] private GameObject[] _deadTrees = null;
        [SerializeField] private GameObject[] _plants = null;
        [SerializeField] private GameObject[] _bushes = null;
        [SerializeField] private GameObject[] _rocks = null;
        [SerializeField] private GameObject[] _grounds = null;
        [SerializeField] private GameObject[] _twigs = null;
        [SerializeField] private GameObject[] _seeds = null;

        private Dictionary<string, GameObject> _prefabsMap = new Dictionary<string, GameObject>();
        private Dictionary<string, Queue<GameObject>> _pool = new Dictionary<string, Queue<GameObject>>();

        #endregion

        #region METHODS

        public UniTask HydratePoolMap()
        {
            foreach (var type in (ForestElementType[])Enum.GetValues(typeof(ForestElementType)))
            {
                var prefabs = GetPrefabsFromType(type);
                foreach (var prefab in prefabs)
                {
                    _prefabsMap.Add(prefab.name, prefab);
                }
            }

            return UniTask.CompletedTask;
        }

        public GameObject Borrow(GameObject prefab)
        {
            return Borrow(prefab.name);
        }

        public GameObject Borrow(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            if (!_pool.ContainsKey(name) || _pool[name] == null)
            {
                _pool.Add(name, new Queue<GameObject>());
            }

            if (_pool[name].Count == 0)
            {
                if (_prefabsMap.TryGetValue(name, out var prefab))
                {
                    return Instantiate(prefab);
                }

                UnityEngine.Debug.LogWarning("Prefab not found", this);
                return null;
            }

            var gameObject = _pool[name].Dequeue();
            gameObject.SetActive(true);
            return gameObject;
        }

        public void Return(GameObject gameObject)
        {
            var name = gameObject.PrefabName();
            gameObject.SetActive(false);
            _pool[name].Enqueue(gameObject);
        }

        private GameObject[] GetPrefabsFromType(ForestElementType type)
        {
            switch (type)
            {
                case ForestElementType.Bush: return _bushes;
                case ForestElementType.DeadTree: return _deadTrees;
                case ForestElementType.Ground: return _grounds;
                case ForestElementType.PalmTree: return _palmTrees;
                case ForestElementType.PineTree: return _pineTrees;
                case ForestElementType.Plant: return _plants;
                case ForestElementType.Rock: return _rocks;
                case ForestElementType.SimpleTree: return _simpleTrees;
                case ForestElementType.Twig: return _twigs;
                case ForestElementType.Seed: return _seeds;
                default: return null;
            }
        }

        #endregion
    }
}
