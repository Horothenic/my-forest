using UnityEngine;
using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class ForestData
    {
        private List<ForestElementData> _forestElements = new List<ForestElementData>();

        public uint SizeLevel { get; private set; }
        public IReadOnlyList<ForestElementData> ForestElements => _forestElements;

        [JsonIgnore]
        public int ForestElementsCount => _forestElements.Count;
        [JsonIgnore]
        public bool IsEmpty => _forestElements.Count == default;

        public ForestData() { }

        [JsonConstructor]
        public ForestData(uint sizeLevel, List<ForestElementData> forestElements)
        {
            SizeLevel = sizeLevel;
            _forestElements = forestElements;
        }

        public void AddForestElement(ForestElementData newForestElement)
        {
            _forestElements.Add(newForestElement);
        }

        public void IncreaseSizeLevel()
        {
            SizeLevel++;
        }
    }

    [Serializable]
    public class ForestElementData
    {
        public int Id { get; private set; }
        public string ElementName { get; private set; }
        public uint Level { get; private set; }
        public SerializedVector3 Position { get; private set; }

        [JsonIgnore]
        public ForestElementConfiguration Configuration { get; private set; }
        [JsonIgnore]
        public bool IsMaxLevel => Level == Configuration.MaxLevel;

        [JsonConstructor]
        public ForestElementData(int id, string elementName, uint level, Vector3 position)
        {
            Id = id;
            ElementName = elementName;
            Level = level;
            Position = position;
        }

        public void Hydrate(IForestElementConfigurationsSource elementConfigurationsSource)
        {
            Configuration = elementConfigurationsSource.GetElementConfiguration(ElementName);
        }

        public void IncreaseLevel()
        {
            Level++;
        }
    }

    public class ForestElementMenuRequest
    {
        public GameObject Requester { get; private set; }
        public ForestElementData ForestElementData { get; private set; }

        public ForestElementMenuRequest(GameObject requester, ForestElementData forestElementData)
        {
            Requester = requester;
            ForestElementData = forestElementData;
        }
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
        Twig,
        Seed
    }
}
