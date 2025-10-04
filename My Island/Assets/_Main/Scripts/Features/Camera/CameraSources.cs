using System;
using UnityEngine;

namespace MyIsland
{
    public interface ICameraInputSource
    {
        IObservable<float> OnPan { get; }
        IObservable<float> OnRotate { get; }
    }

    public interface ICameraTargetSource
    {
        Ray IslandTargetRay { get; }
    }
}
