using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Zenject;
using UniRx;

namespace MyForest
{
    public class GridController : MonoBehaviour
    {
        #region FIELDS
        
        [Inject] private ICameraGesturesControlSource _cameraGesturesControlSource = null;
        [Inject] private IGridDataSource _gridDataSource = null;
        [Inject] private DiContainer _container = null;
        
        [Header("COMPONENTS")]
        [SerializeField] private Transform _gridParent = null;
        [SerializeField] private HexagonTile _tilePrefab = null;

        private readonly Dictionary<(int, int), HexagonTile> _tiles = new();
        private float _hexRadius = 0;
        private float _squareRootOfThree = 0;
        private float _threeOverTwo = 0;

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
            CalculateParameters();
            _gridDataSource.GridObservable.Subscribe(LoadGrid).AddTo(this);
        }

        private void CalculateParameters()
        {
            _hexRadius = _tilePrefab.Radius;
            _squareRootOfThree = Mathf.Sqrt(3.0f);
            _threeOverTwo = 3.0f / 2.0f;
        }

        private void LoadGrid(GridData gridData)
        {
            if (gridData.IsEmpty)
            {
                CreateTileBatch(BiomeType.Forest, 0, 0);
                return;
            }
            
            foreach (var tileData in gridData.Tiles)
            {
                LoadTile(tileData);
            }
            
            _cameraGesturesControlSource.UpdateDragLimits(_tiles.Values.ToList());
        }

        private void LoadTile(TileData tileData)
        {
            var q = tileData.Q;
            var r = tileData.R;
            
            var positionX = _hexRadius * _squareRootOfThree * (q + r / 2f);
            var positionZ = _hexRadius * _threeOverTwo * r;

            if (!_tiles.TryGetValue((q, r), out var tile))
            {
                tile = _container.Instantiate(_tilePrefab, new Vector3(positionX, 0, positionZ), Quaternion.identity, _gridParent);
                _tiles.Add((q, r), tile);
            }
            
            tile.Initialize(tileData);
        }

        private void CreateTileBatch(BiomeType biomeType, int q, int r)
        {
            var addedTiles = new List<TileData>();
            
            CreateNewTile(biomeType, q, r).AddTo(addedTiles);
            
            CreateNewTile(biomeType, q, r + 1).AddTo(addedTiles);
            CreateNewTile(biomeType, q + 1, r).AddTo(addedTiles);
            CreateNewTile(biomeType, q + 1, r - 1).AddTo(addedTiles);
            CreateNewTile(biomeType, q, r - 1).AddTo(addedTiles);
            CreateNewTile(biomeType, q - 1, r).AddTo(addedTiles);
            CreateNewTile(biomeType, q - 1, r + 1).AddTo(addedTiles);
            
            _gridDataSource.AddTiles(addedTiles);
        }

        private TileData CreateNewTile(BiomeType biomeType, int q, int r)
        {
            if (_tiles.ContainsKey((q, r))) return null;
            
            var tileData = new TileData(biomeType, q, r);
            return tileData;
        }

        #endregion
    }
}
