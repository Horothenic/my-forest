using System;
using System.Collections.Generic;

namespace MyForest
{
    public interface IForestDataSource
    {
        IObservable<ForestData> ForestObservable { get; }
    }

    public interface ITreeCollectionSource
    {
        TreeConfiguration GetTreeConfiguration(string treeID);
        IReadOnlyList<TreeConfiguration> GetAllElementConfigurations();
    }
}
