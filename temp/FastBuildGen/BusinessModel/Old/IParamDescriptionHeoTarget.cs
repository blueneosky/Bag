using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace FastBuildGen.BusinessModel.Old
{
    public interface IParamDescriptionHeoTarget : IParamDescription
    {
        event NotifyCollectionChangedEventHandler DependenciesChanged;

        IEnumerable<IParamDescriptionHeoModule> Dependencies { get; }

        void AddDependencies(IEnumerable<IParamDescriptionHeoModule> dependencies);

        void AddDependency(IParamDescriptionHeoModule dependency);

        void ClearDependencies();

        bool RemoveDependency(string name);
    }
}