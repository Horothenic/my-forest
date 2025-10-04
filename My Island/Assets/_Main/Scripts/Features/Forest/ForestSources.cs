using UnityEngine;

namespace MyIsland
{
    public interface IForestSource
    {
        ForestData Data { get; }

        void PlantTree(Ray ray);
    }
}
