using System;
using System.Collections.Generic;

namespace MyForest
{
    public interface IForestDataSource
    {
        ForestData ForestData { get; }
        IObservable<ForestData> ForestObservable { get; }
        IObservable<TreeData> NewTreeAddedObservable { get; }
    }

    public interface IForestConfigurationSource
    {
        TreeRarity GetRandomTreeRarity();
    }

    public interface ITreeConfigurationCollectionSource
    {
        TreeConfiguration GetTreeConfiguration(int treeID);
        TreeConfiguration GetRandomConfiguration();
    }
}
