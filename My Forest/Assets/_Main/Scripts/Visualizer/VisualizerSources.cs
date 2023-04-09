using System;
using UnityEngine;

using UniRx;

namespace MyForest
{
    public interface IVisualizerLoaderSource
    {
        IObservable<GameObject> VisualizerLoadedObservable { get; }
        void LoadVisualizer(GameObject objectToLoad);
    }
}
