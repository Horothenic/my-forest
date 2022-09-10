using System.Collections.Generic;

using Zenject;

namespace UnityEngine
{
    public partial class ObjectPool : MonoBehaviour
    {
        #region FIELDS

        [Inject] private DiContainer _zenjectContainer = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform _root = null;

        private Dictionary<string, Queue<GameObject>> _pool = new Dictionary<string, Queue<GameObject>>();

        #endregion

        #region METHODS

        private GameObject Borrow(GameObject prefab)
        {
            if (prefab == null)
            {
                return null;
            }

            var prefabName = prefab.name;

            if (!_pool.ContainsKey(prefabName))
            {
                _pool.Add(prefabName, new Queue<GameObject>());
            }

            if (_pool[prefabName].Count == 0)
            {
                return _zenjectContainer.Instantiate(prefab);
            }

            var gameObject = _pool[prefabName].Dequeue();
            gameObject.SetActive(true);
            return gameObject;
        }

        private T Borrow<T>(T prefab) where T : MonoBehaviour
        {
            return Borrow(prefab.gameObject).GetComponent<T>();
        }

        private void Return(GameObject gameObject)
        {
            var prefabName = gameObject.PrefabName();

            if (!_pool.ContainsKey(prefabName))
            {
                UnityEngine.Debug.LogError("Prefab queue does not exist.", this);
            }

            gameObject.SetActive(false);
            gameObject.transform.SetParent(_root);
            _pool[prefabName].Enqueue(gameObject);
        }

        #endregion
    }

    public partial class ObjectPool : IObjectPoolSource
    {
        GameObject IObjectPoolSource.Borrow(GameObject prefab) => Borrow(prefab);
        T IObjectPoolSource.Borrow<T>(T prefab) => Borrow<T>(prefab);

        void IObjectPoolSource.Return(GameObject gameObject) => Return(gameObject);
    }
}
