using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

using DG.Tweening;
using Zenject;

namespace MyForest
{
    public interface ICollectionElementUI
    {
        void Load(object data);
        Transform Transform { get; }
    }
    
    public class CollectionContainerUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region FIELDS
        
        private const int MAX_SIMULTANEOUS_ELEMENTS = 3;
        private const int LEFT = 0;
        private const int MIDDLE = 1;
        private const int RIGHT = 2;
        
        [Inject] private IObjectPoolSource _objectPoolSource = null;
        [Inject] private IVisualizerLoaderSource _visualizerLoaderSource = null;
 
        [Header("COMPONENTS")]
        [SerializeField] private RectTransform _elementsParent = null;
        [SerializeField] private RectTransform _gizmosParent = null;

        [Header("SNAPPING")]
        [SerializeField][Range(0, 100)] private float _screenPercentageToSnap = 33;
        [SerializeField] private float _snapTransitionTime = 0.5f;

        [Header("SPACING")]
        [SerializeField] private float _elementSpacing = 500;

        [Header("GIZMOS")]
        [SerializeField] private GameObject _gizmoPrefab = null;
        [SerializeField] private float _gizmoNormalSize = 1;
        [SerializeField] private float _gizmoCurrentSize = 1;

        private GameObject _prefab = null;
        private IReadOnlyList<object> _dataCollection = null;
        private List<ICollectionElementUI> _elementsList = new();
        private List<GameObject> _gizmoList = new();
        private int _currentElementIndex = 0;
        private int _currentGizmo = 0;
        
        private float _offsetToSnap = default;
        private float _startPosition = default;
        private float _endPosition = default;
        private float _startDragPosition = default;
        private float _currentDragOffset = default;
        private bool _snapped = false;

        private int CurrentLeft => _currentElementIndex - 1 < 0 ? _dataCollection.Count - 1 : _currentElementIndex - 1;
        private int CurrentRight => _currentElementIndex + 1 > _dataCollection.Count - 1 ? 0 : _currentElementIndex + 1;

        #endregion

        #region UNITY

        private void Awake()
        {
            _offsetToSnap = Screen.width * _screenPercentageToSnap / 100;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = _elementsParent.localPosition.x;
            _startDragPosition = eventData.position.x;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (_snapped) return;
            
            _currentDragOffset = eventData.position.x - _startDragPosition;
            _elementsParent.localPosition = new Vector3(_startPosition + _currentDragOffset, 0);
            
            if (_currentDragOffset > _offsetToSnap)
            {
                _snapped = true;
                ShiftElementsRight();
            }
            else if (_currentDragOffset <  -_offsetToSnap)
            {
                _snapped = true;
                ShiftElementsLeft();
            }
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (_snapped)
            {
                _snapped = false;
                return;
            }
            
            Snap();
        }

        #endregion

        #region METHODS

        public void Initialize(GameObject prefab, IReadOnlyList<object> dataCollection)
        {
            _currentElementIndex = 0;
            _prefab = prefab;
            _dataCollection = dataCollection;
            
            CreateElements();
            CreateGizmos();
        }
        
        private void CreateElements()
        {
            for (var i = 0; i < MAX_SIMULTANEOUS_ELEMENTS; i++)
            {
                var element = _objectPoolSource.Borrow<ICollectionElementUI>(_prefab);
                element.Transform.SetParent(_elementsParent);
                _elementsList.Add(element);
            }

            for (var i = 0; i < _elementsList.Count; i++)
            {
                _elementsList[i].Transform.localPosition = new Vector3((i - 1) * _elementSpacing, 0);
            }

            RefreshElementsAtWith(MIDDLE, _dataCollection[_currentElementIndex]);
        }
        
        private void CreateGizmos()
        {
            for (var i = 0; i < _dataCollection.Count; i++)
            {
                var gizmo = _objectPoolSource.Borrow(_gizmoPrefab);
                gizmo.transform.SetParent(_gizmosParent);
                _gizmoList.Add(gizmo);
            }
        }

        private void RefreshElementsAtWith(int index, object data)
        {
            _elementsList[index].Load(data);
        }

        private void ShiftElementsLeft()
        {
            RefreshElementsAtWith(RIGHT, _dataCollection[CurrentRight]);
            
            _currentElementIndex = CurrentRight;
            _endPosition -= _elementSpacing;
            
            var left = _elementsList[0];
            left.Transform.localPosition = _elementsList.Last().Transform.localPosition + Vector3.right * _elementSpacing;
            _elementsList.RemoveAt(0);
            _elementsList.Add(left);

            Snap();
        }

        private void ShiftElementsRight()
        {
            RefreshElementsAtWith(LEFT, _dataCollection[CurrentLeft]);
            
            _currentElementIndex = CurrentLeft;
            _endPosition += _elementSpacing;
            
            var right = _elementsList.Last();
            right.Transform.localPosition = _elementsList.First().Transform.localPosition + Vector3.left * _elementSpacing;
            _elementsList.RemoveAt(_elementsList.Count - 1);
            _elementsList.Insert(0, right);
            
            Snap();
        }

        private void Snap()
        {
            _elementsParent
                .DOLocalMoveX(_endPosition, _snapTransitionTime);
            
            if (_currentElementIndex == _currentGizmo) return;
            
            _gizmoList[_currentGizmo].transform.DOScale(_gizmoNormalSize, _snapTransitionTime);
            _gizmoList[_currentGizmo = _currentElementIndex].transform.DOScale(_gizmoCurrentSize, _snapTransitionTime);
        }
        
        #endregion
    }
}
