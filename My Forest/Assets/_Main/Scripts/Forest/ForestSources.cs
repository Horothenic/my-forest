using System;
using System.Collections.Generic;

namespace MyForest
{
    public interface IForestDataSource
    {
        ForestData ForestData { get; }
        IObservable<ForestData> ForestObservable { get; }
    }

    public interface IForestEventSource
    {
        void AddNewTree(TreeData treeData);
    }

    public interface IForestConfigurationSource
    {
        TreeRarity GetRandomTreeRarity();
    }

    public interface ITreeConfigurationCollectionSource
    {
        TreeConfiguration GetTreeConfiguration(string treeID);
        IReadOnlyList<TreeConfiguration> GetAllElementConfigurations();
        TreeConfiguration GetRandomConfiguration();
    }
}
