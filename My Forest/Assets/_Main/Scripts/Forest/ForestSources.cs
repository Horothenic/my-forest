using System;
using System.Collections.Generic;

namespace MyForest
{
    public interface IForestDataSource
    {
        IObservable<ForestData> ForestObservable { get; }
        IObservable<TreeData> GetTreeDataObservable(TreeData treeData);
    }

    public interface ITreeCollectionSource
    {
        TreeConfiguration GetTreeConfiguration(string treeID);
        IReadOnlyList<TreeConfiguration> GetAllElementConfigurations();
    }
}
