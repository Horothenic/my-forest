using System;

using UniRx;

namespace MyForest
{
    public interface IAdsDataSource
    {
        IObservable<bool> InitializedObservable { get; }
    }
}
