using UnityEngine;

namespace MyIsland
{
    public partial class ForestManager : DataManager<ForestData>
    {
        protected override string Key => "Forest";
        protected override SaveStyle SaveStyle => SaveStyle.Json;
        
        public ForestManager(ISaveSource saveSource) : base(saveSource) { }
    }

    public partial class ForestManager : IForestSource
    {
        ForestData IForestSource.Data => Data;

        void IForestSource.PlantTree(Ray ray)
        {
            DetectBiomeOnRaycast(ray);
        }
        
        private Biome DetectBiomeOnRaycast(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject.name);
            }

            return Biome.Forest;
        }
    }
}
