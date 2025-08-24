using System;
using UnityEngine;

namespace MyIsland
{
    public interface ICameraInputSource
    {
        GestureType CurrentGesture { get; }
        IObservable<GestureType> OnGestureChanged { get; }

        IObservable<Vector2> OnPan { get; }
        IObservable<float> OnRotate { get; }
    }
}
