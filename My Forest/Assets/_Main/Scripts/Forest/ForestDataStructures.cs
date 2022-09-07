using UnityEngine;
using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class ForestData
    {
        private List<string> _elementPrefabsNames = new List<string>();
        private List<Vector3> _elementPositions = new List<Vector3>();
        private List<Vector3> _elementRotations = new List<Vector3>();

        public int Length { get; private set; }
        public IReadOnlyList<string> ElementPrefabsNames => _elementPrefabsNames;
        public IReadOnlyList<Vector3> ElementPositions => _elementPositions;
        public IReadOnlyList<Vector3> ElementRotations => _elementRotations;

        public ForestData() { }

        [JsonConstructor]
        public ForestData(List<string> elementPrefabsNames, List<Vector3> elementPositions, List<Vector3> elementRotations)
        {
            Length = elementPrefabsNames.Count;
            _elementPrefabsNames = elementPrefabsNames;
            _elementPositions = elementPositions;
            _elementRotations = elementRotations;
        }

        public void AddElement(GameObject gameObject)
        {
            _elementPrefabsNames.Add(gameObject.PrefabName());
            _elementPositions.Add(gameObject.transform.position);
            _elementRotations.Add(gameObject.transform.localEulerAngles);
        }
    }

    [Serializable]
    public class ForestBuildParameter
    {
        [SerializeField] private int _scoreNeeded = 0;
        [SerializeField] private int _gridSize = 0;
        [SerializeField] private ForestElementAmount[] _elementsMap = null;

        public int ScoreNeeded => _scoreNeeded;
        public int GridSize => _gridSize;
        public IReadOnlyList<ForestElementAmount> ElementsMap => _elementsMap;
    }

    [Serializable]
    public class ForestElementAmount
    {
        [SerializeField] private ForestElementType _type = default;
        [SerializeField] private int _amount = 0;

        public ForestElementType Type => _type;
        public int Amount => _amount;
    }

    public enum ForestElementType
    {
        Bush,
        DeadTree,
        Ground,
        PalmTree,
        PineTree,
        Plant,
        Rock,
        SimpleTree
    }
}
