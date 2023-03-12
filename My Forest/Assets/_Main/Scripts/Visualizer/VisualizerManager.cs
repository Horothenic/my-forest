using System;
using UnityEngine;

using UniRx;

namespace MyForest
{
    public partial class VisualizerManager
    {
        #region FIELDS

        private Subject<GameObject> _visualizerLoadedSubject = new Subject<GameObject>();

        #endregion
    }
    
    public partial class VisualizerManager : IVisualizerLoaderSource
    {
        IObservable<GameObject> IVisualizerLoaderSource.VisualizerLoadedObservable => _visualizerLoadedSubject.AsObservable();

        void IVisualizerLoaderSource.LoadVisualizer(GameObject objectToLoad)
        {
            _visualizerLoadedSubject.OnNext(objectToLoad);   
        }
    }
}
