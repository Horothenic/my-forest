using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

using DG.Tweening;

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

        [Header("COMPONENTS")]
        [SerializeField] private RectTransform _elementsParent = null;
        [SerializeField] private RectTransform _gizmosParent = null;

        [Header("SNAPPING")]
        [SerializeField][Range(0, 100)] private float _screenPercentageToSnap = 33;
        [SerializeField] private float _snapTransitionTime = 0.5f;

        [Header("SPACING")]
        [SerializeField] private float _elementSpacing = 500;

        [Header("GIZMOS")]
        [SerializeField] private Transform _gizmoPrefab = null;
        [SerializeField] private float _gizmoNormalSize = 1;
        [SerializeField] private float _gizmoCurrentSize = 1;

        private MonoBehaviour _prefab = null;
        private IReadOnlyList<object> _dataCollection = null;
        private List<ICollectionElementUI> _elementsList = new();
        private List<Transform> _gizmoList = new();
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

        public void Initialize<T>(T prefab, IReadOnlyList<object> dataCollection) where T : MonoBehaviour
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
                _elementsList.Add(Instantiate(_prefab, _elementsParent).GetComponent(typeof(ICollectionElementUI)) as ICollectionElementUI);
            }

            for (var i = 0; i < _elementsList.Count; i++)
            {
                _elementsList[i].Transform.localPosition = new Vector3((i - 1) * _elementSpacing, 0);
            }

            RefreshElementsData();
        }
        
        private void CreateGizmos()
        {
            for (var i = 0; i < _dataCollection.Count; i++)
            {
                _gizmoList.Add(Instantiate(_gizmoPrefab, _gizmosParent));
            }
        }

        private void RefreshElementsData()
        {
            _elementsList[0].Load(_dataCollection[CurrentLeft]);
            _elementsList[1].Load(_dataCollection[_currentElementIndex]);
            _elementsList[2].Load(_dataCollection[CurrentRight]);
        }

        private void ShiftElementsLeft()
        {
            _currentElementIndex = CurrentLeft;
            _endPosition -= _elementSpacing;
            
            var left = _elementsList[0];
            left.Transform.localPosition = _elementsList.Last().Transform.localPosition + Vector3.right * _elementSpacing;
            
            _elementsList.RemoveAt(0);
            _elementsList.Add(left);

            Snap();
        }

        private void ShiftElementsRight()
        {
            _currentElementIndex = CurrentRight;
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
                .DOLocalMoveX(_endPosition, _snapTransitionTime)
                .OnComplete(RefreshElementsData);
            
            if (_currentElementIndex == _currentGizmo) return;
            
            _gizmoList[_currentGizmo].DOScale(_gizmoNormalSize, _snapTransitionTime);
            _gizmoList[_currentGizmo = _currentElementIndex].DOScale(_gizmoCurrentSize, _snapTransitionTime);
        }
        
        #endregion
    }
}
