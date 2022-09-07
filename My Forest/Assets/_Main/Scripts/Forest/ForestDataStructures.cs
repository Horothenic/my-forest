using UnityEngine;
using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class ForestData
    {
        private List<ForestCellData> _forestCells = new List<ForestCellData>();

        public bool IsEmpty => _forestCells.Count == 0;
        public int ForestSize { get; private set; }
        public IReadOnlyList<ForestCellData> ForestCells => _forestCells;

        public ForestData() { }

        [JsonConstructor]
        public ForestData(List<ForestCellData> forestCells)
        {
            _forestCells = forestCells;
            ForestSize = _forestCells.Count;
        }

        public void AddForestCell(ForestCellData newForestCell)
        {
            _forestCells.Add(newForestCell);
        }
    }

    [Serializable]
    public class ForestCellData
    {
        private List<ForestElementData> _decorations = new List<ForestElementData>();

        public ForestCellType Type { get; private set; }
        public int Level { get; private set; }
        public ForestElementData Ground { get; private set; }
        public ForestElementData Tree { get; private set; }
        public IReadOnlyList<ForestElementData> Decorations => _decorations;

        [JsonConstructor]
        public ForestCellData(ForestCellType type, int level, ForestElementData ground, ForestElementData tree, List<ForestElementData> decorations)
        {
            Type = type;
            Level = level;
            Ground = ground;
            Tree = tree;
            _decorations = decorations;
        }
    }

    [Serializable]
    public class ForestElementData
    {
        public string PrefabName { get; private set; }
        public SerializedVector3 Position { get; private set; }

        [JsonConstructor]
        public ForestElementData(string prefabName, Vector3 position)
        {
            PrefabName = prefabName;
            Position = position;
        }
    }

    public enum ForestCellType
    {
        Tree,
        Ground,
        Decoration
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
        SimpleTree,
        Twig
    }
}
