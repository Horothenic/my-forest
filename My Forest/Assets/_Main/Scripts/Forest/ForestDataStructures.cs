using UnityEngine;
using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace MyForest
{
    [Serializable]
    public class ForestData
    {
        private List<GroundElementData> _groundElements = new List<GroundElementData>();
        private List<ForestElementData> _forestElements = new List<ForestElementData>();

        [JsonIgnore]
        public uint GroundWidth => (uint)Mathf.FloorToInt(Mathf.Sqrt(GroundElementsCount));

        [JsonIgnore]
        public uint GroundLevel => GroundWidth / 2;
        [JsonIgnore]
        public int GroundElementsCount => _groundElements.Count;
        [JsonIgnore]
        public int ForestElementsCount => _forestElements.Count;
        public IReadOnlyList<GroundElementData> GroundElements => _groundElements;
        public IReadOnlyList<ForestElementData> ForestElements => _forestElements;

        public ForestData() { }

        [JsonConstructor]
        public ForestData(List<GroundElementData> groundElements, List<ForestElementData> forestElements)
        {
            _groundElements = groundElements;
            _forestElements = forestElements;
        }

        public void AddGroundElement(GroundElementData newGroundElement)
        {
            _groundElements.Add(newGroundElement);
        }

        public void AddForestElement(ForestElementData newForestElement)
        {
            _forestElements.Add(newForestElement);
        }
    }

    [Serializable]
    public class GroundElementData
    {
        public string GroundName { get; private set; }
        public SerializedVector3 Position { get; private set; }

        [JsonConstructor]
        public GroundElementData(string groundName, Vector3 position)
        {
            GroundName = groundName;
            Position = position;
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
