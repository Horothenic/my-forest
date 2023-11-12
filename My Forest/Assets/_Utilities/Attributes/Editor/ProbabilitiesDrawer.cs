using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(ProbabilitiesAttribute))]
    public class ProbabilitiesDrawer : PropertyDrawer
    {
        private const string BOX_STYLE_PATH = "Assets/_Utilities/Attributes/Editor/Styles/BoxStyle.uss";
        private const int PRECISION_MULTIPLIER = 1000;
        
        private readonly List<Box> _boxes = new List<Box>();
        
        private VisualElement _root;
        private Array _sections;
        private int _currentDragSeparator;
        private SerializedProperty _probabilitiesArray;
        
        private bool _dragging;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _root = new VisualElement();
            _root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(BOX_STYLE_PATH));
            
            var probabilitiesAttribute = attribute as ProbabilitiesAttribute;
            _probabilitiesArray = property.FindPropertyRelative("_probabilities");
            var type = probabilitiesAttribute?.EnumType;

            if (probabilitiesAttribute == null || _probabilitiesArray == null || type == null)
            {
                _root.Add(new Label("ERROR! Wrong types used."));
                return _root;
            }

            _root.Add(DrawTitle(probabilitiesAttribute.Title));

            if (!type.IsEnum)
            {
                _root.Add(new Label("You must use an Enum."));
                return _root;
            }

            _sections = Enum.GetValues(type);
            CreateDefaults(_probabilitiesArray);
            
            _root.Add(DrawContainer());
            UpdateSizes();
            
            return _root;
        }

        private void CreateDefaults(SerializedProperty property)
        {
            if (_sections.Length != property.arraySize)
            {
                ResetProperty();
            }

            var count = 0f;
            for (var i = 0; i < property.arraySize; i++)
            {
                var element = property.GetArrayElementAtIndex(i);
                count += element.floatValue;
            }

            if (count > 1f)
            {
                ResetProperty();
            }
            
            void ResetProperty()
            {
                property.ClearArray();
                
                for (var i = 0; i < _sections.Length; i++)
                {
                    property.InsertArrayElementAtIndex(i);
                    var element = property.GetArrayElementAtIndex(i);
                    element.floatValue = 1f / _sections.Length;
                }

                property.serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
        }

        private VisualElement DrawTitle(string titleText)
        {
            var title = new BindableElement
            {
                style =
                {
                    flexDirection = FlexDirection.Row
                }
            };
            
            title.AddToClassList("title");
            
            var box = new Box();
            box.AddToClassList("title-box");
            
            var titleLabel = new Label(titleText);
            box.Add(titleLabel);

            title.Add(box);

            return title;
        }

        private VisualElement DrawContainer()
        {
            var container = new VisualElement()
            {
                style =
                {
                    flexDirection = FlexDirection.Row
                }
            };
            
            container.AddToClassList("container");
            RegisterContainerToEvents(container);
            _boxes.Clear();

            for (var i = 0; i < _sections.Length; i++)
            {
                var box = new Box();
                box.AddToClassList("box");
                
                var sectionName = new Label(_sections.GetValue(i).ToString());
                var percentageAmount = new Label(FormatProbability(i))
                {
                    name = "percentageAmount"
                };

                box.Add(sectionName);
                box.Add(percentageAmount);

                container.Add(box);
                _boxes.Add(box);

                if (i >= _sections.Length - 1) continue;
                
                var separator = new Box();
                separator.AddToClassList("box");
                separator.AddToClassList("separator");
                
                container.Add(separator);
                RegisterSeparatorEvents(separator, i);
            }
            
            return container;
        }

        private void RegisterContainerToEvents(VisualElement container)
        {
            container.RegisterCallback<MouseUpEvent>(evt =>
            {
                CancelDrag();
            });
            
            container.RegisterCallback<MouseLeaveEvent>(evt =>
            {
                CancelDrag();
            });

            void CancelDrag()
            {
                if (!_dragging) return;
                
                _dragging = false;
                
                _root.UnregisterCallback<MouseMoveEvent>(DragResize);
            }
        }

        private void RegisterSeparatorEvents(Box separator, int index)
        {
            separator.RegisterCallback<MouseDownEvent>(evt =>
            {
                if (evt.button != (int)MouseButton.LeftMouse) return;

                _dragging = true;
                _currentDragSeparator = index;
                
                _root.RegisterCallback<MouseMoveEvent>(DragResize);
            });
        }

        private float GetProbability(int index)
        {
            return _probabilitiesArray.GetArrayElementAtIndex(index).floatValue;
        }

        private void SetProbability(int index, float newValue)
        {
            _probabilitiesArray.GetArrayElementAtIndex(index).floatValue = newValue;
        }
        
        private void DragResize(MouseMoveEvent evt)
        {
            var dx = evt.mouseDelta.x / _root.contentRect.width;
            var fx = Mathf.Min( GetProbability(_currentDragSeparator) + dx, 0);
            
            fx = Mathf.Min(GetProbability(_currentDragSeparator + 1) - dx, fx);
            dx += fx * Mathf.Sign(dx);

            SetProbability(_currentDragSeparator, GetProbability(_currentDragSeparator) + dx);
            SetProbability(_currentDragSeparator + 1, GetProbability(_currentDragSeparator + 1) - dx);
            
            SetProbability(_currentDragSeparator, Mathf.RoundToInt(GetProbability(_currentDragSeparator) * PRECISION_MULTIPLIER) / (float)PRECISION_MULTIPLIER);
            SetProbability(_currentDragSeparator + 1, Mathf.RoundToInt(GetProbability(_currentDragSeparator + 1) * PRECISION_MULTIPLIER) / (float)PRECISION_MULTIPLIER);
            
            _probabilitiesArray.serializedObject.ApplyModifiedProperties();

            UpdateSizes();
        }
        
        private void UpdateSizes()
        {
            for (var i = 0; i < _boxes.Count; i++)
            {
                _boxes[i].style.flexGrow = GetProbability(i);
                _boxes[i].Q<Label>("percentageAmount").text = FormatProbability(i);
            }
        }

        private string FormatProbability(int index)
        {
            return $"{GetProbability(index):P0}";
        }
    }
}
