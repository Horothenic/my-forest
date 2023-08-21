using UnityEngine;
using System.Collections.Generic;

using Zenject;
using UniRx;

namespace MyForest
{
    public partial class ForestController : MonoBehaviour
    {
        #region FIELDS

        [Inject] private IObjectPoolSource _objectPoolSource = null;
        [Inject] private IForestDataSource _forestDataSource = null;

        [Header("CONFIGURATIONS")]
        [SerializeField] private Transform _root = null;
        [SerializeField] private Tree _treePrefab = null;

        private readonly List<Tree> _trees = new List<Tree>();

        #endregion

        #region UNITY

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region METHODS

        private void Initialize()
        {
            _forestDataSource.ForestObservable.Subscribe(BuildForest).AddTo(this);
            _forestDataSource.NewTreeAddedObservable.Subscribe(CreateNewTree).AddTo(this);

            BuildForest(_forestDataSource.ForestData);
        }

        private void ResetForest()
        {
            foreach (var tree in _trees)
            {
                _objectPoolSource.Return(tree.gameObject);
            }
            _trees.Clear();
        }

        private void BuildForest(ForestData forestData)
        {
            ResetForest();

            for (var i = 0; i < forestData.TreeCount; i++)
            {
                var treeData = forestData.Trees[i];
                CreateTree(treeData, false);
            }
        }

        private void CreateNewTree(TreeData treeData)
        {
            CreateTree(treeData);
        }

        private void CreateTree(TreeData treeData, bool withAnimation = true)
        {
            var newForestElement = _objectPoolSource.Borrow(_treePrefab);
            newForestElement.gameObject.Set(treeData.Position, _root);
            newForestElement.Initialize(treeData, withAnimation);
            _trees.Add(newForestElement);
        }

        #endregion
    }
}
